<script context="module" lang="ts">
	import {
		AuthService,
		AuthenticationResultStatus
	} from '$lib/auth.service';
  import Loading from '$lib/components/Loading.svelte';
	import { BehaviorSubject } from 'rxjs';
	import { take } from 'rxjs/operators';
	import {
		LoginActions,
		LogoutActions,
		QueryParameterNames,
		ApplicationPaths,
		ReturnUrlType
	} from '$lib/constants/auth';

	const message = new BehaviorSubject<string>(null);

	const authService = AuthService.getInstance();

	export async function load({ page }) {
    let msg;
		const action = page;
		switch (action.path) {
			case LoginActions.Login:
				await login(getReturnUrl());
				break;
			case LoginActions.LoginCallback:
				await processLoginCallback();
				break;
			case LoginActions.LoginFailed:
				msg = new URLSearchParams(window.location.search).get(
					QueryParameterNames.Message
				);
				message.next(msg);
				break;
			case LoginActions.Profile:
				redirectToProfile();
				break;
			case LoginActions.Register:
				redirectToRegister();
				break;
			case LogoutActions.Logout:
				if (window.history.state.local) {
					await logout(getReturnUrl());
				} else {
					// This prevents regular links to <app>/authentication/logout from triggering a logout
					message.next(
						'The logout was not initiated from within the page.'
					);
				}
				break;
			case LogoutActions.LogoutCallback:
				await processLogoutCallback();
				break;
			case LogoutActions.LoggedOut:
				message.next('You successfully logged out!');
				break;

			default:
				throw new Error(`Invalid action '${action}'`);
		}
    return {};
	}

	async function login(returnUrl: string): Promise<void> {
		const state: INavigationState = { returnUrl };
		const result = await authService.signIn(state);
    message.next(undefined);
		switch (result.status) {
			case AuthenticationResultStatus.Redirect:
				break;
			case AuthenticationResultStatus.Success:
				await navigateToReturnUrl(returnUrl);
				break;
			case AuthenticationResultStatus.Fail:
				// await this.router.navigate(ApplicationPaths.LoginFailedPathComponents, {
				//   queryParams: { [QueryParameterNames.Message]: result.message }
				// });
				break;
			default:
				throw new Error(
					`Invalid status result ${(result as any).status}.`
				);
		}
	}

	async function processLoginCallback(): Promise<void> {
		const url = window.location.href;
		const result = await authService.completeSignIn(url);
		switch (result.status) {
			case AuthenticationResultStatus.Redirect:
				// There should not be any redirects as completeSignIn never redirects.
				throw new Error('Should not redirect.');
			case AuthenticationResultStatus.Success:
				await navigateToReturnUrl(getReturnUrl(result.state));
				break;
			case AuthenticationResultStatus.Fail:
				message.next(result.message);
				break;
		}
	}

	function redirectToRegister(): any {
		redirectToApiAuthorizationPath(
			`${ApplicationPaths.IdentityRegisterPath}?returnUrl=${encodeURI(
				'/' + ApplicationPaths.Login
			)}`
		);
	}

	function redirectToProfile(): void {
		redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath);
	}

	async function navigateToReturnUrl(returnUrl: string) {
		window.location.replace(returnUrl);
		// It's important that we do a replace here so that we remove the callback uri with the
		// fragment containing the tokens from the browser history.
		// await this.router.navigateByUrl(returnUrl, {
		//   replaceUrl: true
		// });
	}

	function redirectToApiAuthorizationPath(apiAuthorizationPath: string) {
		// It's important that we do a replace here so that when the user hits the back arrow on the
		// browser they get sent back to where it was on the app instead of to an endpoint on this
		// component.
		const redirectUrl = `${window.location.origin}/${apiAuthorizationPath}`;
		window.location.replace(redirectUrl);
	}

	async function logout(returnUrl: string): Promise<void> {
		const state: INavigationState = { returnUrl };
		const isauthenticated = await authService
			.isAuthenticated()
			.pipe(take(1))
			.toPromise();
		if (isauthenticated) {
			const result = await authService.signOut(state);
			switch (result.status) {
				case AuthenticationResultStatus.Redirect:
					break;
				case AuthenticationResultStatus.Success:
					await navigateToReturnUrl(returnUrl);
					break;
				case AuthenticationResultStatus.Fail:
					message.next(result.message);
					break;
				default:
					throw new Error('Invalid authentication result status.');
			}
		} else {
			message.next('You successfully logged out!');
		}
	}

	async function processLogoutCallback(): Promise<void> {
		const url = window.location.href;
		const result = await authService.completeSignOut(url);
		switch (result.status) {
			case AuthenticationResultStatus.Redirect:
				// There should not be any redirects as the only time completeAuthentication finishes
				// is when we are doing a redirect sign in flow.
				throw new Error('Should not redirect.');
			case AuthenticationResultStatus.Success:
				await navigateToReturnUrl(getReturnUrl(result.state));
				break;
			case AuthenticationResultStatus.Fail:
				message.next(result.message);
				break;
			default:
				throw new Error('Invalid authentication result status.');
		}
	}

	function getReturnUrl(state?: INavigationState): string {
		const fromQuery = new URLSearchParams(window.location.search).get(
			'returnUrl'
		);
		// If the url is coming from the query string, check that is either
		// a relative url or an absolute url
		if (
			fromQuery &&
			!(
				fromQuery.startsWith(`${window.location.origin}/`) ||
				/\/[^\/].*/.test(fromQuery)
			)
		) {
			// This is an extra check to prevent open redirects.
			throw new Error(
				'Invalid return url. The return url needs to have the same origin as the current page.'
			);
		}
		return (
			(state && state.returnUrl) ||
			fromQuery ||
			ApplicationPaths.DefaultLoginRedirectPath
		);
	}

	interface INavigationState {
		[ReturnUrlType]: string;
	}
</script>
<Loading />

<script context="module" lang="ts">
    //import { UserManager, User } from "oidc-client";
    import { ApplicationPaths } from "./auth.constants";
    import { AuthService } from "./auth.service";
    import type { ReturnUrlType } from "./auth.constants";


    const authService: AuthService = AuthService.getInstance();
    const ApplicationName = `AgoraSocialApp`;

    export enum Roles {
        Anonymous = 0,
        AgoraUser = 1 << 1,
    }

    // authenticate action
    export async function authenticate(
        node: HTMLElement,
        role: Roles = Roles.Anonymous
    ) {
        var signin = await authService.signIn(getReturnUrl());
        console.log('signin', signin);
    }

    export async function signOut() {
        var signout = await authService.signOut({returnUrl: '/'});
        console.log('signout', signout);
    }

    function getReturnUrl(state?: INavigationState): string {
        const fromQuery = new URLSearchParams(window.location.search).get('returnUrl');
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
                "Invalid return url. The return url needs to have the same origin as the current page."
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

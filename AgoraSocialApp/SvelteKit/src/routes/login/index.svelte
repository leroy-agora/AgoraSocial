<script context="module">
  import { authGuard } from '$lib/stores/auth';
  export function load() {
    return authGuard(
      {
        status: 307,
        redirect: '/app'
      },
      false
    );
  }
</script>
<script>
  import { page } from '$app/stores';
  import Nav from '$lib/components/Nav.svelte';
  import Loading from '$lib/components/Loading.svelte';
  import { AuthService } from '$lib/auth.service';
  import * as providers from '$lib/constants/providers';
import { goto } from '$app/navigation';

  const authService = AuthService.getInstance();
  let loggingIn = false;

	const login = async () => {
    loggingIn = true;
    // TODO: remove hardcoded returnUrl
    const returnUrl = '/app';
    const signIn = await authService.signIn({ returnUrl, provider: providers.GOOGLE });
    
    if (signIn.status === 0) {
      goto(returnUrl);
    }
  };
</script>
{#if loggingIn}
  <Loading />
{:else}
<Nav />
<div class="absolute top-0 left-0 right-0 bottom-0 container mx-auto flex items-center justify-center text-center">
  <div class="prose prose-sm">
    <h3>Welcome to Agora!</h3>
    <p>
      Your subscription with <i>{$page.params.conversation}</i> will give you access to a community<br />
      of fellow subscribers and a platform to explore more Creator content across the Agora.
    </p>
    <p>
      Sign up with your Google Account to join the conversation!
    </p>
    <button class="btn" on:click={login}>
      Sign in with Google Account
    </button>
    <h4>
      Already sign up?
    </h4>
    <button class="link" on:click={login}>
      Log in with you Google Account here!
    </button>
  </div>
</div>
{/if}
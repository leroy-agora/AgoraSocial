<script lang="ts">
  import { page } from '$app/stores';
  import { AuthService } from '$lib/Auth/auth.service';

  const authService = AuthService.getInstance();
  // TODO remove hardcoded redirectUrl
	const login = () => authService.signIn({ redirectUrl: '/app' });
</script>
<div class="absolute top-0 left-0 right-0 bottom-0 container mx-auto flex items-center justify-center prose prose-sm text-center">
  <div>
    <h3>Welcome to Agora!</h3>
    <p>
      Your subscription with <b>{$page.params.conversation}</b> will give you access to a community<br />
      of fellow subscribers and a platform to explore more Creator content across the Agora.
    </p>
    <p>
      Sign up with your Google Account to join the conversation!
    </p>
    <button on:click={login}>
      Sign in with Google Account
    </button>
    <h3>
      Already sign up?
    </h3>
    <button on:click={login}>
      Log in with you Google Account here!
    </button>
  </div>
</div>

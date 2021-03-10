<script lang="ts">
  import { page } from '$app/stores';

  import { AuthService } from '$components/Auth/auth.service';

  const authService = AuthService.getInstance();
	const login = () => authService.signIn({});
</script>
<div class="container sm:mx-auto">
<h1>Welcome to Agora!</h1>
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

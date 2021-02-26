<script lang="ts">
	import Auth, {signOut} from '$components/Auth/Auth.svelte';
  import { AUTH_CONTEXT } from '$components/Auth/auth.constants';
  import { getContext, onMount } from 'svelte';
  import type { AuthService } from '$components/Auth/auth.service';
  import { session } from '$stores/session';
  import { user } from '$stores/user';

  const authService: AuthService = getContext(AUTH_CONTEXT);

  onMount(() => {
     authService.getAccessToken().subscribe(token => $session = token);
     authService.getUser().subscribe(u => $user.name = u.name);
  });
 
	let count: number = 0;

	const increment = () => {
		count += 1;
	};

	const decrement = () => {
		count -= 1;
	};

	const clear = () => {
		count = 0;
	};
</script>

<button on:click={increment}>
	Hello: {$user.name}
</button>
<button on:click={decrement}>
	Clicks: {count}
</button>
<button on:click={clear}>
	Clicks: {count}
</button>

<button on:click={signOut}>Sign Out</button>

<style lang="scss">
	button {
		font-family: inherit;
		font-size: inherit;
		padding: 1em 2em;
		color: #ff3e00;
		background-color: rgba(255, 62, 0, 0.1);
		border-radius: 2em;
		border: 2px solid rgba(255, 62, 0, 0);
		outline: none;
		width: 200px;
		font-variant-numeric: tabular-nums;
	}

	button:focus {
		border: 2px solid #ff3e00;
	}

	button:active {
		background-color: rgba(255, 62, 0, 0.2);
	}
</style>
<script lang="ts">
    //import { AuthService } from "$components/Auth/auth.service";
    import { setContext } from 'svelte';
    import Nav from '$components/Nav.svelte';

    //const authService = new AuthService();
    //setContext('authService', authService);


	//import { goto } from '$app/navigation';
	//goto('/new/route');

</script>

<main>
    <Nav />
    <slot></slot>
</main>

<style lang="scss">
	:root {
		font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
	}

	main {
		text-align: center;
		padding: 1em;
		margin: 0 auto;
	}
</style>

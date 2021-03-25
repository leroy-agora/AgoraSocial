<script context="module">
  import { AuthService } from '$lib/auth.service';

  export async function load({ session }) {
    if (typeof window == 'undefined') return;

    const authService = AuthService.getInstance();
    
    authService.isAuthenticated().subscribe(isAuthed => {
      session.authenticated = isAuthed;
    });

    authService.getAccessToken().subscribe(t => {
      session.token = t;
    });
  }
</script>

<script>
  import '$lib/global.css';
  import Nav from '$lib/components/Nav.svelte';
  import Loading from '$lib/components/Loading.svelte';
  import { needToLogin } from '$lib/stores/user';

  const notError = true;
</script>
<Nav />
{#if ($needToLogin || $needToLogin === null) && notError}
  <Loading />
{:else}
<main class="container mx-auto p-12">
  <slot />
</main>
{/if}
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
  import { session } from '$app/stores';
  import Loading from '$lib/components/Loading.svelte';

  const notError = true;
</script>
{#if $session.authenticated !== null || !notError}
  <slot />
{:else}
  <Loading />
{/if}
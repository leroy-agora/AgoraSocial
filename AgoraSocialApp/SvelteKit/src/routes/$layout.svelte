<script context="module">
  import { AuthService } from '$lib/Auth/auth.service';

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
  import { goto } from '$app/navigation';
  import { session } from '$app/stores';
  import { onMount } from 'svelte';
  import Nav from '$lib/components/Nav.svelte';
  import { needToLogin } from '$lib/stores/user';

  const notError = true;
  const authService = AuthService.getInstance();
  onMount(() => {
    needToLogin.subscribe(gotoLogin => gotoLogin ? goto('/login') : null); 
  });
</script>

<Nav signout={() => authService.signOut({})} authenticated={$session.authenticated} />
{#if ($needToLogin || $needToLogin === null) && notError}
<div class="container mx-auto p-12 flex items-center justify-center h-screen">
  <div>Loading</div>
</div>
{:else}
<main class="container mx-auto p-12">
  <slot />
</main>
{/if}
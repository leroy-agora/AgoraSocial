<script context="module">
  import { AuthService } from '$components/Auth/auth.service';

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

<script lang="ts">
  import '$common/global.css';
  import { goto } from '$app/navigation';
  import { onMount } from 'svelte';
  import { needToLogin } from '$stores/user';

  const notError = true;

  onMount(() => {
    needToLogin.subscribe(gotoLogin => gotoLogin ?
      goto('/login') : null);
  });
</script>
<header>
  <img width="100px" src="/images/logo.png" alt="Agora Logo" />
</header>
<main>
{#if ($needToLogin || $needToLogin === null) && notError}
  <div>Loading</div>
{:else}
  <slot />
{/if}
</main>

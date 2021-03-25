<script>
  import { AuthService } from '$lib/auth.service';
  import { goto } from '$app/navigation';
  import { onMount } from 'svelte';
  import { needToLogin } from '$lib/stores/user';
  import { page } from '$app/stores';

  const authService = AuthService.getInstance();
  onMount(() => {
    needToLogin.subscribe(gotoLogin => gotoLogin ? goto('/login') : null); 
  });
</script>
<header class="fixed w-full h-12 bg-white">
  <nav class="container mx-auto sm:flex justify-between">
    <a href="/">
      <img class="h-12" src="/images/logo.png" alt="Agora Logo" />
    </a>
    {#if $page.path.startsWith('/app')}
    <ul class="sm:self-center text-xl">

      <li class="sm:inline-block">
        <button on:click={() => authService.signOut({})} class="text-purple-700 hover:text-purple-500">
          Sign Out
        </button>
      </li>
    </ul>
    {/if}
  </nav>
</header>
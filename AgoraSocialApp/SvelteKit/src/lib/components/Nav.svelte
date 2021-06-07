<script>
  import { AuthService } from '$lib/auth.service';
  
  const authService = AuthService.getInstance();

  export let signout = false;
</script>
<header class="fixed w-full h-12 bg-white">
  <nav class="container mx-auto sm:flex justify-between">
    <a href="/">
      <img class="h-12" src="/images/logo.png" alt="Agora Logo" />
    </a>
    {#if signout}
    <ul class="sm:self-center">
      <li class="sm:inline-block">
        <button on:click={() => authService.signOut({})} class="text-purple-700 hover:text-purple-500">
          Sign Out
        </button>
      </li>
    </ul>
    {/if}
  </nav>
</header>
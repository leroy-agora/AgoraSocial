<script>
  export let authenticated;
  export let signout;
</script>
<nav class="shadow-lg">
  <div class="container mx-auto">
    <div class="sm:flex justify-between">
      <a href="/">
        <img class="h-12" src="/images/logo.png" alt="Agora Logo" />
      </a>
      {#if authenticated}
      <ul class="sm:self-center text-xl">
        <li class="sm:inline-block">
          <a href="/" class="text-purple-700 hover:text-purple-500">
            Home
          </a>
        </li>
        <li class="sm:inline-block">
          <a href="/about" class="text-purple-700 hover:text-purple-500">  
            About
          </a>
        </li>
        <li class="sm:inline-block">
          <button on:click={signout} class="text-purple-700 hover:text-purple-500">
            Sign Out
          </button>
        </li>
      </ul>
      {/if}
    </div>
  </div>
</nav>
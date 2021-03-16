<script>
  export let authenticated;
  export let signout;
</script>
<nav class="shadow-lg">
  <div class="container mx-auto">
    <div class="sm:flex">
      <img width="100px" src="/images/logo.png" alt="Agora Logo" />
    </div>
  </div>
  {#if authenticated}
  <ul>

  </ul>
  {/if}
  <a href="/">Home</a>
  <a href="/about">About</a>
  <button on:click={signout}>
  Sign Out
  </button>
</nav>
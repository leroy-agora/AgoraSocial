<script context="module">
  import { goto } from '$app/navigation';

  export async function load({ session }) {
    if (typeof window == 'undefined') return; 

    // TODO: for now same as app conditions but will get more complex
    //       will add conditions for which data user has / seen
    if (!session.authenticated) {
      goto('/login');
    }
  }
</script>
<script>
  import Nav from '$lib/components/Nav.svelte';
  import { session } from '$app/stores';
  import Loading from '$lib/components/Loading.svelte';
</script>
{#if $session.authenticated}
  <Nav />
  <main class="container mx-auto p-12">  
    <slot />
  </main>
{:else}
  <Loading />
{/if}
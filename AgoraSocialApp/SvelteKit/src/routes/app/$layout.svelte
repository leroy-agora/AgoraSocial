<script context="module">
  import { goto } from '$app/navigation';

  export async function load({ session }) {
    if (typeof window == 'undefined') return; 

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
  <Nav signout />
  <main class="container mx-auto p-12">
    <slot />
  </main>
{:else}
  <Loading />
{/if}
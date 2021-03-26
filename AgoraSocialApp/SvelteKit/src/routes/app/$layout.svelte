<script context="module">
  import { browser } from '$app/env';

  export async function load({ session }) {
    if (!browser || session.authenticated) return {};
  
    return {
      status: 302,
      redirect: '/login'
    };
  }
</script>
<script>
  import { session } from '$app/stores';
  import Nav from '$lib/components/Nav.svelte';
  import Loading from '$lib/components/Loading.svelte';
</script>
{#if $session.authenticated === null}
  <Loading />
{:else}
  <Nav signout />
  <main class="container mx-auto p-12">
    <slot />
  </main>
{/if}
<script context="module">
  import { browser } from '$app/env';

  export async function load({ session }) {
    if (browser && !session.authenticated) {
      return {
        status: 302,
        redirect: '/login'
      };
    }

    return {};
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
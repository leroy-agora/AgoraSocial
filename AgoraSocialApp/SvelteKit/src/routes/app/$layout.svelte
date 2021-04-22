<script context="module">
  export function load({ session }) {
    if (session.authenticated) return {};
  
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
    <slot></slot>
  </main>
{/if}
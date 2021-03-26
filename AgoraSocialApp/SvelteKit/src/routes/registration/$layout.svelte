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
  import Nav from '$lib/components/Nav.svelte';
</script>
<Nav />
<main class="container mx-auto p-12">  
  <slot></slot>
</main>
<script context="module">
  export async function load({ session }) {
    if (session.authenticated) return {};
    
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
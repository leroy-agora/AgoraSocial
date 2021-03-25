<script context="module">
  import { goto } from '$app/navigation';
  import Loading from '$lib/components/Loading.svelte';

  export async function load({ session }) {
    if (typeof window == 'undefined') return;
  
    if (session.authenticated) {
      goto('/app');
    } else {
      goto('/login');
    }
  }
</script>
<Loading />

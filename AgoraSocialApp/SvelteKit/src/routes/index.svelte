<script context="module">
  export function load({ session }) {
    console.log('is this being called on the error');
    if (session.authenticated === false) {
      return {
        status: 307,
        redirect: '/login'
      };
    }

    return {
      status: 307,
      redirect: '/app'
    };
  }
</script>
<script>
  import Loading from '$lib/components/Loading.svelte';
</script>
<Loading />
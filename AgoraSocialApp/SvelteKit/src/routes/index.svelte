<script context="module">
  import { browser } from '$app/env';
  export async function load({ session }) {
    if (!browser) return {};

    if (session.authenticated === false) {
      return {
        status: 302,
        redirect: '/login'
      };
    } 
    return {
      status: 302,
      redirect: '/app'
    };
  }
</script>
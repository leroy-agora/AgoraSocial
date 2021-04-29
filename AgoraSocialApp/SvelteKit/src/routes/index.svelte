<script context="module">
  import { authGuard } from '$lib/stores/auth';
  export function load() {
    return authGuard({
      status: 307,
      redirect: '/app'
    });
  }
</script>

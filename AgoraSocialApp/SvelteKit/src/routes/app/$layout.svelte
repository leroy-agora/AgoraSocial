<script context="module">
  import { authGuard } from '$lib/stores/auth';
  export function load() {
    return authGuard({});
  }
</script>
<script>
  import Nav from '$lib/components/Nav.svelte';
</script>
<Nav signout />
<main class="container mx-auto p-12">
  <slot />
</main>

<script>
  import Home from '$components/Home.svelte';
  import Login from '$components/Auth/Login.svelte';
  import { isAuthed } from '$stores/session';
</script>

{#if $isAuthed}
  <Home />
{:else}
  <Login  />
{/if}
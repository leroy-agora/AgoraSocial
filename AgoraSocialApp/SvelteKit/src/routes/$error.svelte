<script>
  import { dev } from '$app/env';
  export let status: number;
  export let error: Error;
</script>
<svelte:head>
  <title>Error: {status}</title>
</svelte:head>
<main>
  <h1>{status}</h1>
  {#if !dev}
  <pre>
    {error.stack}
  </pre>
  {:else}
    <p>
      Sorry, it appears something has gone wrong.
    </p> 
  {/if}
</main>
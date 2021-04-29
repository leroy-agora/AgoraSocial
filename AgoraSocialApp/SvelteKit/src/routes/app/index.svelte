<script>
  import { conversations } from '$lib/stores/conversations';
  import { token } from '$lib/stores/auth';
  import { user } from '$lib/stores/user';
  import { AgoraWeather } from '$lib/constants/agora-api';
  import { get } from '$lib/api';
  import { onMount } from 'svelte';
  import Loading from '$lib/components/Loading.svelte';

  let forecast;

  onMount(() => {
    forecast = async () => {
      const weather= await get(AgoraWeather, $token);
      $conversations.data = weather.map(w => w.summary);
    };
  });
  
</script>
{#if !$user}
  <Loading />
{:else}
<button class="btn" on:click={forecast}>
  Hello: {$user.name}
</button>
<ul>
  {#each $conversations.data as conversation}
    <li>{conversation}</li>
  {/each}
</ul>
{/if}
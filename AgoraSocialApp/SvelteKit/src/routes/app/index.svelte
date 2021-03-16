<script>
  import { conversations } from '$lib/stores/conversations';
  import { session } from '$app/stores';
  import { user } from '$lib/stores/user';
  import { AgoraWeather } from '$lib/constants/agora-api';
  import { get } from '$lib/api';
  import { onMount } from 'svelte';

  let forecast;

  onMount(() => {
    forecast = async () => {
      const weather= await get(AgoraWeather, $session.token);
      $conversations.data = weather.map(w => w.summary);
    };
  });
  
</script>
<div class="main-wrapper">
  <button class="btn" on:click={forecast}>
    Hello: {$user.name}
  </button>
  <ul>
    {#each $conversations.data as conversation}
      <li>{conversation}</li>
    {/each}
  </ul>
  <div class="input-area">
    <input type="text" />
  </div>
</div>
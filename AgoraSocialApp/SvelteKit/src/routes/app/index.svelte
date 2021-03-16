<script>
  import { conversations } from '$stores/conversations';
  import { session } from '$app/stores';
  import { user } from '$stores/user';
  import { get } from '$common/api';
  import { AuthService } from '$components/Auth/auth.service';
  import { onMount } from 'svelte';

  let forecast;
  let signout;

  onMount(() => {
    const authService = AuthService.getInstance();
    signout = () => authService.signOut({});
    forecast = async () => {
      const weather= await get('/WeatherForecast', $session.token);
      $conversations.data = weather.map(w => w.summary);
    };
  });
  
</script>
<div class="main-wrapper">
  <nav>
    <a href="/">Home</a>
    <a href="/about">About</a>
    <button on:click={signout}>
    Sign Out
    </button>
  </nav>
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
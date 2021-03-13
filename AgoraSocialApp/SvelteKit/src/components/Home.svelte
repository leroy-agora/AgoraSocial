<script lang="ts">
  import { conversations } from '$stores/conversations';
  import { user } from '$stores/user';
  import { get } from '$common/api';
  import Logo from './UI/Logo.svelte';
  import { AuthService } from '$components/Auth/auth.service';

  const authService = AuthService.getInstance();
	const forecast = async () => {
		const weather= await get('/WeatherForecast');
    $conversations.data = weather.map(w => w.summary);
	};
</script>
<div class="main-wrapper">
  <Logo width={75} />
  <nav>
    <a href="/">Home</a>
    <a href="/about">About</a>
    <button on:click={() => authService.signOut({})}>
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

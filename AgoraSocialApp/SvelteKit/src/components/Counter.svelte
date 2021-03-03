<script lang="ts">
  import { AuthService } from '$components/Auth/auth.service';
  import { onMount } from 'svelte';
  import Button from '$components/ui/Button.svelte';
  import { conversations } from '$stores/conversations';
  import { user } from "$stores/user";

  let session;
  const authService = AuthService.getInstance();
  onMount(async () => {
    authService.getAccessToken().subscribe(token => session = token);
    authService.getUser().subscribe(u => u && ($user = u));
  });
 
	let count: number = 0;

	const forecast = async () => {
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${session}`);
		const res = await fetch('/WeatherForecast', { headers });
    const weatherData = await res.json();
    $conversations.data = weatherData.map(w => w.summary);
	};
</script>

<Button on:click={forecast}>
	Hello: {$user.name}
</Button>

<ul>
  {#each $conversations.data as conversation}
    <li>{conversation}</li>
  {/each}
</ul>
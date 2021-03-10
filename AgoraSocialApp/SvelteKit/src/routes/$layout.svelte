<script lang="ts">
  /* eslint-disable @typescript-eslint/no-unused-vars */
  
  import './global.css';
  import { AuthService } from '$components/Auth/auth.service';
  import Logo from '$components/UI/Logo.svelte';
  import { onMount } from 'svelte';
  import { user } from '$stores/user';
  import { isAuthed, token } from '$stores/session';

  const authService = AuthService.getInstance();

  onMount(async () => {
    authService.getUser().subscribe(u => u && ($user = u));
    authService.isAuthenticated().subscribe(t => $isAuthed = t);
    authService.getAccessToken().subscribe(t => $token = t);
  });
</script>
<header>
  <Logo />
</header>
<main>
  <slot />
</main>

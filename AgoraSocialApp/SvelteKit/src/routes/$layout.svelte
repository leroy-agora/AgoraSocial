<script context="module">
  import { AuthService } from '$lib/auth.service';
  import { browser } from '$app/env';
  import { take } from 'rxjs/operators';

  export async function load({ session }) {
    if (browser) {
      const authService = AuthService.getInstance();

      session.authenticated = await authService.isAuthenticated().pipe(take(1)).toPromise();

      authService.isAuthenticated().subscribe(isAuthed => {
        session.authenticated = isAuthed;
      });

      authService.getAccessToken().subscribe(t => {
        session.token = t;
      });
    }

    return {};
  }
</script>

<script>
  import { session } from '$app/stores';
  import '$lib/global.css';
  import Loading from '$lib/components/Loading.svelte';
  const notError = true;
</script>
{#if $session.authenticated !== null || !notError}
  <slot></slot>
{:else}
  <Loading />
{/if}
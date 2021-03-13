<script context="module">
  import { AuthService } from '$components/Auth/auth.service';
  import { goto } from '$app/navigation';
  import { take } from 'rxjs/operators';

  export async function load() {
    if (typeof window == 'undefined') return;

    const authService = AuthService.getInstance();
    const isAuthed = await authService.isAuthenticated().pipe(take(1)).toPromise();

    console.log(isAuthed);
    if (isAuthed) {
      return {
        props: {
          isAuthed
        }
      };
    }
    // change when "Error: TODO client-side redirects" is implemented in sveltekit
    goto('/login');
  }
</script>
<script>
  import Home from '$components/Home.svelte';
  export let isAuthed;
</script>
{#if isAuthed}
<Home />
{/if}
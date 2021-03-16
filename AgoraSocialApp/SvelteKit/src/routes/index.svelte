<script context="module">
  import { AuthService } from '$components/Auth/auth.service';
  import { take } from 'rxjs/operators';
  import { goto } from '$app/navigation';
import Auth from '$components/Auth/Auth.svelte';

  export async function load() {
    if (typeof window == 'undefined') return;

    const authService = AuthService.getInstance();

    const isAuthed = await authService.isAuthenticated().pipe(take(1)).toPromise();
    if (isAuthed) {
      goto('/app');
    } else {
      goto('/login');
    }
  }
</script>
<script context="module">
  import '$lib/global.css';
  import { AuthService } from '$lib/auth.service';
  import { browser } from '$app/env';
  import { take } from 'rxjs/operators';

  export async function load({ session }) {
    if (browser) {
      const authService = AuthService.getInstance();

      authService.isAuthenticated().subscribe(isAuthed => {
        session.authenticated = isAuthed;
      });

      authService.getAccessToken().subscribe(t => {
        session.token = t;
      });

      return {
        props: {
          authenticated: await authService.isAuthenticated().pipe(take(1)).toPromise()
        }
      };
    }

    return {};
  }
</script>
<slot></slot>
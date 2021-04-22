<script context="module">
  import '$lib/global.css';
  import { AuthService } from '$lib/auth.service';

  const authService = AuthService.getInstance();

  export async function load({ session }) {
      authService.isAuthenticated().subscribe(isAuthed => {
        session.authenticated = isAuthed;
      });

      authService.getAccessToken().subscribe(t => {
        session.token = t;
      });

      return {};
  }
</script>
<slot></slot>
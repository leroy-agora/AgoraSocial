import { AuthService } from '$lib/auth.service';
import { writable } from 'svelte/store';

const authService = AuthService.getInstance();

export const user = writable(null, set => {
  const subscription = authService.getUser().subscribe(user => set(user));

  return () => subscription.unsubscribe();
});

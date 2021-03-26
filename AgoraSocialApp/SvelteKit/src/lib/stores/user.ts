import { AuthService } from '$lib/auth.service';
import { take } from 'rxjs/operators';
import { writable, get } from 'svelte/store';

export const user = writable(null);

export const fetchCurrentUser = async () => {
  if (get(user) !== null) return;

  const authService = AuthService.getInstance();

  const currentUser = await authService.getUser().pipe(take(1)).toPromise();

  user.set(currentUser);
  return currentUser;
};

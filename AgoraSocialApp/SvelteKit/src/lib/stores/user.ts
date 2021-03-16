import type { IUser } from '$lib/Auth/auth.service';
import { derived, writable } from 'svelte/store';
import { session, page } from '$app/stores';

export const needToLogin = derived(
  [session, page],
  ([$s, $p]) => {
    const isApp = $p.path.startsWith('/app');
    const authed = $s.authenticated;
    
    if (authed === null) return null;

    if (isApp && authed === false) {
      return true;
    }
    return false;
  }
);

export const user= writable<IUser>({
  name: 'Anonymous'
});

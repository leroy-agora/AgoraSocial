import { AuthService } from '$lib/auth.service';
import { readable, get } from 'svelte/store';

const authService = AuthService.getInstance();

export const isAuthed = readable(null, set => {
  const subscription = authService.isAuthenticated().subscribe(isAuthed => set(isAuthed));

  return () => subscription.unsubscribe();
});

export const token = readable(null, set => {
  const subscription = authService.getAccessToken().subscribe(t => set(t));

  return () => subscription.unsubscribe();
});

export const authGuard = async (retObj: Object, redirectToLogin = true) => {
  if (get(isAuthed)) return retObj;

  try {
    await authService.signinSilent({});
    return retObj;
  } catch (e) {
    if (!redirectToLogin) return {};

    return {
      status: 307,
      redirect: '/login'
    };
  }
};

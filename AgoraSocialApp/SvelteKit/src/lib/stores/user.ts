import type { IUser } from '$lib/auth.service';
import { writable } from 'svelte/store';
export const user= writable<IUser>({
  name: 'Anonymous'
});

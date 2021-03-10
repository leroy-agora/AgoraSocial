import type { IUser } from '$components/Auth/auth.service';
import { writable, Writable } from 'svelte/store';

export const user: Writable<IUser> = writable({
  name: 'Anonymous'
});

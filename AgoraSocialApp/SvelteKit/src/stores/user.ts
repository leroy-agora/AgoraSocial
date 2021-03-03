import type { IUser } from '$components/Auth/auth.service';
import { writable } from 'svelte/store';

export const user = writable({});
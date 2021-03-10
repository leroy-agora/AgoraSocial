import { writable } from 'svelte/store';

export const token = writable(null);

export const isAuthed = writable(null);

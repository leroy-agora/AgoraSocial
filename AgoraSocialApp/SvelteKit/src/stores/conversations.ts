import { writable } from 'svelte/store';

export const conversations = writable({ data: [] });
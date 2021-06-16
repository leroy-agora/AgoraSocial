import { writable } from 'svelte/store';
import * as ANALYTICS from '$lib/constants/analytics';

export const analytics = writable({
  [ANALYTICS.TOKENS_REDEEEMED]: [
    {
      x: 1,
      y: 400
    },
    {
      x: 2,
      y: 600
    },
    {
      x: 3,
      y: 300
    },
    {
      x: 4,
      y: 750
    },
    {
      x: 5,
      y: 400
    },
    {
      x: 6,
      y: 800
    },
    {
      x: 7,
      y: 1100
    }
  ],
  [ANALYTICS.NEW_VISITORS]: [
    {
      x: 1,
      y: 100
    },
    {
      x: 2,
      y: 300
    },
    {
      x: 3,
      y: 250
    },
    {
      x: 4,
      y: 600
    },
    {
      x: 5,
      y: 400
    },
    {
      x: 6,
      y: 150
    },
    {
      x: 7,
      y: 650
    }
  ],
  [ANALYTICS.TIME_ON_SITE]: [
    {
      x: 1,
      y: 40
    },
    {
      x: 2,
      y: 50
    },
    {
      x: 3,
      y: 10
    },
    {
      x: 4,
      y: 34
    },
    {
      x: 5,
      y: 56
    },
    {
      x: 6,
      y: 5
    },
    {
      x: 7,
      y: 12
    }
  ]
});
import { resolve } from 'path';

export default {
  resolve: {
    alias: {
      $components: resolve('src/components'),
      $stores: resolve('src/stores')
    }
  },
  server: {
    hmr: {
      protocol: 'ws'
    }
  }
};

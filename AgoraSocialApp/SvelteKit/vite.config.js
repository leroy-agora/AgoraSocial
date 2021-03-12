import { resolve } from 'path';

export default {
  resolve: {
    alias: {
      $components: resolve('src/components'),
      $stores: resolve('src/stores'),
      $common: resolve('src/common')
    }
  },
  server: {
    hmr: {
      protocol: 'ws'
    }
  }
};

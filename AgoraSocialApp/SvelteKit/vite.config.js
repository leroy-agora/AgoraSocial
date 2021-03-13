import { resolve } from 'path';

export default {
  logLevel: 'error',
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

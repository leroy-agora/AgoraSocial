import { resolve } from 'path';

export default {
  resolve: {
    alias: {
      $lib: resolve('src/lib')
    }
  },
  server: {
    hmr: {
      protocol: 'ws'
    }
  }
};

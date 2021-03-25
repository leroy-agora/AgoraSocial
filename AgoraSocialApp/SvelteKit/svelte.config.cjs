const sveltePreprocess = require('svelte-preprocess');
const node = require('@sveltejs/adapter-node');
const { resolve } = require('path');
module.exports = {
	// Consult https://github.com/sveltejs/svelte-preprocess
	// for more information about preprocessors
	preprocess: sveltePreprocess({
		defaults: {
			script: 'typescript',
			style: 'postcss'
		}
	}),
	kit: {
		// By default, `npm run build` will create a standard Node app.
		// You can create optimized builds for different platforms by
		// specifying a different adapter
		adapter: node(),

		// hydrate the <div id="svelte"> element in src/app.html
		target: '#svelte',
    vite: {
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
    }
	}
};

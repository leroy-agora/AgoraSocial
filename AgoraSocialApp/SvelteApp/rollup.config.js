import svelte from 'rollup-plugin-svelte';
import commonjs from '@rollup/plugin-commonjs';
import resolve from '@rollup/plugin-node-resolve';
import livereload from 'rollup-plugin-livereload';
import { terser } from 'rollup-plugin-terser';
import sveltePreprocess from 'svelte-preprocess';
import typescript from '@rollup/plugin-typescript';
import css from 'rollup-plugin-css-only';

const production = !process.env.ROLLUP_WATCH;

function serve() {
	let server;

	function toExit() {
		if (server) server.kill(0);
	}

	return {
		writeBundle() {
			if (server) return;
			server = require('child_process').spawn('npm', ['run', 'start', '--', '--dev', '--port 4200'], {
				stdio: ['ignore', 'inherit', 'inherit'],
				shell: true
			});

			process.on('SIGTERM', toExit);
			process.on('exit', toExit);
		}
	};
}

export default {
	input: 'src/main.ts',
	output: {
		sourcemap: true,
		format: 'iife',
		name: 'app',
		file: 'public/build/bundle.js'
	},
	plugins: [
		svelte({
			preprocess: sveltePreprocess(),
			compilerOptions: {
				// enable run-time checks when not in production
				dev: !production
			}
		}),
		// we'll extract any component CSS out into
		// a separate file - better for performance
		css({ output: 'bundle.css' }),

		// If you have external dependencies installed from
		// npm, you'll most likely need these plugins. In
		// some cases you'll need additional configuration -
		// consult the documentation for details:
		// https://github.com/rollup/plugins/tree/master/packages/commonjs
		resolve({
			browser: true,
			dedupe: ['svelte']
		}),
		commonjs(),
		typescript({
			sourceMap: !production,
			inlineSources: !production
		}),

		// In dev mode, call `npm run start` once
		// the bundle has been generated
		!production && serve(),

		// Watch the `public` directory and refresh the
		// browser on changes when not in production
		!production && livereload({
			watch: 'public'/*,
			https: {
				cert: `-----BEGIN CERTIFICATE REQUEST-----
				MIIBrjCCARcCAQAwbjELMAkGA1UEBhMCVVMxEzARBgNVBAgMCk5ldy1KZXJzZXkx
				EzARBgNVBAcMClJpdmVyLUVkZ2UxITAfBgNVBAoMGEludGVybmV0IFdpZGdpdHMg
				UHR5IEx0ZDESMBAGA1UEAwwJbG9jYWxob3N0MIGfMA0GCSqGSIb3DQEBAQUAA4GN
				ADCBiQKBgQC0TyAPTbSjdHk/vNCV+MLe53+ix6jrB7FnDGQkKRseRGEe+jnoS8Ym
				PuFrGl3czJAfh+widLj6pbPe0QfsrQi8VVroH+BSeYJg3h0Z+C+k7jPAO1Ba5SU2
				li20TRJB9UpJyXXKauLUEecrytHuYXw98RFf01Dk/CG/OorI0Il39wIDAQABoAAw
				DQYJKoZIhvcNAQELBQADgYEAXwASMcOEYja3cckOE1a5EPLU0Ij5Nt2lgOfQp/J8
				1eMwswISfXNU/JLcUThjdu+l5/VWn1GZpgW///NRePHvNhomrongIvt7qz/QAHd9
				ykOtgyeiCremLTJhqEbJipESu4esZUmDWP7SHJCQhpuLkg9V/wTMb5yBUZpI6w4n
				oJc=
				-----END CERTIFICATE REQUEST-----`,
				key: `-----BEGIN RSA PRIVATE KEY-----
				MIICWwIBAAKBgQC0TyAPTbSjdHk/vNCV+MLe53+ix6jrB7FnDGQkKRseRGEe+jno
				S8YmPuFrGl3czJAfh+widLj6pbPe0QfsrQi8VVroH+BSeYJg3h0Z+C+k7jPAO1Ba
				5SU2li20TRJB9UpJyXXKauLUEecrytHuYXw98RFf01Dk/CG/OorI0Il39wIDAQAB
				AoGAV2vRsHnNbj7HlsaWH1CIMwjs0mCZnrKRKdRRLVMcydT/fcR2lRFAFkUAiCw1
				fBtWeWMucxpCoYv7pLmWbQMMK2rHZkthfqYZRZENEx/CsmAoPkE+B7Gw6V5WD0Rf
				n2YZZGsWSIySaSTcTuqzGMrqDIVwihjLrrsKzaKv08GPEsECQQDrV15nydPvELtx
				+GbG647rZkcFvGU2jYIYt4oZA8IFaGaCB1nWPtjJR5KAw20PQKl0kOaxjc6XAKiy
				N5S0YWfhAkEAxCMQI1LUjo2vnprjvC4dSYsG59x6ZLNAD7wRSeg7hbB68xrYz4bk
				LM7owo9RpmK9aQEX3owWq9JxDZYYJnp61wJAUMzlb9L+vdTL1pyHhQJ5Rl8OQWzm
				fy1knmHduyrsIXkvwLMPaByZx7mYFjeCqTr1Mz1Qx2M9q+ChCtdEag4LYQJAGeS3
				ILKytQ8nfDzei91aqHbeNoX48opnjqw5bBRop0GhOa71qJAaV4AJYoYhNWIbt10Z
				lZvQ8mYZ3r1N+uZ4iQJASJz09O+KzUYhQdpxbGFZLDFUZHE6cCMFLOJ4kM09GTA2
				YHhDGmwKI1pwdAXW9Jy7imEsb3xamp2hICYwianwQQ==
				-----END RSA PRIVATE KEY-----`
			}*/}),

		// If we're building for production (npm run build
		// instead of npm run dev), minify
		production && terser()
	],
	watch: {
		clearScreen: false
	}
};

                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                                                                
                                                                                        const { tailwindExtractor } = require('tailwindcss/lib/lib/purgeUnusedStyles');

module.exports = {
	purge: {
		content: [
			'./src/**/*.html',
			'./src/**/*.svelte'
		],
		options: {
			defaultExtractor: (content) => [
				// This is an internal Tailwind function that we're not supposed to be allowed to use
				// So if this stops working, please open an issue at
				// https://github.com/svelte-add/tailwindcss/issues
				// rather than bothering Tailwind Labs about it
				...tailwindExtractor(content),
				// Match Svelte class: directives (https://github.com/tailwindlabs/tailwindcss/discussions/1731)
				...[...content.matchAll(/(?:class:)*([\w\d-/:%.]+)/gm)].map(([_match, group, ..._rest]) => group)
			],
			keyframes: true
		}
	},
	theme: {
		extend: {
      typography: {
        DEFAULT: {
          css: {
            maxWidth: '100%'
          }
        },
        sm: {
          css: {
            h1: {
              fontWeight: 'normal',
              fontSize: '2em'
            }
          }
        }
      }
    }
	},
	variants: {
		extend: {}
	},
	plugins: [
    require('@tailwindcss/typography'),
    require('@tailwindcss/forms')
  ]
};

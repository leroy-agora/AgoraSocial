module.exports = {
  purge: {
    enabled: true,
    content: [
      '../Pages/**/*.cshtml',
      '../Areas/Identity/Pages/**/*.cshtml'
    ]
  },
  darkMode: false, // or 'media' or 'class'
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
    extend: {},
  },
  plugins: [
    require('@tailwindcss/typography'),
    require('@tailwindcss/forms')
  ],
}

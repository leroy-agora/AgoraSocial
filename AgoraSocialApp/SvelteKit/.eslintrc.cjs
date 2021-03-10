module.exports = {
  extends: '@sveltejs',
  rules: {
    'import/no-unresolved': 0
  },
  settings: {
    'svelte3/typescript': require('typescript')
  }
};

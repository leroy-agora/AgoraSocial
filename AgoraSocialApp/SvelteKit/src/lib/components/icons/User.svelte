<script>
  import SmallIcon from '$lib/components/icons/_Small.svelte';
  
  let className = '';
  export { className as class };
</script>
<SmallIcon class={`circle-icon ${className}`}>
  <circle cx="10" cy="10" r="7.9" />
  <path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-6-3a2 2 0 11-4 0 2 2 0 014 0zm-2 4a5 5 0 00-4.546 2.916A5.986 5.986 0 0010 16a5.986 5.986 0 004.546-2.084A5 5 0 0010 11z" clip-rule="evenodd" />
</SmallIcon>

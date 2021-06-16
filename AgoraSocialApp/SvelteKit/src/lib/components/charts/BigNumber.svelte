<script>
  export let value;
  export let title;
</script>
<div on:click class="big-number flex flex-col items-center cursor-pointer py-6 px-12 shadow-md rounded-md border">
  <div class="font-sans text-7xl font-thin">
    {typeof value === 'number' ? value : '-'}
  </div>
  <div class="font-serif text-s">
    {title}
  </div>
</div>
<style>
  .big-number {
    width: 31%;
  }
</style>
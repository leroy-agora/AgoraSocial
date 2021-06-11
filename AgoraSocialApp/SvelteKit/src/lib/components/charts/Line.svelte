<script>
  import {
    Chart,
    Grid,
    Svg,
    SvgLine,
    Point
  } from '@sveltejs/pancake';

  export let title;
  export let values = [];
</script>
<h1>{title}</h1>
{#each values as value}
  <div>{value}</div>
{/each}
<script>
  import BigNumber from '$lib/components/charts/BigNumber.svelte';
  import Line from '$lib/components/charts/Line.svelte';
</script>
<BigNumber number={100} />
<Line title="chart" values={[1, 2]} />

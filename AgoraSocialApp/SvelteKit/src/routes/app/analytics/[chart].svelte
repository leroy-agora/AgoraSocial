<script context="module">
  export function load({ page }) {
    return {
      props: {
        chart: page.params.chart
      }
    }
  }
</script>
<script>
  import { goto } from '$app/navigation';
  import * as ANALYTICS from '$lib/constants/analytics';
  import { analytics } from '$lib/stores/analytics';
  import BigNumber from '$lib/components/charts/BigNumber.svelte';
  import Line from '$lib/components/charts/Line.svelte';
import { onMount } from 'svelte';

  export let chart = ANALYTICS.TOKENS_REDEEEMED;
  let values = [];
  $: values = $analytics[chart];

  onMount(() => { console.log('mount')});
</script>
<div class="prose prose-sm">
  <h1>Analytics</h1>
  <div class="flex justify-between">
    <BigNumber
      value={1200}
      title="Tokens Redeemed"
      on:click={() => goto(`/app/analytics/${ANALYTICS.TOKENS_REDEEEMED}`)}
    />
    <BigNumber
      value={600}
      title="New Visitors"
      on:click={() => goto(`/app/analytics/${ANALYTICS.NEW_VISITORS}`)}
    />
    <BigNumber
      value={30}
      title="Time on Site"
      on:click={() => goto(`/app/analytics/${ANALYTICS.TIME_ON_SITE}`)}
    />
  </div>
  <div class="h-96 mt-6">
    <Line title="chart" values={values} />
  </div>
</div>
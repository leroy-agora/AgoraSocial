<script>
  import * as Pancake from '@sveltejs/pancake';
  import { draw, fade } from 'svelte/transition';

  export let title = '';
  export let values = [];

  let minx;
  let maxx;
  let miny;
  let maxy;

  $: {
    minx = values[0].x;
    maxx = values[values.length -1].x;

    miny = +Infinity;
    maxy = -Infinity;
    
    for (let i = 0; i < values.length; i++) {
      const val = values[i];

      if (val.y < miny) {
        miny = val.y;
      }

      if (val.y > maxy) {
        maxy = val.y;
      }
    }
  }
</script>
<div class="px-16 pt-6 pb-12 shadow-lg border rounded-md h-full">
  <Pancake.Chart x1={minx} x2={maxx} y1={miny} y2={maxy}>
    <Pancake.Grid horizontal count={5} let:value>
      <div class="grid-line horizontal">
        <span class="label">
          {value}
        </span>
      </div>
    </Pancake.Grid>
  
    <Pancake.Grid vertical count={7} let:value>
      <div class="grid-line vertical"></div>
      <span class="label vertical-label">{value}</span>
    </Pancake.Grid>
  
    <Pancake.Svg>
      <Pancake.SvgLine data={values} x="{d => d.x}" y="{d => d.y}" let:d>
        <path
          class="line"
          transition:draw="{{ duration: 2000 }}"
          {d}
        />
      </Pancake.SvgLine>
    </Pancake.Svg>
  </Pancake.Chart>
</div>
<style>
  .grid-line {
    position: relative;
  }
  .grid-line.horizontal {
    width: calc(100% + 6em);
    left: -3em;
    border-bottom: 1px dashed #ccc;
  }

  .grid-line.horizontal span {
    position: absolute;
  }

  .grid-line.vertical {
    height: 100%;
    border-left: 1px dashed #ccc;
  }

  .label {
    color: #999;
    position: absolute;
    line-height: 1;
    font-size: 14px;
  }

  .grid-line span  {
    bottom: 2px;
  }
  
  .vertical-label {
    left: -3px;
    bottom: -2em;
  }

  path.line {
    stroke: rgba(165, 52, 165, 0.9);
    stroke-width: 1px;
    fill: none;
    stroke-linejoin: round;
		stroke-linecap: round;
  }
</style>
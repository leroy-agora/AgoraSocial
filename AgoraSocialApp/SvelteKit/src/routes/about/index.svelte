<h1>About</h1>
<p>wheeee</p>

<script lang="ts">
import { page } from '$app/stores';
import { onMount } from 'svelte';

onMount(async () => {
    console.log('test');
    console.log(page);
});

</script>
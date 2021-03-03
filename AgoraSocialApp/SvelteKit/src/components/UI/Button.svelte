<button on:click>
  <slot />
</button>

<style>
	button {
		font-family: inherit;
		font-size: inherit;
    cursor: pointer;
		padding: 1em 2em;
		color: #ffffff;
		background-color: rgba(41, 0, 88, 0.8);
		border-radius: 4px;
		border: 1px solid rgba(41, 0, 88, 0.8);
		outline: none;
		width: 200px;
		font-variant-numeric: tabular-nums;
	}

	button:focus {
		border: 2px solid rgba(41, 0, 88, 0.8);
	}

	button:active {
		background-color:rgba(41, 0, 88, 0.8);
	}
</style>
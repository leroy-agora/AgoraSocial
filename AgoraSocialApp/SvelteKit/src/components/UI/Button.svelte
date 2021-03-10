<button on:click>
  <slot />
</button>

<style>
	button {
		font-family: inherit;
		font-size: inherit;
    cursor: pointer;
		padding: 1em;
		color: #ffffff;
		background-color: rgba(113, 28, 211, 0.8);
		border-radius: 4px;
		border: 1px solid rgba(113, 28, 211, 0.8);;
		outline: none;
		font-variant-numeric: tabular-nums;
	}

	button:focus {
		border: 2px solid rgba(113, 28, 211, 0.8);
	}

	button:active {
		background-color:rgba(113, 28, 211, 0.8);
	}
</style>
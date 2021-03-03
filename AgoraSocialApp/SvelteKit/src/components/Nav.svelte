<script type="ts">
  import { AuthService } from '$components/Auth/auth.service';
  import Logo from "$components/UI/Logo.svelte";
  import { goto } from "$app/navigation";
</script>
<header>
  <Logo />
  <nav>
    <a href="/">Home</a>
    <a href="/about">About</a>
    <button on:click={() => true ?
      goto('/authentication/logout')
      : 
      goto('/authentication/login')}
    >
      {true ? 'Sign Out' : 'Sign In'}
    </button>
</nav>
</header>

<style>
  nav {
    float: right;
  }

  a, button {
    cursor: pointer;
    color: rgba(41, 0, 88, 0.8);
    font-family: inherit;
    background-color: transparent;
    border: 0;
    text-decoration: none;
    font-size: 20px;
    padding: 0;
    margin: 1rem;
  }

  button:focus {
    outline: none;
  }
</style>
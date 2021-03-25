<script>
  import UserIcon from '$lib/components/icons/User.svelte';
</script>
<div class="max-w-md mx-auto prose prose-sm">
  <div class="flex flex-col items-center">
    <UserIcon class="w-28" />
    <h1>Welcome to Agora !</h1>
  </div>
  <div class="grid grid-cols-1 gap-4">
    <label class="block">
      <span>Google Account</span>
      <input type="email" class="input" />
    </label>
    <label class="block">
      <span>First name</span>
      <input type="text" class="input" />
    </label>
    <label class="block">
      <span>Last name</span>
      <input type="text" class="input" />
    </label>
    <label class="block">
      <span>Handle</span>
      <input type="text" class="input" />
      <span class="">Enter a unique user name with a minimum of 5 characters</span>
    </label>
  <button class="btn">
    Confirm
  </button>
  </div>
</div>

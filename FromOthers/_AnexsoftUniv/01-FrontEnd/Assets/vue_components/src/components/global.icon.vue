<template>
  <i :class="'fa fa-' + name"></i>
</template>

<script>
export default {
  name: 'icon',
  props: ['name']
}
</script>
<template>
  <div :style="{ height: height + 'px', position: 'relative' }">
    <div class="block-loading" ></div>  
  </div>
</template>

<script>
export default {
  name: 'loader',
  props: ['height']
}
</script>
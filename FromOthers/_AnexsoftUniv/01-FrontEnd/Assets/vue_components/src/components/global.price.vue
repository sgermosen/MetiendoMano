<template>
  <span>$ {{ valueForView }}</span>
</template>

<script>
export default {
  name: 'price',
  props: {
    value: {
      type: Number,
      required: true
    }
  },
  computed: {
    valueForView() {
      return this.value.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
    }
  }
}
</script>
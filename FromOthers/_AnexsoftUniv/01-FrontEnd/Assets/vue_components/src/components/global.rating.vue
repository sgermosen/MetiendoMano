<template>
  <span>
    <i :class="star1"></i>
    <i :class="star2"></i>
    <i :class="star3"></i>
    <i :class="star4"></i>
    <i :class="star5"></i>
    {{ value }} / 10
  </span>
</template>

<script>
export default {
  name: 'rating',
  props: {
    value: {
      type: Number,
      required: true
    }
  },
  computed: {
    star1() {
      return {
        'fa fa-star': (Math.floor(this.value) > 0),
        'fa fa-star-o': (Math.floor(this.value) == 0),
      }
    },
    star2() {
      return {
        'fa fa-star': (Math.floor(this.value) > 2),
        'fa fa-star-o': (Math.floor(this.value) <= 2),
      }
    },
    star3() {
      return {
        'fa fa-star': (Math.floor(this.value) > 4),
        'fa fa-star-o': (Math.floor(this.value) <= 4),
      }
    },
    star4() {
      return {
        'fa fa-star': (Math.floor(this.value) > 6),
        'fa fa-star-o': (Math.floor(this.value) <= 6),
      }
    },
    star5() {
      return {
        'fa fa-star': (Math.floor(this.value) > 8),
        'fa fa-star-o': (Math.floor(this.value) <= 8),
      }
    }
  }
}
</script>
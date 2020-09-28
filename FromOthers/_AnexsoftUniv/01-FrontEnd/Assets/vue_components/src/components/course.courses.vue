<template>
<div>

</div>
</template>

<script>
import icon from './global.icon.vue'
export default {
  components: {
    icon
  },
  name: 'courses',
  data() {
    return {
      courses: []
    }
  },
  props: {
    categoryId: {
      type: Number,
      default: 0
    }
  },
  mounted() {
    var self = this;
  },
  methods: {
    all() {
      var self = this;
      $.post('/course/all', { categoryId: self.categoryId }, function(r){ 
        self.courses = r;
      }, 'json')
    }
  }
}
</script>
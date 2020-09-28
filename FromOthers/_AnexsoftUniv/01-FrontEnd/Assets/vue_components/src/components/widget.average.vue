<template>
<ul class="list-group">
  <li class="list-group-item">
      Ingreso Anexu <span class="badge"><price :value="IncomeCompany"></price></span>
  </li>
  <li class="list-group-item">
      Alumnos <span class="badge">{{ NewStudents }}</span>
  </li>
  <li class="list-group-item">
      Cursos creados <span class="badge">{{ NewCourses }}</span>
  </li>
</ul>
</template>

<script>
import price from './global.price.vue'
export default {
  name: 'widgetaverage',
  components: { price },
  props: {

  },
  data() {
    return {
      IncomeCompany: 0,
      NewStudents: 0,
      NewCourses: 0
    }
  },
  mounted() {
    this.get();
  },
  methods: {
    get() {
      let self = this;
      $.post('/panel/WidgetGetAverage', function(r) {
        self.IncomeCompany = r.IncomeCompany;
        self.NewStudents = r.NewStudents;
        self.NewCourses = r.NewCourses;
      }, 'json')
    }
  }
}
</script>
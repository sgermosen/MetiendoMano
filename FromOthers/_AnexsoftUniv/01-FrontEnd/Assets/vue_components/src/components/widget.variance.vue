<template>
<ul class="list-group">
  <li class="list-group-item">
      Ingreso Anexu <span class="badge"><i :class="'fa fa-level-' + (IncomeCompany > 0 ? 'up' : 'down')"></i> {{ IncomeCompany }}%</span>
  </li>
  <li class="list-group-item">
      Alumnos <span class="badge"><i :class="'fa fa-level-' + (NewStudents > 0 ? 'up' : 'down')"></i> {{ NewStudents }}%</span>
  </li>
  <li class="list-group-item">
      Cursos creados <span class="badge"><i :class="'fa fa-level-' + (NewCourses > 0 ? 'up' : 'down')"></i> {{ NewCourses }}%</span>
  </li>
</ul>
</template>

<script>
export default {
  name: 'widgetvariance',
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
      $.post('/panel/WidgetGetVariance', function(r) {
        self.IncomeCompany = r.IncomeCompany;
        self.NewStudents = r.NewStudents;
        self.NewCourses = r.NewCourses;
      }, 'json')
    }
  }
}
</script>
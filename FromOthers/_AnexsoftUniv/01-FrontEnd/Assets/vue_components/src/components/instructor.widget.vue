<template>
<div class="well income-report text-center">
    <div class="row">
        <div class="col-sm-3 income">
            <h5>Total de ingresos</h5>
            <span class="description">
              <price :value="Total"></price>
            </span>
        </div>
        <div class="col-sm-3 income">
            <h5>Ingreso del mes</h5>
            <span class="description">
              <price :value="TotalPerMonth"></price>
            </span>
        </div>
        <div class="col-sm-3 income">
            <h5>Calificación media</h5>
            <span class="description">{{ ReputationForView }}/10</span>
        </div>
        <div class="col-sm-3 income last">
            <h5>Total de estudiantes</h5>
            <span class="description">{{ Students }}</span>
        </div>
    </div>
</div>
</template>

<script>
import price from './global.price.vue'
export default {
  name: 'instructorwidget',
  components: { price },
  props: {
    url: {
      type: String,
      requide: true
    },
    userId: {
      type: String,
      requide: true
    }
  },
  data() {
    return {
      Total: 0,
      TotalPerMonth: 0,
      Students: 0,
      Reputation: 0
    }
  },
  mounted() {
    this.get();
  },
  computed: {
    ReputationForView() {
      return this.Reputation.toFixed(2);
    }
  },
  methods: {
    get() {
      let self = this;
      $.post(self.url, {
        userId: self.userId
      }, function(r) {
        self.Total = r.Total;
        self.TotalPerMonth = r.TotalPerMonth;
        self.Students = r.Students;
        self.Reputation = r.Reputation;
      }, 'json')
    }
  }
}
</script>
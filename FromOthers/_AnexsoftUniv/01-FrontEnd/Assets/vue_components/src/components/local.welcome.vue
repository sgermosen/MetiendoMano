<template>
    <div class="jumbotron">
      <h1>{{ project }}</h1>
      <p class="lead">Buscamos ser la <b>mejor plataforma online de eseñanza en español</b>. <b>Regístrate</b> hoy mismo y adquiere <b>50 USD</b> para tomar cualquiera de nuestros cursos.</p>
      <p><a :href="link" class="btn btn-primary btn-lg">Registrate hoy mismo &raquo;</a></p>
    </div>
</template>

<script>
export default {
  name: 'welcome',
  props: {
    project: {
      type: String,
      required: true
    },
    link: {
      type: String,
      requide: true
    }
  },
  mounted() {
    this.$parent.$on('welcomeSelectedID', x => {
      this.project = x;
    })
  }
}
</script>
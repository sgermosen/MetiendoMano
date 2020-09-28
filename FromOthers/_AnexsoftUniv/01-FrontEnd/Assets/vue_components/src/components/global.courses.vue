<template>
<div>
  <loader v-if="loading" height="500"></loader>
  <div v-if="!loading" class="row">
  <!-- Cuando no se han encontrado cursos -->
  <div v-if="courses.length === 0" class="text-center col-sm-12">
    <div class="alert alert-info">
      <icon name="frown-o"></icon> No se han encontrado cursos
    </div>
  </div>

  <!-- Listado de cursos -->
  <div v-for="c in courses" class="col-md-4">
    <div class="course">
        <a :href="'/' + c.Slug">
            <img class="picture" :src="'/' + c.Image" :alt="c.name" />
        </a>
        <h2 class="title">
            <a :title="c.Name" :href="'/' + c.Slug">{{ truncate(c.Name) }}</a>
        </h2>
        <span class="category">
            <icon :name="c.CategoryIcon"></icon> {{ c.CategoryName }}
        </span>
        <div class="statistics">
            <rating :value="c.Vote"></rating> -
            <icon name="users"></icon> ({{ c.Students }})
        </div>
        <span class="teacher">
            <icon name="user"></icon> Por {{ c.Instructor }}
        </span>
        <p class="description">
            {{ c.Description }}
        </p>
        <div class="price text-center text-success well">
            <span v-if="c.price === 0">¡Gratis!</span>
            <price v-if="c.price > 0" :value="c.Price"></price>
        </div>
    </div>
  </div>
  </div>
</div>
</template>

<script>
import loader from './global.loader.vue'
import price from './global.price.vue'
import rating from './global.rating.vue'
import icon from './global.icon.vue'

export default {
  components: {
    loader, price, rating, icon
  },
  name: 'courses',
  data() {
    return {
      loading: false,
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
    self.all();
  },
  methods: {
    all() {
      let self = this;
      self.loading = true;

      $.post('/course/all', {
        categoryId: self.categoryId
      }, function(r) {
        self.courses = r;
        self.loading = false;
      }, 'json')
    },
    truncate(value) {
      if(value.length > 40) return value.substr(0, 37) + ' ..';
      else return value;
    }
  }
}
</script>
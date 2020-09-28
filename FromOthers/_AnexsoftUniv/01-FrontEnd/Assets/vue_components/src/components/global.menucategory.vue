<template>
<ul class="nav navbar-nav">
    <li v-for="d in data">
        <a :href="'/' + d.Slug">
            <i :class="'fa fa-' + d.Icon"></i> {{ d.Name }}
        </a>
    </li>
</ul>
</template>

<script>
export default {
  name: 'menucategory',
  props: {
    data: {
      type: Array,
      required: true
    }
  }
}
</script>
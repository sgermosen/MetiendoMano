<template>
<div>
  <input v-model="Id" type="hidden" name="Id">
  <div class="alert-container"></div>
  <div class="form-group">
      <label>Ícono {{ Id }}</label>
      <div class="input-group">
          <input v-model="Icon" type="text" class="form-control" name="Icon">
          <span class="input-group-addon"><icon :name="Icon"></icon></span>
      </div>
      <span data-key="Icon" class="form-validation-failed"></span>
  </div>
  <div class="form-group">
      <label>Nombre</label>
      <input v-model="Name" type="text" class="form-control" name="Name">
      <span data-key="Name" class="form-validation-failed"></span>
  </div>
</div>
</template>

<script>
import icon from './global.icon.vue'
export default {
  components: {
    icon
  },
  data() {
    return {
      Id: 0,
      Icon: 'code',
      Name: '',
    }
  },
  name: 'category',
  props: {
    url: {
      type: String,
      required: true
    }
  },
  mounted() {
    var self = this;
    this.$parent.$on('categorySelectedID', function(v) {
      self.Id = v;
    })
  },
  watch: {
    Id(newValue, oldValue) {
      if(newValue > 0) {
        this.getCategory(newValue);
      } else {
        this.newRecord();
      }
    }
  },
  methods: {
    getCategory(id) {
      var self = this;
      $.post(self.url, { id: id }, function(r){ 
        self.Name = r.Name;
        self.Icon = r.Icon;
        self.Id   = id;
      }, 'json')
    },
    newRecord() {
      var self = this;
      self.Name = '';
      self.Icon = 'code';
      self.Id   = 0;
    }
  }
}
</script>
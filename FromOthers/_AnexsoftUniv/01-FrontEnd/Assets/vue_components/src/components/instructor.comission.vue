<template>
  <div class="form-group">
      <label>Precio <span class="text-danger">*</span></label>
      <div class="input-group">
          <input v-model="price" type="number" name="Price" class="form-control"  />
          <span class="input-group-addon"><b>Ingreso ({{ comissionForView }}%)</b>: <price :value="comissionForPrice"></price></span>
      </div>
      <span data-key="Price" class="form-validation-failed"></span>
  </div>
</template>

<script>
import price from './global.price.vue'
export default {
  name: 'comission',
  components: { price },
  data() {
    return {
      price: 0
    }
  },
  props: {
    value: {
      type: Number,
      required: true
    },
    comission: {
      type: Number,
      required: true
    }
  },
  mounted() {
    this.price = this.value;
  },
  computed: {
    comissionForPrice() {
      return this.comission * this.price;
    },
    comissionForView() {
      return 100 * this.comission;
    }
  }
}
</script>
<template>
<div class="custom-container">
  <h2>Usuarios con mayor participación</h2>
  <el-table
    :data="grid.items"
    :default-sort = "{prop: 'name', order: 'ascending'}"
    style="width: 100%">
    <el-table-column prop="user" label="Nombre" sortable></el-table-column>
    <el-table-column prop="likes" label="Likes" sortable width="100"></el-table-column>
    <el-table-column prop="comments" label="Comentarios" sortable width="140"></el-table-column>
    <el-table-column prop="photos" label="Fotos" sortable width="100"></el-table-column>
    <el-table-column prop="score" label="Score" sortable width="100"></el-table-column>
  </el-table>
  <div class="pager">
    <el-pagination
      @size-change="sizeChange"
      @current-change="currentChange"
      :current-page.sync="grid.pagination.page"
      :page-sizes="[20]"
      layout="total, sizes, prev, pager, next, jumper"
      :total="grid.total">
    </el-pagination>
  </div>
</div>
</template>

<script>
export default {
  name: "report",
  created() {
    let self = this;

    if(window.User.Role !== 'Admin') {
      self.$router.push('/');
    }

    self.getAll();
  },
  data() {
    return {
      grid: {
        items: [],
        total: 0,
        pagination: {
          rowsPerPage: 20,
          page: 1,
          sortBy: "name",
          descending: false
        },
        loading: false
      }
    };
  },
  methods: {
    getAll() {
      let self = this;
      self.grid.loading = true;

      self.$store.state.services.reportService
        .getPaged(self.grid.pagination)
        .then(r => {
          self.grid.items = r.data.data || [];
          self.grid.total = r.data.total;

          self.grid.loading = false;
        })
        .catch(r => {
          self.grid.items = [];
          self.grid.total = 0;

          self.grid.loading = false;
        });
    },
    sizeChange(val) {
      this.grid.pagination.rowsPerPage = val;
      this.getAll();
    },
    currentChange(val) {
      this.grid.pagination.page = val;
      this.getAll();
    }
  }
};
</script>
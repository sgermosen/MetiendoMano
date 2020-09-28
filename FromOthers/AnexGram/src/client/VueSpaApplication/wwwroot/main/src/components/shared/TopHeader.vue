<template>
  <el-row>
    <el-col :span="10">
      <h1 @click="goTo('/')"><i class="fa fa-camera"></i> AnexGram</h1>
    </el-col>
    <el-col :span="4" class="text-center">
      <el-autocomplete suffix-icon="fa fa-search" :trigger-on-focus="false" style="width:100%;" :fetch-suggestions="searchUsers" placeholder="Buscar ..">
      <template slot-scope="props">
        <div @click="goTo('/u/' + props.item.seourl)">
          {{ props.item.value }}
        </div>
      </template>
      </el-autocomplete>
    </el-col>
    <el-col :span="10" class="text-right user-options">
      <a v-if="isAdmin" class="item" href="#/reports">
        <i class="fas fa-chart-line"></i> Reporte
      </a>
      <a class="item" href="#/mi-informacion">
        {{ user.Name }}
      </a>
      <a class="item" href="/auth/logout">
        <i class="fa fa-sign-out-alt"></i>
      </a>
      <a :href="'#/u/' + user.SeoUrl">
        <img class="avatar" v-if="image != null" :src="image" />
      </a>
    </el-col>
  </el-row>
</template>

<style>
#header {
  height: 60px;
  line-height: 60px;
  background: #fff;
  border-bottom: 1px solid #ccc;
  box-shadow: 0px 0px 15px 0px #eee;
}

#header h1 {
  margin: 0;
}

#header .user-options a {
  color: #222;
  text-decoration: none;
}

#header .user-options .avatar {
  height: 60px;
  float: right;
}

#header .user-options .item {
  font-size: 1.2em;
  padding: 5px 15px;
  border-radius: 10px;
  margin-right: 5px;
}

#header .user-options .item:hover {
  background: #fff;
}

#header .el-header .user-options .item:last-child {
  margin-right: 0;
}
</style>

<script>
export default {
  name: "TopHeader",
  data: () => ({
    user: window.User
  }),
  methods: {
    goTo(path) {
      if (path === undefined) return;
      
      this.$router.push(path);
    },
    searchUsers(queryString, cb) {
      if (queryString.trim().length === 0) return;

      let self = this;

      self.$store.state.services.userService
        .getAll({
          name: queryString.trim(),
          sort: 'name',
          desceding: true
        })
        .then(r => {
          let data = [];

          r.data.forEach(x => {
            data.push({
              value: `${x.lastname}, ${x.name}`,
              userId: x.userId,
              seourl: x.seoUrl
            });
          });

          cb(data);
        })
        .catch(r => {
          self.$message({
            message: "Ocurrió un error inesperado",
            type: "error"
          });
        });
    }
  },
  computed: {
    isAdmin() {
      return window.User.Role === 'Admin'
    },
    image() {
      let user = window.User;

      if (user.Image != null && user.Image.length > 0) {
        return `${window.Api.url}uploads/${user.Image}`;
      }

      return null;
    }
  }
};
</script>
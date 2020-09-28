<template>
<div class="custom-container" v-loading="loading">
  <el-tabs v-model="selectedTab">
    <el-tab-pane label="Información" name="first">
      <el-form ref="form" :model="form" :rules="rules" label-width="120px">
        <h2 class="page-title">Mi información personal</h2>

        <el-form-item label="Nombre" prop="name">
          <el-input v-model="form.name"></el-input>
        </el-form-item>

        <el-form-item label="Apellido" prop="lastname">
          <el-input v-model="form.lastname"></el-input>
        </el-form-item>

        <el-form-item label="Email">
          <el-input :readonly="true" v-model="form.email"></el-input>
        </el-form-item>

        <el-form-item label="Acerca de mí" prop="aboutUs">
          <el-input type="textarea" :rows="3" v-model="form.aboutUs"></el-input>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="onSubmit">Actualizar</el-button>
          <el-button @click="$router.push(`/`)">Cancelar</el-button>
        </el-form-item>
      </el-form>
    </el-tab-pane>
    <el-tab-pane label="Imagen" name="second">
      <el-form label-width="120px">
        <h2 class="page-title">Mi imagen</h2>

        <el-form-item label="Imagen">
          <input id="userImage" type="file" accept="image/*" />
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="upload">Subir</el-button>
        </el-form-item>
      </el-form>
    </el-tab-pane>
  </el-tabs>
</div>
</template>

<script>
export default {
  name: "Info",
  created() {
    let self = this;
    self.get();
  },
  data: () => {
    return {
      loading: true,
      selectedTab: "first",
      rules: {
        name: [
          {
            required: true,
            message: "Campo requerido"
          },
          {
            min: 3,
            max: 15,
            message: "Como mínimo 3 a 15 caracteres"
          }
        ],
        lastname: [
          {
            required: true,
            message: "Debe ingresar su apellido"
          }
        ],
        aboutUs: [
          {
            required: true,
            message: "Debe ingresar la información acerca de usted"
          }
        ]
      },
      form: {
        name: null,
        lastname: null,
        email: null,
        aboutUs: null,
        image: null
      }
    };
  },
  methods: {
    onSubmit() {
      let self = this;

      self.loading = true;
      self.$store.state.services.userService
        .partial(window.User.UserId, self.form)
        .then(r => {
          self.loading = false;

          self.$store.state.services.userService.refreshClaims();
        })
        .catch(r => {
          console.log(r);
          self.loading = false;
        });
    },
    get() {
      let self = this;
      self.$store.state.services.userService
        .get(window.User.UserId, {
          userId: window.User.UserId
        })
        .then(r => {
          self.form = r.data;
          self.loading = false;
        })
        .catch(r => {
          console.log(r);
          self.loading = false;
        });
    },
    upload() {
      let self = this,
          input = document.getElementById("userImage"),
          files = input.files;

      if (files != null || files.length > 0) {
        self.$store.state.services.fileService
          .get(files[0])
          .then(file => {
            self.loading = true;

            self.$store.state.services.userService
              .image(window.User.UserId, file)
              .then(r2 => {
                self.loading = false;
                input.value = null;

                self.$store.state.services.userService.refreshClaims();
              })
              .catch(r2 => {
                self.loading = false;
                console.log(r2);
              });
          })
          .catch(r1 => {
            console.log(r1);
          });
      }
    }
  }
};
</script>
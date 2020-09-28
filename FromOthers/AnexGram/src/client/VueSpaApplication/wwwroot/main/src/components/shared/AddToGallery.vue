<template>
<div id="uploader">
    <el-button :disabled="loading" type="primary" @click="openDialogFiles">
        <i :class="loading ? 'fas fa-circle-notch fa-spin' : 'fas fa-plus'"></i>
    </el-button>
    <input @change="selectedFile" class="input-file hidden" type="file" accept="image/*" />
</div>
</template>

<script>
export default {
  name: "AddToGallery",
  props: {},
  data: () => ({
      loading: false
  }),
  methods: {
    openDialogFiles() {
      document.querySelector("#uploader .input-file").click();
    },
    selectedFile() {
      let self = this,
        input = document.querySelector("#uploader .input-file"),
        files = input.files;

      if (files != null || files.length > 0) {
        self.$store.state.services.fileService
          .get(files[0])
          .then(file => {
            self.loading = true;

            self.$store.state.services.photoService
              .create({
                  file,
                  model: {
                      userId: window.User.UserId
                  }
              })
              .then(r2 => {
                self.loading = false;
                input.value = null;

                self.$parent.getAll();

                self.$notify({
                    title: 'Realizado',
                    message: 'Se subió su imagen al servidor',
                    type: 'success'
                })
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
  },
  created() {}
};
</script>

<style>
#uploader {
  position: fixed;
  right: 20px;
  bottom: 20px;
  z-index: 2;
}

#uploader .el-button {
  border-radius: 100%;
  padding: 15px;
  font-size: 1.4em;
}
</style>

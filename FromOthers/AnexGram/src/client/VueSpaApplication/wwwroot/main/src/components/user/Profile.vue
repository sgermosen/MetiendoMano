<template>
<div id="user-profile" v-loading="loading">
    <div class="custom-container" v-loading="loading">
        <div v-if="user.image != undefined && user.image != null" class="avatar">
          <img :src="imageApiPath">
        </div>
        <h2>{{user.name}} {{user.lastname}}</h2>
        <b>Acerca de mí</b>
        <p>{{user.aboutUs}}</p>
        <p><b>Email</b>: {{user.email}}</p>
        <p><b>Sígueme en</b>: anexgram.com/u/{{user.seoUrl}}</p>
        <div class="clearfix"></div>
    </div>
    <div class="album">
      <gallery v-if="!loading" rows="5" :can-upload-image="false" :by-user="user.seoUrl"></gallery>
    </div>
</div>
</template>

<style>
#user-profile .avatar {
  float: left;
  margin-right: 20px;
}

#user-profile .avatar img{
  max-width: 300px;
}

#user-profile .album {
  background: #fff;
  margin-top: 20px;
}

</style>

<script>
import gallery from "@/components/shared/Gallery";

export default {
  name: "profile",
  components: {
    gallery
  },
  created() {
    let self = this;
    self.get();
  },
  data: () => {
    return {
      loading: false,
      user: {}
    };
  },
  methods: {
    get() {
      let self = this;
      let url = self.$route.params.url;

      self.loading = true;

      self.$store.state.services.userService
        .get(url, {
          seoUrl: url
        })
        .then(r => {
          self.loading = false;
          self.user = r.data;
        })
        .catch(r => {
          console.log(r);
          self.loading = false;
        });
    }
  },
  computed: {
    imageApiPath() {
      let self = this;
      return window.Api.url + 'uploads/' + self.user.image;
    }
  }
};
</script>
<template>
<el-dialog
:title="'Publicado por ' + item.userName"
:visible.sync="isVisible"
:before-close="close"
width="40%">
  <div v-if="loading" v-loading="loading" class="gallery-dialog-loader"></div>
  <div v-if="!loading" id="gallery-dialog">
    <div class="img-container" @click="openDialog(item)">
        <img :src="imageApiPath" />
    </div>
    <div class="like-container">
      <el-button v-if="!item.iLikedIt" type="danger" round @click="like">
        <i class="fas fa-heart"></i> Darle me gusta
      </el-button>
      <el-button v-if="item.iLikedIt" round @click="like" type="warning">
        <i class="fas fa-cancel"></i> Ya no me gusta
      </el-button>
    </div>
    <div class="comment-container">
      <el-input
        @keypress="comment"
        v-model="commentEntry.comment"
        type="textarea"
        :rows="2"
        placeholder="Ingrese un comentario y luego presione enter"></el-input>
        <div class="button-container">
          <el-button :disabled="commentEntry.comment.trim().length <= 3" type="primary" @click="comment">
          <i class="fas fa-comment"></i> Comentar
        </el-button>
        </div>
    </div>
    <div class="comments-container" v-if="comments.length > 0">
      <div v-for="item in comments" class="comment">
        <span class="user">@{{item.user.toLowerCase()}} <small>{{ item.createdAt | moment("calendar") }}</small></span>{{item.comment}} 
      </div>
    </div>
  </div>
</el-dialog>
</template>

<script>
export default {
  name: "Default",
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    item: {
      type: Object,
      default: null
    }
  },
  data() {
    let self = this;
    return {
      loading: false,
      commentEntry: {
        comment: "",
        photoId: self.item.photoId,
        userId: window.User.UserId
      },
      comments: []
    };
  },
  methods: {
    close() {
      let self = this;
      self.$parent.selectedItem = null;
      self.$parent.getAll();
    },
    like() {
      let self = this;
      self.loading = true;

      if (!self.item.iLikedIt) {
        self.$store.state.services.likeService
          .create({
            photoId: self.item.photoId,
            userId: window.User.UserId
          })
          .then(r => {
            self.loading = false;
            self.item.iLikedIt = true;

            self.$notify({
              title: "Realizado",
              message: "Acaba de dar like a la fotografía",
              type: "success"
            });
          })
          .catch(r => {
            console.log(r);
            self.loading = false;
          });
      } else {
        self.$store.state.services.likeService
          .remove(self.item.photoId)
          .then(r => {
            self.loading = false;
            self.item.iLikedIt = false;

            self.$notify({
              title: "Realizado",
              message: "Acaba de retirar el like a la fotografía",
              type: "success"
            });
          })
          .catch(r => {
            console.log(r);
            self.loading = false;
          });
      }
    },
    comment() {
      let self = this;
      self.loading = true;

      self.$store.state.services.commentService
        .create(self.commentEntry)
        .then(r => {
          self.loading = false;
          self.commentEntry.comment = '';

          self.getAllComments()

          self.$notify({
            title: "Realizado",
            message: "Acaba de comentar una fotografía",
            type: "success"
          });
        })
        .catch(r => {
          console.log(r);
          self.loading = false;
        });
    },
    getAllComments() {
      let self = this;

      self.$store.state.services.commentService
        .getAll(20, {
          photoId: self.item.photoId
        })
        .then(r => {
          self.comments = r.data
        })
        .catch(r => {
          console.log(r);
          self.loading = false;
        });
    }
  },
  created() {
    this.getAllComments()
  },
  computed: {
    isVisible() {
      let self = this;
      return self.$parent.selectedItem != null;
    },
    imageApiPath() {
      let self = this;
      return window.Api.url + self.item.imagePath;
    }
  }
};
</script>

<style>
.gallery-dialog-loader {
  height: 300px;
}

#gallery-dialog {
  min-height: 150px;
}

#gallery-dialog .img-container {
  overflow: hidden;
  margin: -20px -20px 0;
}

#gallery-dialog .img-container img {
  width: 100%;
}

#gallery-dialog .like-container button {
  width: 100%;
  margin-bottom: 10px;
}

#gallery-dialog .comments-container {
  overflow-y: scroll;
  max-height: 200px;
}

#gallery-dialog .button-container {
  margin-top: 10px;
  margin-bottom: 10px;
}

#gallery-dialog .button-container button {
  width: 100%;
}

#gallery-dialog .comments-container .comment {
  border-bottom: 1px solid #ddd;
  font-size: 0.9em;
  padding: 10px;
}

#gallery-dialog .comments-container .comment .user {
  font-weight: bold;
  display: block;
}

#gallery-dialog .comments-container .comment small {
  color: #888;
  font-weight: normal;
}

#gallery-dialog .comments-container .comment:last-child {
  border-bottom: none;
}
</style>
<template>
<div>
  <div v-loading="loading" id="gallery-container" v-masonry transition-duration="0.3s" item-selector=".item">
      <div v-masonry-tile v-for="item in items" :class="'item item-' + rows">
          <el-card :body-style="{ padding: '0px' }">
              <div slot="header" class="clearfix">
                  <el-row>
                      <el-col :span="20">
                        <a :href="'/#/u/' + item.userSeoUrl">
                          <img v-if="item.userPic != null" class="avatar" :src="imageApiPath(item.userPic)" />
                          <span v-if="item.userPic == null" class="avatar default"></span>
                        </a>
                          @{{item.userName.toLowerCase()}}
                      </el-col>
                      <el-col :span="4" class="text-right">
                          <i v-if="!item.iLikedIt" class="far fa-heart"></i>
                          <i title="Te ha gustado esta fotografía" v-if="item.iLikedIt" class="fas fa-heart"></i>
                      </el-col>
                  </el-row>
              </div>
              <div class="img-container" @click="openDialog(item)">
                  <div class="expand">
                      <i class="fa fa-expand"></i>
                  </div>
                  <img :src="imageApiPath(item.image)" />
              </div>
              <div class="content">
                  {{item.description}}
                  <el-row class="info">
                    <el-col :span="15" class="time">
                      <i class="fa fa-calendar"></i> {{ item.createdAt | moment("calendar") }}
                    </el-col>
                    <el-col :span="9" class="report">
                      <span><i class="far fa-comment"></i> {{item.comments}}</span>
                      <span><i class="far fa-heart"></i> {{item.likes}}</span>
                    </el-col>
                  </el-row>
              </div>
          </el-card>
      </div>
  </div>
  <item-dialog :item="selectedItem" v-if="selectedItem != null"></item-dialog>
  <uploader v-if="canUploadImage"></uploader>
</div>
</template>

<script>
import itemDialog from  './galleryItemDialog.vue'
import uploader from "@/components/shared/AddToGallery"

export default {
  name: "Default",
  components: {
    itemDialog, uploader
  },
  props: {
    rows: {
      type: String,
      default: "5"
    },
    canUploadImage: {
      type: Boolean,
      default: false
    },
    byUser: {
      type: String,
      default: null
    }
  },
  data: () => ({
    items: [],
    loading: true,
    selectedItem: null
  }),
  methods: {
    getAll() {
      let self = this,
          params = {userId: window.User.UserId};

      if(self.byUser != null) {
        params.seoUrl = self.byUser
      }

      self.$store.state.services.photoService
        .getAll(20, params)
        .then(r => {
          self.loading = false;
          self.items = r.data
        })
        .catch(r => {
          console.log(r);
          self.loading = false;
        });
    },
    imageApiPath(image) {
      let self = this
      return window.Api.url + 'uploads/' + image
    },
    openDialog(item) {
      let self = this
      self.selectedItem = item
    }
  },
  created() {
    let self = this
    self.getAll()
  }
};
</script>

<style>
#gallery-container {
  min-height: 150px;
}

#gallery-container .item-6 {
  width: calc(100% / 6);
}

#gallery-container .item-5 {
  width: calc(100% / 5);
}

#gallery-container .item-4 {
  width: calc(100% / 4);
}

#gallery-container .item-3 {
  width: calc(100% / 3);
}

#gallery-container .item-2 {
  width: calc(100% / 2);
}

#gallery-container .item-1 {
  width: 100%;
}

#gallery-container .el-card {
  margin: 20px;
}

#gallery-container .item .img-container {
  position: relative;
  overflow: hidden;
}

#gallery-container .item .el-card__header {
  height: 60px;
  line-height: 60px;
  padding: 0;
  padding: 0 14px;
}

#gallery-container .item .img-container .expand {
  position: absolute;
  top: -5px;
  left: 0;
  height: 100%;
  width: 100%;
  background: #222;
  color: #fff;
  z-index: 1;
  opacity: 0.5;
  font-size: 6em;
  justify-content: center;
  align-items: center;
  display: none;
  cursor: pointer;
}

#gallery-container .item .img-container:hover .expand {
  display: flex;
}

#gallery-container .item .img-container img {
  width: 100%;
  margin: 0;
  padding: 0;
  min-height: 100%;
}

#gallery-container .item .avatar {
  float: left;
  border-radius: 100%;
  margin-right: 10px;
  height: 36px;
  margin-top: 10px;
  border: 2px solid #409eff;
  padding: 2px;
}

#gallery-container .item .avatar.default {
  height: 36px;
  width: 36px;
  background: #eee;
}

#gallery-container .item .content {
  padding: 15px;
  font-size: .9em;
}

#gallery-container .item .info {
  color: #888;
  display: block;
  font-size: 0.8em;
  margin-top: 10px;
  text-transform: uppercase;
}

#gallery-container .item .report {
  text-align: right;
}

#gallery-container .item .report span:first-child {
  margin-right: 3px;
}

#gallery-container .item .time .fa {
  margin-right: 4px;
}
</style>
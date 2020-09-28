<template>
<el-container>
  <el-header id="header">
    <topheader></topheader>
  </el-header>
  <el-container>
    <el-main>
      <el-row>
        <el-col :span="24">
          <router-view :key="$route.fullPath"></router-view>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
  <el-footer id="footer">
    © <a target="_blank" href="http://anexsoft.com">{{ 'Anexsoft ' + new Date().getFullYear() }}</a> - Curso de NETCore API RESTFull + VueJs.
  </el-footer>
</el-container>
</template>

<script>
import topheader from "@/components/shared/TopHeader"

export default {
  components: {
    topheader
  },
  props: {
    source: String
  }
};
</script>
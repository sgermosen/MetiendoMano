<template>
<div>
  <gallery rows="5" :can-upload-image="true"></gallery>
</div>
</template>

<script>
import gallery from "@/components/shared/Gallery";

export default {
  components: {
    gallery
  },
  name: "Default"
};
</script>
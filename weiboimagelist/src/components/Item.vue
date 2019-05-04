<template>
    <div>
        <img :src="url" @click="viewBigImage" class="smallImg"/>
    </div>
</template>

<script>
import Vue from "vue"
export default {
    name: "Item",
    data() {
        return {  innerHeight: 0}
    },
    props: ["url", "height", "width"],
    methods:{},
    created(){
    },
    mounted() {
        console.log("item created " + this.url);
    },
    methods: {
        viewBigImage(){
            this.$emit("viewBigImage", this.url)
        }
    }
}
</script>

<style scoped>
img{
  object-fit: cover;
  --wekit-object-fit: cover;
}

.smallImg{
  width: 100%;
}

div{
    width: 100%;
}
</style>



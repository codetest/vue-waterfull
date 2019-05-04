<template>
    <div>
        <img :src="bigImgUrl" @click="closeImage" @touchstart.stop="touchstart" @touchmove.stop="touchmove" @touchend.stop="touchend" v-bind:style="imgStyleObj"/>
        <label class="cntLabel">{{inx}}/{{cnt}}</label>
    </div>
</template>

<script>
import Vue from "vue"
export default {
    name: "BigItem",
    data() {
        return {startX: 0}
    },
    props: ["bigImgUrl", "originUrl", "imgStyleObj", "inx", "cnt", "textStyleObj"],
    methods:{},
    created(){
    },
    mounted() {
        console.log("item created " + this.bigImgUrl);
    },
    methods: {
        closeImage() {
            this.$emit("closeImage", "")
        },
        touchmove(event){
        },
        touchstart(event) {
            console.log("touchstart")
            this.startX = event.changedTouches[0].clientX
        },
        touchend(event){
            console.log("touchend")
            if (this.startX - event.changedTouches[0].clientX > window.innerWidth / 5){
                this.$emit("nextImage", this.originUrl)
            }
            else if (this.startX - event.changedTouches[0].clientX < -window.innerWidth / 5){
                this.$emit("previousImage", this.originUrl)
            }
        }
    }
}
</script>

<style scoped>
div{
    text-align: center;
    height: 100%;
    width: 100%;
}

img{    
  object-fit: cover;
  --wekit-object-fit: cover;
}
.cntLabel{
    position: absolute;
    font-size: 0.4rem;
    color: yellowgreen;
    top: 0px;
}
</style>



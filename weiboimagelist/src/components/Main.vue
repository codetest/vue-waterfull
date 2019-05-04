<template>
  <div class="water">
    <div class="piping" id="wf1">
      <ItemComponent v-for="moment in momentsAll1" v-bind:key="moment[0]" :url="moment[0]" :height="moment[1]" :width="moment[2]" v-on:viewBigImage="viewBigImage"/>
    </div>    
    <div class="piping" id="wf2">
      <ItemComponent v-for="moment in momentsAll2" v-bind:key="moment[0]" :url="moment[0]" :height="moment[1]" :width="moment[2]" v-on:viewBigImage="viewBigImage"/>
    </div>  
    <div v-if="showBigImage" class="bigImageDiv" v-bind:style="styleObj">
      <BigItemComponent :inx="bigImageInx" :textStyleObj="textStyleObj" :cnt="bigImageCnt" :imgStyleObj="imgStyleObj" :bigImgUrl="bigImgUrl" :originUrl="originUrl" v-on:closeImage="closeImageParent" v-on:previousImage="previousImage" v-on:nextImage="nextImage"/>
    </div>
  </div>
</template>

<script>
import Vue from "vue"
export default {
  name: 'HelloWorld',
  data () {
    return {
      moments: [],
      available: true,
      imgHeight1: 0,
      imgHeight2: 0,
      momentsAll1: [],
      momentsAll2: [],
      momentsAll: [],
      j: 0,
      bigImgUrl: null, 
      originUrl: null,
      styleObj: {}, 
      textStyleObj: {},
      showBigImage: false,
      forcedBigImage: false,
      loadingBigImage: false,
      bigImageInx: 0,
      bigImageCnt: 0,
    }
  },
  created(){
    this.fetchMoments();
  },
  mounted() {
    window.addEventListener("scroll", this.handleScroll)
  },
  methods: {
    previousImage(imgUrl){
      console.log("previousImage")
      if (this.loadingBigImage){
        return;
      }

      let inx = this.momentsAll.indexOf(imgUrl)
      if (inx > 0){
        this.forcedBigImage = true
        this.bigImageInx = inx
        this.viewBigImage(this.momentsAll[inx - 1])
      }
      else{
        this.forcedBigImage = false
      }
    },
    nextImage(imgUrl){
      console.log("nextImage")
      if (this.loadingBigImage){
        return;
      }

      let inx = this.momentsAll.indexOf(imgUrl)
      if (inx < this.momentsAll.length - 1){
        this.forcedBigImage = true
        this.bigImageInx = inx + 2
        this.viewBigImage(this.momentsAll[inx + 1])
      }
      else{
        this.forcedBigImage = false
      }
    },
    closeImageParent() {
      Vue.prototype.BigImageInView = false
      this.showBigImage = false
      this.bigImagUrl = null
      this.forcedBigImage = false
    },
    viewBigImage(url) {
        if (Vue.prototype.BigImageInView && !this.forcedBigImage){
            return;
        }


        this.bigImageInx = this.momentsAll.indexOf(url) + 1
        this.bigImageCnt = this.momentsAll.length
        Vue.prototype.BigImageInView = true
        this.loadingBigImage = true
        let tempImageUrl = url
        let img = new Image()
        img.src = tempImageUrl
        let that = this
        img.onload = () => {
            let width = window.innerWidth
            let height = window.innerHeight
            if (img.width * height > width * img.height){
                that.styleObj = {width: "100%", top: ((height - img.height * width / img.width) / 2) + 'px', left:0}
                that.imgStyleObj = {width: "100%"}
                that.textStyleObj = {bottom: (height - (height - img.height * width / img.width) / 2) + 'px'}
            }
            else{
                that.styleObj = {height: "100%", top: 0, left:  (width - img.width * height / img.height) / 2 + 'px'}
                that.imgStyleObj = {height: "100%"}
                that.textStyleObj = {bottom: (height) + 'px'}
            }

            that.bigImgUrl = tempImageUrl
            that.originUrl = url
            that.showBigImage = true
            this.loadingBigImage = false
        }

        img.onerror = () => {
            Vue.prototype.BigImageInView = false
            that.showBigImage = false
            this.loadingBigImage = false
        }
    },
    handleScroll() {
      var body = document.getElementsByTagName("body")[0]
      if(window.pageYOffset + window.innerHeight + (window.innerHeight / 30) > body.scrollHeight && this.available && this.j == 0){
          this.fetchMoments();
      }
    },
    sort(){
      if (this.j < this.moments.length) {
        let that = this;
        let newImg = new Image();
        newImg.src = "/image/load?img=" + that.moments[that.j];
        newImg.onload = () => {
          let min = that.imgHeight1 < that.imgHeight2 ? 0: 1;
          if (min == 0){
            that.momentsAll1.push([newImg.src, newImg.height, newImg.width])
            that.imgHeight1 += newImg.height
          }
          else{
            that.momentsAll2.push([newImg.src, newImg.height, newImg.width])
            that.imgHeight2 += newImg.height
          }

          that.momentsAll.push(newImg.src)
          that.sort()
        };

        newImg.onerror = (err) => {
          console.log(err)
          that.sort()
        }
 
        this.j++;
      }
      else{
        this.j = 0;
      }
    },
    fetchMoments(){
      this.available = false;
      fetch("/api/fetch/moments?size=10", {method: "POST"})
      .then(res => res.json())
      .then(res => {
        this.moments = res
        this.available = true;
        this.sort(0)
      })
      .catch(err => {
        this.available = true
      })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.water{
  display: -webkit-flex; /* Safari */
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  width: 100%;
  height: 100%;
  overflow-y: hidden;
}
.piping{
  display: flex;
  flex-direction: column;
  width: 3.75rem;
}

.bigImageDiv{
    position: fixed;
    top: 0;
    left: 0;
}
</style>

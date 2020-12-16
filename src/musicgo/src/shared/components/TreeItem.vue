<template>
<div>
    <li class="li-tree">
        <div
            :class="{bold: isGroup()}"
            @click="toggle()">
            {{ item.Name }}
            <span v-if="isGroup()">[{{ isOpen ? '-' : '+' }}]</span>
        </div>   
    </li>
    <ul v-show="isOpen" v-if="isGroup()">
      <tree-item
        class="item"
        v-for="(child, index) in item.Permissions"
        :key="index"
        :item="child"
       
      ></tree-item>
      <!-- <li class="add" @click="$emit('add-item', item)">+</li> -->
    </ul>
</div>    
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from "vue-property-decorator";
@Component({})
export default class TreeItem extends Vue {

    private isOpen : boolean = false;

    @Prop() item : any;

    computed(){
        this.isGroup();
    }


    isGroup(){
        return this.item.Permissions  ;
    }


    toggle(){
        if(this.isGroup()){
            this.isOpen = !this.isOpen;
        }
    }





}
</script>

<style scoped>
.bold {
  font-weight: bold;
}

.tree-ul {
  padding-left: 1em;
  line-height: 1.5em;
  list-style-type: dot;
}

.item {
  cursor: pointer;
  color:white;
}

.li-tree{
    list-style: none;
    margin:15px 0;
}
</style>
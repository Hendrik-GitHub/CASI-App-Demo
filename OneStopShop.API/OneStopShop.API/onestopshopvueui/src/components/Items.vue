<template>
    <div>
        <v-card style="height: 100%; overflow: hidden;">
            <v-toolbar color="blue" dark>
                <v-toolbar-title>
                    Items
                </v-toolbar-title>
                <v-spacer></v-spacer>
            </v-toolbar>          

            <v-list two-line style="height: calc(100% - 128px); overflow-y: scroll;">
            <v-list-item color="blue" @click.prevent="openCreateNewItemModal()">
                <v-list-item-content>
                    Create a new shopping list item
                </v-list-item-content>

                <v-list-item-action>
                    <v-list-item-title>
                        <v-icon>add</v-icon>
                    </v-list-item-title>
                </v-list-item-action>               
            </v-list-item>
            
            <template
                v-for="(Item, key) in ITEMS">
                <Item v-bind:key = "key" :Item="Item" :index="key"/>
            </template>     

            </v-list>

            <v-divider></v-divider>

            <v-list-item v-if="openNewItemFormValue">
                <NewItem />
            </v-list-item>
            <v-list-item v-if="openEditItemFormValue">
                <EditItem />
            </v-list-item>
        </v-card> 
        <router-view :key="$route.fullPath" name="NewItem"></router-view> 
        <router-view :key="$route.fullPath" name="EditItem"></router-view>     
    </div>
      
</template>

<script>

import Item from "./Item"
import NewItem from "./NewItem"
import EditItem from "./EditItem"

export default {
    name: "Items",
    components: {
        Item,
        NewItem,
        EditItem
    },
    data: () => ({
        
    }),
    computed: {
        ITEMS () {
            return this.$store.getters.ITEMS(this.$route.params.shoppinglistid);
        },
        openNewItemFormValue: {
            get () {
                return this.$store.getters.NEW_ITEMS_FORM;
            },
            set (value) {
                this.$store.commit("SET_NEW_ITEMS_FORM", value)
            }
        },
        openEditItemFormValue: {
            get () {
                return this.$store.getters.EDIT_ITEM_FORM;
            },
            set (value) {
                this.$store.commit("SET_EDIT_ITEM_FORM", value)
            }
        }
    },
    methods: {
        openCreateNewItemModal () {
            this.$router.push({
                name: "NewItem"
            })
        }
    },
    mounted () {
        this.$store.dispatch("GET_ITEMS", this.$route.params.shoppinglistid);
    }
}
</script>
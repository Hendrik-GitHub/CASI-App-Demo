<template>
    <div v-bind:style="{maxHeight: '99%', height: '100%', backgroundSize: 'cover'}">
        <v-container fluid fill-height pl-2 pr-0 pb-0 pt-0> 
            <v-layout>
                <v-flex lg3 pr-2>
                    <ShoppingLists />
                </v-flex>

                <v-flex lg3 pr-2 pl-2>
                  <router-view name="Items" :key="$route.fullPath"></router-view>
                </v-flex>
            </v-layout>
        </v-container>
        <v-footer height="auto" color="indigo">
            <v-layout justify-center>
                <v-flex indigo white--text text-xs-center>
                    &copy;2021
                    <strong>One Stop Shop</strong>
                </v-flex>
            </v-layout>
        </v-footer>
        <Notification/>
    </div> 
</template>

<script>

import ShoppingLists from "./ShoppingLists"
//import Items from "./Items"
import Notification from './Notification'

export default {
    name: "OneStopShop",
    components: {
        ShoppingLists,
        Notification
        //Items
    }
}
</script>
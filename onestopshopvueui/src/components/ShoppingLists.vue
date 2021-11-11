<template>
    <v-navigation-drawer permanent style="width: 100%;">
        <v-toolbar dark color="blue">
            <v-spacer />
            <v-toolbar-title>Welcome {{ USER }}!</v-toolbar-title>
            <v-spacer />
        </v-toolbar> 
        <v-toolbar color="blue" dark>
            <v-toolbar-title>My Shopping Lists</v-toolbar-title>
            <v-spacer />
            <v-btn icon>
                <v-btn rounded color="primary" dark @click.prevent="logout()">Logout<v-icon>logout</v-icon></v-btn>
            </v-btn>
            <v-spacer />
        </v-toolbar>
        <v-list>
            <v-list-item color="blue" @click.prevent="openCreateNewListModal()">
                <v-list-item-content>
                    Create a new shopping list
                </v-list-item-content>

                <v-list-item-action>
                    <v-list-item-title>
                        <v-icon>add</v-icon>
                    </v-list-item-title>
                </v-list-item-action>
            </v-list-item>           

            <v-list-item v-if="openNewListFormValue">
                <NewShoppingList />
            </v-list-item>
                       
        </v-list>
        <v-divider></v-divider>
        <v-list style="height: calc(100% - 128px);">
          
            <v-list-item 
                :to="{ name: 'Items', params: { shoppinglistid: list.shoppinglistid} }"
                v-for="(list, key) in LISTS" 
                v-bind:key="key">
           
                <v-list-item-action>                  
                    <v-btn icon @click.prevent="openEditListModal(list.shoppinglistid)">
                        <v-icon color="pink">edit</v-icon>  
                    </v-btn>                  
                </v-list-item-action> 

                <v-list-item-content>
                    <v-list-item-title>{{ list.name }}</v-list-item-title>
                    <v-list-item-subtitle>{{ list.description}}</v-list-item-subtitle>
                </v-list-item-content>

                <v-list-item-action>
                    <v-btn icon @click.prevent="deleteList(list.shoppinglistid)">
                        <v-icon color="red">delete</v-icon>
                    </v-btn>
                </v-list-item-action>
           
            </v-list-item>
        </v-list>

        <v-list-item v-if="openEditListFormValue">
            <EditShoppingList />
        </v-list-item>

        <router-view :key="$route.fullPath" name="NewShoppingList"></router-view> 
        <router-view :key="$route.fullPath" name="EditShoppingList"></router-view> 
    </v-navigation-drawer>
</template>

<script>
//import SearchBar from "./SearchBar";
import { mapGetters } from 'vuex';
import NewShoppingList from "./NewShoppingList";
import EditShoppingList from "./EditShoppingList";

export default {
    name: "Shoppinglists",
    components: {
        //SearchBar,
        NewShoppingList,
        EditShoppingList
    },
    data: () => ({}),
    computed: {
        USER () {
            return this.$store.getters.USER;
        }, 
        ...mapGetters(['DISPLAY_SEARCH_LIST', 'LISTS']), //The "..." is called the spread operator 
        openNewListFormValue: {
            get () {
                return this.$store.getters.NEW_LIST_FORM;
            },
            set (value) {
                this.$store.commit("SET_NEW_LIST_FORM", value)
            }
        },
        openEditListFormValue: {
            get () {
                return this.$store.getters.EDIT_LIST_FORM;
            },
            set (value) {
                this.$store.commit("SET_EDIT_LIST_FORM", value)
            }
        }       
    },
    methods: {
        openNewListForm () {
            this.$store.commit("SET_NEW_LIST_FORM", true)
        },
        openEditListForm () {
            this.$store.commit("SET_EDIT_LIST_FORM", true)
        },
        toggleSearchList() {
            this.$store.commit('SET_DISPLAY_SEARCH_LIST', !this.DISPLAY_SEARCH_LIST);
        },
        openCreateNewListModal () {
            this.$router.push({
                name: "NewShoppingList"
            });
        },
        openEditListModal (listId) {
            this.$router.push({
                name: "EditShoppingList",
                params: { shoppinglistid: listId }
            });
        },
        deleteList(listId) {
            this.$store.dispatch("DELETE_LIST", {
                    shoppinglistid: listId
                })
                .then((status) => {
                if (status === 204 || status === 200) {
                    this.$store.commit("SET_NOTIFICATION", {
                    display: true,
                    text: "Shopping list has been removed!",
                    alertClass: "success"
                    });
                    this.open = false;

                    this.$router.push({
                        name: "OneStopShop",
                        params: {
                            shoppinglistid: this.$route.params.shoppinglistid
                        }
                    });
                }
                })
                .catch(error => {
                this.$store.commit("SET_NOTIFICATION", {
                    display: true,
                    text: "Something bad happened!",
                    alertClass: "error"
                });
            });
        },
        logout() {
            localStorage.removeItem('Token');
            this.$router.push({
                name: "login"
            });
        }
    },   
    mounted () {
        this.$store.dispatch("GET_LISTS");
    }
}
</script>
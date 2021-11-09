<template>
    <v-layout row justify-center>
        <v-dialog v-model="open" max-width="50%">
            <v-card>
                <v-card-title>
                    <span class="headline">Edit Shopping List</span>
                </v-card-title>          
                <v-card-text>
                    <v-container>
                        <v-form>
                            <v-text-field
                                v-model="shoppinglistname"
                                :rules="[rules.required]"
                                name="shoppinglistname"
                                label="Name"
                                type="text"
                            >
                            </v-text-field>
                            <v-textarea
                                v-model="shoppinglistdescription"
                                :rules="[rules.required]"
                                name="shoppinglistdescription"
                                label="Description"
                                type="text"
                            >                               
                            </v-textarea>                        
                        </v-form>
                    </v-container>                   
                </v-card-text>
                <v-divider light></v-divider> 
                <v-card-actions>
                    <v-btn @click.prevent="saveShoppingList()" rounded color="success" dark>Save</v-btn>                   
                    <v-spacer></v-spacer>
                    <v-btn elevation="1" rounded color="primary" dark @click.prevent="open = false">Close</v-btn>
                </v-card-actions> 
            </v-card>
        </v-dialog>
    </v-layout>
</template>

<script>
export default {
    name: "EditShoppingList",
    data: () => ({
        shoppinglistname: "",
        shoppinglistdescription: "",
        rules: {
            required: value => !!value || "Required"          
        },
        open: true
    }),    
    methods: {
        saveShoppingList() {
            this.$store.dispatch("POST_UPDATE_LIST", {
                id: this.$route.params.id,
                shoppinglistname: this.shoppinglistname,
                shoppinglistdescription: this.shoppinglistdescription
            })
            .then(response => {
            this.$store.commit("SET_NOTIFICATION", {
                display: true,
                text: "Shopping List has been updated!",
                alertClass: "success"
            });

            this.shoppinglistname = "";
            this.shoppinglistdescription = "";
            this.open = false;

            this.$store.dispatch("GET_LISTS", false);
            });
        } 
    },
    computed: {
        DETAILS() {
            return this.$store.getters.LIST_DETAILS(this.$route.params.id);
        }     
    },
    watch: {
         open: function(value) {
            if (value === false) {
                    this.$router.push({
                    name: "Todo",
                    params: {
                        id: this.$route.params.id
                    }
                });
            }
        } 
    },
    mounted () {
        this.shoppinglistname = this.DETAILS.name;
        this.shoppinglistdescription = this.DETAILS.description;
    }
}
</script>
<template>
    <v-layout row justify-center>
        <v-dialog v-model="open" max-width="50%">
            <v-card>
                <v-card-title>
                    <span class="headline">New Shopping List</span>
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
                    <v-btn @click.prevent="newShoppingList()" rounded color="success" dark>Save</v-btn>                   
                    <v-spacer></v-spacer>
                    <v-btn elevation="1" rounded color="primary" dark @click.prevent="open = false">Close</v-btn>
                </v-card-actions> 
            </v-card>
        </v-dialog>
    </v-layout>
</template>

<script>
export default {
    name: "NewShoppingList",
    data: () => ({
        shoppinglistname: "",
        shoppinglistdescription: "",
        rules: {
            required: value => !!value || "Required"          
        },
        open: true
    }),
    methods: {
        newShoppingList() {
            if (this.shoppinglistname != "" && this.shoppinglistdescription != "") {
                this.$store.dispatch("POST_LIST", {
                shoppinglistname: this.shoppinglistname,
                shoppinglistdescription: this.shoppinglistdescription
                })
                .then(response => {
                this.$store.commit("SET_NOTIFICATION", {
                    display: true,
                    text: "Shopping List has been created!",
                    alertClass: "success"
                });

                this.shoppinglistname = "";
                this.shoppinglistdescription = "";
                this.open = false;

                this.$store.dispatch("GET_LISTS", false);
                });
            }
            else {
                this.$store.commit("SET_NOTIFICATION", {
                    display: true,
                    text: "There are missing fields!",
                    alertClass: "warning"
                });
            }       
        }
    },
    watch: {
        open: function(value) {
            if (value === false) {
                    this.$router.push({
                    name: "OneStopShop",
                    params: {
                        id: this.$route.params.id
                    }
                });
            }
        }
    }
}
</script>
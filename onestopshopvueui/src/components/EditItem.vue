<template>
    <v-layout row justify-center>
        <v-dialog v-model="open" max-width="50%">
            <v-card>
                <v-card-title>
                    <span class="headline">Edit Shopping List Item</span>
                </v-card-title>          
                <v-card-text>
                    <v-container>
                        <v-form>
                            <v-text-field
                                v-model="shoppinglistitemdescription"
                                :rules="[rules.required]"
                                name="shoppinglistitemdescription"
                                label="Item Description"
                                type="text"
                            >
                            </v-text-field>
                            <v-text-field
                                v-model="quantitydescription"
                                :rules="[rules.required]"
                                name="quantitydescription"
                                label="Quantity Description"
                                type="text"
                            >                               
                            </v-text-field>                      
                        </v-form>
                    </v-container>                   
                </v-card-text>
                <v-divider light></v-divider> 
                <v-card-actions>
                    <v-btn @click.prevent="updateShoppingListItem()" rounded color="success" dark>Save</v-btn>                   
                    <v-spacer></v-spacer>
                    <v-btn elevation="1" rounded color="primary" dark @click="open = false">Close</v-btn>
                </v-card-actions> 
            </v-card>
        </v-dialog>
    </v-layout>
</template>

<script>
export default {
    name: "EditItem",
    data: () => ({
        shoppinglistitemdescription: "",
        quantitydescription: "",
        itemchecked: false,
        shoppinglistid: 0,
        rules: {
            required: value => !!value || "Required"          
        },
        open: true
    }),    
    methods: {
        updateShoppingListItem() {
            if (this.shoppinglistitemdescription != "" && this.quantitydescription != "") {
                    this.$store.dispatch("POST_UPDATE_ITEM", {
                    id: this.$route.params.ItemId,
                    shoppinglistitemdescription: this.shoppinglistitemdescription,
                    quantitydescription: this.quantitydescription,
                    itemchecked: this.itemchecked,
                    shoppinglistid: this.$route.params.id
                })
                .then(response => {
                this.$store.commit("SET_NOTIFICATION", {
                    display: true,
                    text: "Item has been updated!",
                    alertClass: "success"
                });

                this.shoppinglistitemdescription = "";
                this.quantitydescription = "";
                this.itemchecked = false;
                this.open = false;
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
    computed: {
        ITEM_DETAILS() {
            return this.$store.getters.ITEM_DETAILS(this.$route.params.ItemId);
        }     
    },
    watch: {
          open: function(value) {
            if (value === false) {
                    this.$router.push({
                    name: "Items",
                    params: {
                        id: this.$route.params.id
                    }
                });
            }
        }  
    },
    mounted () {
        this.shoppinglistitemdescription = this.ITEM_DETAILS.ShoppingListItemDescription;
        this.quantitydescription = this.ITEM_DETAILS.QuantityDescription;
    }
}
</script>
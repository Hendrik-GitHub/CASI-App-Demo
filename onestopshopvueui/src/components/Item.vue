<template>
    <v-list-item ripple @click.prevent="toggle(index)">

        <v-list-item-action>           
            <v-btn icon @click="openEditItemModal()">
                <v-icon color="pink">edit</v-icon>
            </v-btn>
        </v-list-item-action> 

        <v-list-item-content>
            <v-list-item-title>{{ Item.ShoppingListItemDescription }}</v-list-item-title>
            <v-list-item-subtitle>{{ Item.QuantityDescription }}</v-list-item-subtitle>
        </v-list-item-content>

        <v-list-item-action-text>
            <v-checkbox v-model="Item.ItemChecked" @click.prevent="toggleItemChecked(index)"></v-checkbox>
        </v-list-item-action-text>

        <v-list-item-action>
            <v-btn icon @click.prevent="deleteItem(Item.id)">
                <v-icon color="red">delete</v-icon>
            </v-btn>
        </v-list-item-action>

    </v-list-item>
</template>

<script>
export default {
    name: "Item",
    props: {
        Item: Object,
        index: Number
    },
    data: () => ({

    }),
    methods: {       
        toggleItemChecked(index) {
            this.$store.dispatch("TOGGLE_ITEMCHECKED", {
                id: this.Item.id,
                ItemChecked: this.Item.ItemChecked
            });
        },
        toggle () {
            
        },
        openEditItemModal () {
            this.$router.push({
                name: "EditItem",
                params: { ItemId: this.Item.id }
            })
        },
        deleteItem(ItemId) {
            this.$store.dispatch("DELETE_ITEM", {
                    ItemId: ItemId
                })
                .then((status) => {
                if (status === 204 || status === 200) {
                    this.$store.commit("SET_NOTIFICATION", {
                    display: true,
                    text: "Item has been removed",
                    alertClass: "success"
                    });
                    this.open = false;
                }
                })
                .catch(error => {
                this.$store.commit("SET_NOTIFICATION", {
                    display: true,
                    text: "Something bad happened!",
                    alertClass: "error"
                });
            });
        }
    }

}
</script>
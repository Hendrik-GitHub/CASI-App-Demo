<template>
    <v-layout row justify-center>
        <v-dialog v-model="open" max-width="50%">
            <v-card>
                <v-card-title>
                    <span class="headline">Shopping List Item Title</span>
                </v-card-title>          
                <v-card-text>
                    <v-container>
                        <v-form>
                            <v-textarea 
                                outlined 
                                v-model="note" 
                                label="Note" 
                                placeholder="Enter your note here...">
                            </v-textarea>
                            <v-btn color="success">
                                Save
                            </v-btn>
                        </v-form>
                    </v-container>
                    <v-list three-line subheader>
                        <v-subheader>Notes</v-subheader>

                        <v-list-item>
                            
                        </v-list-item>
                    </v-list>
                </v-card-text>
                <v-card-actions>
                    <v-btn elevation="1" plain color="primary">Close</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-layout>
</template>

<script>
export default {
    name: "NotesModal",
    data: () => ({
        note: "",
        open: true
    })
}
</script>
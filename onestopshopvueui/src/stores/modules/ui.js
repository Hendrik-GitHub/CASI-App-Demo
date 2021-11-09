export default {
    state: {
      drawer: false,
      notification: {
        display: false,
        text: "Notification placeholder text",
        timeout: 3000,
        class: "success"
      },
      displaySearchList: false,
      newListForm: false,
      newItemsForm: false,
      editListForm: false,
      editItemForm: false
    },
    getters: {
      NOTIFICATION: state => {
        return state.notification;
      },
      DISPLAY_SEARCH_LIST: state => {
        return state.displaySearchList;
      },
      NEW_LIST_FORM: state => {
        return state.newListForm;
      },
      NEW_ITEMS_FORM: state => {
        return state.newItemsForm;
      } ,
      EDIT_LIST_FORM: state => {
        return state.editListForm;
      },
      EDIT_ITEM_FORM: state => {
        return state.editItemForm;
      }
    },
    mutations: {
      SET_NOTIFICATION: (state, { display, text, alertClass }) => {
        state.notification.display = display;
        state.notification.text = text;
        state.notification.class = alertClass;
      },
      SET_DISPLAY_SEARCH_LIST: (state, payload) => {
        state.displaySearchList = payload;
      },
      SET_NEW_LIST_FORM: (state, payload) => {
        state.newListForm = payload;
      },
      SET_NEW_ITEMS_FORM: (state, payload) => {
        state.newListForm = payload;
      },
      SET_EDIT_LIST_FORM: (state, payload) => {
        state.editListForm = payload;
      },
      SET_EDIT_ITEM_FORM: (state, payload) => {
        state.editItemForm = payload;
      }
    },
    actions: {}
  };
  
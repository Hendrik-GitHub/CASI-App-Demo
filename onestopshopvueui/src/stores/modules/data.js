import Vue from "vue";
import axios from "axios";

export default {
  state: {
    lists: [],
    items: []
  },
  getters: {
    LISTS: state => {      
        return state.lists;
    }, 
    ITEMS: state => index => {
      if (index) {
        return state.items;
      }
    },
    LIST_DETAILS: state => (listId) => {
      return state.lists.find(list => list.shoppinglistid === listId);
    },
    ITEM_DETAILS: state => (itemid) => {
      return state.items.find(Item => Item.itemid === itemid);
    }
  },
  mutations: {
    SET_LISTS: (state, payload) => {
      state.lists = payload;
    },
    ADD_LIST: (state, payload) => {
      state.lists.unshift(payload);
    },
    UPDATE_LIST: (state, { data }) => {
      state.lists.find(list => list.shoppinglistid === data.shoppinglistid).shoppinglistname = data.shoppinglistname;
      state.lists.find(list => list.shoppinglistid === data.shoppinglistid).shoppinglistdescription = data.shoppinglistdescription;
    },
    SET_ITEMS: (state, payload) => {
      state.items = payload;
    },
    ADD_ITEM: (state, payload) => {
      state.items.unshift(payload);
    },
    UPDATE_ITEM: (state, { data }) => {
      state.items.find(Item => Item.itemid === data).ShoppingListItemDescription = data.shoppinglistitemdescription;
      state.items.find(Item => Item.itemid === data).QuantityDescription = data.quantitydescription;
    },
    SET_ITEM_STATUS: (state, { data }) => {
      state.items.find(Item => Item.itemid === data.itemid).ItemChecked = data.ItemChecked;
    },
    REMOVE_ITEM: (state, { payload }) => {
      let rs = state.items.filter(currentItem => {
        return currentItem.itemid !== payload.itemid;
      });

      state.items = [...rs];
    },
    REMOVE_LIST: (state, { payload }) => {
      let rs = state.lists.filter(currentList => {
        return currentList.shoppinglistid !== payload.shoppinglistid;
      });

      state.lists = [...rs];
    }
  },
  actions: {
    GET_LISTS: ({ commit }, payload) => {    
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.get(`ShoppingListController/GetShoppingLists/`, { headers })
          .then(({ data, status }) => {
            if (status === 200) {
              commit("SET_LISTS", data);
              resolve(true);
            }
          })
          .catch(error => {
            reject(error);
          });
      });
    },
    POST_LIST: ({ commit }, payload) => {
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.post(`ShoppingListController/CreateShoppingList/`, payload, { headers })
          .then(({ data, status }) => {
            commit("ADD_LIST", payload);
            if (status === 200 || status === 201 && (data.Success === true)) {
              resolve({ data, status });
            }
          })
          .catch(error => {
            reject(error);
          });
      });
    },
    POST_UPDATE_LIST: ({ commit }, payload) => {
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.put(`ShoppingListController/UpdateShoppingList/`, payload, { headers })
          .then(({ data, status }) => {
            if (status === 200 || status === 201 && (data.Success === true)) {
              resolve({ data, status });
            }
          })
          .catch(error => { 
            reject(error);
          });
      });
    },
    POST_ITEM: ({ commit }, payload) => {
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.post(`ShoppingListController/CreateShoppingListItem/`, payload, { headers })
          .then(({ data, status }) => {
            commit("ADD_ITEM", payload);
            if (status === 200 || status === 201 && (data.Success === true)) {
              resolve({ data, status });
            }
          })
          .catch(error => {
            reject(error);
          });
      });
    },
    POST_UPDATE_ITEM: ({ commit }, payload) => {
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.put(`ShoppingListController/UpdateShoppingListItem/`, payload, { headers })
          .then(({ data, status }) => {
            if (status === 200 || status === 201 && (data.Success === true)) {
              resolve({ data, status });
            }
          })
          .catch(error => { 
            reject(error);
          });
      });
    },
    GET_ITEMS: ({ commit }, payload) => {    
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.get(`ShoppingListController/GetShoppingListItems/` + payload, { headers })
          .then(({ data, status }) => {
            if (status === 200) {
              commit("SET_ITEMS", data);
              resolve(true);
            }
          })
          .catch(error => {
            reject(error);
          });
      });
    },
    TOGGLE_ITEMCHECKED: ({ commit }, payload) => {
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.post(`ShoppingListController/ToggleShoppingListItemChecked/`, payload, { headers })
          .then(({ data, status }) => {
            if (status === 200 || status === 201 && (data.Success === true)) {
              resolve({ data, status });
            }
          })
          .catch(error => {
            reject(error);
          });
      });
    },
    DELETE_ITEM: ({ commit }, payload ) => {
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.delete(`ShoppingListController/DeleteShoppingListItem/`+ payload.itemid, { headers })
          .then(({ data, status }) => {
            commit("REMOVE_ITEM", {
              payload
            });
            resolve(status);
          })
          .catch(error => {
            reject(error);
          });
      });
    },
    DELETE_LIST: ({ commit }, payload ) => {
      const headers = {
        'Authorization' : "Bearer " + JSON.parse(localStorage.getItem('Token'))
      };

      return new Promise((resolve, reject) => {
        axios.delete(`ShoppingListController/DeleteShoppingList/`+ payload.shoppinglistid, { headers })
          .then(({ data, status }) => {
            commit("REMOVE_LIST", {
              payload
            });
            resolve(status);
          })
          .catch(error => {
            reject(error);
          });
      });
    }
  }
};

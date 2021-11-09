import Vue from "vue";
import vuetify from "./plugins/vuetify";
import VueRouter from "vue-router";

import axios from "axios";

import store from "./stores/store";

import App from "./App.vue";
import Login from "./components/auth/Login"; 
import Signup from "./components/auth/Signup"; 
import OneStopShop from "./components/OneStopShop"; 
import Items from "./components/Items"; 
import NewShoppingList from "./components/NewShoppingList";
import NewItem from "./components/NewItem";
import EditShoppingList from "./components/EditShoppingList";
import EditItem from "./components/EditItem";

Vue.config.productionTip = false

Vue.use(VueRouter);

axios.defaults.baseURL = "http://localhost:11201/api/";
axios.defaults.withCredentials = false;

const routes = [
  {
    path: "/",
    component: OneStopShop,
    name: "OneStopShop",
    children: [
      {
        path: "shoppinglist/:id",
        components: { Items: Items },
        name: "Items",
        children: [
          {
            path: "item/:ItemId",
            components: { 
              EditItem: EditItem 
            },
            name: "EditItem"
          },
          {
            path: "newitem",
            components: { 
              NewItem: NewItem 
            },
            name: "NewItem"
          }                        
        ]
      },
      {
        path: "/shoppinglistnew",
        components: { 
          NewShoppingList: NewShoppingList 
        },
        name: "NewShoppingList"
      },
      {
        path: "/editshoppinglist/:id",
        components: { 
          EditShoppingList: EditShoppingList 
        },
        name: "EditShoppingList"
      }                    
    ]
  },
  {
    path: "/login",
    component: Login,
    name: "login"
  },
  {
    path: "/signup",
    component: Signup,
    name: "signup"
  }
];

let isRefreshing = false;
let subscribers = [];

axios.interceptors.response.use(
  response => {
    return response;
  },
  err => {
    const {
      config,
      response: { status, data }
    } = err;

    const originalRequest = config;

    console.log(status, data);

    if (status === 401) {
      router.push({ name: "login" });
    }

    /* if (data.message === "Missing token") {
      router.push({ name: "login" });
      return Promise.reject(false);
    }

    if (originalRequest.url.includes("Login")) {
      return Promise.reject(err);
    }

    if (status === 401 && data.message === "Expired token") {
      if (!isRefreshing) {
        isRefreshing = true;
        store
          .dispatch("REFRESH_TOKEN")
          .then(({ status }) => {
            if (status === 200 || status == 204) {
              isRefreshing = false;
            }
            subscribers = [];
          })
          .catch(error => {
            console.error(error);
          });
      }

      const requestSubscribers = new Promise(resolve => {
        subscribeTokenRefresh(() => {
          resolve(axios(originalRequest));
        });
      });

      onRefreshed();

      return requestSubscribers;
    } */
  }
);

function subscribeTokenRefresh(cb) {
  subscribers.push(cb);
}

function onRefreshed() {
  subscribers.map(cb => cb());
}

subscribers = [];

const router = new VueRouter({
  mode: "history",
  routes,
  base: "/"
}) 

new Vue({
  vuetify,
  router,
  store,
  render: h => h(App),  
}).$mount('#app')   

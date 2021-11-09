import axios from "axios";

export default {
    state: {},
    getters: {},
    mutations: {},
    actions: {
        LOGIN: ({ commit }, payload) => {
          return new Promise((resolve, reject) => {
            axios.post(`AuthController/Login/`, payload)
              .then(({ data, status }) => {
                if (status === 200) {                
                  localStorage.setItem('Token', JSON.stringify(data.Token));
                  resolve(true);
                }
              })
              .catch(error => {
                reject(error);
              });
          });
        },
        REGISTER: ({ commit }, { username, emailaddress, password }) => {
          return new Promise((resolve, reject) => {
            axios
              .post(`UserController/CreateUser/`, {
                username,
                emailaddress,
                password
              })
              .then(({ data, status }) => {
                if (status === 200) {
                  resolve(true);
                }
              })
              .catch(error => {       
                reject(error);
              });
          });
        },
        REFRESH_TOKEN: () => {
          return new Promise((resolve, reject) => {
            axios
              .post(`AuthController/RefreshToken`)
              .then(response => {
                resolve(response);
              })
              .catch(error => {
                reject(error);
              });
          });
        }
    }
}
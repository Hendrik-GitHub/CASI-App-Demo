import axios from "axios";

export default {
    state: {
      userName: ""
    },
    getters: {
      USER: state => {
        return state.userName;
      },
    },
    mutations: {
      SET_USER: (state, payload) => {
        state.userName = payload.username;
      }
    },
    actions: {
        LOGIN: ({ commit }, payload) => {
          return new Promise((resolve, reject) => {
            axios.post(`AuthController/Login/`, payload)
              .then(({ data, status }) => {
                if (status === 200) {                
                  localStorage.setItem('Token', JSON.stringify(data.Token));
                  commit("SET_USER", payload);
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
            axios.post(`UserController/CreateUser/`, {
                username,
                emailaddress,
                password
              })
              .then(({ data, status }) => {
                console.log(data);
                console.log(status);
                if (status === 200 && data.Success == true) {
                  resolve(true);
                }
                else if (data.Success == false) {
                  reject(data.Message);
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
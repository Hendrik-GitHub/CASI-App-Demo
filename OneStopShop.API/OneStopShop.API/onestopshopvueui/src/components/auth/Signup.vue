<template>
    <v-container fill-height>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md8>
        <v-card class="elevation-8">
          <v-toolbar dark color="blue">
            <v-toolbar-title>Signup form</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-form>
                <v-alert
                :value="userExists"
                color="error"
                icon="warning">This user already exists, try a different set of data.</v-alert>
              
              <v-text-field
                prepend-icon="person"
                name="login"
                v-model="username"
                label="Login"
                :rules="[rules.required]"
        
              ></v-text-field>

              <v-text-field
                prepend-icon="email"
                name="emailaddress"  
                v-model="emailaddress"      
                label="Email Address"
                :rules="[rules.required, rules.email]"
      
              ></v-text-field>

              <v-text-field
                prepend-icon="lock"
                name="password"
                label="Password"
                type="password"
                :rules="[rules.required]"
                v-model="password"
            
              ></v-text-field>

              <v-text-field
                prepend-icon="lock"
                name="password"
                label="Confirm Password"
                type="password"
                :rules="[rules.required]"
                v-model="confirm_password"
                :error="!valid()"

              ></v-text-field>
            </v-form>
          </v-card-text>
          <v-divider light></v-divider>
          <v-card-actions>
            <v-btn to="/login" rounded color="black" dark>Login</v-btn>
            <v-spacer></v-spacer>
            <v-btn rounded color="success" @click.prevent="register()">
              Register
              <v-icon>keyboard_arrow_up</v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
    name: "signup",
    data: () => ({
        userExists: false,
        username: "",
        emailaddress: "",
        password: "",
        confirm_password: "",
        rules: {
            required: value => !!value || "Required",
            email: value => {
                const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return pattern.test(value) || "Invalid e-mail.";
            } 
        }
    }),
    methods: {
        register() {
          if (this.valid()) {

            if (this.username != "" && this.emailaddress != "" && this.password != "" && this.confirm_password != "") {
              this.$store.dispatch("REGISTER", {
                username: this.username,
                emailaddress: this.emailaddress,
                password: this.password
              })
              .then(({ status }) => {
                this.$store.commit("SET_NOTIFICATION", {
                  display: true,
                  text: "Your account has been created!",
                  alertClass: "danger"
                });

                this.$router.push("/login");
              })
              .catch(error => {
                this.userExists = true;
              })
            }                   
          }               
        },
        valid() {
            return this.password === this.confirm_password;
        }
    }
}
</script>
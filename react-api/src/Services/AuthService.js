import axios from 'axios';
import authHeader from './AuthHeader';
const API_URL = "http://localhost:8080/api/";
class AuthService {
    login(username, password) {
        var bodyFormData = new FormData();
        bodyFormData.append('login', username)
        bodyFormData.append('password', password)
        return axios.
            post(API_URL + "auth/sign-in", bodyFormData, {
                headers: {
                    'Accept': '*/*',
                    'Content-Type': 'application/x-www-form-urlencoded',
                }
            })
            .then(response => {
                if (response.data.token) {
                    var myDate = new Date();
                    myDate.setHours(myDate.getHours() + 1); //one hour from now
                    localStorage.setItem('expiration', JSON.stringify(myDate));
                    localStorage.setItem("user", JSON.stringify(response.data))
                }

                return response.data;
            })
    }


    register(username, password) {
        var bodyFormData = new FormData();
        bodyFormData.append('login', username)
        bodyFormData.append('password', password)
        return axios.post(API_URL + "auth/register", bodyFormData, {
            headers: {
                'Accept': '*/*',
                'Content-Type': 'application/x-www-form-urlencoded',
            }
        })
            .then(response => {
                if (response.data.token) {
                    localStorage.setItem("user", JSON.stringify(response.data))
                }

                return response.data;
            });
    }

    logout() {
        return axios.get(API_URL + "auth/logout", {
            headers: {
                'Authorization': authHeader().Authorization
            }
        })
            .then(() => {
                localStorage.clear()
                window.location.href = '/login'
            })
    }

    changePassword(password) {
        return axios.post(API_URL + "auth/change-password", {
            password
        })
    }

    getCurrentUser() {
        return JSON.parse(localStorage.getItem('user'));
    }

    isLoggedIn() {
        var user = this.getCurrentUser();
        if (user != null) { return true } else {
            return false;
        }
    }
}

function checkExpiration() {
    //check if past expiration date
    var values = JSON.parse(localStorage.getItem('expiration'));
    //check "my hour" index here
    if (values < new Date()) {
        localStorage.removeItem("user")
        localStorage.removeItem("expiration")
    }
}

function myFunction() {
    var myinterval = 15 * 60 * 1000; // 15 min interval
    setInterval(function () { checkExpiration(); }, myinterval);
}

myFunction();

export default new AuthService();
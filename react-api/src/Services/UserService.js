import axios from 'axios';
import authHeader from './AuthHeader';

const API_URL = "http://localhost:8080/api/";

class UserService {
    getProfile(userId) {
        return axios.get(API_URL + "accounts/profile", {
            params: {
                accountId: userId
            }
        }, {
            headers: {
                'Authorization': authHeader().Authorization
            }
        }).then(response => { return response.data })
    }
    getProfilePosts(userId){
        return axios.get(API_URL + "accounts/posts", {params:{
            accountId: userId,
            pageNumber: 1,
            pageSize: 10
        }}).then(response => {return response.data})
    }

    getProfileTopics(userId){
        return axios.get(API_URL + "accounts/topics", {params:{
            accountId: userId,
            pageNumber: 1,
            pageSize: 10
        }}).then(response => {return response.data})
    }
}

export default new UserService();
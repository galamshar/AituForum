import axios from 'axios';
import authHeader from './AuthHeader';

const API_URL = "https://localhost:44382/api/";

class PostService{
    getPostById(postId){
        return axios.get(API_URL +"posts/by-topic/"+postId+"?pageNumber=1&pageSize=100",{
            headers : {
                'Authorization': authHeader().Authorization
            }
        })
        .then(response => {
            return response.data;
        })
    }

    getPostByTopicId(topicId,pageNumber,pageSize){
        return axios.get(API_URL + "by-topic/" + topicId, {params : {
            pageNumber : pageNumber,
            pageSize : pageSize
        }})
    }

    post(topicId,text){
        var bodyFormData = new FormData();
        bodyFormData.append('topicId', topicId);
        bodyFormData.append('text', text)
        return axios.post(API_URL + "posts/" + topicId + "/post", bodyFormData, {
            headers : {
                'Authorization': authHeader().Authorization
            }
        })
    }
}

export default new PostService();
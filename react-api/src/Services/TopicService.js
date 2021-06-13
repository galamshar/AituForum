import axios from 'axios';
import authHeader from './AuthHeader';

const API_URL = "https://localhost:44382/api/";

class TopicService {
    getRootTopics(pageNumber, pageSize) {
        return axios.get(API_URL + "topics/root-topics", {
            params: {
                pageNumber: pageNumber,
                pageSize: pageSize
            }
        })
            .then(response => {
                return response.data
            })
    }

    getSubTopics(parentTopicId, pageNumber, pageSize) {
        return axios.get(API_URL + "topics/sub-topics", {
            params: {
                topicId: parentTopicId,
                pageNumber: pageNumber,
                pageSize: pageSize
            }
        })
            .then(response => {
                return response.data
            })
    }

    getCountOfPosts(topicId) {
        return axios.get(API_URL + "topics/posts-count/" + topicId)
            .then(response => {
                return response.data
            }
            )
    }

    createSubtopic(parentTopicId, name, description) {
        var bodyFormData = new FormData();
        bodyFormData.append('parentTopicId', parentTopicId);
        bodyFormData.append('name', name);
        bodyFormData.append('description', description);
        bodyFormData.append('canOwnPosts', true);
        bodyFormData.append('rolesAllowedToRead', "Student");
        bodyFormData.append('rolesAllowedToWrite', "Student");
        return axios.post(API_URL + "topics/create-subtopic", bodyFormData, {
            headers: {
                'Authorization': authHeader().Authorization
            }
        })
    }
}

export default new TopicService();
import axios from 'axios';

class Crud {
    static get(url, config) {
        return axios.get(url, config);
    }

    static post(url, config, body) {
        return axios.post(url, config, body);
    }
}

export default Crud;
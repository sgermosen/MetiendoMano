class PhotoService {
    axios
    baseUrl

    constructor(axios, baseUrl) {
        this.axios = axios
        this.baseUrl = `${baseUrl}photos`
    }

    getAll(take, filter) {
        let self = this;
        let query = `take=${take}&filter=${JSON.stringify(filter)}`

        return self.axios.get(`${self.baseUrl}?${query}`);
    }

    create(model) {
        let self = this;
        return self.axios.post(`${self.baseUrl}`, model);
    }
}

export default PhotoService
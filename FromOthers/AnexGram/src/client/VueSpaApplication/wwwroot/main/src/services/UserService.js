class UserService {
    axios
    baseUrl

    constructor(axios, baseUrl) {
        this.axios = axios
        this.baseUrl = `${baseUrl}users`
    }

    getAll(filter) {
        let self = this;
        let query = `filter=${JSON.stringify(filter)}`

        return self.axios.get(`${self.baseUrl}?${query}`);
    }

    get(identifier, filter) {
        let self = this;
        let query = `filter=${JSON.stringify(filter)}`

        return self.axios.get(`${self.baseUrl}/${identifier}?${query}`);
    }

    partial(id, params) {
        let self = this;
        return self.axios.patch(`${self.baseUrl}/${id}`, params);
    }

    image(id, file) {
        let self = this;
        return self.axios.put(`${self.baseUrl}/${id}/image`, file);
    }

    refreshClaims() {
        window.location.href = '/auth/refresh';
    }
}

export default UserService
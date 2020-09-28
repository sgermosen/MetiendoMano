class ReportService {
    axios
    baseUrl

    constructor(axios, baseUrl) {
        this.axios = axios
        this.baseUrl = `${baseUrl}reports`
    }

    getPaged(params) {
        let self = this;
        return self.axios.post(`${self.baseUrl}/GreaterUsersParticipation`, params);
    }
}

export default ReportService
import axios from "axios"

// Make a connection to the backend plans API
const client = axios.create({
    baseURL: 'https://localhost:44338/api/plans',
    json: true
})

export default {
    // Create a generic method to assoicate with all the API options
    async execute(method, resource, data) {
        return client({
            method,
            url: resource,
            data,
        }).then(req => {
            return req.data
        })
    },
    // Get all the Plans
    getAll() {
        return this.execute('get', '/')
    },
    // Get a plan based on the ID given
    getById(id) {
        return this.execute('get', `/${id}`)
    },
    // Get a plan based on the ID given
    removePlan(id) {
        return this.execute('delete', `/${id}`)
    },
    // Create a Plan
    create(data) {
        return this.execute('post', '/', data)
    },
    // Update a Plan
    update(id, data) {
        return this.execute('put', `/${id}`, data)
    },
}
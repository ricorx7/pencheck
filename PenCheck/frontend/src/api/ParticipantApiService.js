import axios from "axios"

const client = axios.create({
    baseURL: 'https://localhost:44338/api/participants',
    json: true
})

export default {
    async execute(method, resource, data) {
        return client({
            method,
            url: resource,
            data,
        }).then(req => {
            // TODO: Can probably remove this return
            return req.data
        })
    },
    // Get all the participants
    getAll() {
        return this.execute('get', '/')
    },
    // Get a Participant based on the ID
    getById(id) {
        return this.execute('get', `/${id}`)
    },
    // Create a Participant based on the data given
    create(data) {
        return this.execute('post', '/', data)
    },
    // Update the Participant data
    update(id, data) {
        return this.execute('put', `/${id}`, data)
    },
    // Get a Participant based on the ID given
    remove(id) {
        return this.execute('delete', `/${id}`)
    },
    // Get all the participants based on the Plan ID
    getAllPlans(id) {
        return this.execute('get', '/partsplan' + `/${id}`)
    },
}
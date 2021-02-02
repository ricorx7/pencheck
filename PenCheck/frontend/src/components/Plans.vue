<template>
    <div class="Plans">
        <b-alert :show="loading" variant="info">Loading...</b-alert>
        <b-alert :show="error.length > 0" variant="danger">{{ error }}</b-alert>

        <div class="row">
            <div class="col-md-4">
                <b-jumbotron header="Add Plan" lead="Create a new Plan">
                    <b-form-input v-model="planName" placeholder="Enter Plan name"></b-form-input>
                    <b-btn v-show="!loading && planName.length > 0" @click.prevent="addPlan()" variant="primary">Add Plan</b-btn>
                </b-jumbotron>
            </div>

            <div class="col-md-8">
                <h1>Current Plans</h1>
                <b-list-group>
                    <b-list-group-item class="d-flex justify-content-between align-items-center" v-for="(plan,index) in planList" v-bind:item="plan" v-bind:index="index" v-bind:key="plan.id">
                        <h5 v-if="plan.participants != null"><b-badge>{{plan.participants.length}}</b-badge> {{ plan.name }}</h5>
                        <h5 v-else><b-badge>0</b-badge> {{ plan.name }}</h5>

                        <b-button-group>
                            <b-button @click="editPlan(plan.id)" size="small">Edit</b-button>
                            <b-button v-if="plan.participants == null || plan.participants.length == 0" @click="removePlan(plan.id)" variant="danger" size="small">Delete</b-button>
                        </b-button-group>
                    </b-list-group-item>
                </b-list-group>
            </div>
        </div>
    </div>
</template>

<script>
    import apiPlans from "@/api/PlansApiService";
    import apiParticipants from "@/api/ParticipantApiService";

export default {
    name: "Plans",
    components: {
        //AddParticipant
    },
    props: { msg: String },
        data() {
            return {
                loading: false,
                planName: "",
                planList: [],
                participantList: [],
                error: ""
            }
        },
        async created() {
            this.loading = true

            // Get all the plans
            this.planList = await apiPlans.getAll()

            // Get all the participants for each plan
            var x
            for (x = 0; x < this.planList.length; x++)
            {
                // Await needed to ensure all the data is loaded and not a promise
                this.planList[x].participants = await apiParticipants.getAllPlans(this.planList[x].id)
            }

            this.loading = false
        },
        methods: {
            async addPlan() {
                // Set the flag that loading
                this.loading = true

                try {
                    // Create a data object with the plan name
                    const data = {
                        Name: this.planName
                    }

                    // Pass the new plan to the list
                    await apiPlans.create(data)

                    // Clear the plan text box
                    this.planName = ""

                    // Get the new list of plans
                    this.planList = await apiPlans.getAll()

                } catch (error) {
                    // Set the error message
                    this.error = "Error getting data: " + error.message
                } finally {
                    // End the loading message
                    this.loading = false
                }
            },
            editPlan(planId) {
                // Move to the edit plan page
                this.$router.push({ name: 'plans', params: { planId } }) // -> /plans/123
            },
            async removePlan(planId) {
                // TODO: Add Modal Warning
                // Delete the plan
                await apiPlans.removePlan(planId)

                // Set the new list
                this.planList = await apiPlans.getAll()
            }
        }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
    h3 {
        margin: 40px 0 0;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        display: inline-block;
        margin: 0 10px;
    }

    a {
        color: #42b983;
    }
</style>

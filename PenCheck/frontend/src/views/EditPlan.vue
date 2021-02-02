<template>
    <div class="EditPlan">
        <b-alert :show="loading" variant="info">Loading...</b-alert>
        <b-alert :show="error.length > 0" variant="danger">{{ error }}</b-alert>

        <b-jumbotron header="Edit Plan" lead="Edit the plan name">
            <form ref="form" @submit.stop.prevent="handleSubmit">
                <b-form-group label="Edit Plan Name" label-for="planName-input" invalid-feedback="Plan Name is required" :state="planNameState">
                    <b-form-input id="planName-input" v-model="selectedPlan.name" placeholder="Plan Name" :state="planNameState" required></b-form-input>
                </b-form-group>
                <b-btn v-show="!loading" @click.prevent="updatePlanName()">Edit</b-btn>
            </form>

            <br />
            <div>
                <b-table striped hover :items="participantList"></b-table>
            </div>
        </b-jumbotron>

    </div>
</template>


<script>
    import apiParticipants from "@/api/ParticipantApiService";
    import apiPlans from "@/api/PlansApiService";

    export default {
        name: "EditPlan",
        props: {
            planId: String
        },
        data() {
            return {
                loading: false,
                participantList: [],
                selectedPlan: {},
                selectedPlanId: this.planId,
                error: "",
                planNameState: null,
            }
        },
        async created() {
            this.selectedPlan = await apiPlans.getById(this.selectedPlanId)
            this.participantList = await apiParticipants.getAllPlans(this.selectedPlanId)
        },
        methods: {
            resetModal() {
                // Clear the text boxes
                this.loading = false
                this.participantList = []
                this.error = ""
            },
            async updatePlanName() {
                if (this.selectedPlan.name.length <= 0) {
                    this.planNameState = false
                }
                else {
                    this.planNameState = true

                    // Update the Plan name
                    await apiPlans.update(this.selectedPlanId, this.selectedPlan)
                }
            },
        }
    }
</script>
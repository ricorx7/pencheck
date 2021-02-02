<template>
    <div class="EditParticipants">
        <b-alert :show="loading" variant="info">Loading...</b-alert>
        <b-alert :show="error.length > 0" variant="danger">{{ error }}</b-alert>

        <div class="row">
            <div class="col-md-4">
                <b-jumbotron header="Edit Participant" lead="Create a new Participant">
                    <form ref="form" @submit.stop.prevent="handleSubmit">
                        <b-alert :show="loading" variant="info">Loading...</b-alert>
                        <b-alert :show="error.length > 0" variant="danger">{{ error }}</b-alert>
                        <b-form-group label="First Name" label-for="firstName-input" invalid-feedback="First Name is required" :state="firstNameState">
                            <b-form-input id="firstName-input" v-model="selectedParticipant.firstName" placeholder="First Name" :state="firstNameState" required></b-form-input>
                        </b-form-group>
                        <b-form-group label="Last Name" label-for="lastName-input" invalid-feedback="Last Name is required" :state="lastNameState">
                            <b-form-input id="lastName-input" v-model="selectedParticipant.lastName" placeholder="Last Name" :state="lastNameState" required></b-form-input>
                        </b-form-group>
                        <b-form-group label="Social Security Number" label-for="ssn-input" invalid-feedback="Social Security is required and must follow the format." :state="ssnState">
                            <b-form-input id="ssn-input" v-model="selectedParticipant.ssn" placeholder="Social Security Number" :state="ssnState" required></b-form-input>
                        </b-form-group>
                        <b-form-group label="Email" label-for="email-input" invalid-feedback="Email must have this format." :state="emailState">
                            <b-form-input id="email-input" v-model="selectedParticipant.email" type="email" placeholder="Email" :state="emailState"></b-form-input>
                        </b-form-group>
                        <b-form-group label="Select Plan" label-for="plan-select">
                            <b-form-select id="plan-select" v-model="selectedPlan" class="mb-3">
                                <b-form-select-option v-for="(plan,index) in planList" v-bind:value="plan" v-bind:index="index" v-bind:key="plan.id">{{plan.name}}</b-form-select-option>
                            </b-form-select>
                        </b-form-group>
                        <b-btn v-show="!loading" @click.prevent="updateParticipant()">Update Participant</b-btn>
                    </form>
                </b-jumbotron>
            </div>
        </div>
    </div>
</template>


<script>
    import apiParticipants from "@/api/ParticipantApiService";
    import apiPlans from "@/api/PlansApiService";

    export default {
        name: "EditParticipants",
        props: {
            id: String
        },
        data() {
            return {
                loading: false,
                planList: [],
                selectedParticipantId: this.id,
                error: "",
                selectedParticipant: {},
                selectedPlan: {},
                firstNameState: null,
                lastNameState: null,
                ssnState: null,
                emailState: null,
            }
        },
        async created() {
            /**
             * Create the display and get all the initial data to populate the fields.
             * */
            this.selectedParticipant = await apiParticipants.getById(this.selectedParticipantId)

            // TODO: Make this selection value based on the ID
            // Get all the plans and determine which one is selected
            console.log(this.selectedParticipant)
            console.log(this.selectedParticipant.planId)
            this.planList = await apiPlans.getAll()
            var x
            for (x = 0; x < this.planList.length; x++) {
                console.log(this.planList[x].id)
                if (this.planList[x].id == this.selectedParticipant.planId) {
                    this.selectedPlan = this.planList[x]
                    console.log(this.selectedPlan)
                }
            }

        },
        methods: {
            checkFormValidity() {
                // Verify a first name was entered
                this.firstNameState = this.selectedParticipant.firstName.length > 0 ? true : false

                // Verify a last name was entered
                this.lastNameState = this.selectedParticipant.lastName.length > 0 ? true : false

                // Verify the social security number has 8 digits
                this.ssnState = this.selectedParticipant.ssn.length == 8 ? true : false

                // If an email address is given, verify it is properly formated
                if (this.selectedParticipant.email.length > 0) {
                    // Email Regex
                    var re = /(.+)@(.+){2,}\.(.+){2,}/;
                    this.emailState = re.test(this.selectedParticipant.email.toLowerCase());
                }
                else {
                    // Since optional, set to true if nothing is given
                    this.emailState = true
                }

                // Verfiy all the states are good
                if (this.firstNameState && this.lastNameState && this.ssnState && this.emailState) {
                    return true
                }

                return false
            },
            async updateParticipant() {
                /**
                 * Update a participant in the database.
                 * Verify the form entry before making the API call.
                 **/

                this.loading = true

                // Exit when the form isn't valid
                if (!this.checkFormValidity()) {
                    this.loading = false
                    return
                }

                try {
                    // Set the new selected plan ID
                    this.selectedParticipant.planId = this.selectedPlan.id

                    // Pass the new participant data
                    await apiParticipants.update(this.selectedParticipantId, this.selectedParticipant)

                } catch (error) {
                    this.error = "Error getting data: " + error.message
                } finally {
                    this.loading = false
                }
            }
        }
    }
</script>
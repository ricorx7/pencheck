<template>
    <div class="Participants">
        <b-alert :show="loading" variant="info">Loading...</b-alert>
        <b-alert :show="error.length > 0" variant="danger">{{ error }}</b-alert>

        <div class="row">
            <div class="col-md-4">
                <b-jumbotron header="Add Participant" lead="Create a new Participant">
                    <form ref="form" @submit.stop.prevent="handleSubmit">
                        <b-alert :show="loading" variant="info">Loading...</b-alert>
                        <b-alert :show="error.length > 0" variant="danger">{{ error }}</b-alert>
                        <b-form-group label="First Name" label-for="firstName-input" invalid-feedback="First Name is required" :state="firstNameState">
                            <b-form-input id="firstName-input" v-model="firstName" placeholder="First Name" :state="firstNameState" required></b-form-input>
                        </b-form-group>
                        <b-form-group label="Last Name" label-for="lastName-input" invalid-feedback="Last Name is required" :state="lastNameState">
                            <b-form-input id="lastName-input" v-model="lastName" placeholder="Last Name" :state="lastNameState" required></b-form-input>
                        </b-form-group>
                        <b-form-group label="Social Security Number" label-for="ssn-input" invalid-feedback="Social Security is required and must follow the format." :state="ssnState">
                            <b-form-input id="ssn-input" v-model="ssn" placeholder="Social Security Number" :state="ssnState" required></b-form-input>
                        </b-form-group>
                        <b-form-group label="Email" label-for="email-input" invalid-feedback="Email must have this format." :state="emailState">
                            <b-form-input id="email-input" v-model="email" type="email" placeholder="Email" :state="emailState"></b-form-input>
                        </b-form-group>
                        <b-form-group label="Select Plan" label-for="plan-select">
                            <b-form-select id="plan-select" v-model="selectedPlan" class="mb-3">
                                <b-form-select-option v-for="(plan,index) in planList" v-bind:value="plan" v-bind:index="index" v-bind:key="plan.id">{{plan.name}}</b-form-select-option>
                            </b-form-select>
                        </b-form-group>
                        <b-btn v-show="!loading" @click.prevent="addParticipant()">Add Participant</b-btn>
                    </form>
                </b-jumbotron>
            </div>

            <div class="col-md-8">
                <h1>Current Participants</h1>
                <b-list-group>
                    <b-list-group-item class="d-flex justify-content-between align-items-center" v-for="(participant,index) in participantList" v-bind:item="participant" v-bind:index="index" v-bind:key="participant.id">
                        <h5>{{participant.firstName}} {{participant.lastName}}</h5>
                        <div>{{participant.email}}</div>
                        <div>{{participant.planId}}</div>

                        <b-button-group>
                            <b-button @click="editParticipant(participant.id)" size="small">Edit</b-button>
                            <b-button @click="removeParticipant(participant.id)" variant="danger" size="small">Remove</b-button>
                        </b-button-group>
                    </b-list-group-item>
                </b-list-group>
            </div>
        </div>

    </div>
</template>


<script>
    import apiParticipants from "@/api/ParticipantApiService";
    import apiPlans from "@/api/PlansApiService";
    //import CreateParticipant from "@/components/CreateParticipant.vue";

    export default {
        name: "Participants",
        components: {
            //CreateParticipant
        },
        data() {
            return {
                loading: false,
                participantList: [],
                error: "",
                planNameState: null,
                firstName: "",
                lastName: "",
                ssn: "",
                email: "",
                planList: [],
                selectedPlan: {},
                firstNameState: null,
                lastNameState: null,
                ssnState: null,
                emailState: null,
            }
        },
        async created() {
            this.planList = await apiPlans.getAll()
            this.participantList = await apiParticipants.getAll()
        },
        methods: {
            resetValues() {
                // Clear the text boxes
                this.loading = false
                this.participantList = []
                this.error = ""

                // Clear the text boxes
                this.firstName = ""
                this.lastName = ""
                this.ssn = ""
                this.email = ""
                this.selectedPlan = {}
                this.firstNameState = null
                this.lastNameState = null
                this.ssnState = null
                this.emailState = null
            },
            async editParticipant(id) {
                // Move to the edit participants page
                this.$router.push({ name: 'editParticipants', params: { id } }) // -> /parts/123
            },
            async removeParticipant(id) {
                this.loading = true

                console.log(id)
                // TODO: Add a warning before removing

                // Remove the Participant from the database
                await apiParticipants.remove(id)

                // Refresh the list
                this.participantList = await apiParticipants.getAll()

                this.loading = false
            },
            checkFormValidity() {
                // Verify a first name was entered
                this.firstNameState = this.firstName.length > 0 ? true : false

                // Verify a last name was entered
                this.lastNameState = this.lastName.length > 0 ? true : false

                // Verify the social security number has 8 digits
                this.ssnState = this.ssn.length == 8 ? true : false

                // If an email address is given, verify it is properly formated
                if (this.email.length > 0) {
                    // Email Regex
                    var re = /(.+)@(.+){2,}\.(.+){2,}/;
                    this.emailState = re.test(this.email.toLowerCase());
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
            async addParticipant() {
               /**
                * Add a participant to the database.
                * Verify the form entry before making the API call.
                **/

                this.loading = true

                // Exit when the form isn't valid
                if (!this.checkFormValidity()) {
                    this.loading = false
                    return
                }


                try {
                    // Create a data object with the participant
                    const data = {
                        FirstName: this.firstName,
                        LastName: this.lastName,
                        SSN: this.ssn,
                        Email: this.email,
                        PlanId: this.selectedPlan.id
                    }

                    // Pass the new participant data
                    await apiParticipants.create(data)

                } catch (error) {
                    this.error = "Error getting data: " + error.message
                } finally {
                    // Clear all the values
                    this.resetValues()

                    // Reload the list
                    this.participantList = await apiParticipants.getAll()

                    this.loading = false
                }
            }
        }
    }
</script>
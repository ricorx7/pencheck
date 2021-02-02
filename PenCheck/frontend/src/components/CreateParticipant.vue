<template>
    <div class="CreateParticipant">
        <b-button v-b-modal.modal-CreateParticipant>Create Participant</b-button>

        <b-modal id="modal-CreateParticipant" title="Create Participant" @show="resetModal" @hidden="resetModal" @ok="handleOk">
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
                <b-form-select label="Select a Plan" v-model="selectedPlan" class="mb-3">
                    <b-form-select-option v-for="(plan,index) in planList" v-bind:value="plan" v-bind:index="index" v-bind:key="plan.id">{{plan.name}}</b-form-select-option>
                </b-form-select>
                <!--b-btn v-show="!loading" @click.prevent="addParticipant()">Add Participant</!--b-btn-->
            </form>
        </b-modal>
    </div>
</template>

<script>
    import apiParticipants from "@/api/ParticipantApiService";
    import apiPlans from "@/api/PlansApiService";

export default {
    name: "CreateParticipant",
    props: { msg: String },
    data() {
        return {
            loading: false,
            firstName: "",
            lastName: "",
            ssn: "",
            email: "",
            error: "",
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
    },
    methods: {
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
        resetModal() {
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
            this.loading = false
        },
        handleOk(bvModalEvt) {
            // Prevent modal from closing
            bvModalEvt.preventDefault()
            // Trigger submit handler
            this.handleSubmit()
        },
        async handleSubmit() {
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
                this.loading = false
            }

            // Hide the modal manually
            this.$nextTick(() => {
                this.$bvModal.hide('modal-CreateParticipant')
            })
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

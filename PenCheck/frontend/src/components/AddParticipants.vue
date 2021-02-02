<template>
    <div class="AddParticipant">
        {{selectPlan}}
    </div>
</template>

<script>
    import apiParticipants from "@/api/ParticipantApiService";
    //import apiPlans from "@/api/PlansApiService";

    export default {
        name: "AddParticipant",
        props: {
            inputPlan: Object
        },
        data() {
            return {
                loading: false,
                participantList: [],
                ///selectedPlanId: this.planId,
                selectedPlan: this.inputPlan,
                error: ""
            }
        },
        async created() {
            this.participantList = await apiParticipants.getAll()
            //this.selectedPlan = await apiPlans.getById(this.selectedPlan.id)
            this.selectedPlan = this.inputPlan
        },
        methods: {
            resetModal() {
                // Clear the text boxes
                this.loading = false
                this.participantList = []
                this.error = ""
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

                this.loading = false

                // Hide the modal manually
                this.$nextTick(() => {
                    this.$bvModal.hide('modal-AddParticipant')
                })
            }
        }
    }
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
<template>
    <div class="about">
        <h1>Enrique Castelo</h1>
        Just a quick application to test creating plans and participants.  Then assoicate participants with plans.
        This will use VueJS and ASP.net.
        <br />
        <a href="mailto:rico.castelo@gmail.com">rico.castelo@gmail.com</a>
        <br />
        Rev: 0.0.1

        <br />
        <br />

        <h4>UI Component</h4>

        <div>
            <button class="button button1">
                <b-icon-cloud-upload class="icon-tc"></b-icon-cloud-upload>
                <div class="button-title">This is the card title</div>
                <div class="button-subtitle">This is the sub-text or description of card</div>
            </button>
        </div>

    </div>
</template>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
    .button {
        background-color: white; /* White */
        border: #571680;
        color: white;
        text-align: center;
        transition-duration: 0.4s;
        cursor: pointer;
        padding-left: 24px;
        padding-right: 24px;
    }
    .button1 {
        background-color: white;
        color: black;
        border: 1px solid #571680;
        border-radius: 10px;
        min-width: 200px;
        max-width: 400px;
    }

    .icon-tc {
        color: #9230D0;
        width: 60px;
        height: 60px;
        padding-top: 32px;
        padding-bottom: 0px;
    }

    .button-title {
        font: Roboto,medium;
        font-size: 20px;
        align-content: center;
        color: #9423DB;
        padding-top: 24px;
        padding-bottom: 0px;
        line-height: 26px;
    }

    .button-subtitle {
        font: Roboto,regular;
        font-size: 14px;
        align-content: center;
        color: #571680;
        padding-top: 8px;
        padding-bottom: 32px;
        line-height: 20px;
    }

    .button1:hover {
        background-color: #9423DB1F;
        color: white;
    }

    .button.selected {
        background-color: #9423DB0D;
    }
</style>

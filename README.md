# PenCheck Coding Exercise

Create a basic CRUD application that behaves as described below. Write the application in a manner that you would in your daily work.

The files can be open in VisualStudio Community 2019

# Start Debugging 

 Start the VueJS debug server
``` bash
cd frontend
npm install
npm run serve
```

Press F5 to start running the ASP.net Server

---

# Usage
Home will take you to all the Plans.  It will allow you to view, add and remove the Plans.  You can click on Edit to view all Participants in the Plan.  You can also change the name of the plan.  The table on Home displays the Plan name, number of Participants and the Edit and Remove button.

The Participant link will take you the page to add and display all the participants.  The Edit button will allow you to edit the Participant and the Remove will remove the Participant.  The number in the list indicates the index for the Participant's plan.  In the future, the INNER JOIN will make this show the actual plan name.'

The About link will give the version number and author of the code.

The data is stored to an SQLite database.

---

# SQLite
The SQLite database file is located in the project folder (Pencheck.db).  It will contain all the data locally.


# Model Update
If the model is updated, run the following commands in the Package Manager Console
```bash
add-migration MessageAboutChanges -context PencheckContext
update-database -context PencheckContext
```
This will update the database with the latest model changes.

---

# Directory Structure
There are 2 sections in the directory structure.  The "frontend" folder contains the VueJS frontend.  The remaining folders are used by the ASP.net backend.
I am not sure if you prefer a backend and frontend folder.  I just stuck to the way the VisualStudio creates projects.

## Controllers
These are the controllers for the models.  This contains all the API calls for the models.  This code was generated by the VisualStudio controller scaffolding.

## Data
These are the DataContext files to connect the models to the database connection (persistant data).

## Frontend
This contains the VueJS frontend code.  The VueJS takes advantage of Axios for API calls and BootstrapVue to use Bootstrap.

## Migrations
Contains all the database modifications.

## Models
Contains all the data models used in the application.  There are basically 2 models, the Plan and Participant.

## Pages
Contains the template pages.

---

# Design Choices
Overall to keep the time down, I limited the amount of UX.  There is a lot of things I would have done differently with more time.  But the basic functionally it there.


## Plan-To-Participant
To make things simple, I made the participant only capable of being in a single project.
This made it easier to assoicate the pariticipant to a single entry of the plan within the participant data.
If the participant could be in more than one plan, then I would have changed the scheme differently.  I would need to think about how to handle when a participant is deleted.

## Add Plan Button
To make things go quicker, I just made the button available when any typing was does in the textbox.  I could have made it an enabled and disabled button to always have the button present.

## UX
I tried to the minimum UX.  Obviously there are many places that things can be improved.

## DB scheme
I could have added a lot more to the database schema like date created and modified.

## Pagnation
To handle thousands of participants, I would add pagnation to list all the participants and plans.

## Link the Participants in Plan to the Edit Page
I would have linked the participants in the plan page to there edit page to quickly edit a user.  

## INNER JOIN
I would have done an INNER JOIN with the participant and plan data, so I would have the plan information for each participant.  I felt is was implied that this would have been done during production, but not necessary right now.

## Vuex
I did not need to utilize anything like Vuex.  The plans could have been stored globally to reduce the server calls.

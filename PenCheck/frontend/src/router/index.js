import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import EditPlan from "../views/EditPlan.vue";
import Participants from "../views/Participants.vue";
import EditParticipants from "../views/EditParticipant.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
    },
    {
    path: '/plans/:planId',
    name: 'plans',
        component: EditPlan,
        props: true
    },
    {
        path: '/participants',
        name: 'participants',
        component: Participants,
        props: true
    },
    {
        path: '/parts/:id',
        name: 'editParticipants',
        component: EditParticipants,
        props: true
    },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;

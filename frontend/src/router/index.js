import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
      meta: {
        requiresAuth: false,
        title: "Home / Szakácskönyv",
      },
    },
    {
      path: "/about",
      name: "about",
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import("../views/AboutView.vue"),
      meta: {
        requiresAuth: false,
        title: "About / Szakácskönyv",
      },
    },
    {
      path: "/receptek",
      name: "receptek",
      component: () => import("../views/ReceptekView.vue"),
      meta: {
        requiresAuth: false,
        title: "Receptek / Szakácskönyv",
      },
    },
    {
      path: "/receptKezeles",
      name: "receptKezeles",
      component: () => import("../views/ReceptKezelesView.vue"),
      meta: {
        requiresAuth: false,
        title: "Recept kezelés / Szakácskönyv",
      },
    },
    {
      path: "/fuvarBevitel",
      name: "fuvarBevitel",
      component: () => import("../views/FuvarBevitel.vue"),
      meta: {
        requiresAuth: false,
        title: "Fuvar bevitel / Szakácskönyv",
      },
    },
    {
      path: "/count1",
      name: "count1",
      component: () => import("../views/Count1View.vue"),
      meta: {
        requiresAuth: false,
        title: "Counter 1 / Szakácskönyv",
      },
    },
    {
      path: "/count2",
      name: "count2",
      component: () => import("../views/Count2View.vue"),
      meta: {
        requiresAuth: false,
        title: "Counter 2 / Szakácskönyv",
      },
    },
    {
      path: "/login",
      name: "login",
      component: () => import("../views/LoginView.vue"),
      meta: {
        requiresAuth: false,
        title: "Login / Szakácskönyv",
      },
    },
    {
      path: "/:pathMatch(.*)*",
      name: "PageNotFound",
      component: () => import("../views/404View.vue"),
      meta: {
        requiresAuth: false,
        title: "404 / Szakácskönyv",
      },
    },
  ],
});

export default router;

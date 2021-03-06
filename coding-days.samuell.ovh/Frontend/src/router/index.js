import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/home.vue'
import Invitation from '../views/invitation.vue'
import Register from '../views/register.vue'
import Hint from '../views/hint.vue'

const routes = [
    {
        path: '/',
        name: 'home',
        component: Home,
        // component: () => import('../views/register.vue'),
    },
    {
        path: '/invitation',
        name: 'invitation',
        component: Invitation,
    },
    {
        path: '/register',
        name: 'register',
        component: Register,
    },
    {
        path: '/hint',
        name: "hint",
        component: Hint,
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router

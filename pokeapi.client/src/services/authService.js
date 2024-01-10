import axios from "axios";

export async function registrationUser(username, email, password) {
    try {
        const newUser = {
            username: String(username),
            email: String(email),
            password: String(password)
        };
        const { data: user } = await axios.post('/user/registration', newUser);
        return user;
    } catch (err) {
        console.log(err);
    }
}

export async function loginUser(email, password) {
    try {
        const loginUser = {
            email: String(email),
            password: String(password)
        };
        const { data: user } = await axios.post('/user/login', loginUser);
        return user;
    } catch (err) {
        console.log(err);
    }
}

export async function logoutUser() {
    try {
        await axios.post('/user/logout');
    } catch (err) {
        console.log(err);
    }
}

export async function getCurrentUser() {
    try {
        const { data: user } = await axios.get('user/current');
        return user;
    } catch (err) {
        console.log(err);
    }
}
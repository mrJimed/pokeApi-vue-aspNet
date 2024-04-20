import axios from "axios"

const ROOT_ROUTE = '/user'

export async function registrationUserAsync(username, email, password) {
    const { data: user } = await axios.post(`${ROOT_ROUTE}/registration`, {
        username,
        email,
        password
    });
    return user;
}

export async function loginUserAsync(email, password) {
    const { data: user } = await axios.post(`${ROOT_ROUTE}/login`, {
        email,
        password
    });
    return user;
}

export async function logoutUserAsync() {
    await axios.post(`${ROOT_ROUTE}/logout`);
}

export async function resetPasswordAsync(email, newPassword) {
    await axios.post(`${ROOT_ROUTE}/reset-password`, {
        email,
        password: newPassword
    });
}

export async function generateResetPasswordCodeAsync(toEmail) {
    const { data: code } = await axios.post(`${ROOT_ROUTE}/generate-reset-code`, {
        toEmail
    });
    return code;
}
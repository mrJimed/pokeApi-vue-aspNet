import axios from "axios";

export async function sendMessage(toEmail, text, subjectMessage) {
    try {
        const message = {
            toEmail: toEmail,
            text: text,
            subjectMessage: subjectMessage
        };
        await axios.post('/mail', message);
    } catch (err) {
        console.log(err);
    }
}
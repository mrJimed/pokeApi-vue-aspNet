export function randomInteger(min, max) {
    let rand = min + Math.random() * (max - min);
    return Math.floor(rand);
} 
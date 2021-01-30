const password_input = document.querySelector("#password");
const hash_input = document.querySelector("#hash");
const but_login = document.querySelector("#but_login");

but_login.addEventListener('click', () => {
    let value = password_input.value;
    hash_input.value = sha256(value);
});
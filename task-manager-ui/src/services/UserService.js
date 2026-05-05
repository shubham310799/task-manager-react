const TOKEN_KEY = "jwt_token";
const BASE_URL = "https://localhost:7131/api/user";
export const getToken = () => {
    const token = localStorage.getItem(TOKEN_KEY);

    if (!token) {
        return null;
    }
    return token;
};

export const Login = async (loginRequest) => {
    const response = await fetch(`${BASE_URL}/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(loginRequest),
    });
    console.log(response);
    if (!response.ok) throw new Error("Failed to login");
    const body = await response.json();
    if (body.success) {
        console.log(body.data);
        localStorage.setItem(TOKEN_KEY, body.data);
    }
    else {
        throw new Error(body.error.message || "Failed to login");
    }
};

export const Signup = async (signupRequest) => {
    const response = await fetch(`${BASE_URL}/signup`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(signupRequest),
    });

    if (!response.ok) throw new Error("Failed to signup");
    const body = await response.json();
    if (body.success) {
        localStorage.setItem(TOKEN_KEY, body.data);
    }
    else {
        throw new Error(body.error.message || "Failed to signup");
    }
};
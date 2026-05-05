import React from 'react';
import { Login } from '../services/UserService';
import { redirect, useNavigate } from 'react-router-dom';

export default function LoginPage() {

    const [email, setEmail] = React.useState("");
    const [password, setPassword] = React.useState("");
    const navigate = useNavigate();
    const handleOnClick = async (e) => {
        e.preventDefault();
        const req = {
            email: email,
            password: password
        };
        console.log(req);
        await Login(req);
        // Navigate("/task-list");
        navigate("/task-list");
    };
    return (
        <div className="container">
            <h2>Login</h2>
            <form>
                <div className="form-group">
                    <label htmlFor="email">Email:</label>
                    <input type="email" id="email" className="form-control" value={email} onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="password">Password:</label>
                    <input type="password" id="password" className="form-control" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <button type="submit" className="btn btn-primary" onClick={handleOnClick}>
                    Login
                </button>
            </form>
        </div>
    );
}

import {FormEvent, useState} from "react";

import style from './style/LoginPage.module.css';
import Navbar from "../components/Navbar.tsx";
import Footer from "../components/Footer.tsx";
import { motion } from "framer-motion";
import {Link, useLocation, useNavigate} from "react-router-dom";
import api from "../axios.ts";

export default function LoginPage() {
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const location = useLocation();
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            await api.post('/auth/login', {
                username: username,
                password: password,
            });
            alert("You have logged successfully.");
            const params = new URLSearchParams(location.search);
            const redirectTo = params.get("redirectTo") || "/";

            navigate(redirectTo, { replace: true });
        } catch(error: any) {
            alert(error.response.data);
            console.warn(error.response.data || "An error occurred while logging.");
        }

    }

    return (
        <>
            <Navbar active={""}/>
            <div className={style.container} >
                <motion.form onSubmit={handleSubmit}
                    initial={{opacity: 0, x: 50}} animate={{opacity: 1, x: 0}} exit={{opacity: 0, x: -50}} transition={{duration: 0.6, ease: "easeOut"}}>
                    <h2>Συμπλήρωσε τα στοιχεία σου</h2>
                    <div className={style.inputs}>
                        <div>
                            <label htmlFor={"username"}>Όνομα Χρήστη</label>
                            <div className={"input-group"}>
                                <span className={"material-symbols-outlined"}>person</span>
                                <input type={"text"} name={"username"} id={"username"} placeholder={"Όνομα Χρήστη"} required={true} value={username}
                                       onChange={(e) => setUsername(e.target.value)}/>
                            </div>
                        </div>

                        <div>
                            <label htmlFor={"password"}>Κωδικός Πρόσβασης</label>
                            <div className={"input-group"}>
                                <span className={"material-symbols-outlined"}>lock</span>
                                <input type={"password"} name={"password"} id={"password"} placeholder={"Κωδικός Πρόσβασης"} required={true}
                                       value={password}
                                       onChange={(e) => setPassword(e.target.value)}/>
                            </div>
                        </div>

                    </div>
                    <Link to={"/auth/register"} className={"text-link"}>Δεν έχεις λογαριασμό; Κάνε εγγραφή.</Link>
                    <button type={"submit"} className={"btn btn-primary"}>Σύνδεση</button>
                </motion.form>
            </div>
            <Footer/>
        </>

    );
}
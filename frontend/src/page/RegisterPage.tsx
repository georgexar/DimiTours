import {FormEvent, useState} from "react";

import style from './style/RegisterPage.module.css';
import Navbar from "../components/Navbar.tsx";
import Footer from "../components/Footer.tsx";
import { motion } from "framer-motion";
import {Link, useNavigate} from "react-router-dom";
import api from "../axios.ts";

export default function RegisterPage() {
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [gender, setGender] = useState<number>(0);
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            await api.post('/auth/register', {
                username: username,
                password: password,
                firstName: firstName,
                lastName: lastName,
                email: email,
                gender: gender,
                imageUrl: ""
            });
            alert("You have registered successfully.");
            navigate("/auth/login");
        } catch(error: any) {
            alert(error.response.data);
            console.warn(error.response.data || "An error occurred while registering.");
        }

    }

    return (
        <>
            <Navbar active={""}/>
            <div className={style.container} >
                <motion.form onSubmit={handleSubmit} initial={{opacity: 0, x: 50}} animate={{opacity: 1, x: 0}} exit={{opacity: 0, x: -50}} transition={{duration: 0.6, ease: "easeOut"}}>
                <h2>Συμπλήρωσε τα στοιχεία σου</h2>
                    <div className={style.inputs}>

                        <div>
                            <label htmlFor={"email"}>Email</label>
                            <div className={"input-group"}>
                                <span className={"material-symbols-outlined"}>person</span>
                                <input type={"email"} name={"email"} id={"email"} placeholder={"Email"}
                                       required={true} value={email}
                                       onChange={(e) => setEmail(e.target.value)}/>
                            </div>
                        </div>

                        <div>
                            <label htmlFor={"username"}>Όνομα Χρήστη</label>
                            <div className={"input-group"}>
                                <span className={"material-symbols-outlined"}>person</span>
                                <input type={"text"} name={"username"} id={"username"} placeholder={"Όνομα Χρήστη"}
                                       required={true} value={username}
                                       onChange={(e) => setUsername(e.target.value)}/>
                            </div>
                        </div>

                        <div>
                            <label htmlFor={"password"}>Κωδικός Πρόσβασης</label>
                            <div className={"input-group"}>
                                <span className={"material-symbols-outlined"}>lock</span>
                                <input type={"password"} name={"password"} id={"password"}
                                       placeholder={"Κωδικός Πρόσβασης"} required={true}
                                       value={password}
                                       onChange={(e) => setPassword(e.target.value)}/>
                            </div>
                        </div>

                        <div>
                            <label htmlFor={"firstName"}>Όνομα</label>
                            <div className={"input-group"}>
                                <span className={"material-symbols-outlined"}>person</span>
                                <input type={"text"} name={"firstName"} id={"firstName"} placeholder={"Όνομα"}
                                       required={true} value={firstName}
                                       onChange={(e) => setFirstName(e.target.value)}/>
                            </div>
                        </div>

                        <div>
                            <label htmlFor={"lastName"}>Επώνυμο</label>
                            <div className={"input-group"}>
                                <span className={"material-symbols-outlined"}>person</span>
                                <input type={"text"} name={"lastName"} id={"lastName"} placeholder={"Επώνυμο"}
                                       required={true} value={lastName}
                                       onChange={(e) => setLastName(e.target.value)}/>
                            </div>
                        </div>

                        <div>

                            <label htmlFor={"name"}
                                   onClick={() => gender === 0 ? setGender(1) : setGender(0)}>Φύλο</label>
                            <div className={"input-group"}>

                                <a className={"pointer"}
                                   style={{color: gender === 0 ? "var(--success-color)" : "var(--gray-color)"}}
                                   onClick={() => setGender(0)}>Άνδρας</a>
                                <a className={"pointer"}
                                   style={{color: gender === 1 ? "var(--success-color)" : "var(--gray-color)"}}
                                   onClick={() => setGender(1)}>Γυναίκα</a>
                            </div>
                        </div>

                    </div>
                    <Link to={"/auth/login"} className={"text-link"}>Έχεις ήδη λογαριασμό; Συνδέσου τώρα.</Link>
                    <button type={"submit"} className={"btn btn-primary"}>Εγγραφή</button>
                </motion.form>
            </div>
            <Footer/>
        </>

    );
}
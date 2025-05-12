import {Link} from "react-router-dom";
import style from "./Navbar.module.css";
import ThemeButton from "./ThemeButton.tsx";
import useUser from "../hooks/useUser.ts";

interface NavbarProps {
    active?: string;
}

export default function Navbar({active = "home"}: NavbarProps) {

    const {user, loading, logout} = useUser();

    if (loading) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

    function LogoutButtons() {
        return (
            <>
                <Link to={"/"} onClick={logout} className={"btn btn-error"}>Αποσύνδεση</Link>
            </>
        );
    }

    function LoginButtons() {
        return (
            <>
                <Link to={"/auth/login"}>Σύνδεση</Link>
                <Link to={"/auth/register"} className={"btn btn-primary"}>Εγγραφή</Link>
            </>
        );
    }

    return (
        <nav className={style.nav}>
              <h2><Link to={"/"}><span>Dimi</span>Tours</Link></h2>
              <div className={style.links}>
                  <Link to={"/"} className={active==="home" ? "hoverable active" : "hoverable"}>Αρχική</Link>
                  <Link to={"/sections"} className={active==="sections" ? "hoverable active" : "hoverable"}>Ενότητες</Link>
                  <Link to={"/tests"} className={active==="tests" ? "hoverable active" : "hoverable"}>Τεστ Αξιολόγησης</Link>
                  <Link to={"/revision"} className={active==="revision" ? "hoverable active" : "hoverable"}>Επαναληπτικά Τεστ Αξιολόγησης</Link>
                  <Link to={"/progress"} className={active==="progress" ? "hoverable active" : "hoverable"}>Πρόοδος</Link>
              </div>
            <div className={style.buttons}>
                {user ? LogoutButtons() : LoginButtons()}

                <ThemeButton/>
            </div>
        </nav>

    );
}
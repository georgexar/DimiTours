import Navbar from "../components/Navbar.tsx";
import style from './style/SectionsPage.module.css';
import useSections from "../hooks/useSections.ts";
import {Link} from "react-router-dom";
import { motion } from "framer-motion";
import Footer from "../components/Footer.tsx";


export default function SectionsPage() {

    const {sections, loading} = useSections();

    if (loading) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

    function NoSections() {
        return (
          <div className={style.empty}>
              <h3>Δεν βρέθηκαν διαθέσιμες ενότητες. Παρακαλώ δοκιμάστε αργότερα.</h3>
          </div>
        );
    }

    function truncateText(text: string, maxLength: number = 50): string {
        return text.length > maxLength ? text.slice(0, maxLength) + "..." : text;
    }


    return (
        <>
            <Navbar active={"sections"}/>
            <div className={style.container}>
                <div className={style.intro}>
                    <h2>Ενότητες</h2>
                    <p>Δες όλες τις διαθέσιμες ενότητες και μάθε περισσότερα για τη Δημητσάνα.</p>
                </div>

                <div className={style.sections}>
                    {sections?.length === 0 && <NoSections/>}
                    {sections?.map((section, index) => (
                        <motion.div className={style.section}
                                    initial={{opacity: 0, x:-400}} animate={{opacity: 1, x: 0}}
                                    exit={{opacity: 0, x: 400}} transition={{ease: "easeOut", duration: 0.8, delay: index * 0.05}}>
                            <div className={style.background} style={{ backgroundImage: `url("/${section.imageUrl}")` }}>
                                <h2>{index+1}</h2>
                            </div>
                            <div className={style.text}>
                                <h3>{section.title}</h3>
                                <p>{truncateText(section.purpose)}</p>
                            </div>

                            <div className={style.buttons}>
                                <Link to={`/sections/${section.id}`} className={`btn btn-primary && ${style.button}`}>Ξεκίνα</Link>
                            </div>

                        </motion.div>
                    ))}

                </div>
            </div>
            <Footer/>
        </>
    );
}
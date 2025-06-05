import Navbar from "../components/Navbar.tsx";
import style from './style/SectionsPage.module.css';
import {Link} from "react-router-dom";
import useActivities from "../hooks/useActivities.ts";
import { motion } from "framer-motion";
import Footer from "../components/Footer.tsx";
import {useActivityProgress} from "../hooks/useActivityProgress.ts";
import {Activity} from "../models/Activity.ts";


export default function ActivitiesPage() {

    const {activities, loading} = useActivities();

    if (loading) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

    function NoActivities() {
        return (
            <div className={style.empty}>
                <h3>Δεν βρέθηκαν διαθέσιμα τεστ αξιολόγησης. Παρακαλώ δοκιμάστε αργότερα.</h3>
            </div>
        );
    }

    function truncateText(text: string, maxLength: number = 50): string {
        return text.length > maxLength ? text.slice(0, maxLength) + "..." : text;
    }

    function ActivityItem({ activity, index }: { activity: Activity, index: number }) {
        const { progress, loading } = useActivityProgress(activity.id);
        if (loading) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

        const x = progress.trim().length >= 11 ? 7 : 6;
        const percent = parseInt(progress.trim().substring(x)) | 0;
        return (
            <motion.div
                className={style.section}
                initial={{ opacity: 0, x: -400 }}
                animate={{ opacity: 1, x: 0 }}
                exit={{ opacity: 0, x: 400 }}
                transition={{ ease: "easeOut", duration: 0.8, delay: index * 0.05 }}
            >
                <div className={style.background} style={{ backgroundImage: `url("/${activity.section.imageUrl}")` }}>
                    <h2>{index + 1}</h2>
                </div>

                <div className={style.text}>
                    <h3>{activity.title}</h3>
                    <p>{truncateText(activity.description)}</p>
                </div>

                <div className={style.fullbar}>
                    {progress}
                <div className={style.progress}>
                    <motion.div
                        initial={{opacity: 0, width: 0}}
                        animate={{ opacity: 1, width: `${loading ? 0 : percent}%` }}
                        transition={{ ease: "easeOut", duration: 1.5, delay: index * 0.3 }}
                        className={style.amount} style={{ width: `${loading ? 0 : percent}%` }} />
                </div>

                </div>
                <div className={style.buttons}>
                    <Link to={`/tests/${activity.id}`} className={`btn ${percent ? "btn-error" : "btn-primary"} && ${style.button}`}>{percent ? "Προσπάθησε Πάλι" : "Ξεκίνα"}</Link>
                </div>
            </motion.div>
        );
    }


    return (
        <>
            <Navbar active={"tests"}/>
            <div className={style.container}>
                <div className={style.intro}>
                    <h2>Τεστ Αξιολόγησης</h2>
                    <p>Δες όλα τα διαθέσιμα τεστ αξιολόγησης για την κάθε ενότητα.</p>
                </div>

                <div className={style.sections}>
                    {activities?.length === 0 && <NoActivities/>}
                    {activities?.map((activity, index) => (
                        <ActivityItem key={activity.id} activity={activity} index={index} />
                    ))}

                </div>
            </div>
            <Footer/>
        </>
    );
}
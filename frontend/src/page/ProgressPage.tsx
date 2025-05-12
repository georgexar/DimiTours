import Navbar from "../components/Navbar.tsx";
import Footer from "../components/Footer.tsx";
import style from "./style/ProgressPage.module.css";
import useUser from "../hooks/useUser.ts";
import useOverallProgress from "../hooks/useOverallProgress.ts";
import useQuestionTypeProgress from "../hooks/useQuestionTypeProgress.ts";
import useActivities from "../hooks/useActivities.ts";
import useAllActivityProgress from "../hooks/useAllActivityProgress.ts";
import {useNavigate} from "react-router-dom";
import { BarChart } from '@mui/x-charts/BarChart';
import useAllAnswers from "../hooks/useAllAnswers.ts";



export default function ProgressPage() {

    const navigate = useNavigate();
    const {user, loading} = useUser();
    const {progress: overallProgress, loading: overallLoading} = useOverallProgress();
    const {progress: questionTypeProgress, loading: questionTypeLoading} = useQuestionTypeProgress();
    const {activities, loading: activitiesLoading} = useActivities();
    const { progressMap: activityProgressMap, loading: activityProgressLoading } = useAllActivityProgress(activities);
    const {answers, loading: answersLoading} = useAllAnswers();
    if (loading || overallLoading || questionTypeLoading || activitiesLoading || activityProgressLoading || answersLoading) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

    if(!user) {
        navigate("/auth/login");
        return;
    }

    function formatDate(date: Date): string {
        return date.toISOString().split('T')[0]; // yyyy-mm-dd
    }

    function translate(successRate: string) {
        switch(successRate) {
            case "HISTORY":
                return "Ιστορία";
            case "GASTRONOMY":
                return "Γαστρονομία";
            case "TRADITION":
                return "Παράδοση";
            case "NATURE":
                return "Φύση";
            case "ARCHITECTURE":
                return "Αρχιτεκτονική";
            default:
                return successRate;
        }
    }

    const countByDay: Record<string, {total: number, correct: number, incorrect: number}> = {};
    answers.forEach((a) => {
        const day = formatDate(new Date(a.userAnswer.createdAt));

        if (!countByDay[day]) {
            countByDay[day] = { total: 0, correct: 0, incorrect: 0 };
        }

        countByDay[day].total += 1;

        if (a.userAnswer.isCorrect) {
            countByDay[day].correct += 1;
        } else {
            countByDay[day].incorrect += 1;
        }
    });

    const dataset = Object.entries(countByDay).map(([day, counts]) => ({
        day,
        answers: counts.total,
        correct: counts.correct,
        incorrect: counts.incorrect,
    }));

    return (
    <>
        <Navbar active={"progress"}/>
        <div className={style.container}>
            <h2>Γεια σου, {user.firstName} {user.lastName}!</h2>
            <div className={style.private}>

                <div className={style.datatables}>
                    <div className={"custom-table"}>
                        <h3>Προσωπικά Στοιχεία</h3>
                        <table>
                            <tbody>
                            <tr>
                                <th>Όνομα</th>
                                <td>{user.firstName}</td>
                            </tr>
                            <tr>
                                <th>Επώνυμο</th>
                                <td>{user.lastName}</td>
                            </tr>
                            <tr>
                                <th>Όνομα Χρήστη</th>
                                <td>{user.userName}</td>
                            </tr>
                            <tr>
                                <th>Φύλο</th>
                                <td>{user.gender === 0 ? "Άνδρας" : "Γυναίκα"}</td>
                            </tr>
                            </tbody>
                        </table>
                    </div>
                    <div className={style.combined}>
                        <h3>Ποσοστά Επιτυχίας</h3>
                        <p>Συνολικό Ποσοστό Επιτυχίας: {overallProgress}</p>
                        <div className={style.row}>
                            <div className={"custom-table"}>
                                <h3>Ανά Κατηγορία</h3>
                                <table>
                                    <tbody>
                                    {questionTypeProgress.map((item) => (
                                        <tr>
                                            <th>{translate(item.questionType)}</th>
                                            <td>{item.successRate}</td>
                                        </tr>
                                    ))}
                                    </tbody>
                                </table>

                            </div>

                            <div className={"custom-table"}>
                                <h3>Ανά Ενότητα</h3>
                                <table>
                                    <tbody>
                                    {activities!==null && activities.map((activity) => (
                                        <tr key={activity.id}>
                                            <th>{activity.title}</th>
                                            <td>{activityProgressMap[activity.id] !== null ? String(activityProgressMap[activity.id]) : "N/A"}</td>

                                        </tr>
                                    ))}
                                    </tbody>
                                </table>

                            </div>
                        </div>

                    </div>

                </div>

                <div>
                    <h3>Γραφήματα</h3>
                    <BarChart
                        dataset={dataset}
                        xAxis={[{ dataKey: 'day' }]}
                        series={[{ dataKey: 'answers', label: 'Total Answers' }, {dataKey: 'correct', label: 'Correct Answers'},
                            {dataKey: 'incorrect', label: 'Incorrect Answers'}]}
                        height={400}
                        width={600}

                    />
                </div>

            </div>
        </div>
        <Footer/>
    </>
    );
}
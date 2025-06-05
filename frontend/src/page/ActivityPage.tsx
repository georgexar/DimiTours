import Navbar from "../components/Navbar.tsx";
import style from './style/ActivityPage.module.css';
import {useActivity} from "../hooks/useActivity.ts";
import {Link, useParams} from "react-router-dom";
import Footer from "../components/Footer.tsx";
import api from "../axios.ts";

export default function ActivityPage() {

    const {id} = useParams();
    const {activity, loading} = useActivity(Number(id));
    if (loading) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

    if(!activity) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

    const handleSubmit = async () => {
        let correctCount = 0;
        const answers: number[] = [];

        activity.questions.forEach((question) => {
            const selected = document.querySelector<HTMLInputElement>(
                `input[name="answer-${question.id}"]:checked`
            );
            if (selected) {
                const answerId = parseInt(selected.value);
                answers.push(answerId);
                const correctAnswer = question.answers.find((a) => a.isCorrect);
                if (correctAnswer && correctAnswer.id === answerId) {
                    correctCount++;
                }
            }
        });

        if (answers.length === 0) {
            alert("Παρακαλώ απαντήστε τουλάχιστον σε μία ερώτηση.");
            return;
        }

        try {
            const response = await api.post("/answer/submit", {
                answerId: answers, // must match DTO name in backend
            });
            console.log(response);
            alert(`Υποβλήθηκε:\nΣωστές απαντήσεις: ${correctCount} από ${activity.questions.length}`);
        } catch (error) {
            console.error("Error submitting answers:", error);
            alert("Προέκυψε σφάλμα κατά την υποβολή.");
        }
    };




    const clearForm = () => {
        activity.questions.forEach((question) => {
            const selected = document.querySelector<HTMLInputElement>(
                `input[name="answer-${question.id}"]:checked`
            );
            if (selected) {
                selected.checked = false;
            }
        });
    }

    return (
        <>
            <Navbar active={"tests"}/>
            <div className={style.container}>
                <div className={style.section}>
                    <h1>{activity.title}</h1>
                    <div className={style.imageContainer}>
                        <img src={`/${activity.section.imageUrl}`} alt={"Section Image"}/>
                    </div>
                    <div className={style.headings}>
                        <h2>{activity.title}</h2>
                        <a>{activity.description}</a>
                    </div>

                    <div className={style.questions}>
                        {activity.questions.map((question,index)=> {



                            return (<div className={style.question}>
                                <div className={style.questionText}>
                                    <h2>Ερώτηση {index+1}</h2>
                                    <span>{question.text}</span>
                                </div>

                                <div className={style.options}>
                                    {question.answers.map((answer)=> (
                                        <label htmlFor={String(answer.id)}>
                                            <input className={"radio"} name={`answer-${question.id}`} id={String(answer.id)} value={String(answer.id)} type={"radio"}/>
                                            {answer.text}
                                        </label>
                                    ))}

                                </div>
                            </div>);
                        })}

                        <div className={style.buttons}>
                            <Link to={"#"} className={"btn btn-error"} onClick={clearForm}>Εκκαθάριση</Link>
                            <Link to={"#"} className={"btn btn-primary"} onClick={handleSubmit}>Υποβολή</Link>
                        </div>

                    </div>


                </div>
            </div>
            <Footer/>
        </>
    );
}
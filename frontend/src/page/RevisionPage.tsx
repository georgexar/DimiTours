import Navbar from "../components/Navbar.tsx";
import style from './style/ActivityPage.module.css';
import {Link} from "react-router-dom";
import Footer from "../components/Footer.tsx";
import api from "../axios.ts";
import useUser from "../hooks/useUser.ts";
import useRevisionQuestions from "../hooks/useRevisionQuestions.ts";
import {Answer} from "../models/Answer.ts";

export default function RevisionPage() {

    const {user, loading: userLoading} = useUser();
    const {questions, loading: revisionLoading} = useRevisionQuestions();
    if (revisionLoading || userLoading || !user) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

    const handleSubmit = async () => {
        let correctCount = 0;
        const answers: number[] = [];

        questions.weakQuestions.forEach((question) => {
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
            alert(`Υποβλήθηκε:\nΣωστές απαντήσεις: ${correctCount} από ${questions.length}`);
        } catch (error) {
            console.error("Error submitting answers:", error);
            alert("Προέκυψε σφάλμα κατά την υποβολή.");
        }
    };




    const clearForm = () => {
        questions.forEach((question) => {
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
            <Navbar active={"revision"}/>
            <div className={style.container}>
                <div className={style.section}>
                    <h1>Επαναληπτικό Τεστ Αξιολόγησης</h1>
                    <div className={style.imageContainer}>
                        <img src={`/dimitsana.png`} alt={"Section Image"}/>
                    </div>
                    <div className={style.headings}>
                        <h2>Επαναληπτικό Τεστ Αξιολόγησης</h2>
                        <a>Αυτό το τεστ περιλαμβάνει επαναληπτικές ερωτήσεις προσαρμοσμένες αποκλειστικά για εσένα σύμφωνα με τις δυσκολίες σου.
                        </a>
                    </div>

                    <div className={style.recommend}>
                        <h2>Ενότητες που έχεις αστοχίες</h2>
                        <div className={style.recommendedSections}>
                                    {questions.recommendedSections.map((r, i)=> (
                                        <a>{r}{i !== questions.recommendedSections.length - 1
                                            ? "," : "."} </a>
                                    ))}
                        </div>
                    </div>

                    <div className={style.questions}>

                        {questions.weakQuestions.map((question, index) => {


                            return (<div className={style.question}>
                                <div className={style.questionText}>
                                    <h2>Ερώτηση {index + 1}</h2>
                                    <span>{question.text}</span>

                                </div>

                                <div className={style.options}>
                                    {question.answers.map((answer: Answer) => (
                                        <label htmlFor={String(answer.id)}>
                                            <input className={"radio"} name={`answer-${question.id}`}
                                                   id={String(answer.id)} value={String(answer.id)} type={"radio"}/>
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
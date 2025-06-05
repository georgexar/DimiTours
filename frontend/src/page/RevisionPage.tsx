import Navbar from "../components/Navbar.tsx";
import style from './style/ActivityPage.module.css';
import {Link} from "react-router-dom";
import Footer from "../components/Footer.tsx";
import api from "../axios.ts";
import useUser from "../hooks/useUser.ts";
import useRevisionQuestions from "../hooks/useRevisionQuestions.ts";
import {Answer} from "../models/Answer.ts";
import {useEffect, useRef, useState} from "react";

export default function RevisionPage() {

    const {user, loading: userLoading} = useUser();
    const [theme, setTheme] = useState<string>('light');
    const [openIndex, setOpenIndex] = useState<number | null>(null);
    const contentRefs = useRef<Array<HTMLDivElement | null>>([]);



    useEffect(() => {
        setTheme(localStorage.getItem("theme") || "light");
    }, [theme]);

    const handleAccordion = (section: string, i : number) => {
        return (
            <div key={i} className="accordion-item">
                <button className="accordion-header" onClick={() => toggle(i)}>{section}</button>
                <div className="accordion-content" ref={(el) => (contentRefs.current[i] = el)} 
                style={{
                    overflow: 'hidden',
                    transition: 'max-height 0.3s ease, padding 0.3s ease'
                }}>
                    <p> {section==="Ιστορία και Πολιτισμός της Δημητσάνας" && "Η Δημητσάνα αποτελεί έναν από τους πιο ξεχωριστούς και ιστορικά φορτισμένους προορισμούς της Πελοποννήσου. Βρίσκεται στην καρδιά της Αρκαδίας, σε ένα εντυπωσιακό ορεινό τοπίο, και ξεχωρίζει για την πλούσια πολιτιστική της κληρονομιά και τη συμβολή της στην Ελληνική Επανάσταση του 1821. Κατά τη διάρκεια της Επανάστασης, η Δημητσάνα διαδραμάτισε καίριο ρόλο, καθώς αποτέλεσε κέντρο παραγωγής πυρίτιδας και γενέτειρα σημαντικών μορφών, όπως ο Παπαφλέσσας. Χτισμένη αμφιθεατρικά πάνω σε βραχώδες έδαφος, η Δημητσάνα γοητεύει με την παραδοσιακή της αρχιτεκτονική: πέτρινα σπίτια, λιθόστρωτα σοκάκια και αρχοντικά που αντανακλούν το μεγαλείο του παρελθόντος. Σήμερα, έχει εξελιχθεί σε δημοφιλή τουριστικό προορισμό, προσφέροντας μια μοναδική εμπειρία που συνδυάζει ιστορία, παράδοση και φυσική ομορφιά. Η περιοχή είναι επίσης γνωστή για την τοπική βιοτεχνία, ιδιαίτερα στην κατασκευή ξύλινων αντικειμένων και εργαλείων, ενώ τα τελευταία χρόνια προσελκύει επισκέπτες που αγαπούν τη φύση και τις υπαίθριες δραστηριότητες, όπως η πεζοπορία, η αναρρίχηση και η ποδηλασία στα γραφικά μονοπάτια της."}
                        {section==="Παράδοση και Πολιτισμός της Δημητσάνας" && "Η Δημητσάνα ξεχωρίζει για τον βαθύ πολιτισμικό της πλούτο και την ισχυρή παράδοσή της. Οι κάτοικοί της διαχρονικά ασχολήθηκαν με πλήθος παραδοσιακών τεχνών, όπως η υφαντική, η κεραμική και η ξυλογλυπτική, μεταδίδοντας τις γνώσεις αυτές από γενιά σε γενιά. Η υφαντική κατέχει ιδιαίτερη θέση στην τοπική πολιτιστική ταυτότητα, με τα χειροποίητα υφαντά να εντυπωσιάζουν για την ποιότητα και την καλλιτεχνική λεπτομέρειά τους. Εξίσου σημαντική είναι και η κεραμική τέχνη, η οποία διατηρείται ζωντανή έως σήμερα μέσα από εργαστήρια και επιδείξεις που προσφέρουν την ευκαιρία σε επισκέπτες να γνωρίσουν από κοντά αυτή την παραδοσιακή δημιουργική δραστηριότητα. Η τοπική μουσική παράδοση της Δημητσάνας παραμένει αναπόσπαστο κομμάτι της πολιτισμικής της ζωής, με όργανα όπως η λύρα και το λαούτο να συνοδεύουν γιορτές και πανηγύρια. Τα δημοτικά τραγούδια, εμπνευσμένα από την ιστορία, τα έθιμα και τις αφηγήσεις του τόπου, συνεχίζουν να τραγουδιούνται και να συγκινούν. Παράλληλα, οι τοπικές εορτές –όπως το Πάσχα και τα παραδοσιακά πανηγύρια– διατηρούν αμείωτη τη ζωντάνια της κοινότητας, συνδυάζοντας μουσική, χορό και αυθεντικές λαϊκές παραδόσεις."}
                        {section==="Ταξίδι στη Φύση της Δημητσάνας" && "Η Δημητσάνα είναι χτισμένη σε μία από τις πιο εντυπωσιακές και καταπράσινες περιοχές της Πελοποννήσου, σε έναν τόπο πλούσιο σε φυσική ομορφιά και βιοποικιλότητα. Το τοπίο της χαρακτηρίζεται από πυκνά δάση, ορεινούς όγκους και τρεχούμενα νερά, που δημιουργούν ιδανικές συνθήκες για δραστηριότητες όπως η πεζοπορία, η αναρρίχηση και η παρατήρηση της φύσης. Στην ευρύτερη περιοχή εκτείνονται ελατοδάση, πευκοδάση και δάση από βελανιδιές, ενώ οι ποταμοί και τα ρέματα που τη διασχίζουν προσφέρουν ένα μαγευτικό φυσικό σκηνικό. Ιδιαίτερα ξεχωρίζει το φαράγγι του Λούσιου, ένας δημοφιλής προορισμός για λάτρεις της υπαίθρου, που προσφέρει μονοπάτια μέσα σε απότομες πλαγιές και σπάνιας ομορφιάς τοπία. Η τοπική πανίδα περιλαμβάνει είδη όπως αγριογούρουνα, λαγούς, ελάφια και πολλά είδη πουλιών, ενώ η χλωρίδα της περιοχής φιλοξενεί σπάνια φυτά και αγριολούλουδα, καθιστώντας τη Δημητσάνα ιδανικό προορισμό για φυσιολάτρες, φωτογράφους και ερευνητές του περιβάλλοντος."}
                        {section==="Τουριστικός Οδηγός της Δημητσάνας" && "Η Δημητσάνα συνδυάζει αρμονικά την ιστορική της σημασία με το απαράμιλλο φυσικό τοπίο, καθιστώντας την έναν από τους πιο αγαπημένους τουριστικούς προορισμούς της Πελοποννήσου. Η περιοχή προσφέρει μια πληθώρα επιλογών για τον επισκέπτη, τόσο σε επίπεδο αξιοθέατων όσο και δραστηριοτήτων στη φύση. Ανάμεσα στα σημαντικότερα σημεία ενδιαφέροντος ξεχωρίζει το Μουσείο Υδροκίνησης, που αναδεικνύει την παραδοσιακή τεχνολογία της περιοχής, καθώς και το Κρυφό Σχολειό, ένας ιστορικός χώρος με έντονο συμβολισμό από την περίοδο της Επανάστασης του 1821. Τα μονοπάτια γύρω από τη Δημητσάνα προσφέρονται για πεζοπορικές και ποδηλατικές διαδρομές, μέσα από ένα μαγευτικό φυσικό περιβάλλον γεμάτο θέα και εναλλαγές τοπίου. Η φιλοξενία στην περιοχή είναι ζεστή και αυθεντική, με τα παραδοσιακά καφενεία και τις ταβέρνες να προσφέρουν γεύσεις της τοπικής κουζίνας, όπως το κλέφτικο, οι πίτες και τα γλυκά του κουταλιού. Οι τουριστικές υποδομές είναι καλοσχεδιασμένες, με πλήθος επιλογών διαμονής που ικανοποιούν κάθε προτίμηση, από παραδοσιακούς ξενώνες μέχρι σύγχρονα καταλύματα. Η Δημητσάνα αποτελεί ιδανική επιλογή για όσους επιθυμούν να συνδυάσουν την επαφή με την ιστορία, την απόδραση στη φύση και την εμπειρία της ελληνικής παράδοσης."}
                        {section==="Ιστορία και Πολιτιστική Κληρονομιά της Δημητσάνας" && "Η Δημητσάνα κατέχει ξεχωριστή θέση στην ελληνική ιστορία, καθώς υπήρξε σημαντικό κέντρο δράσης κατά την Επανάσταση του 1821. Η στρατηγική της τοποθεσία και το δύσβατο φυσικό περιβάλλον την καθιστούσαν ασφαλές καταφύγιο για τους αγωνιστές της ελευθερίας. Από την περιοχή αυτή πέρασαν και έδρασαν κορυφαίες μορφές του Αγώνα, όπως ο Παπαφλέσσας, ενώ λέγεται πως εδώ ιδρύθηκε και ένα από τα πρώτα μυστικά σχολεία της εποχής της Τουρκοκρατίας. Η παραδοσιακή φυσιογνωμία της Δημητσάνας διατηρείται αναλλοίωτη μέσα στον χρόνο, με τα πετρόκτιστα σπίτια, τα γραφικά σοκάκια και τα παλιά καφενεία να μαρτυρούν το ιστορικό της παρελθόν. Τα μουσεία της περιοχής προσφέρουν πολύτιμες γνώσεις στον επισκέπτη· το Μουσείο Υδροκίνησης αποκαλύπτει τη συμβολή των νερών στην τοπική οικονομία, ενώ το Μουσείο της Επανάστασης παρουσιάζει τους ήρωες και τα γεγονότα που διαμόρφωσαν τον εθνικό αγώνα. Παράλληλα, η Δημητσάνα διατηρεί ζωντανές τις παραδοσιακές τέχνες και χειροτεχνίες της, όπως η ξυλογλυπτική, η μεταλλοτεχνία και η κατασκευή παραδοσιακών φορεσιών και αξεσουάρ. Όλα αυτά συνθέτουν έναν τόπο που ενώνει με μοναδικό τρόπο το ιστορικό βάθος, την πολιτιστική ταυτότητα και το φυσικό μεγαλείο της Ελλάδας, καθιστώντας τη Δημητσάνα έναν αυθεντικό και αξέχαστο προορισμό."}
                    </p>
                </div>
            </div>
        );
    }



    const toggle = (index: number) => {
        setOpenIndex(prev => (prev === index ? null : index));
    };

    useEffect(() => {
        contentRefs.current.forEach((el, idx) => {
            if (!el) return;

            if (openIndex === idx) {
                el.style.maxHeight = el.scrollHeight + 'px';
                el.style.padding = '1rem';
            } else {
                el.style.maxHeight = '0px';
                el.style.padding = '0 1rem';
            }
        });
    }, [openIndex]);

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
                                            ? "," : "."}</a>
                                    ))}

                        </div>
                        <div className={style.recommendedText}>
                            <div className={"accordion"}>
                                {questions.recommendedSections.map((r, i)=> (
                                    handleAccordion(r, i)
                                ))}
                            </div>
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
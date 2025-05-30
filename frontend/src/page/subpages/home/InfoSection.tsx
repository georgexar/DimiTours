import style from './InfoSection.module.css';
import {Link} from "react-router-dom";

export default function InfoSection() {
    return (
        <div className={style.info} id={"info"}>

            <div className={style.text}>
                <h2>Είσαι έτοιμος για ένα ταξίδι στο παρελθόν;</h2>
                <p>
                    Κρυμμένη ανάμεσα στις βουνοπλαγιές της Ορεινής Αρκαδίας, η Δημητσάνα στέκει περήφανη και
                    αινιγματική, σαν να φυλάει
                    μυστικά που δεν λέγονται εύκολα. Ένα πέτρινο σκηνικό που μοιάζει να έχει ξεφύγει από τον χρόνο, με
                    καμπαναριά, παραδοσιακά αρχοντικά
                    και σοκάκια που ψιθυρίζουν ιστορίες σε όποιον τα περπατήσει.
                    Δεν είναι απλώς ένα χωριό· είναι ένας ζωντανός θρύλος.
                    Όμως η Δημητσάνα δεν φανερώνεται σε όλους. Πρέπει να τη γνωρίσεις με το δικό της ρυθμό· να ακούσεις
                    το θρόισμα των φύλλων, να σταθείς στη θέα του φαραγγιού,
                    να αφουγκραστείς το νερό που κυλά στον Λούσιο και να αισθανθείς την παρουσία όσων πέρασαν από εκεί
                    και άφησαν σημάδια ανεξίτηλα.
                    Αν νιώσεις το κάλεσμα, τότε είσαι έτοιμος να ξεκινήσεις ένα ταξίδι που δεν μοιάζει με κανένα άλλο.
                </p>
            </div>

            <div className={style.cta}>
                <div className={style.action}>
                    <div>
                        <h2>Θες να ανακαλύψεις τα κρυμμένα μυστικά της Δημητσάνας;</h2>
                        <p>Κάνε εγγραφή για να αποκτήσεις πρόσβαση σε αποκλειστικές ενότητες, διαδραστικό περιεχόμενο, στατιστικά και τεστ αξιολόγησης.</p>
                    </div>

                    <Link to="/auth/register" className="btn btn-secondary">Εγγραφή Τώρα</Link>
                </div>

            </div>


        </div>
    );
}
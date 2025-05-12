import style from './HeroSection.module.css';
import Carousel from "../../../components/Carousel.tsx";

export default function HeroSection() {

    const carousel = [
        "dimitsana.png",
        "dimitsana2.jpg",
        "dimitsana3.jpg",
        "dimitsana4.jpg",
        "dimitsana5.jpg"
    ];

    return (
      <div className={style.hero}>
          <h1>Δημητσάνα: Η Μυστική Πρωτεύουσα της Επανάστασης</h1>
         <Carousel images={carousel}/>
          <a href={"#info"} className={"btn btn-primary"}><p>Μάθε Περισσότερα</p> <span className="material-symbols-outlined">keyboard_arrow_down</span></a>
      </div>
    );
}
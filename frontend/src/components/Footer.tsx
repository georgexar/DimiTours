import style from './Footer.module.css';
import {Link} from "react-router-dom";

export default function Footer() {
    return (

      <footer className={style.footer}>

          <p>&copy; 2025 DimiTours. All Rights Reserved.</p>
          <p>Designed by <Link to={"https://github.com/chrinikolaou"}><span><b>chrinikolaou</b></span></Link></p>
      </footer>
    );
}
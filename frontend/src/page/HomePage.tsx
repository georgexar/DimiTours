import Navbar from "../components/Navbar.tsx";
import HeroSection from "./subpages/home/HeroSection.tsx";
import Footer from "../components/Footer.tsx";
import InfoSection from "./subpages/home/InfoSection.tsx";
import useUser from "../hooks/useUser.ts";

export default function HomePage() {
    const {loading} = useUser();
    if (loading) return <div className={"loader-overlay"}><div className={"loader"}/></div>;


    return (
      <>
          <Navbar active={"home"}/>
          <HeroSection/>
          <InfoSection/>
          <Footer/>
      </>
    );
}
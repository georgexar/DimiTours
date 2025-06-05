import './App.css'
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import HomePage from "./page/HomePage.tsx";
import RegisterPage from "./page/RegisterPage.tsx";
import LoginPage from "./page/LoginPage.tsx";
import SectionsPage from "./page/SectionsPage.tsx";
import ProtectedRoute from "./components/ProtectedRoute.tsx";
import SectionPage from "./page/SectionPage.tsx";
import ActivitiesPage from "./page/ActivitiesPage.tsx";
import ActivityPage from "./page/ActivityPage.tsx";
import ProgressPage from "./page/ProgressPage.tsx";
import RevisionPage from "./page/RevisionPage.tsx";


function App() {

  return (
    <>
        <Router>
            <Routes>
                <Route path={"/"} element={<HomePage/>}/>
                <Route path={"/auth/register"} element={<RegisterPage/>}/>
                <Route path={"/auth/login"} element={<LoginPage/>}/>
                <Route path={"/sections"} element={<ProtectedRoute/>}>
                    <Route path={"/sections"} element={<SectionsPage/>}/>
                    <Route path={"/sections/:id"} element={<SectionPage/>}/>
                </Route>

                <Route path={"/tests"} element={<ProtectedRoute/>}>
                    <Route path={"/tests"} element={<ActivitiesPage/>}/>
                    <Route path={"/tests/:id"} element={<ActivityPage/>}/>
                </Route>


                <Route path={"/revision"} element={<ProtectedRoute/>}>
                    <Route path={"/revision"} element={<RevisionPage/>}/>
                </Route>

                <Route path={"/progress"} element={<ProtectedRoute/>}>
                    <Route path={"/progress"} element={<ProgressPage/>}/>
                </Route>
            </Routes>
        </Router>
    </>
  )
}

export default App

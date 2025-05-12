import {useEffect, useState} from "react";

export default function ThemeButton() {
    const [theme, setTheme] = useState<string>('light');

    useEffect(() => {
        const current = localStorage.getItem("theme") || "light";
        document.documentElement.setAttribute("data-theme", current);
        setTheme(current);
    }, []);

    const switchTheme = () => {
        const newTheme = theme === "light" ? "dark" : "light";
        document.documentElement.setAttribute("data-theme", newTheme);
        localStorage.setItem("theme", newTheme);
        setTheme(newTheme);
    }

    return (
        <div className="theme-switcher" onClick={switchTheme} role="button" aria-label="Toggle Theme">
            <div className={`toggle-switch ${theme === "dark" ? "checked" : ""}`}/>
        </div>
    );

}

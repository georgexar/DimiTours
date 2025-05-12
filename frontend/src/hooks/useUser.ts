import {User} from "../models/User.ts";
import {useEffect, useState} from "react";
import api from "../axios.ts";

export default function useUser() {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        api.get("/user/me")
            .then((res)=> {
                setUser(res.data);
            })
            .catch(()=> {
                setUser(null);
            })
            .finally(()=> {
               setLoading(false);
            });
    }, []);

    const logout = async () => {
        try {
            await api.post("/auth/logout");
            alert("You have logged out.");
        } catch(error) {
            alert(error.response.data || "Failed to logout.");
            console.error("Failed to logout.", error);
        } finally {
            setUser(null);
        }
    }

    return {user, isLoggedIn: !!user, loading, logout}

}
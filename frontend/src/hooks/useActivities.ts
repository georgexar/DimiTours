import {useEffect, useState} from "react";
import api from "../axios.ts";
import {Activity} from "../models/Activity.ts";

export default function useActivities() {
    const [activities, setActivities] = useState<Activity[] | null>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        async function fetch() {
            try {
                const response = await api.get("/activity/all");
                setActivities(response.data);
            } catch(error) {
                console.error(error || "Failed to fetch activities.");
            }
            finally {
                setLoading(false);
            }
        }

        fetch();
    }, []);

    return {activities, loading};
}
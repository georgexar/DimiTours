import { useEffect, useState } from "react";
import api from "../axios.ts";
import {Activity} from "../models/Activity.ts";

export function useActivity(id: number) {
    const [activity, setActivity] = useState<Activity | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        let isMounted = true;

        const fetchActivity = async () => {
            try {
                const response = await api.get(`/activity/${id}`);
                console.log(response.data);
                if (isMounted) {
                    setActivity(response.data);
                }
            } catch (err) {
                if (isMounted) {
                    setError("Failed to fetch activity");
                    setLoading(false);
                }
            }
            finally {
                setLoading(false);
            }
        };

        fetchActivity();

        return () => {
            isMounted = false;
        };
    }, [id]);

    return { activity, loading, error };
}

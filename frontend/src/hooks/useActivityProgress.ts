import { useEffect, useState } from "react";
import api from "../axios.ts";

export function useActivityProgress(id: number) {
    const [progress, setProgress] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {

        const fetchActivityProgress = async () => {
            try {
                const response = await api.get(`/user/activity/${id}/success`);
                setProgress(response.data.successRate);
            } catch (err) {
                console.log(err.response.data || "Failed to fetch progress.");
            }
            finally {
                setLoading(false);
            }
        };

        fetchActivityProgress();

    }, [id]);

    return { progress, loading };
}

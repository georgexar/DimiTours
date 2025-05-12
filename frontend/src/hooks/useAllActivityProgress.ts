import { useEffect, useState } from "react";
import api from "../axios.ts";
import { Activity } from "../models/Activity.ts";

interface ActivityProgress {
    successRate: string;
    // add more fields from the API response if needed
}

export default function useAllActivityProgress(activities: Activity[] | null) {
    const [progressMap, setProgressMap] = useState<Record<number, ActivityProgress>>({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!activities || activities.length === 0) {
            setLoading(false);
            return;
        }

        const fetchAll = async () => {
            const results: Record<number, ActivityProgress> = {};
            await Promise.all(
                activities.map(async (activity) => {
                    try {
                        const res = await api.get(`/user/activity/${activity.id}/success`);
                        results[activity.id] = res.data.successRate;

                    } catch (e) {
                        console.log(e.response.data || "Failed to fetch progress.");
                    }
                })
            );
            setProgressMap(results);
            setLoading(false);
        };

        fetchAll();
    }, [activities]);

    return { progressMap, loading };
}

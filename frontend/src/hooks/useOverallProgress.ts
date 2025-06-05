import api from '../axios.ts';
import {useEffect, useState} from "react";

export default function useOverallProgress() {
    const [progress, setProgress] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const fetch = async () => {
            try {
                const response = await api.get(`/user/profile/overall/success`);
                setProgress(response.data.successRate);
            } catch (err) {
                console.log(err.response.data || "Failed to fetch progress.");
            }
            finally {
                setLoading(false);
            }
        }
        fetch();
    }, []);

    return {progress, loading};
}
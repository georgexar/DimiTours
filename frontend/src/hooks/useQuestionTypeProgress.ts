import api from '../axios.ts';
import {useEffect, useState} from "react";

interface QuestionTypeSuccessRate {
    questionType: string;
    successRate: string;
}

export default function useQuestionTypeProgress() {
    const [progress, setProgress] = useState<QuestionTypeSuccessRate[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const fetch = async () => {
            try {
                const response = await api.get('/user/profile/success/questiontype');
                setProgress(response.data);
            } catch (err: any) {
                console.error(err.response?.data || "Failed to fetch progress.");
            } finally {
                setLoading(false);
            }
        };

        fetch();
    }, []);

    return { progress, loading };
}
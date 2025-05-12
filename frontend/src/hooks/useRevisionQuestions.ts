import api from '../axios.ts';
import {useEffect, useState} from "react";
import {Question} from "../models/Question.ts";

export default function useRevisionQuestions() {
    const [questions, setQuestions] = useState<Question[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const fetch = async () => {
            try {
                const response = await api.get(`/activity/adaptive/recommendations`);
                setQuestions(response.data);
                console.log(response.data);
            } catch (err) {
                console.log(err.response.data || "Failed to fetch recommendations.");
            }
            finally {
                setLoading(false);
            }
        }
        fetch();
    }, []);

    return {questions, loading};
}
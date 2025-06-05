import api from '../axios.ts';
import {useEffect, useState} from "react";
import UserAnswer from "../models/UserAnswer.ts";

export default function useAllAnswers() {
    const [answers, setAnswers] = useState<UserAnswer[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const fetch = async () => {
            try {
                const response = await api.get(`/user/profile/answers`);
                setAnswers(response.data.result);
            } catch (err) {
                console.log(err.response.data || "Failed to fetch answers.");
            }
            finally {
                setLoading(false);
            }
        }
        fetch();
    }, []);

    return {answers, loading};
}
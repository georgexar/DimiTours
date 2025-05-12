import {Section} from "../models/Section.ts";
import {useEffect, useState} from "react";
import api from "../axios.ts";

export default function useSections() {
    const [sections, setSections] = useState<Section[] | null>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        async function fetch() {
            try {
                const response = await api.get("/section/all");
                setSections(response.data);
            } catch(error) {
                console.error(error || "Failed to fetch sections.");
            }
            finally {
                setLoading(false);
            }
        }

        fetch();
    }, []);

    return {sections, loading};
}
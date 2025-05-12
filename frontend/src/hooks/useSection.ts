import { useEffect, useState } from "react";
import {Section} from "../models/Section.ts";
import api from "../axios.ts";

export function useSection(id: number) {
    const [section, setSection] = useState<Section | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        let isMounted = true;

        const fetchSection = async () => {
            try {
                const response = await api.get(`/section/${id}`);
                if (isMounted) {
                    setSection(response.data);
                }
                console.log(response.data);
            } catch (err) {
                if (isMounted) {
                    setError("Failed to fetch section");
                    setLoading(false);
                }
            }
            finally {
                setLoading(false);
            }
        };

        fetchSection();

        return () => {
            isMounted = false;
        };
    }, [id]);

    return { section, loading, error };
}

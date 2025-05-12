import {Question} from "./Question.ts";
import {Section} from "./Section.ts";

export interface Activity {
    id: number;
    title: string;
    description: string;
    section: Section;
    questions: Question[];
}
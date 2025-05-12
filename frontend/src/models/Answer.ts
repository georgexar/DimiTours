import {Question} from "./Question.ts";

export interface Answer {
    id: number;
    text: string;
    isCorrect: boolean;
    questionId: number;
    question: Question;
}
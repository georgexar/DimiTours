import {Answer} from "./Answer.ts";

export enum QuestionType {
    HISTORY,
    GEOGRAPHY,
    MYTHOLOGY,
    TRADITION,
    MUSEUMS,
    NATURE,
    ART
}

export interface Question {
    id: number;
    questionType: QuestionType;
    text: string;
    answers: Answer[];

}
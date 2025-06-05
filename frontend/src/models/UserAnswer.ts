import {Answer} from "./Answer.ts";
import {User} from "./User.ts";

export default interface UserAnswer {
    id: number;
    userId: number;
    user: User;
    answerId: number;
    answer: Answer;
    createdAt: Date;
}
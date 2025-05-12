import {MediaItem} from "./MediaItem.ts";

export interface Section {
    id: number;
    title: string;
    purpose: string;
    contentText: string;
    mediaItems: MediaItem[];
    imageUrl: string;
}
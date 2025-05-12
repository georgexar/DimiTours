export enum MediaType {
    VIDEO,
    PHOTO,
    AUDIO
}

export interface MediaItem {
    id: number;
    mediaType: MediaType;
    url: string;
}
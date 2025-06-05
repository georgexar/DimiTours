import Navbar from "../components/Navbar.tsx";
import style from './style/SectionPage.module.css';
import {useSection} from "../hooks/useSection.ts";
import {useParams} from "react-router-dom";
import {MediaItem, MediaType} from "../models/MediaItem.ts";
import Footer from "../components/Footer.tsx";

function convertToEmbedUrl(url: string): string {
    const regex = /(?:youtube\.com\/watch\?v=|youtu\.be\/)([a-zA-Z0-9_-]+)/;
    const match = url.match(regex);
    if (match && match[1]) {
        return `https://www.youtube.com/embed/${match[1]}`;
    }
    return "";
}

export default function SectionPage() {

    const {id} = useParams();
    const {section, loading} = useSection(Number(id));
    if (loading) return <div className={"loader-overlay"}><div className={"loader"}/></div>;

    if(!section) return <div className={"loader-overlay"}><div className={"loader"}/></div>;



    const buildVideo = (mediaItem: MediaItem, index: number) => {

        if(!String(mediaItem.url).includes("https:")) {
            return (
            <video key={index} controls className={style.media}>
                <source src={`/${mediaItem.url}`} type="video/mp4" />
                Your browser does not support the video tag.
            </video>
            );    
    }
        return (
            <iframe
                key={index}
                className={style.media}
                src={convertToEmbedUrl(mediaItem.url)}
                frameBorder="0"
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                allowFullScreen
            ></iframe>
        );

    }

    return (
      <>
          <Navbar active={"sections"}/>
          <div className={style.container}>
              <div className={style.section}>
                  <h1>Ενότητα {section.id}</h1>
                  <div className={style.imageContainer}>
                      <img src={`/${section.imageUrl}`} alt={"Section Image"}/>
                  </div>
                  <div className={style.headings}>
                      <h2>{section.title}</h2>
                      <a>{section.purpose}</a>
                  </div>

                  <div className={style.text}>
                      {section.contentText}
                  </div>

                  <div className={style.media}>
                      <h2>Διαθέσιμα Media</h2>
                      <div className={style.files}>
                          {section.mediaItems.length === 0 && "Δεν βρέθηκε media."}
                          {section.mediaItems !== null && section.mediaItems.map((media, index) => {
                              switch (media.mediaType) {
                                  case MediaType.PHOTO:
                                      return (
                                          <img
                                              key={index}
                                              src={`/${media.url}`}
                                              alt={`Media File ${index + 1}`}
                                              className={style.media}
                                          />
                                      );
                                  case MediaType.VIDEO:
                                    return buildVideo(media, index);
                                  case MediaType.AUDIO:
                                      return (
                                          <audio key={index} controls className={style.media}>
                                              <source src={`/${media.url}`} type="audio/mpeg" />
                                              Your browser does not support the audio element.
                                          </audio>
                                      );
                                  default:
                                      return null;
                              }
                          })}
                      </div>
                  </div>
              </div>


          </div>
          <Footer/>
      </>
    );
}
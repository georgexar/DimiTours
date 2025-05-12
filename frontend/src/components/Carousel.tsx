import {useEffect, useState} from "react";
import {AnimatePresence, motion} from "framer-motion";
import style from './Carousel.module.css';

interface CarouselProps {
    images: string[];
    interval?: number;
}

export default function Carousel({images, interval=4000}: CarouselProps) {
    const [current, setCurrent] = useState<number>(0);
    const [progressKey, setProgressKey] = useState(0)


    useEffect(()=> {
        const timer = setInterval(()=> {
            setCurrent((prev)=>(prev+1) % images.length)
            setProgressKey((prev) => prev + 1)
        }, interval)
        return () => clearInterval(timer)
    }, [images.length, interval]);

    return (
        <div className={style.carousel}>
            <AnimatePresence mode="popLayout">
                <motion.img
                    key={images[current]}
                    src={images[current]}
                    alt={`Image ${current + 1}`}
                    initial={{opacity: 0, x: 50}}
                    animate={{opacity: 1, x: 0}}
                    exit={{opacity: 0, x: -50}}
                    transition={{duration: 1.0}}
                />

                <motion.div
                    className={style.progress}
                    key={progressKey} // force re-animation on key change
                    initial={{width: '0%'}}
                    animate={{width: '300px'}}
                    transition={{duration: interval / 1000, ease: 'linear'}}
                    style={{

                        height: '4px',
                        opacity: 0.8,
                    }}
                />

            </AnimatePresence>
        </div>
    )

}

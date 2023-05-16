export default {
    // inserted: (el) => {
    //     function loadImage() {
    //         el.src = el.dataset.src;
    //     }
    //     function callback(entries, observer) {
    //         entries.forEach((entry) => {
    //             if (entry.isIntersecting) {
    //                 loadImage();
    //                 observer.unobserve(el);
    //             }
    //         });
    //     }
    //     function createIntersetionObserver() {
    //         const options = {
    //             root: null,
    //             threshold: 0,
    //         };
    //         const observer = new IntersectionObserver(callback, options);
    //         observer.observe(el);
    //     }
    //     if (!window['IntersectionObserver']) {
    //         loadImage();
    //     } else {
    //         createIntersetionObserver();
    //     }
    // },

    mounted(el, binding) {
        const options = {
            root: null,
            rootMargin: '0px',
            threshold: 0.5,
        };

        const observer = new IntersectionObserver((entries) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    const iframe = document.createElement('iframe');
                    iframe.src = binding.value;
                    iframe.setAttribute('allowfullscreen', '');
                    iframe.setAttribute('allow', 'autoplay');
                    el.appendChild(iframe);
                    observer.unobserve(el);
                }
            });
        }, options);

        observer.observe(el);
    },
};

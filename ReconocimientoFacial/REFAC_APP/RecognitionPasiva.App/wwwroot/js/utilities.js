window.trackrkc = {};

window.destroyCameraGlobal = async () => {
    if (typeof (window.trackrkc) !== "undefined" && window.trackrkc !== null && window.trackrkc) {
        if (typeof (window.trackrkc.stop) !== "undefined") {
            await window.trackrkc.stop();
        }
    }
}

window.getUserAgent = () => {
    return navigator.userAgent;
}

function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}
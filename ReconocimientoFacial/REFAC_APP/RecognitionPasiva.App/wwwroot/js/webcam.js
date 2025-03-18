function startVideoTimer(dotNetHelper, src, canvas, sensor) {
    const constraints = { video: true, audio: false };
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia(constraints)
            .then(function (stream) {
                trackrkc = stream.getTracks()[0];
                let video = document.getElementById(src);
                if ("srcObject" in video) {
                    video.srcObject = stream;
                } else {
                    video.src = window.URL.createObjectURL(stream);
                }
                video.onloadedmetadata = function (e) {
                    video.play();
                    let _canvas = document.getElementById(canvas);
                    _canvas.width = video.clientWidth;
                    _canvas.height = video.clientHeight;
                    let _sensor = document.getElementById(sensor);
                    _sensor.width = video.clientWidth;
                    _sensor.height = video.clientHeight;
                    dotNetHelper.invokeMethodAsync('StartTimer', video.clientHeight, video.clientWidth);
                };
            })
            .catch(function (err) {
                console.log(err.name + ": " + err.message);
                errorInitCamera = "ERROR INIT CAMERA";
            });
    }
}
function startVideo(dotNetHelper, src, canvas, sensor) {
    const constraints = { video: true, audio: false };
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia(constraints)
            .then(function (stream) {
                trackrkc = stream.getTracks()[0];
                let video = document.getElementById(src);
                if ("srcObject" in video) {
                    video.srcObject = stream;
                } else {
                    video.src = window.URL.createObjectURL(stream);
                }
                video.onloadedmetadata = function (e) {
                    video.play();
                    let _canvas = document.getElementById(canvas);
                    _canvas.width = video.clientWidth;
                    _canvas.height = video.clientHeight;
                    let _sensor = document.getElementById(sensor);
                    _sensor.width = video.clientWidth;
                    _sensor.height = video.clientHeight;
                    //dotNetHelper.invokeMethodAsync('StartInstruction', video.clientHeight, video.clientWidth);
                };
            })
            .catch(function (err) {
                console.log(err.name + ": " + err.message);
                errorInitCamera = "ERROR INIT CAMERA";
            });
    }
}
function startVideoCanvas(dotNetHelper, src, canvas, sensor) {
    let errorInitCamera = '';
    const constraints = { video: true, audio: false };
    //const constraints = { video: { facingMode: { exact: 'environment' } }, audio: false };
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia(constraints)
            .then(function (stream) {
                trackrkc = stream.getTracks()[0];
                let video = document.getElementById(src);
                if ("srcObject" in video) {
                    video.srcObject = stream;
                } else {
                    video.src = window.URL.createObjectURL(stream);
                }
                video.onloadedmetadata = function (e) {
                    video.play();
                    let _canvas = document.getElementById(canvas);
                    _canvas.width = video.clientWidth;
                    _canvas.height = video.clientHeight;
                    let _sensor = document.getElementById(sensor);
                    _sensor.width = video.clientWidth;
                    _sensor.height = video.clientHeight;
                    dotNetHelper.invokeMethodAsync('OnLoad');
                };
            })
            .catch(function (err) {
                console.log(err.name + ": " + err.message);
            });
    }
    return errorInitCamera;
}

function getFrame(src, dest, dotNetHelper, width, height) {
    let video = document.getElementById(src);
    if (video != null) {
        let canvas = document.getElementById(dest);
        canvas.getContext('2d').drawImage(video, 0, 0, width, height);
        let dataUrl = canvas.toDataURL("image/png");
        dotNetHelper.invokeMethodAsync('ProcessImage', dataUrl);
    }
}

function stopVideo(src) {
    let video = document.getElementById(src);
    if (video != null) {
        if ("srcObject" in video) {
            stream = video.srcObject;
        } else {
            stream = video.src;
        }
        if (stream != null) {
            const tracks = stream.getTracks();
            if (tracks != null) {
                tracks.forEach(function (track) {
                    track.stop();
                });
            }
        }
        video.srcObject = null;
        video.src = null;
    }
}
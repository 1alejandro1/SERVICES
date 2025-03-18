var $cropper;
var $imageCropper;

//seccion emtodos subir archivo
function ejecutarFile() {
    document.getElementById('fileCargarCI').click();
}

function subirFotografia() {
    $imageCropper = document.getElementById('panelCI');
    var input_img = document.getElementById('fileCargarCI').files[0];
    var reader_img = new FileReader();
    reader_img.onload = function () {
        destroyCapture();
        var dataURL = reader_img.result;
        var output_img = document.getElementById('panelCI');
        let imgSize = document.getElementById('hTamanioImagen');
        imgSize.value = (output_img.size / 1024);
        output_img.src = dataURL;
        $cropper = new Cropper(output_img, {
            dragMode: 'move',
            autoCrop: true,
            guides: true,
            modal: true,
            zoomable: true,
            cropBoxMovable: false,
            cropBoxResizable: true
        });
        input_img.value = null;
    };
    reader_img.readAsDataURL(input_img);
}

function rotarImagen(sentido) {
    if (sentido == 'L') {
        $cropper.rotate(-90);
    }

    else if (sentido == 'R') {
        $cropper.rotate(90);
    }
    cropDocument();
}

function cropDocument() {
    const cropper_container = document.getElementsByClassName("cropper-container")[0];
    const canvas_data = $cropper.getCanvasData();
    let data;
    if (canvas_data.width > canvas_data.height) {
        data = {
            left: cropper_container.clientWidth / 2 - (canvas_data.width * 0.40),
            top: cropper_container.clientHeight / 2 - (canvas_data.height * 0.45),
            width: canvas_data.width > cropper_container.clientWidth ? cropper_container.clientWidth : canvas_data.width - (canvas_data.width * 0.20),
            height: canvas_data.height > cropper_container.clientHeight ? cropper_container.clientHeight : canvas_data.height - (canvas_data.height * 0.10)
        };
    } else {
        data = {
            left: cropper_container.clientWidth / 2 - (canvas_data.width * 0.45),
            top: cropper_container.clientHeight / 2 - (canvas_data.height * 0.40),
            width: canvas_data.width > cropper_container.clientWidth ? cropper_container.clientWidth : canvas_data.width - (canvas_data.width * 0.10),
            height: canvas_data.height > cropper_container.clientHeight ? cropper_container.clientHeight : canvas_data.height - (canvas_data.height * 0.20)
        };
    }

    $cropper.setCropBoxData(data);
}

function guardarImagen(dotnetHelper) {
    //if (document.getElementById('hTamanioImagen').value > 2000) {
    //    var canvasData = $cropper.getCroppedCanvas({
    //        maxWidth: 1200
    //    });
    //} else {
    //    var canvasData = $cropper.getCroppedCanvas();        
    //}


    //var canvasData = $cropper.getCroppedCanvas({ maxWidth: 1200 });
    var image = canvasData.toDataURL('image/jpeg')
    dotnetHelper.invokeMethodAsync('ProcesarImagen', image).then((res) => {
    });
    destroyCapture();
}

function destroyCapture() {
    if ($cropper) {
        $cropper.destroy();
        document.getElementById('panelCI').src = null;
    }
}

//metodos capturar fotografia
var cameraView;
var canvas_cropper;

function getPointsFromVideo(camera) {
    var cameraView = document.getElementById(camera);
    //            sx    sy   sw  sh  dw  dh
    var puntos = [0.0, 0.0, 0.0, 0.0, 0.0, 0.0];

    var sxmax = 0.25;
    var symax = 0.225;
    var swmax = 0.50;
    var shmax = 0.55;
    var dwmax = 0.50;
    var dhmax = 0.55;

    var sxmin = 0.35;
    var symin = 0.175;
    var swmin = 0.30;
    var shmin = 0.65;
    var dwmin = 0.30;
    var dhmin = 0.65;

    if (cameraView.clientWidth > cameraView.clientHeight) {
        puntos[0] = (cameraView.clientWidth / 2) - (cameraView.clientWidth * sxmax);
        puntos[1] = (cameraView.clientHeight / 2) - (cameraView.clientHeight * symax);
        puntos[2] = cameraView.clientWidth - (cameraView.clientWidth * swmax);
        puntos[3] = cameraView.clientHeight - (cameraView.clientHeight * shmax);
        puntos[4] = cameraView.clientWidth - (cameraView.clientWidth * dwmax);
        puntos[5] = cameraView.clientHeight - (cameraView.clientHeight * dhmax);
    }
    else {
        puntos[0] = cameraView.clientWidth / 2 - (cameraView.clientWidth * sxmin);
        puntos[1] = cameraView.clientHeight / 2 - (cameraView.clientHeight * symin);
        puntos[2] = cameraView.clientWidth - (cameraView.clientWidth * swmin);
        puntos[3] = cameraView.clientHeight - (cameraView.clientHeight * shmin);
        puntos[4] = cameraView.clientWidth - (cameraView.clientWidth * dwmin);
        puntos[5] = cameraView.clientHeight - (cameraView.clientHeight * dhmin);
    }
    return puntos;
}

function drawSquareToVideo(camera, canvas) {
    var canvas_cropper = document.getElementById(canvas);
    const ctx = canvas_cropper.getContext('2d');
    ctx.beginPath();
    let region = new Path2D();
    var points = getPointsFromVideo(camera);
    region.rect(points[0], points[1], points[2], points[3]);
    region.rect(0, 0, canvas_cropper.width, canvas_cropper.height);
    ctx.clip(region, "evenodd");
    //ctx.fillStyle = '#1f2227a3';
    ctx.fillRect(0, 0, canvas_cropper.width, canvas_cropper.height);
    //ctx.lineWidth = "5";
    //ctx.strokeStyle = "#FFFFFF";
    ctx.rect(points[0], points[1], points[2], points[3]);
    ctx.stroke();
}


function captureDocumentFromVideo(dotnetHelper, camera) {
    //var points = getPointsFromVideo(camera);
    //console.log(points)

    var _camera = document.getElementById(camera);
    console.log(_camera.clientWidth);
    console.log(_camera.clientHeight);
    var sensor = document.getElementById("canvas_sensor");
    sensor.getContext('2d').drawImage(_camera, 0, 0, _camera.clientWidth, _camera.clientHeight);
    console.log(sensor.toDataURL('image/jpeg'));
    dotnetHelper.invokeMethodAsync('ProcessImagePasiva', sensor.toDataURL('image/jpeg'));

}

function guardarFotografia(dotnetHelper) {
    var canvas_document = document.getElementById("sensor_document");
    var image = canvas_document.toDataURL('image/jpeg')
    dotnetHelper.invokeMethodAsync('ProcesarImagen', image).then((res) => {
    });
}
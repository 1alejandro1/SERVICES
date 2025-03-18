function DrawCanvasEllipse(src, left, width, top, height) {
    let canvas = document.getElementById(src);
    const ctx = canvas.getContext('2d');
    let region = new Path2D();
    region.ellipse(left + (width / 2),
        top + (height / 2),
        width / 2,
        height / 2,
        0,
        0,
        2 * Math.PI);
    region.rect(0, 0, canvas.width, canvas.height);
    ctx.clip(region, "evenodd");
    ctx.fillStyle = '#000000a8';
    ctx.fillRect(0, 0, canvas.width, canvas.height);

}

function DrawCanvasRectangle(src, left, width, top, height) {
    let canvas = document.getElementById(src);
    const ctx = canvas.getContext('2d');
    ctx.beginPath();
    ctx.lineWidth = "1";
    ctx.strokeStyle = '#26DE81';
    ctx.rect(left, top, width, height);
    ctx.stroke();
    ctx.fillStyle = '#26DE81';
    ctx.fillRect(left, top, width, height);

}
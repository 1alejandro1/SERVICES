function GenerarExcel(data, title) {
    var wb = XLSX.utils.book_new();
    wb.Props = {
        Title: "-",
        Subject: "-",
        Author: "-",
        CreatedDate: new Date()
    };
    wb.SheetNames.push("Reporte");
    var ws = XLSX.utils.json_to_sheet(data);
    wb.Sheets["Reporte"] = ws;
    var wbout = XLSX.write(wb, { bookType: 'xlsx', type: 'binary' });
    saveAs(new Blob([DownloadFile(wbout)], { type: "application/octet-stream" }), title);
}
function DownloadFile(s) {
    var buf = new ArrayBuffer(s.length); //convert s to arrayBuffer
    var view = new Uint8Array(buf);  //create uint8array as viewer
    for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF; //convert to octet
    return buf;
}
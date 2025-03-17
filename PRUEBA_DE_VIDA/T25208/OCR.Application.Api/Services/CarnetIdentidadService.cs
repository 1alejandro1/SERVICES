using BCP.CROSS.LOGGER;
using BCP.CROSS.SECRYPT;
using ClosedXML.Excel;
using FuzzySharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using OCR.Application.Api.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OCR.Application.Api.Services
{
    public interface ICarnetIdentidadService
    {
        Task<ValidarCarnetIdentidadModel> Validar(string operacion, string tipo, string image);
        Task<ValidarCIModel> ValidarCI(string operacion, string tipo, string image);
        
    }
    public class CarnetIdentidadService : ICarnetIdentidadService
    {
        private readonly IManagerSecrypt secrypt;
        private readonly ILogger logger;
        private readonly AzureCredentialsConfig azureCredentials;
        private readonly FlagConfig flag;
        static string base64String = null;
        public int count = 0;
        public CarnetIdentidadService(IConfiguration configuration, IManagerSecrypt secrypt, ILogger logger)
        {
            this.secrypt = secrypt;
            this.logger = logger;
            azureCredentials = new AzureCredentialsConfig();
            configuration.GetSection("AzureCredentials").Bind(azureCredentials);
            azureCredentials.Key = this.secrypt.Desencriptar(azureCredentials.Key);

            flag = new FlagConfig();
            configuration.GetSection("Flag").Bind(flag);
        }
        public async Task<ValidarCarnetIdentidadModel> Validar(string operacion, string tipo, string image)
        {
            ValidarCarnetIdentidadModel model = new ValidarCarnetIdentidadModel();
            switch (tipo)
            {
                case "ANVERSO":
                    model = await Anverso_(operacion, image);
                    break;
                case "REVERSO":
                    model = await Reverso1(operacion, image);
                    break;
                default:
                    break;
            }
            return model;
        }
        public async Task<ValidarCIModel> ValidarCI(string operacion, string tipo, string image)
        {
            ValidarCIModel model = new ValidarCIModel();
            switch (tipo)
            {
                case "ANVERSO":
                    model = await AnversoCI(operacion, image);
                    break;
                case "REVERSO":
                    model = await ReversoCI(operacion, image);
                    break;
                default:
                    break;
            }
            return model;
        }
       
        public static string ImageToBase64(string tifpath)
        {
            var bytes = Convert.FromBase64String(tifpath);
            using var contents = new MemoryStream(bytes);
            Image image = Image.FromStream(contents);
          
                    image.Save(contents, ImageFormat.Jpeg);
                    byte[] imageBytes = contents.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;              
            
        }
          
        public IActionResult DownloadExcel()
        {
            List<Author> authors = new List<Author>
         {
             new Author { Id = 1, FirstName = "Joydip", LastName = "Kanjilal" },
             new Author { Id = 2, FirstName = "Steve", LastName = "Smith" },
             new Author { Id = 3, FirstName = "Anand", LastName = "Narayaswamy"}
         };
            //required using ClosedXML.Excel;
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "document.xlsx";
           
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Authors");
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "FirstName";
                    worksheet.Cell(1, 3).Value = "LastName";
                    for (int index = 1; index <= authors.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = authors[index - 1].Id;
                        worksheet.Cell(index + 1, 2).Value = authors[index - 1].FirstName;
                        worksheet.Cell(index + 1, 3).Value = authors[index - 1].LastName;
                    }
                    //required using System.IO;
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();                   
                    return new FileContentResult(content, contentType);
                }
                }
            
        }
        private async Task<ValidarCarnetIdentidadModel> Anverso_(string operacion, string image)
        {

            ValidarCarnetIdentidadModel model = new ValidarCarnetIdentidadModel();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {    
                var ocrResults = await GetTextAzureAsync1(image);
                var textUrlFileResults = ocrResults.AnalyzeResult.ReadResults;
                foreach (ReadResult page in textUrlFileResults)
                {
                    string _line = string.Empty;
                    foreach (Line line in page.Lines)
                    {
                        string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                        string textoSinAcentos = reg.Replace(textoNormalizado, "");
                        _line += textoSinAcentos + " ";
                    }
                    lstText.Add(_line.TrimEnd().ToUpper());
                }
                model.texto = lstText;
                logger.Debug($"[{operacion}] - {string.Join("|", lstText)}");
                model.operacion = operacion;
                model.image = image;
                if (lstText.Count > 0)
                {
                    if (lstText.Count(x => x.Contains("FIRMA")) > 0)
                    {
                        if (lstText.Count(x => x.Contains("CEDULA DE IDENTIDAD")) > 0)
                        {
                            model.valido = true;
                        }
                    }
                    if (model.valido)
                    {
                        List<string> list = new List<string>();
                        foreach (var item in lstText)
                        {
                            list = item.Split(' ').ToList();
                        }
                        int flag = 0;
                        foreach (var item in list)
                        {                            
                            if(item.Equals("NO") || item.Equals("N0") || item.Equals("NA") || item.Equals("N"))
                            {
                                flag ++;                               
                            }
                            if (Regex.IsMatch(item, @"^[0-9]+$") && item.Length > 5 && flag == 1)
                            {
                                model.idc = item;
                                flag = 0;
                            }

                        }

                        if ((lstText.Count(x => x.Contains("INDEFINIDO")) > 0))
                        {
                            model.fechaVencimiento = "01/01/9999";
                        }
                        else
                        {
                            DateTime fecha = FechaVencimiento(string.Join("", lstText));
                            model.fechaVencimiento = fecha.ToString("dd/MM/yyyy");
                        }
                    }
                }
                return model;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                logger.Error("Error: " + ex);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                // Its a some other issue
                logger.Error("Error: " + ex);
                throw;
            }
        }
        private async Task<ValidarCIModel> AnversoCI(string operacion, string image)
        {

            ValidarCIModel model = new ValidarCIModel();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {
                var ocrResults = await GetTextAzureAsync1(image);
                var textUrlFileResults = ocrResults.AnalyzeResult.ReadResults;
                foreach (ReadResult page in textUrlFileResults)
                {
                    string _line = string.Empty;
                    foreach (Line line in page.Lines)
                    {
                            string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                            _line += line.Text + " "; 
                    }
                    lstText.Add(_line.TrimEnd().ToUpper());
                }
                model.texto = lstText;                
                model.operacion = operacion;
                
                return model;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                logger.Error("Error: " + ex);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                // Its a some other issue
                logger.Error("Error: " + ex);
                throw;
            }
        }
        private async Task<ValidarCarnetIdentidadModel> Anverso(string operacion, string image)
        {

            ValidarCarnetIdentidadModel model = new ValidarCarnetIdentidadModel();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {
                Bitmap source = Base64StringToBitmap(image);
                int alto = source.Height;
                int ancho = source.Width;

                int xCI = (ancho * 7) / 100;
                int yCI = (alto * 53) / 100;
                int wCI = (ancho * 32) / 100;
                int hCI = (alto * 20) / 100;

                var ocrResults = await GetTextAzureAsync(image);
                foreach (var region in ocrResults.Regions)
                {
                    foreach (var line in region.Lines)
                    {
                        string _line = string.Empty;
                        foreach (var word in line.Words)
                        {
                            string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                            _line += textoSinAcentos + " ";
                        }
                        lstText.Add(_line.TrimEnd().ToUpper());
                    }
                }
                model.texto = lstText;
                logger.Debug($"[{operacion}] - {string.Join("|", lstText)}");
                model.operacion = operacion;
                var _image = DrawImageAzure(image, ocrResults);
                model.image = Convert.ToBase64String(_image);
                if (lstText.Count > 0)
                {
                    if (lstText.Count(x => x.Contains("FIRMA")) > 0)
                    {
                        if (lstText.Count(x => x.Contains("CEDULA DE IDENTIDAD")) > 0)
                        {
                            model.valido = true;
                        }
                    }
                    if (model.valido)
                    {
                        string lstTextIDC = String.Empty;

                        Bitmap source2 = Base64StringToBitmap(image);
                        Rectangle rectOrig2 = new Rectangle(xCI, yCI, wCI, hCI);
                        Bitmap CroppedImage2 = CropImage(source2, rectOrig2);
                        var rectangle2 = BitmapToBase64String(CroppedImage2);
                        var ocrResults2 = await GetTextAzureAsync(rectangle2);

                        foreach (var region in ocrResults2.Regions)
                        {
                            foreach (var line in region.Lines)
                            {
                                string _line = string.Empty;
                                foreach (var word in line.Words)
                                {
                                    string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                    _line += textoSinAcentos + " ";
                                }
                                lstTextIDC = _line.TrimEnd().TrimStart().ToUpper();
                                lstTextIDC = lstTextIDC.Replace("NO", "");
                                lstTextIDC = lstTextIDC.Replace("N", "");
                                lstTextIDC = lstTextIDC.Replace("NA", "");
                                lstTextIDC = lstTextIDC.Replace(" ", "");
                            }
                        }
                        model.idc = lstTextIDC;

                        if ((lstText.Count(x => x.Contains("INDEFINIDO")) > 0))
                        {
                            model.fechaVencimiento = "01/01/9999";
                        }
                        else
                        {
                            DateTime fecha = FechaVencimiento(string.Join("", lstText));
                            model.fechaVencimiento = fecha.ToString("dd/MM/yyyy");
                        }
                    }
                }
                return model;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                logger.Error("Error: " + ex);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                // Its a some other issue
                logger.Error("Error: " + ex);
                throw;
            }
        }
       
        private async Task<ValidarCarnetIdentidadModel> Anverso1(string operacion, string image)
        {

            ValidarCarnetIdentidadModel model = new ValidarCarnetIdentidadModel();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");

            try
            {
                if(flag.Resize == "ON")
                {
                    //************Resize Image**************
                    byte[] byteBuffer = Convert.FromBase64String(image);
                    var ResizeImg = ResizeImage(byteBuffer);
                    image = ResizeImg;
                    //************Fin Resize Image**************
                    
                    Bitmap source = Base64StringToBitmap(image);
                    //Rectangle rectOrig = new Rectangle(250, 35, 2400, 400);
                    Rectangle rectOrig = new Rectangle(200, 1000, 2300, 470);
                    Bitmap CroppedImage = CropImage(source, rectOrig);                    
                    var rectangle = BitmapToBase64String(CroppedImage);
                    var ocrResults = await GetTextAzureAsync(rectangle);

                    foreach (var region in ocrResults.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            string _line = string.Empty;
                            foreach (var word in line.Words)
                            {
                                string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                _line += textoSinAcentos + " ";
                            }
                            lstText.Add(_line.TrimEnd().ToUpper());
                        } 
                    }

                    Bitmap source3 = Base64StringToBitmap(image);
                    //Rectangle rectOrig3 = new Rectangle(400, 1000, 2050, 470);
                    Rectangle rectOrig3 = new Rectangle(200, 1000, 2300, 470);
                    Bitmap CroppedImage3 = CropImage(source3, rectOrig3);
                    var rectangle3 = BitmapToBase64String(CroppedImage3);
                    var ocrResults3 = await GetTextAzureAsync(rectangle3);

                    foreach (var region in ocrResults3.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            string _line = string.Empty;
                            foreach (var word in line.Words)
                            {
                                string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                _line += textoSinAcentos + " ";
                            }
                            lstText.Add(_line.TrimEnd().ToUpper());
                        }
                    }                    
                    

                    Bitmap source4 = Base64StringToBitmap(image);
                    Rectangle rectOrig4 = new Rectangle(500, 1400, 1800, 450);
                    Bitmap CroppedImage4 = CropImage(source4, rectOrig4);
                    var rectangle4 = BitmapToBase64String(CroppedImage4);
                    var ocrResults4 = await GetTextAzureAsync(rectangle4);

                    foreach (var region in ocrResults4.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            string _line = string.Empty;
                            foreach (var word in line.Words)
                            {
                                string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                _line += textoSinAcentos + " ";
                            }
                            lstText.Add(_line.TrimEnd().ToUpper());
                        }
                    }

                    model.texto = lstText;
                    logger.Debug($"[{operacion}] - {string.Join("|", lstText)}");
                    model.operacion = operacion;
                    var _image = DrawImageAzure(image, ocrResults);
                    model.image = Convert.ToBase64String(_image);                  
                    if (lstText.Count > 0)
                    {
                        if (lstText.Count(x => x.Contains("FIRMA")) > 0)
                        {
                            if (lstText.Count(x => x.Contains("CEDULA DE IDENTIDAD")) > 0)
                            {
                                model.valido = true;
                            }
                        }
                        if (model.valido)
                        {
                            string lstTextIDC = String.Empty;                      

                            Bitmap source2 = Base64StringToBitmap(image);
                            Rectangle rectOrig2 = new Rectangle(200, 970, 900, 300);
                            Bitmap CroppedImage2 = CropImage(source2, rectOrig2);
                            var rectangle2 = BitmapToBase64String(CroppedImage2);
                            var ocrResults2 = await GetTextAzureAsync(rectangle2);

                            foreach (var region in ocrResults2.Regions)
                            {
                                foreach (var line in region.Lines)
                                {
                                    string _line = string.Empty;
                                    foreach (var word in line.Words)
                                    {
                                        string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                        string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                        _line += textoSinAcentos + " ";
                                    }
                                    lstTextIDC= _line.TrimEnd().TrimStart().ToUpper();
                                    lstTextIDC = lstTextIDC.Replace("NO", "");
                                    lstTextIDC = lstTextIDC.Replace("N", "");
                                    lstTextIDC = lstTextIDC.Replace("NA", "");
                                    lstTextIDC = lstTextIDC.Replace(" ", "");
                                }
                            }
                            //model.idc = Regex.Match(lstTextIDC.ToString(), @"(\d+)").Value;
                            model.idc = lstTextIDC;

                            if ((lstText.Count(x => x.Contains("INDEFINIDO")) > 0))
                            {
                                model.fechaVencimiento = "01/01/9999";
                            }
                            else
                            {     
                                DateTime fecha = FechaVencimiento(string.Join("", lstText));
                                model.fechaVencimiento = fecha.ToString("dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            string lstTextIDC = String.Empty;

                            Bitmap source2 = Base64StringToBitmap(image);
                            Rectangle rectOrig2 = new Rectangle(200, 970, 900, 300);
                            Bitmap CroppedImage2 = CropImage(source2, rectOrig2);
                            var rectangle2 = BitmapToBase64String(CroppedImage2);
                            var ocrResults2 = await GetTextAzureAsync(rectangle2);

                            foreach (var region in ocrResults2.Regions)
                            {
                                foreach (var line in region.Lines)
                                {
                                    string _line = string.Empty;
                                    foreach (var word in line.Words)
                                    {
                                        string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                        string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                        _line += textoSinAcentos + " ";
                                    }
                                    lstTextIDC = _line.TrimEnd().TrimStart().ToUpper();
                                    lstTextIDC = lstTextIDC.Replace("NO", "");
                                    lstTextIDC = lstTextIDC.Replace(" ", "");
                                }
                            }
                            //model.idc = Regex.Match(lstTextIDC.ToString(), @"(\d+)").Value;
                            model.idc = lstTextIDC;
                        }
                    }
                    return model;
                }
                else
                {                   

                    Bitmap source = Base64StringToBitmap(image);
                    int alto = source.Height;
                    int ancho = source.Width;

                    int x = (ancho * 9) / 100;
                    int y = (alto * 2) / 100;
                    int w = (ancho * 86) / 100;
                    int h = (alto * 26) / 100;

                    int x2 = (ancho * 7) / 100;
                    int y2 = (alto * 55) / 100;
                    int w2 = (ancho * 83) / 100;
                    int h2 = (alto * 26) / 100;                    

                    int x3 = (ancho * 18) / 100;
                    int y3 = (alto * 77) / 100;
                    int w3 = (ancho * 64) / 100;
                    int h3 = (alto * 25) / 100;

                    int xCI = (ancho * 7) / 100;
                    int yCI = (alto * 53) / 100;
                    int wCI = (ancho * 32) / 100;
                    int hCI = (alto * 20) / 100;

                    Rectangle rectOrig = new Rectangle(x, y, w, h);
                    Bitmap CroppedImage = CropImage(source, rectOrig);
                    var rectangle = BitmapToBase64String(CroppedImage);
                    var ocrResults = await GetTextAzureAsync(rectangle);

                    foreach (var region in ocrResults.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            string _line = string.Empty;
                            foreach (var word in line.Words)
                            {
                                string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                _line += textoSinAcentos + " ";
                            }
                            lstText.Add(_line.TrimEnd().ToUpper());
                        }
                    }

                    Rectangle rectOrig3 = new Rectangle(x2, y2, w2, h2);
                    Bitmap CroppedImage3 = CropImage(source, rectOrig3);
                    var rectangle3 = BitmapToBase64String(CroppedImage3);
                    var ocrResults3 = await GetTextAzureAsync(rectangle3);

                    foreach (var region in ocrResults3.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            string _line = string.Empty;
                            foreach (var word in line.Words)
                            {
                                string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                _line += textoSinAcentos + " ";
                            }
                            lstText.Add(_line.TrimEnd().ToUpper());
                        }
                    }


                    Rectangle rectOrig4 = new Rectangle(x3, y3, w3, h3);
                    Bitmap CroppedImage4 = CropImage(source, rectOrig4);
                    var rectangle4 = BitmapToBase64String(CroppedImage4);
                    var ocrResults4 = await GetTextAzureAsync(rectangle4);

                    foreach (var region in ocrResults4.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            string _line = string.Empty;
                            foreach (var word in line.Words)
                            {
                                string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                _line += textoSinAcentos + " ";
                            }
                            lstText.Add(_line.TrimEnd().ToUpper());
                        }
                    }

                    model.texto = lstText;
                    logger.Debug($"[{operacion}] - {string.Join("|", lstText)}");
                    model.operacion = operacion;
                    var _image = DrawImageAzure(image, ocrResults);
                    model.image = Convert.ToBase64String(_image);
                    if (lstText.Count > 0)
                    {
                        if (lstText.Count(x => x.Contains("FIRMA")) > 0)
                        {
                            if (lstText.Count(x => x.Contains("CEDULA DE IDENTIDAD")) > 0)
                            {
                                model.valido = true;
                            }
                        }
                        if (model.valido)
                        {
                            string lstTextIDC = String.Empty;

                            Rectangle rectOrig2 = new Rectangle(xCI, yCI, wCI, hCI);
                            Bitmap CroppedImage2 = CropImage(source, rectOrig2);
                            var rectangle2 = BitmapToBase64String(CroppedImage2);
                            var ocrResults2 = await GetTextAzureAsync(rectangle2);

                            foreach (var region in ocrResults2.Regions)
                            {
                                foreach (var line in region.Lines)
                                {
                                    string _line = string.Empty;
                                    foreach (var word in line.Words)
                                    {
                                        string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                        string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                        _line += textoSinAcentos + " ";
                                    }
                                    lstTextIDC = _line.TrimEnd().TrimStart().ToUpper();
                                    lstTextIDC = lstTextIDC.Replace("NO", "");
                                    lstTextIDC = lstTextIDC.Replace(" ", "");
                                }
                            }
                            model.idc = lstTextIDC;

                            if ((lstText.Count(x => x.Contains("INDEFINIDO")) > 0))
                            {
                                model.fechaVencimiento = "01/01/9999";
                            }
                            else
                            {
                                DateTime fecha = FechaVencimiento(string.Join("", lstText));
                                model.fechaVencimiento = fecha.ToString("dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            string lstTextIDC = String.Empty;

                            Bitmap source2 = Base64StringToBitmap(image);
                            Rectangle rectOrig2 = new Rectangle(200, 970, 900, 300);
                            Bitmap CroppedImage2 = CropImage(source2, rectOrig2);
                            var rectangle2 = BitmapToBase64String(CroppedImage2);
                            var ocrResults2 = await GetTextAzureAsync(rectangle2);

                            foreach (var region in ocrResults2.Regions)
                            {
                                foreach (var line in region.Lines)
                                {
                                    string _line = string.Empty;
                                    foreach (var word in line.Words)
                                    {
                                        string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                                        string textoSinAcentos = reg.Replace(textoNormalizado, "");
                                        _line += textoSinAcentos + " ";
                                    }
                                    lstTextIDC = _line.TrimEnd().TrimStart().ToUpper();
                                    lstTextIDC = lstTextIDC.Replace("NO", "");
                                    lstTextIDC = lstTextIDC.Replace(" ", "");
                                }
                            }
                            model.idc = lstTextIDC;
                        }
                    }
                    return model;
                }
                


            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                logger.Error("Error: "+ex);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                // Its a some other issue
                logger.Error("Error: " + ex);
                throw;
            }
        }
        private async Task<ValidarCarnetIdentidadModel> Reverso(string operacion, string image)
        {
            ValidarCarnetIdentidadModel model = new ValidarCarnetIdentidadModel();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {
                var ocrResults = await GetTextAzureAsync(image);
                foreach (var region in ocrResults.Regions)
                {
                    foreach (var line in region.Lines)
                    {
                        string _line = string.Empty;
                        foreach (var word in line.Words)
                        {
                            string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                            _line += textoSinAcentos + " ";
                        }
                        lstText.Add(_line.TrimEnd().ToUpper());
                    }
                }
                model.texto = lstText;
                logger.Debug($"[{operacion}] - {string.Join("|", lstText)}");
                model.operacion = operacion;
                var _image = DrawImageAzure(image, ocrResults);
                model.image = Convert.ToBase64String(_image);
                if (lstText.Count > 0)
                {
                    if (lstText.Count(x => x.Contains("EL SERVICIO GENERAL DE IDENTIFICACION PERSONAL")) > 0)
                    {
                        if (lstText.Count(x => x.Contains("CERTIFICA: QUE LA FIRMA, FOTOGRAFIA")) > 0)
                        {
                            if (lstText.Count(x => x.Contains("E IMPRESION PERTENECE")) > 0)
                            {
                                model.valido = true;
                            }
                        }
                    }
                    if (model.valido)
                    {
                        DateTime fecha = FechaVencimiento(string.Join("", lstText));
                        model.fechaVencimiento = fecha.ToString("dd/MM/yyyy");
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex);
                throw;
            }
        }
        private async Task<ValidarCarnetIdentidadModel> Reverso1(string operacion, string image)
        {
            ValidarCarnetIdentidadModel model = new ValidarCarnetIdentidadModel();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {
                var ocrResults = await GetTextAzureAsync1(image);
                var textUrlFileResults = ocrResults.AnalyzeResult.ReadResults;
                foreach (ReadResult page in textUrlFileResults)
                {
                    string _line = string.Empty;
                    foreach (Line line in page.Lines)
                    {
                        string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                        string textoSinAcentos = reg.Replace(textoNormalizado, "");
                        _line += textoSinAcentos + " ";
                    }
                    lstText.Add(_line.TrimEnd().ToUpper());
                }              
                model.texto = lstText;
                logger.Debug($"[{operacion}] - {string.Join("|", lstText)}");
                model.operacion = operacion;              
                model.image = image;
                if (lstText.Count > 0)
                {
                    if (lstText.Count(x => x.Contains("EL SERVICIO GENERAL DE IDENTIFICACION PERSONAL")) > 0)
                    {
                        if (lstText.Count(x => x.Contains("CERTIFICA QUE LA FIRMA FOTOGRAFIA")) > 0)
                        {
                            if (lstText.Count(x => x.Contains("E IMPRESION PERTENECE")) > 0)
                            {
                                model.valido = true;
                            }
                        }
                    }
                    if (model.valido)
                    {
                        List<string> list = new List<string>();
                        foreach (var item in lstText)
                        {
                            list = item.Split(' ').ToList();
                        }
                        int flag = 0;
                        foreach (var item in list)
                        {
                            if (item.Equals("CN") || item.Equals("C") || item.Equals("N"))
                            {
                                flag++;
                            }
                            if (Regex.IsMatch(item, @"^[0-9]+$") && item.Length > 5 && flag == 1)
                            {
                                model.idc = item;
                                flag = 0;
                            }

                        }

                        DateTime fecha = FechaVencimiento(string.Join("", lstText));
                        model.fechaVencimiento = fecha.ToString("dd/MM/yyyy");
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex);
                throw;
            }
        }
        private async Task<ValidarCIModel> ReversoCI(string operacion, string image)
        {
            ValidarCIModel model = new ValidarCIModel();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {
                var ocrResults = await GetTextAzureAsync1(image);
                var textUrlFileResults = ocrResults.AnalyzeResult.ReadResults;
                foreach (ReadResult page in textUrlFileResults)
                {
                    string _line = string.Empty;
                    foreach (Line line in page.Lines)
                    {
                            string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                            _line += line.Text + " ";
                    }
                    lstText.Add(_line.TrimEnd().ToUpper());
                }
                model.texto = lstText;
                logger.Debug($"[{operacion}] - {string.Join("|", lstText)}");
                model.operacion = operacion;
               
                return model;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex);
                throw;
            }
        }
        private async Task<ReadOperationResult> GetTextAzureAsync1(string imageData)
        {           

            try
            {
                ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(azureCredentials.Key);
                var cvClient = new ComputerVisionClient(credentials)
                {
                    Endpoint = azureCredentials.EndPoint
                };
                if (flag.FilterBW == "ON")
                {
                    return null;
                }
                else
                {
                    var bytes = Convert.FromBase64String(imageData);
                    using var contents = new MemoryStream(bytes);
                    var textHeaders = await cvClient.ReadInStreamAsync(contents);
                    string operationLocation = textHeaders.OperationLocation;
                    const int numberOfCharsInOperationId = 36;
                    string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);
                    ReadOperationResult results;
                    do
                    {
                        results = await cvClient.GetReadResultAsync(Guid.Parse(operationId));
                    }
                    while ((results.Status == OperationStatusCodes.Running || results.Status ==  OperationStatusCodes.NotStarted));
                    return results;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex);
                throw;
            }
        }
        private async Task<OcrResult> GetTextAzureAsync(string imageData)
        {            
            try
            {
                ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(azureCredentials.Key);
                var cvClient = new ComputerVisionClient(credentials)
                {
                    Endpoint = azureCredentials.EndPoint
                };
                if(flag.FilterBW == "ON")
                {
                    //var _imageData = ConvertBlackAndWhite(imageData, 150);
                    var _imageData = ToGrayScale(imageData);
                    var bytes = Convert.FromBase64String(_imageData);
                    using var contents = new MemoryStream(bytes);
                                                       
                    var ocrResults = await cvClient.RecognizePrintedTextInStreamAsync(detectOrientation: true, image: contents, language: OcrLanguages.Es, modelVersion: "latest");

                    return ocrResults;
                }
                else
                {
                    var bytes = Convert.FromBase64String(imageData);
                    using var contents = new MemoryStream(bytes);

                    var ocrResults = await cvClient.RecognizePrintedTextInStreamAsync(detectOrientation: true, image: contents, language: OcrLanguages.Es, modelVersion: "latest");
                    return ocrResults;
                }                
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex);
                throw;
            }
        }       
        private string ToGrayScale(string image)
        {
            Bitmap Bmp = Base64StringToBitmap(image);
            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)Math.Round(.299 * c.R + .587 * c.G + .114 * c.B);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return BitmapToBase64String(Bmp);
        }
        private string ConvertBlackAndWhite(string image, int umb)
        {
            if (umb == 0)
            {
                return image;
            }
            else
            {
                Bitmap source = Base64StringToBitmap(image);
                Bitmap target = new Bitmap(source.Width, source.Height, source.PixelFormat);
                for (int i = 0; i < source.Width; i++)
                {
                    for (int e = 0; e < source.Height; e++)
                    {
                        Color col = source.GetPixel(i, e);
                        byte gray = (byte)(col.R * 0.3f + col.G * 0.59f + col.B * 0.11f);
                        byte value = 0;
                        if (gray > umb)
                        {
                            value = 255;
                        }
                        Color newColor = Color.FromArgb(value, value, value);
                        target.SetPixel(i, e, newColor);
                    }
                }
                return BitmapToBase64String(target);
            }
        }
        private Bitmap Base64StringToBitmap(string base64String)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            using (MemoryStream memoryStream = new MemoryStream(byteBuffer))
            {
                memoryStream.Position = 0;
                Bitmap bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);
                memoryStream.Close();
                return bmpReturn;
            }
        }
        private string BitmapToBase64String(Bitmap bImage)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bImage.Save(ms, ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();
                return Convert.ToBase64String(byteImage);
            }
        }
        private byte[] DrawImageAzure(string imageData, OcrResult ocrResults)
        {
            try
            {
                var bytes = Convert.FromBase64String(imageData);
                using var contents = new MemoryStream(bytes);
                Image image = Image.FromStream(contents);
                Graphics graphics = Graphics.FromImage(image);
                Pen pen = new Pen(Color.Green, 5);
                foreach (var region in ocrResults.Regions)
                {
                    foreach (var line in region.Lines)
                    {
                        int[] dims = line.BoundingBox.Split(",").Select(int.Parse).ToArray();
                        Rectangle rect = new Rectangle(dims[0], dims[1], dims[2], dims[3]);
                        graphics.DrawRectangle(pen, rect);
                    }
                }
                using MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex);
                throw;
            }
        }
        private DateTime FechaVencimiento(string texto)
        {
            try
            {
                List<string> lstDatos = new List<string>();
                string[] keys = new string[] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
                Tuple<int, string> first = null;
                Tuple<int, string> second = null;
                foreach (var item in keys)
                {
                    int i = texto.IndexOf(item);
                    if (i != -1)
                    {
                        first = new Tuple<int, string>(i, item);
                        string temp = texto.Substring(i + item.Length);
                        i = temp.IndexOf(item);
                        if (i != -1)
                        {
                            second = new Tuple<int, string>(i + first.Item1 + item.Length, item);
                        }
                        break;
                    }
                }
                if (first != null)
                {
                    lstDatos.Add(texto.Substring(first.Item1 - 10));
                }
                if (second != null)
                {
                    lstDatos.Add(texto.Substring(second.Item1 - 10));
                }
                string[] _replace_1 = { " DE " };
                string dia = string.Empty;
                int mes = 0;
                int anio = 0;
                foreach (var item in lstDatos)
                {
                    string[] _split = item.Split(_replace_1, StringSplitOptions.RemoveEmptyEntries);
                    if (_split.Length >= 1 && string.IsNullOrEmpty(dia))
                        dia = GetDia(_split[0]);
                    if (_split.Length >= 2 && mes == 0)
                        mes = GetMes(_split[1]);
                    if (_split.Length >= 3 && anio == 0)
                        anio = int.Parse(_split[2].Trim().Substring(0, 4));
                }
                if (anio <= DateTime.Now.Year)
                    anio += 10;
                return (new DateTime(anio, mes, int.Parse(dia)));
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex);
                return DateTime.Now;
            }
        }
        private string GetDia(string texto)
        {
            for (int i = 0; i < texto.Length; i++)
            {
                if (char.IsDigit((texto)[i]))
                {
                    texto = texto.Substring(i);
                    break;
                }
            }
            int value = 1;
            return int.TryParse(texto, out value) ? value.ToString() : "1";
        }

        private int GetMes(string texto)
        {
            int _mes = 0;
            string[] keys = new string[] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            string sKeyResult = keys.FirstOrDefault<string>(s => texto.Contains(s));
            if (string.IsNullOrEmpty(sKeyResult))
                sKeyResult = ComparePartialRatio(keys.ToList(), texto);
            switch (sKeyResult)
            {
                case "ENERO":
                    _mes = 1;
                    break;
                case "FEBRERO":
                    _mes = 2;
                    break;
                case "MARZO":
                    _mes = 3;
                    break;
                case "ABRIL":
                    _mes = 4;
                    break;
                case "MAYO":
                    _mes = 5;
                    break;
                case "JUNIO":
                    _mes = 6;
                    break;
                case "JULIO":
                    _mes = 7;
                    break;
                case "AGOSTO":
                    _mes = 8;
                    break;
                case "SEPTIEMBRE":
                    _mes = 9;
                    break;
                case "OCTUBRE":
                    _mes = 10;
                    break;
                case "NOVIEMBRE":
                    _mes = 11;
                    break;
                case "DICIEMBRE":
                    _mes = 12;
                    break;
                default:
                    break;
            }
            return _mes;
        }
        public string ComparePartialRatio(List<string> items, string value)
        {
            string response = string.Empty;
            foreach (var item in items)
            {
                if (Fuzz.PartialRatio(item, value) >= 75)
                {
                    response = item;
                    break;
                }
            }
            return response;
        }
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }
        string ResizeImage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                var image = Image.FromStream(ms);

                var ratioX = (double)150 / image.Width;
                var ratioY = (double)50 / image.Height;

                var ratio = Math.Min(ratioX, ratioY);

                var width = (int)(500);
                var height = (int)(300);

                var newImage = new Bitmap(width, height);

                Graphics.FromImage(newImage).DrawImage(image, 0, 0, width, height);

                Bitmap bmp = new Bitmap(newImage);

                ImageConverter converter = new ImageConverter();

                data = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

                return Convert.ToBase64String(data);
            }
        }
    }
}
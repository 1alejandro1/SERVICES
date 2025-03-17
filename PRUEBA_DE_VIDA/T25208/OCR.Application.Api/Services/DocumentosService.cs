using BCP.CROSS.LOGGER;
using BCP.CROSS.SECRYPT;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using OCR.Application.Api.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Style.XmlAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OCR.Application.Api.Services
{
    public interface IDocumentosService
    {
        Task<ValidarDocumentoResponse> ValidarDoc(string operacion, string path);
        Task<ValidateDocumentResponse> ValidarASFI(string operacion, string path);
        Task<ValidateDocumentResponse> ValidarSeguro(string operacion, string image);
        Task<ValidateDocumentResponse> ValidarFormulario(string operacion, string image, string tipo);
    }
    public class DocumentosService : IDocumentosService
    {
        private readonly IManagerSecrypt secrypt;
        private readonly ILogger logger;
        private readonly AzureCredentialsConfig azureCredentials;
        private readonly FlagConfig flag;
        static string base64String = null;
        public int count = 0;
        public DocumentosService(IConfiguration configuration, IManagerSecrypt secrypt, ILogger logger)
        {
            this.secrypt = secrypt;
            this.logger = logger;
            azureCredentials = new AzureCredentialsConfig();
            configuration.GetSection("AzureCredentials").Bind(azureCredentials);
            azureCredentials.Key = this.secrypt.Desencriptar(azureCredentials.Key);

            flag = new FlagConfig();
            configuration.GetSection("Flag").Bind(flag);
        }
        public async Task<ValidarDocumentoResponse> ValidarDoc(string operacion, string path)
        {
            ValidarDocumentoResponse model = new ValidarDocumentoResponse();

            model = await documento(operacion, path);

            return model;
        }
        public async Task<ValidateDocumentResponse> ValidarASFI(string operacion, string img)
        {
            ValidateDocumentResponse model = new ValidateDocumentResponse();

            model = await documentoASFI(operacion, img);

            return model;
        }
        public async Task<ValidateDocumentResponse> ValidarSeguro(string operacion, string image)
        {
            ValidateDocumentResponse model = new ValidateDocumentResponse();
            model = await AllDocument(operacion, image);

            return model;
        }
        public async Task<ValidateDocumentResponse> ValidarFormulario(string operacion, string image, string tipo)
        {
            ValidateDocumentResponse model = new ValidateDocumentResponse();
            FirmaResponse modelFirma = new FirmaResponse();
            if (tipo.Trim().ToUpper() == "FIRMA")
            {
                modelFirma = await AllDocumentFirma(operacion, image);
                model.texto = modelFirma.data;
                model.operacion = modelFirma.operacion;
            }
            else if(tipo.Trim().ToUpper() == "DOCUMENTO")
            {
                model = await AllDocument(operacion, image);
            }               

            return model;
        }
        private async Task<ValidarDocumentoResponse> documento(string operacion, string image)
        {
            // Creamos el archivo
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileStream fs = new FileStream("Circulares.xlsx", FileMode.Create);
            ExcelPackage Excel = new ExcelPackage(fs);

            /* Creación del estilo. */
            Excel.Workbook.Styles.CreateNamedStyle("Moneda");
            ExcelNamedStyleXml General = Excel.Workbook.Styles.NamedStyles[1];// 0 = Normal, 1 (El que acabamos de agregar).

            //moneda.Style.Numberformat.Format = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
            General.Style.Fill.PatternType = ExcelFillStyle.Solid;
            General.Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            /* Creación de hoja de trabajo. */
            Excel.Workbook.Worksheets.Add("Circular 1");
            ExcelWorksheet hoja = Excel.Workbook.Worksheets["Circular 1"];

            /* Num Caracteres + 1.29 de Margen.
            Los índices de columna empiezan desde 1. */
            hoja.Column(1).Width = 45.29f;
            hoja.Column(2).Width = 91.29f;



            string[] dirs = Directory.GetFiles(@"C:\Circulares\", "*.TIF");
            int cantidad = dirs.Length;


            ValidarDocumentoResponse model = new ValidarDocumentoResponse();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {
                int j = 0;
                for (int i = 0; i < cantidad; i++)
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(dirs[i]);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    image = base64ImageRepresentation;


                    var imageJpg = ConvertTiffToJpeg(image);
                    image = imageJpg[0];


                    Bitmap source = Base64StringToBitmap(image);
                    int alto = source.Height;
                    int ancho = source.Width;

                    int x = (ancho * 7) / 100;
                    int y = (alto * 15) / 100;
                    int w = (ancho * 70) / 100;
                    int h = (alto * 10) / 100;

                    int x1 = (ancho * 30) / 100;
                    int y1 = (alto * 35) / 100;
                    int w1 = (ancho * 90) / 100;
                    int h1 = (alto * 10) / 100;

                    int x2 = (ancho * 7) / 100;
                    int y2 = (alto * 45) / 100;
                    int w2 = (ancho * 100) / 100;
                    int h2 = (alto * 30) / 100;

                    Bitmap source1 = Base64StringToBitmap(image);
                    Rectangle rectOrig1 = new Rectangle(x, y, w, h);
                    Bitmap CroppedImage1 = CropImage(source1, rectOrig1);
                    var rectangle1 = BitmapToBase64String(CroppedImage1);

                    var ocrResults1 = await GetTextAzureAsync1(rectangle1);
                    var textUrlFileResults1 = ocrResults1.AnalyzeResult.ReadResults;
                    foreach (ReadResult page in textUrlFileResults1)
                    {
                        count++;
                        string _line = string.Empty;
                        foreach (Line line in page.Lines)
                        {
                            string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                            _line += textoSinAcentos + " ";
                        }
                        lstText.Add(_line.TrimEnd().ToUpper());

                    }
                    Bitmap source2 = Base64StringToBitmap(image);
                    Rectangle rectOrig2 = new Rectangle(x1, y1, w1, h1);
                    Bitmap CroppedImage2 = CropImage(source2, rectOrig2);
                    var rectangle2 = BitmapToBase64String(CroppedImage2);

                    var ocrResults2 = await GetTextAzureAsync1(rectangle2);
                    var textUrlFileResults2 = ocrResults2.AnalyzeResult.ReadResults;
                    foreach (ReadResult page in textUrlFileResults2)
                    {
                        count++;
                        string _line = string.Empty;
                        foreach (Line line in page.Lines)
                        {
                            string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                            _line += textoSinAcentos + " ";
                        }
                        lstText.Add(_line.TrimEnd().ToUpper());

                    }
                    Bitmap source3 = Base64StringToBitmap(image);
                    Rectangle rectOrig3 = new Rectangle(x2, y2, w2, h2);
                    Bitmap CroppedImage3 = CropImage(source3, rectOrig3);
                    var rectangle3 = BitmapToBase64String(CroppedImage3);

                    var ocrResults3 = await GetTextAzureAsync1(rectangle3);
                    var textUrlFileResults3 = ocrResults3.AnalyzeResult.ReadResults;
                    foreach (ReadResult page in textUrlFileResults3)
                    {
                        count++;
                        string _line = string.Empty;
                        foreach (Line line in page.Lines)
                        {
                            string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                            _line += textoSinAcentos + " ";
                        }
                        lstText.Add(_line.TrimEnd().ToUpper());
                    }
                    try
                    {


                        ExcelRange rangoTitle = hoja.Cells[j + 1, 1];
                        rangoTitle.Value = "";
                        rangoTitle.StyleName = "General";
                        rangoTitle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rangoTitle.Style.Fill.BackgroundColor.SetColor(Color.AliceBlue);

                        ExcelRange rangoCite = hoja.Cells[j + 2, 1];
                        rangoCite.Value = "CITE: ";
                        rangoCite.StyleName = "General";
                        rangoCite.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rangoCite.Style.Fill.BackgroundColor.SetColor(Color.White);
                        rangoCite.Style.Font.Bold = true;


                        ExcelRange rangoRef = hoja.Cells[j + 3, 1];
                        rangoRef.Value = "Referencia: ";
                        rangoRef.StyleName = "General";
                        rangoRef.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rangoRef.Style.Fill.BackgroundColor.SetColor(Color.White);
                        rangoRef.Style.Font.Bold = true;

                        ExcelRange rangoDetalle = hoja.Cells[j + 4, 1];
                        rangoDetalle.Value = "Cuerpo: ";
                        rangoDetalle.StyleName = "General";
                        rangoDetalle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rangoDetalle.Style.Fill.BackgroundColor.SetColor(Color.White);
                        rangoDetalle.Style.Font.Bold = true;

                        ExcelRange rangoOcr = hoja.Cells[j + 1, 2];
                        rangoOcr.Value = "DETALLE OCR";
                        rangoOcr.StyleName = "General";
                        rangoOcr.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rangoOcr.Style.Fill.BackgroundColor.SetColor(Color.AliceBlue);
                        rangoOcr.Style.Font.Bold = true;

                        foreach (var item in lstText)
                        {
                            if ((item.Contains("REF")) == true)
                            {
                                ExcelRange rango = hoja.Cells[j + 3, 2];
                                rango.Value = item;
                                rango.StyleName = "General";
                                rango.Style.WrapText = true;
                            }
                            else
                            {
                                if ((item.Contains("LA PAZ")) == true)
                                {
                                    ExcelRange rango = hoja.Cells[j + 2, 2];
                                    rango.Value = item;
                                    rango.StyleName = "General";
                                    rango.Style.WrapText = true;
                                }
                                else
                                {
                                    ExcelRange rango = hoja.Cells[j + 4, 2];
                                    rango.Value = item;
                                    rango.StyleName = "General";
                                    rango.Style.WrapText = true;
                                }
                            }

                        }
                        //logger.Debug($"[{operacion}] - CONTADOR: " + j);
                        j = j + 7;

                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error: " + ex);
                        throw;
                    }

                }
                var newFile = new FileInfo(@"C:\Documents\Circulares.xlsx");
                Excel.SaveAs(newFile);


                model.texto = lstText;
                logger.Debug($"[{operacion}] - {string.Join("|", lstText)}");
                model.operacion = operacion;
                model.image = image;

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
        private async Task<ValidateDocumentResponse> documentoASFI(string operacion, string image)
        {
        
            ValidateDocumentResponse model = new ValidateDocumentResponse();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {                   
                    var imageJpg = ConvertTiffToJpeg(image);

                foreach (var item in imageJpg)
                {

                    image = item;

                    var ocrResults = await GetTextAzureAsync1(image);
                    var textUrlFileResults1 = ocrResults.AnalyzeResult.ReadResults;
                    foreach (ReadResult page in textUrlFileResults1)
                    {
                        count++;
                        string _line = string.Empty;
                        foreach (Line line in page.Lines)
                        {
                            string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                            _line += line.Text + " ";
                        }
                        lstText.Add(_line.TrimEnd().ToUpper());

                    }
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
        private async Task<ValidateDocumentResponse> AllDocumentLine(string operacion, string image)
        {

            ValidateDocumentResponse model = new ValidateDocumentResponse();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {

                var ocrResults = await GetTextAzureAsync1(image);
                //foreach (var region in ocrResults.Regions)
                //{
                //    foreach (var line in region.Lines)
                //    {
                //        string _line = string.Empty;
                //        foreach (var word in line.Words)
                //        {
                //            string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                //            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                //            _line += word.Text + " ";
                //        }

                //        lstText.Add(_line.TrimEnd().ToUpper());
                //    }
                //}
                var textUrlFileResults1 = ocrResults.AnalyzeResult.ReadResults;
                foreach (ReadResult page in textUrlFileResults1)
                {
                    string _line = string.Empty;
                    foreach (Line line in page.Lines)
                    {
                        string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                        string textoSinAcentos = reg.Replace(textoNormalizado, "");
                        _line += textoSinAcentos + " ";

                        lstText.Add(_line.TrimEnd().ToUpper());
                    }

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
        private async Task<FirmaResponse> AllDocumentFirma(string operacion, string image)
        {
            FirmaResponse model = new FirmaResponse();            
            
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            try
            {

                var ocrResults = await GetTextAzureAsyncSignature(image);
                string _line = string.Empty;
                foreach (var description in ocrResults.Captions)
                {                   
                    Console.WriteLine("{0}\t\t with confidence {1}", description.Text, description.Confidence);
                    _line += "text: " + description.Text + " | confidence: " + description.Confidence;
                    lstText.Add(_line.TrimEnd().ToUpper());
                }
                //foreach (var region in ocrResults.Regions)
                //{
                //    foreach (var line in region.Lines)
                //    {
                //        string _line = string.Empty;
                //        foreach (var word in line.Words)
                //        {
                //            string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                //            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                //            _line += word.Text + " ";
                //        }

                //        lstText.Add(_line.TrimEnd().ToUpper());
                //    }
                //}

                //var textUrlFileResults1 = ocrResults .AnalyzeResult.ReadResults;
                //foreach (ReadResult page in textUrlFileResults1)
                //{
                //    string _line = string.Empty;
                //    foreach (Line line in page.Lines)
                //    {
                //        string textoNormalizado = line.Text.Normalize(NormalizationForm.FormD);
                //        string textoSinAcentos = reg.Replace(textoNormalizado, "");
                //        _line += line.Text + " ";
                //    }
                //    lstText.Add(_line.TrimEnd().ToUpper());
                //}

                model.data = lstText;
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
        private async Task<ValidateDocumentResponse> AllDocument(string operacion, string image)
        {
            ValidateDocumentResponse model = new ValidateDocumentResponse();
            List<string> lstText = new List<string>();
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
                   
                try
                {

                    var ocrResults = await GetTextAzureAsync1(image);
                    //foreach (var region in ocrResults.Regions)
                    //{
                    //    foreach (var line in region.Lines)
                    //    {
                    //        string _line = string.Empty;
                    //        foreach (var word in line.Words)
                    //        {
                    //            string textoNormalizado = word.Text.Normalize(NormalizationForm.FormD);
                    //            string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    //            _line += word.Text + " ";
                    //        }

                    //        lstText.Add(_line.TrimEnd().ToUpper());
                    //    }
                    //}
                    var textUrlFileResults1 = ocrResults.AnalyzeResult.ReadResults;
                    foreach (ReadResult page in textUrlFileResults1)
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
                    while ((results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted));
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
                if (flag.FilterBW == "ON")
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
        private async Task<ImageDescription> GetTextAzureAsyncSignature(string imageData)
        {
            try
            {
                ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(azureCredentials.Key);
                var cvClient = new ComputerVisionClient(credentials)
                {
                    Endpoint = azureCredentials.EndPoint
                };
             
                    var bytes = Convert.FromBase64String(imageData);
                    using var contents = new MemoryStream(bytes);
                    var results = await cvClient.DescribeImageInStreamAsync(image: contents);
            
                return results;
                
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
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }
        public static string[] ConvertTiffToJpeg(string fileName)
        {
            var bytes = Convert.FromBase64String(fileName);
            using var contents = new MemoryStream(bytes);

            using (Image imageFile = Image.FromStream(contents))
            {
                FrameDimension frameDimensions = new FrameDimension(
                    imageFile.FrameDimensionsList[0]);

                // Gets the number of pages from the tiff image (if multipage) 
                int frameNum = imageFile.GetFrameCount(frameDimensions);
                MemoryStream[] jpegPaths = new MemoryStream[frameNum];

                string[] base64 = new string[frameNum];

                for (int frame = 0; frame < frameNum; frame++)
                {

                    // Selects one frame at a time and save as jpeg. 
                    imageFile.SelectActiveFrame(frameDimensions, frame);
                    using (Bitmap bmp = new Bitmap(imageFile))
                    {
                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, ImageFormat.Jpeg);
                        jpegPaths[frame] = ms;
                        byte[] byteImage = jpegPaths[frame].ToArray();
                        base64[frame] = Convert.ToBase64String(byteImage);
                    }
                }

                return base64;
            }
        }
      
    }
}

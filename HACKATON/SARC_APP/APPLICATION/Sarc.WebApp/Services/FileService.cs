using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Caso;
using BCP.CROSS.MODELS.SharePoint;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sarc.WebApp.Connectors;
using Sarc.WebApp.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sarc.WebApp.Services
{
    public class FileService : IFileService
    {
        private readonly SarcApiConnector _sarcApiConnector;
        private readonly IOptions<SarcApiSettings> _sarcApiSettings;

        public FileService(SarcApiConnector sarcApiConnector, IOptions<SarcApiSettings> sarcApISettings)
        {
            _sarcApiConnector = sarcApiConnector;
            _sarcApiSettings = sarcApISettings;
        }

        public async Task<ServiceResponse<bool?>> UploadSharePointAsync(ArchivosAdjuntosDTO requestDTO)
        {
            var request = new ArchivosAdjuntos {
                archivosAdjuntos = ArchivosAdjuntos(requestDTO.archivosAdjuntos),
                base64 = true,
                nombreCarpeta = requestDTO.nombreCarpeta,
                nombreProceso = requestDTO.nombreProceso
            };

            var response = await _sarcApiConnector.PostV2Async<bool?, ArchivosAdjuntos>(
                _sarcApiSettings.Value.SharePoint.InsertArchivosAdjuntos, request);

            return response;
        }

        public List<ArchivoAdjunto> ArchivosAdjuntos(List<IFormFile> file)
        {
            List<ArchivoAdjunto> adjuntos = new();
            if (file is null)
            {
                return adjuntos;
            }
            long sizeFiles = file.Sum(x => x.Length);
            foreach (var formFile in file)
            {
                if (formFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    formFile.CopyTo(ms);
                    var fileByte = ms.ToArray();
                    var adj = new ArchivoAdjunto
                    {
                        Archivo = Convert.ToBase64String(fileByte),
                        Nombre = formFile.FileName
                    };
                    adjuntos.Add(adj);
                }
            }

            return adjuntos;
        }
    }
}

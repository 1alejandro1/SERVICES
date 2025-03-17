using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Caso;
using BCP.CROSS.MODELS.SharePoint;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sarc.WebApp.Contracts
{
    public interface IFileService
    {
        List<ArchivoAdjunto> ArchivosAdjuntos(List<IFormFile> file);
        Task<ServiceResponse<bool?>> UploadSharePointAsync(ArchivosAdjuntosDTO request);
    }
}

using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.WcfSwamp;
using Microsoft.Extensions.Options;
using Sarc.WebApp.Connectors;
using Sarc.WebApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sarc.WebApp.Services
{
    public class WcfSwampService : IWcfSwampService
    {
        private readonly SarcApiConnector _sarcApiConnector;
        private readonly IOptions<SarcApiSettings> _sarcApiSettings;

        public WcfSwampService(SarcApiConnector sarcApiConnector, IOptions<SarcApiSettings> sarcApiSettings)
        {
            _sarcApiConnector = sarcApiConnector;
            _sarcApiSettings = sarcApiSettings;
        }

        public async Task<ServiceResponse<bool?>> AbanarAsync(DevolucionPR request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, DevolucionPR>(
                _sarcApiSettings.Value.WcfSwamp.Abono, request);

            return response;
        }

        public async Task<ServiceResponse<bool?>> DebitarAsync(CobroPR request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, CobroPR>(
                _sarcApiSettings.Value.WcfSwamp.Debito, request);

            return response;
        }
    }
}

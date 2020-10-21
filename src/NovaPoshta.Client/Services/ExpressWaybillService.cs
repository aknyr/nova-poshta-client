﻿using NovaPoshta.Client.Abstractions;
using NovaPoshta.Client.Abstractions.Connection;
using NovaPoshta.Client.Abstractions.Services;
using NovaPoshta.Client.Models;
using NovaPoshta.Client.Models.Data.ExpressWaybill;
using NovaPoshta.Client.Models.Props.ExpressWaybill;
using System;
using System.Threading.Tasks;

namespace NovaPoshta.Client.Services
{
    public sealed class ExpressWaybillService : ApiClient, IExpressWaybillService, IModelNameResolver
    {
        public ExpressWaybillService(IApiConnection connection)
            : base(connection)
        {
        }

        public string ModelName => "InternetDocument";

        public Task<ResponsePayload<GetArchiveDocumentsByPhoneData>> GetArchieveDocumentList(int page, int limit)
        {
            var payload = new RequestPayload<GetArchiveDocumentsByPhoneProps>
            {
                ApiKey = ApiConnection.ApiKey,
                ModelName = ModelName,
                //CalledMethod = "getDocumentList",
                CalledMethod = "getArchiveDocumentsByPhone",
                MethodProperties = new GetArchiveDocumentsByPhoneProps
                {
                    DateFrom = $"{DateTime.UtcNow.AddDays(-90).Date.ToString("dd.MM.yyyy")} 00:00:00",
                    DateTo = $"{DateTime.UtcNow.Date.ToString("dd.MM.yyyy")} 00:00:00",
                    Page = page,
                    Limit = limit
                }
            };

            return ApiConnection.PostAsync<GetArchiveDocumentsByPhoneProps, GetArchiveDocumentsByPhoneData>(payload);
        }

        public Task<ResponsePayload<GetIncomingDocumentsByPhoneData>> GetIncomingDocumentList(int page, int limit)
        {
            var payload = new RequestPayload<GetIncomingDocumentsByPhoneProps>
            {
                ApiKey = ApiConnection.ApiKey,
                ModelName = ModelName,
                CalledMethod = "getIncomingDocumentsByPhone",
                MethodProperties = new GetIncomingDocumentsByPhoneProps
                {
                    DateFrom = $"{DateTime.UtcNow.AddDays(-60).Date.ToString("dd.MM.yyyy")} 00:00:00",
                    DateTo = $"{DateTime.UtcNow.AddDays(30).Date.ToString("dd.MM.yyyy")} 00:00:00",
                    Page = page,
                    Limit = limit
                }
            };

            return ApiConnection.PostAsync<GetIncomingDocumentsByPhoneProps, GetIncomingDocumentsByPhoneData>(payload);
        }
    }
}

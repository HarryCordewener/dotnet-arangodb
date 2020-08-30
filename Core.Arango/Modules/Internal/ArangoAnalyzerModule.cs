﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Arango.Protocol;
using Newtonsoft.Json.Linq;

namespace Core.Arango.Modules.Internal
{
    internal class ArangoAnalyzerModule : ArangoModule, IArangoAnalyzerModule
    {
        internal ArangoAnalyzerModule(IArangoContext context) : base(context)
        {
        }

        public async Task<List<ArangoAnalyzer>> ListAsync(ArangoHandle database,
            CancellationToken cancellationToken = default)
        {
            return (await SendAsync<QueryResponse<ArangoAnalyzer>>(HttpMethod.Get,
                ApiPath(database, "analyzer"),
                cancellationToken: cancellationToken)).Result;
        }

        public async Task CreateAsync(ArangoHandle database,
            ArangoAnalyzer analyzer,
            CancellationToken cancellationToken = default)
        {
            await SendAsync<QueryResponse<JObject>>(HttpMethod.Post,
                ApiPath(database, "analyzer"),
                Serialize(analyzer),
                cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(ArangoHandle database,
            string analyzer, bool force = false,
            CancellationToken cancellationToken = default)
        {
            await SendAsync<QueryResponse<JObject>>(HttpMethod.Delete,
                ApiPath(database, $"analyzer/{UrlEncode(analyzer)}?force={(force ? "true" : "false")}"),
                cancellationToken: cancellationToken);
        }
    }
}
using System.Net.Http;
using System.Threading.Tasks;
using Klinked.Cqrs.Queries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Klinked.Cqrs.Console.Queries
{
    public class HttpGetQueryHandler : IQueryHandler<string, string>
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpGetQueryHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<string> ExecuteAsync(string args)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync(args);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
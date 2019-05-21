using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoBogus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Faker360
{
    public static class Faker
    {
        [FunctionName("Faker")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")]HttpRequestMessage req, ILogger log)
        {
            var faker = AutoFaker.Create();

            var shipTo = new AutoFaker<ShipTo>()
            .RuleFor(f => f.name, f => f.Name.FirstName())
            .RuleFor(f => f.address, f => f.Address.FullAddress())
            .RuleFor(f => f.city, f => f.Address.City())
            .RuleFor(f => f.state, f => f.Address.State())
            .RuleFor(f => f.zip, f => f.Address.ZipCode());

            var billTo = new AutoFaker<BillTo>()
            .RuleFor(f => f.name, f => f.Name.FirstName())
            .RuleFor(f => f.address, f => f.Address.FullAddress())
            .RuleFor(f => f.city, f => f.Address.City())
            .RuleFor(f => f.state, f => f.Address.State())
            .RuleFor(f => f.zip, f => f.Address.ZipCode());

            var order = new AutoFaker<PoOrder>()
            .RuleFor(f => f.ponumber, f => f.Random.Int(0))
            .RuleFor(f => f.name, f => f.Name.FullName())
            .RuleFor(f => f.sku, f => f.Finance.BitcoinAddress())
            .RuleFor(f => f.quantity, f => f.Random.Number())
            .RuleFor(f => f.messagetype, f => f.Finance.TransactionType())
            .RuleFor(f => f.shipTo, () => shipTo)
            .RuleFor(f => f.billTo, () => billTo);

            var result = JsonConvert.DeserializeObject<PoOrder>(JsonConvert.SerializeObject(order.Generate()));
            return req.CreateResponse(HttpStatusCode.Accepted, result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_intercorp.Util;
using reto_intercorp.ViewModel;

namespace reto_intercorp.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ClientViewModel>> Get()
        {

            try
            {

                FirebaseUTIL firebase = new FirebaseUTIL();
                var clients = firebase.getClientsAsync().Result;

                return Ok(clients);

            }
            catch (Exception)
            {
                throw;
            }


        }

        [HttpGet("kpideclientes")]
        public ActionResult<KPIClientViewModel> kpiClients()
        {

            KPIClientViewModel kpiClient = new KPIClientViewModel();

            try
            {

                FirebaseUTIL firebase = new FirebaseUTIL();
                var clients = firebase.getClientsAsync().Result;

                var ages = clients.Select(x => x.Age).ToArray();

                var avg_age = ages.Average();
                var var = ages.Select(x => (x - avg_age) * (x - avg_age)).Sum();
                var sd = Math.Sqrt(var / ages.Length);

                var data = ages.GroupBy(x=>x)
                    .Select(z => new Data { age = z.Key, count = z.Count()}).OrderBy(x=>x.age).ToList();

              
                kpiClient.avg_age = (int)avg_age;
                kpiClient.desvstnd = sd;
                kpiClient.clientsAge = data;

                return Ok(kpiClient);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpPost("creacliente")]
        public async Task<ActionResult<bool>> Create(ClientViewModel model)
        {

            try
            {
                FirebaseUTIL firebase = new FirebaseUTIL();
                var result = await firebase.saveClientAsync(new Models.Client
                {
                    ClientId = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Last_Name = model.Last_Name,
                    Age = model.Age,
                    Birthday = model.Birthday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                });

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
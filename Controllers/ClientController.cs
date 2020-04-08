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
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {

        [HttpGet]
        [Route("")]
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

        [HttpGet]
        [Route("kpideclientes​")]
        [ActionName("kpidecliente")]
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
                    .Select(z => new Data { age = z.Key, count = z.Count()}).ToList();

              
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

        [HttpPost]
        [Route("creacliente​")]
        [ActionName("creaclient")]
        public ActionResult<bool> Create(ClientViewModel model) 
        {

            try
            {
                FirebaseUTIL firebase = new FirebaseUTIL();
                var result = firebase.saveClientAsync(new Models.Client { 
                    ClientId = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Last_Name = model.Last_Name,
                    Age = model.Age,
                    Birthday = model.Birthday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                });

                return Ok(result);

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
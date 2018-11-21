using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetFinderBackOffice.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetFinderBackOffice.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly PetFinderDBContext context = new PetFinderDBContext();

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            this.context.Usuario.Add(new Usuario()
            {
                Avatar = "",
                Direccion = "Palacios 374",
                Email = "tdortona@gmail.com",
                Nombre = "Tomas"
            });

            this.context.SaveChanges();

            return this.context.Usuario.LastOrDefault();
        }

        // POST api/<controller>
        [HttpPost("/api/Values/Prueba")]
        public void Post([FromBody]PruebaModel algo)
        {
            algo.Value = algo.Value + " hola";
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class PruebaModel
    {
        public string Value { get; set; }
    }
}

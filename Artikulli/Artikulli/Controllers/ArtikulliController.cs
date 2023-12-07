using ArtikullServices.Models;
using ArtikullServices.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Artikulli.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtikulliController : ControllerBase
    {



        public ProduktService _produktService;
        public ArtikulliController(ProduktService produktService)
        {
                this._produktService = produktService;
        }
        // GET: api/<TestControllers>
        [HttpGet]
        public IEnumerable<Produkt> Get()
        {
            return _produktService.getProdukts();
        }



        [HttpGet]
        [Route("/api/Artikulli/search/{name}")]
        public IEnumerable<Produkt> search(string name)
        {
            return _produktService.GetProduktByName(name);
        }

        // GET api/<TestControllers>/5
        [HttpGet("{id}")]
        public Produkt Get(int id)
        {
            return _produktService.getProductById(id);
        }

        // POST api/<TestControllers>
        [HttpPost]
        public Produkt Post([FromBody] Produkt value)
        {
            _produktService.addProdukt(value);
            return new Produkt();
        }

        // PUT api/<TestControllers>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Produkt value)
        {
            _produktService.EditProdukt(value,id);
        }

        // DELETE api/<TestControllers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _produktService.deleteProdukt(id);
        }
    }
}

using ArtikullServices.Models;
using ArtikullServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace Artikulli.Controllers
{
    public class UserInterfaceController : Controller
    {
        private readonly ProduktService _produktService;

        public UserInterfaceController(ProduktService produktService)
        {
            _produktService = produktService;
        }
        // GET: Produkt

        public IActionResult Index()
        {
            var Index = _produktService.getProdukts();
            return View(Index);
        }

       

        // GET: Produkt/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produkt/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                _produktService.addProdukt(produkt);
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        // GET: Produkt/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = _produktService.getProductById(id.Value);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkt/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Produkt produkt)
        {
            if (id != produkt.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _produktService.EditProdukt(produkt, id);
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }





        public IActionResult GetSearch(string name)
        {
            if(name==null || name == "")
                return PartialView("_Search", _produktService.getProdukts());

            return PartialView("_Search", _produktService.GetProduktByName(name));
        }

        // GET: Produkt/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = _produktService.getProductById(id.Value);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkt/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _produktService.deleteProdukt(id);
            return RedirectToAction(nameof(Index));
        }

        //// GET: Produkt/Search
        //public IActionResult Search(string name)
        //{
        //    var produkts = _produktService.GetProduktByName(name);
        //    return View(produkts);
        //}
    }
}

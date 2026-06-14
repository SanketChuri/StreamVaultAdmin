using Microsoft.AspNetCore.Mvc;
using StreamVaultAdmin.Models;
using StreamVaultAdmin.Services;

namespace StreamVaultAdmin.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly CatalogueService _service;

        public CatalogueController(CatalogueService service)
        {
            _service = service;
        }

        // GET: /Catalogue
        public IActionResult Index(string? type = null, string? search = null)
        {
            var items = _service.GetAll(type, search);
            ViewBag.CurrentType = type;
            ViewBag.CurrentSearch = search;
            return View(items);
        }

        // GET: /Catalogue/Create
        public IActionResult Create(string type)
        {
            ViewBag.Type = type;
            return View();
        }

        // POST: /Catalogue/Create
        [HttpPost]
        public IActionResult Create(string type, IFormCollection form)
        {
            ContentItem item = type switch
            {
                "Movie"      => new Movie(),
                "Series"     => new Series(),
                "Audiobook"  => new Audiobook(),
                "MusicAlbum" => new MusicAlbum(),
                _            => throw new ArgumentException("Invalid type")
            };

            item.UpdateSharedFields(form);
            item.UpdateTypeFields(form);

            _service.Add(item);
            return RedirectToAction("Index");
        }

        // GET: /Catalogue/Edit/1
        public IActionResult Edit(int id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Catalogue/Edit/1
        [HttpPost]
        public IActionResult Edit(int id, IFormCollection form)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();

            item.UpdateSharedFields(form);
            item.UpdateTypeFields(form);

            _service.Update(item);
            return RedirectToAction("Index");
        }

        // GET: /Catalogue/Delete/1
        public IActionResult Delete(int id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Catalogue/Delete/1
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
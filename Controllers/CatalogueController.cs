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
        // Show the catalogue list with optional filter and search
        public IActionResult Index(string? type = null, string? search = null)
        {
            var items = _service.GetAll(type, search);
            ViewBag.CurrentType = type;
            ViewBag.CurrentSearch = search;
            return View(items);
        }

        // GET: /Catalogue/Create?type=Movie
        // Show the empty create form
        public IActionResult Create(string type)
        {
            ViewBag.Type = type;
            return View();
        }

        // POST: /Catalogue/Create?type=Movie
        // Save the new item to the database
        [HttpPost]
        public IActionResult Create(string type, IFormCollection form)
        {
            var item = CreateEmptyItem(type);
            if (item == null)
                return RedirectToAction("Index");

            item.UpdateSharedFields(form);
            item.UpdateTypeFields(form);

            // Validate shared and type specific fields
            var errors = item.ValidateSharedFields();
            errors.AddRange(item.ValidateTypeFields());

            // If errors found send back to form with error messages
            if (errors.Any())
            {
                ViewBag.Errors = errors;
                ViewBag.Type = type;
                ViewBag.FormData = form;
                return View();
            }

            _service.Add(item);
            return RedirectToAction("Index");
        }

        // GET: /Catalogue/Edit/1?type=Movie
        // Show the edit form pre-filled with existing data
        public IActionResult Edit(int id, string type)
        {
            var item = _service.GetById(id, type);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Catalogue/Edit/1
        // Save the updated item to the database
        [HttpPost]
        public IActionResult Edit(int id, IFormCollection form)
        {
            var item = _service.GetById(id, form["ContentType"]);
            if (item == null) return NotFound();

            item.UpdateSharedFields(form);
            item.UpdateTypeFields(form);

            // Validate shared and type specific fields
            var errors = item.ValidateSharedFields();
            errors.AddRange(item.ValidateTypeFields());

            // If errors found send back to form with error messages
            if (errors.Any())
            {
                ViewBag.Errors = errors;
                return View(item);
            }

            _service.Update(item);
            return RedirectToAction("Index");
        }

        // GET: /Catalogue/Delete/1?type=Movie
        // Show the delete confirmation page
        public IActionResult Delete(int id, string type)
        {
            var item = _service.GetById(id, type);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Catalogue/Delete/1?type=Movie
        // Delete the item from the database
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id, string type)
        {
            _service.Delete(id, type);
            return RedirectToAction("Index");
        }

        // Create the right empty object based on type
        // Kept in one place so type checking is not scattered
        private ContentItem? CreateEmptyItem(string type)
        {
            if (type == "Movie")      return new Movie();
            if (type == "Series")     return new Series();
            if (type == "Audiobook")  return new Audiobook();
            if (type == "MusicAlbum") return new MusicAlbum();
            return null;
        }
    }
}
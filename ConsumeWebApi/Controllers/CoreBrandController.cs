using ConsumeWebApi.Service;
using Crud_with_webApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeWebApi.Controllers
{
    public class CoreBrandController : Controller
    {
        private readonly IBrandService _iBrandService;

        public CoreBrandController(IBrandService iBrandService)
        {
            _iBrandService = iBrandService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Brand> brands = await _iBrandService.GetAll();
            return View(brands);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Brand model)
        {
            try
            {
                HttpResponseMessage message = await _iBrandService.Create(model);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest("Data insert failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brands = await _iBrandService.GetByID(id);
            return View(brands);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Brand model)
        {
            try
            {
                HttpResponseMessage message = await _iBrandService.Edit(model);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest("Data Update failed");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var brands = await _iBrandService.GetByID(id);
            return View(brands);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            HttpResponseMessage response = await _iBrandService.Delete(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return BadRequest("Product not deleted succesfully");
        }
    }
}

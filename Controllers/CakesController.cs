using Microsoft.AspNetCore.Mvc;
using CakeApp.Models;
using Services;
using System.Threading.Tasks;

namespace CakeApp.Controllers
{
    public class CakesController : Controller
    {
        private readonly CakesService _cakeService;

        public CakesController(CakesService cakeService)
        {
            _cakeService = cakeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Cakes> cakeList = await _cakeService.GetAsync();
            return View(cakeList);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            Cakes cake = await _cakeService.GetAsync(id);
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cakes newCake)
        {
            if (ModelState.IsValid)
            {
                newCake.OrderDate = DateTime.Now;
                await _cakeService.PostAsync(newCake);
                return RedirectToAction("Get", "Cakes");
            }
            else { return View("Create", newCake); }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Cakes cake = await _cakeService.GetAsync(id);
            return View(cake);
        }
        [HttpPost("Update/id")]
        public async Task<IActionResult> Update(string id, Cakes cake)
        {
            await _cakeService.Updateasync(id, cake);
            return RedirectToAction("Get", "Cakes");
        }

        [HttpGet]
        public IActionResult ConfirmDelete(string id)

        {
            TempData["id"] = id;
            return View();
        }

        [HttpPost("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)

        {
            await _cakeService.Deleteasync(id);
            return RedirectToAction("Get", "Cakes");
        }

    }
}
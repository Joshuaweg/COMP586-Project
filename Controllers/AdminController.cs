using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Back(Admin ad)
        {
            return View("Index",ad);
        }
        public async Task<IActionResult> fireDoctor(Admin ad) {
            await ad.manageDoctors();
            return View("Index", ad);        
        }
        public async Task<IActionResult> billPatient(Admin ad)
        {
            await ad.billCustomer();
            return View("Index", ad);
        }
        public async Task<IActionResult> order(Admin ad)
        {
            await ad.orderSupplies();
            return View("Index", ad);
        }
        public async Task<IActionResult> displayOrders(Admin ad) {
            ad.payload = await ad.getSupplies();
            return View("Orders", ad);
        }
    }
}

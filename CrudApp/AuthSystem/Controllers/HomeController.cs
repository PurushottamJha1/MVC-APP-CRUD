using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;



namespace AuthSystem.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _dbContext; 
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, AuthDbContext dbContext)
        {
            _logger = logger;
            this._userManager = userManager;
            _dbContext = dbContext; 
        }

        public async Task<IActionResult> Index()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["Email"] = user.Email;
                ViewData["FirstName"] = user.FirstName;
                ViewData["LastName"] = user.LastName;
                ViewData["Address"] = user.Address;
               
            }
            var userId = _userManager.GetUserId(this.User);
            var listtt = _dbContext.AddProfiles.Where(n => n.UserId == userId).ToList();
            return View(listtt);

        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Save(string itemName, string itemValue)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);


                var additionalItem = new AddProfile
                {
                    ProfileName = itemName,
                    ProfileUrl = itemValue,
                    UserId = await _userManager.GetUserIdAsync(user)
                };


                _dbContext.AddProfiles.Add(additionalItem);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("YourFormView");
            }
        }




        //[HttpPost]
        //public IActionResult Edit(int editItemId, string editItemName, string editItemValue)
        //{
        //    var userId = _userManager.GetUserId(this.User);


        //    var note = _dbContext.AddProfiles.FirstOrDefault(n => n.UserId == userId && n.Id == editItemId);
        //    if (note != null)
        //    {
        //        // Update the properties
        //        note.ProfileName = editItemName;
        //        note.ProfileUrl = editItemValue;
        //        _dbContext.SaveChanges();
        //        return RedirectToAction(nameof(Index), "Home");
        //    }

        //    return Content("Error updating record.");
        //}



        [HttpPost]
        public async Task<IActionResult> Edit(int editItemId, string editItemName, string editItemValue)
        {
            
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var userId = user.Id;

                    var note = _dbContext.AddProfiles.FirstOrDefault(n => n.UserId == userId && n.Id == editItemId);

                    if (note != null)
                    {
                        note.ProfileName = editItemName;
                        note.ProfileUrl = editItemValue;

                        _dbContext.SaveChanges();

                        return RedirectToAction(nameof(Index), "Home");
                    }
                }
            
            return Content("Error updating record.");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {

            var itemToDelete = _dbContext.AddProfiles.Find(id);
            if (itemToDelete != null)
            {
                _dbContext.AddProfiles.Remove(itemToDelete);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Test(String id)
        {

            Console.WriteLine("Hello");
            return View();
        }


    }
}
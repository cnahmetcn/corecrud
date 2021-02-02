using corecrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace corecrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UsersContext _context;

        public HomeController(ILogger<HomeController> logger, UsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var list = _context.Users.ToList();
            return View(list);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Create(Users user)
        {
            if (user.Id == 0)
            {
                await _context.AddAsync(user);
            }
            else
            {
                _context.Update(user);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }

        public IActionResult New(int? Id)
        {
            Users user;
            if (Id.HasValue)
            {
                user = _context.Users.Find(Id);

            }
            else
            {
                user = new Users();
            }
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

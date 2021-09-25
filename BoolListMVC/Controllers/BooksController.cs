using BoolListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoolListMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //API Call 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { date = await _db.Books.ToListAsync() });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var _bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (_bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Books.Remove(_bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}

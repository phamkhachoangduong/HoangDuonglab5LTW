using Microsoft.AspNetCore.Mvc;
using HoangDuonglab5.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HoangDuonglab5.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public IActionResult Index()
        {
            List<Account> accounts = new List<Account>();
            return View(accounts);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            Account model = new Account();
            return View(model);
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account model)
        {
            if (ModelState.IsValid)
            {
                // Thêm thành công chuyển về Index
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Remote validation kiểm tra số điện thoại
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPhone(string phone)
        {
            Regex _isPhone = new Regex(@"^(\+?(\d{1,3}))?[-. ]?(\d{3})[-. ]?(\d{3})[-. ]?(\d{4})$");
            if (!string.IsNullOrEmpty(phone) && !_isPhone.IsMatch(phone))
            {
                return Json($"Số điện thoại {phone} không đúng định dạng, VD: 0986421127 hoặc 098.421.1127");
            }
            return Json(true);
        }
    }
}
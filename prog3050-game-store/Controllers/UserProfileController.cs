﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using ClosedXML.Excel;
using System.IO;

namespace GameStore.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;

        public UserProfileController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            var aspNetUsers = await _userManager.FindByIdAsync(userId);
            if (aspNetUsers==null)
            {

                return NotFound();
            }
            return View(aspNetUsers);
        }
        // GET: UserProfile/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            id = _userManager.GetUserId(HttpContext.User);
            ViewBag.Gender = new List<string>() { "Male", "Female", "Other" };

            var aspNetUsers = await _userManager.FindByIdAsync(id);

            //ViewData["ProvinceCode"] = new SelectList(_context.Province.Where(x => x.CountryCode == "CA"), "ProvinceCode", "ProvinceCode", aspNetUsers.province);

            if (id == null)
            {
                return NotFound();
            }
            if (aspNetUsers == null)
            {
                return NotFound();
            }
            return View(aspNetUsers);

        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,userName,first_name,last_name,gender,dob,receive_promotions")] User aspNetUsers)
        {
            id = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(id);
            user.first_name = aspNetUsers.first_name;
            user.last_name = aspNetUsers.last_name;
            user.gender = aspNetUsers.gender;
            user.dob = aspNetUsers.dob;
            user.receive_promotions = aspNetUsers.receive_promotions;
           

            if (id != aspNetUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _userManager.UpdateAsync(user);
                    TempData["message"] = "Your profile has been updated!!";
                    

                }

                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Gender = new List<string>() { "Male", "Female", "Other" };
            //ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", aspNetUsers.province);
            aspNetUsers.UserName = user.UserName;
            return View(aspNetUsers);
        }
        public IActionResult PrintMemberDetails()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Members");
                var currentRow = 1;
                int count = 0;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "User Name";
                worksheet.Cell(currentRow, 3).Value = "Email";
                worksheet.Cell(currentRow, 4).Value = "Date of birth";
                worksheet.Cell(currentRow, 5).Value = "Age";
                worksheet.Cell(currentRow, 6).Value = "Street";
                worksheet.Cell(currentRow, 7).Value = "City";
                worksheet.Cell(currentRow, 8).Value = "Province";
                worksheet.Cell(currentRow, 9).Value = "Postal Code";
               

                var user = _context.AspNetUsers;
                var address = _context.Address;
                foreach (var item in user)
                {
                    currentRow++;
                    count++;
                    worksheet.Cell(currentRow, 1).Value = item.FirstName+" "+item.LastName;
                    worksheet.Cell(currentRow, 2).Value = item.UserName;
                    worksheet.Cell(currentRow, 3).Value = item.Email;
                    worksheet.Cell(currentRow, 4).Value = item.Dob;
                    int age;
                    if (item.Dob != null)
                    {
                        DateTime today = DateTime.Now;
                        DateTime dob = DateTime.Parse(item.Dob.ToString());
                        age = today.Year - dob.Year;
                        worksheet.Cell(currentRow, 5).Value = age;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 5).Value = "";
                    }
                    foreach (var add in address)
                    {
                        if (add.UserId==item.Id)
                        {
                            worksheet.Cell(currentRow, 6).Value = add.StreetAddress;
                            worksheet.Cell(currentRow, 7).Value = add.City;
                            worksheet.Cell(currentRow, 8).Value = add.Province;
                            worksheet.Cell(currentRow, 9).Value = add.PostalCode;
                        }
                    }
                }
                

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "MemberDetails.xlsx"
                        );
                }

            }

        }
        public IActionResult PrintMemberList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Members");
                var currentRow = 1;
                int count = 0;
                worksheet.Cell(currentRow, 2).Value = "User Name";


                var games = _context.Game;
                var user = _context.AspNetUsers;
                var address = _context.Address;
                foreach (var item in user)
                {
                    currentRow++;
                    count++;
                    worksheet.Cell(currentRow, 1).Value = item.UserName;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "MemberList.xlsx"
                        );
                }

            }

        }


    }
}

      

          

       

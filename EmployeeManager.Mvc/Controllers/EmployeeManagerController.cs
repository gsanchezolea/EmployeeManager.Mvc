﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManager.Mvc
{
    public class EmployeeManagerController : Controller
    {
        private AppDbContext db = null;

        public EmployeeManagerController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Insert()
        {
            FillCountries();
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                db.Employees.Add(model);
                db.SaveChanges();
                ViewBag.Message = "Employee inserted succesfully";
            }
            return View(model);
        }

        public IActionResult List()
        {
            List<Employee> model =
                (from e in db.Employees
                 orderby e.EmployeeID
                 select e).ToList();

            return View(model);
        }

        public IActionResult Update(int id)
        {
            FillCountries();
            Employee model = db.Employees.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                db.Employees.Update(model);
                db.SaveChanges();
                ViewBag.Message = "Employee updated succesfully";
            }
            return View(model);
        }

        public void FillCountries()
        {
            List<SelectListItem> countries =
                (from c in db.Countries
                 orderby c.Name ascending
                 select new SelectListItem()
                 {
                     Text = c.Name,
                     Value = c.Name
                 })
                 .ToList();
            ViewBag.Countries = countries;
        }
    }
}

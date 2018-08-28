using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _dbContext;
        public StudentController(StudentContext context)
        {
            _dbContext = context;
        }
        public IActionResult Index()
        {
            ViewBag.lstStudents = _dbContext.Students.ToList();
            return View();
        }
    }
}
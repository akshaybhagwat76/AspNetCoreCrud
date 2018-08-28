using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Data
{
    public static class DbInitialize
    {
        public static void Initialize(StudentContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;
            }
            var students = new Student[]
            {
                new Student{ Name="Tarun",Email="abc@gmail.com"},
                new Student{ Name="Ramesh  ",Email="ramesh@gmail.com"},
            };
            foreach (var item in students)
            {
                context.Students.Add(item);
            }
            context.SaveChanges();
        }
    }
}

using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using FlightTicketAdminApplication.Models;

namespace FlightTicketAdminApplication.Controllers
{
    public class UserController : Controller
    {

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44361/api/Admin/GetUsers";

          

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var result = response.Content.ReadAsAsync<List<User>>().Result;

            return View(result);
        }

        [HttpPost("[action]")]
        public IActionResult ChangeRole(User model)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44361/api/Admin/ChangeRole";


            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");


            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index");
        }




        [HttpGet("[action]")]
        public IActionResult ImportUsers()
        {
            return View();
        }

        [HttpPost("[action]")]
        public IActionResult ImportUsers(IFormFile file)
        {

            
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";


            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }

            

            List<User> users = getUsersFromExcelFile(file.FileName);

            HttpClient client = new HttpClient();

            string URL = "https://localhost:44361/api/Admin/ImportAllUsers";



            HttpContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");


            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index", "Ticket");
        }

        private List<User> getUsersFromExcelFile(string fileName)
        {

            string pathToFile = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            List<User> userList = new List<User>();

            using (var stream = System.IO.File.Open(pathToFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        userList.Add(new Models.User
                        {
                            Email = reader.GetValue(0).ToString(),
                            Password = reader.GetValue(1).ToString(),
                            ConfirmPassword = reader.GetValue(2).ToString(),
                            Role = Enum.Parse<Role>(reader.GetValue(3).ToString()),
                           
                        });
                    }
                }
            }

            return userList;

        }
    }
}

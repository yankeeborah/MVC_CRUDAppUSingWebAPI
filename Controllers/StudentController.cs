using CRUDAppUSingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace CRUDAppUSingWebAPI.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7109/api/StudentAPI/";  //root url
        public HttpClient client = new HttpClient(); //Manage Request-Response
        
        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result; //storing the result json
                var data = JsonConvert.DeserializeObject<List<Student>>(result); //convert jason string to list of student
                if (data != null) 
                {
                    students = data;
                }
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            string data = JsonConvert.SerializeObject(std); //convert object to Json and store in string variable
            StringContent content = new StringContent(data,Encoding.UTF8,"application/json");    //convert into string content which converts json data into formatting text
            HttpResponseMessage response = client.PostAsync(url, content).Result; // only accepts formatted string content
            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student Added";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = client.GetAsync(url+id).Result; //concatenating id as in url id is expected in api
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result; //storing the result json
                var data = JsonConvert.DeserializeObject<Student>(result); //convert jason string to single student
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            string data = JsonConvert.SerializeObject(std); //convert object to Json and store in string variable
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");    //convert into string content which converts json data into formatting text
            HttpResponseMessage response = client.PutAsync(url+std.Id, content).Result; // only accepts formatted string content and passing id in url (put)
            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student updated";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result; //concatenating id as in url id is expected in api
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result; //storing the result json
                var data = JsonConvert.DeserializeObject<Student>(result); //convert jason string to single student
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result; //concatenating id as in url id is expected in api
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result; //storing the result json
                var data = JsonConvert.DeserializeObject<Student>(result); //convert jason string to single student
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }
        [HttpPost,ActionName("Delete")]
        
        public IActionResult DeleteConfirmed(int id)
        {
            
            HttpResponseMessage response = client.DeleteAsync(url + id).Result; //concatenating id as in url id is expected in api (delete)
            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Student deleted";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

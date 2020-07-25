using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost("{menuItemid}")]
        public Cart Post(int menuItemid)
        {
            string token=GetToken("http://52.154.173.160//api/token");
            var cart =new Cart()
            {
                Id = 1,
                userId=1,
                menuItemId = menuItemid,
                menuItemName = getname(menuItemid,token)
            };
            return cart;
        }

        private string getname(int menuItemid,string token)
        {
            string name;
            using (var client=new HttpClient())
            {
                client.BaseAddress=new Uri("http://52.154.173.160/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/string"));
                HttpResponseMessage response = client.GetAsync("api/MenuItem/"+menuItemid).Result;
                if (response.IsSuccessStatusCode)
                {
                    name = response.Content.ReadAsStringAsync().Result;
                }
                else
                    name = null;
            }
            return name;
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        static string GetToken(string url)
        {
            var user = new User { Name = "admin", Password = "admin" };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {


                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                dynamic details = JObject.Parse(json: name);
                return details.token;
            }
        }


    }
}

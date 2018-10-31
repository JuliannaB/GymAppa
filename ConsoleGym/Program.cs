using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymApp;         // dodac w reference > add> gymapp <czy inny namespace z aplikacji webowej>
using System.Net.Http;
using System.Net.Http.Headers;
using GymApp.Models;

namespace ConsoleGym
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient myClient = new HttpClient();
            myClient.BaseAddress = new Uri("http://localhost:65345/");
            myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            DoRConsolaGym(myClient).Wait();
            DoConsoleCreateGym(myClient).Wait();
            ConsolaGymUpdate(myClient).Wait();
        }

        private static async Task DoConsoleCreateGym(HttpClient myClient)
        {
            Member g6 = new Member() { memberID = 1, memberName = "Ewcia", memberSurname = "Czus", joinDate = new DateTime(2014, 2, 12) }; //g6
            HttpResponseMessage response = await myClient.PostAsJsonAsync("api/Gym/", g6);   //dodac System.Net.Http.Formatting.Extension, nie inne gowno g6
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Movie {0} added", g6.memberName);   //g6. jakas refer
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
            }


        }

        private static async Task ConsolaGymUpdate (HttpClient myClient)
        {

        }

        private static async Task DoRConsolaGym(HttpClient myClient)
        {
            try
            {
                HttpResponseMessage response = await myClient.GetAsync("api/Gym");
                if (response.IsSuccessStatusCode)
                {
                    // read results 
                    var members = await response.Content.ReadAsAsync<IEnumerable<Member>>();
                    foreach (var member in members)
                    {
                        Console.WriteLine(member.memberID);
                    }
                }
                else
                {
                    Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GymApp.Models;
namespace GymApp.Controllers
{
    [RoutePrefix("Api/Gym")]
    public class GymController : ApiController
    {
        Entities memContext = new Entities();     //Entities kurwa mac, znajdziesz ta nazwe w GymModel.Context.cs
        // GET: api/Gym
        public IEnumerable<Member> Get()
        {
            //  return memContext.Members.ToList();   simple return
            return memContext.Members.ToList().OrderBy(p => p.joinDate);
            //posortowane wg daty dolaczenia. Potem tylko odpalic apke, dodac api/Gym i masz
        }

        // GET: api/Gym/5
        [Route("{id:int}")]
        public IEnumerable<Member> Get(int id)
        {
            return memContext.Members.Where(p => (p.memberID == id));
            //nie wiem co to daje. Kod sie zgadza ale nie dziala. Pewnie nie taka baza danych

        }

        // POST: api/Gym
        // wazne
        public HttpResponseMessage Post([FromBody]Member value)
        //  public void Post([FromBody]string value)
        {
            if (value == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to read input");
            }  // wazne
            var x = value.memberName;
            if (memContext.Members.Count(p => p.memberName.Equals(value.memberName)) != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Member " + value.memberName + " already in database.");
            }
            memContext.Members.Add(value); // wazne
            memContext.SaveChanges();// wazne
            return Request.CreateResponse(HttpStatusCode.Created, "Movie " + value.memberName + " added to database.");// wazne
            /* Dodalam w restlet: {
  "memberID": "7",
"memberName": "Krzys",
"memberSurname": "Moj",
"joinDate": "2013, 1, 1"
}
i dodalam nowy obiekt */
        }

        // PUT: api/Gym/5
        public HttpResponseMessage Put(int id, [FromBody]Member value) // wazne, szuka po id
        {
            if (value == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "Failed to read input");
            } // wazne

            var record = memContext.Members.SingleOrDefault(p => p.memberID == id);

            if (record == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Failed to find that Movie");
            }

            try
            {
                record.memberName = value.memberName;    // wazne
                record.memberSurname = value.memberSurname;    // wazne
                record.joinDate = value.joinDate;      // wazne
                memContext.SaveChanges();    // wazne
                return Request.CreateResponse(HttpStatusCode.OK, "Record updated");  // wazne
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Update operation failed with exception {0}", e.Message);
            }
        }

        // DELETE: api/Gym/5
        public void Delete(int id)
        {
        }
    }
}

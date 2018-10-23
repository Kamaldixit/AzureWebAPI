using Azure.model;
using Azure.dbconfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Azure.daoimpl;
using Azure.dao;


namespace MyApp1.Controllers
{
    public class ValuesController : ApiController
    {
        UserDetailsDAO dao = new UserDetailsImpl();

        [ActionName("List")]
        public IEnumerable<UserDetails> Get()
        {
            UserDetailsDAO impl = new UserDetailsImpl();
            IEnumerable<UserDetails> uall = impl.getAllUsers();

      

            return uall;
        }

        // GET api/values/5
        public UserDetails Get([FromUri]int id)
        {
            UserDetails u = new UserDetails();

            try
            {
                


                using (var context = new MyDbContext())
                {
                    u = (from n in context.UserDetails where n.UserId1 == id select n).Single();

                }



                return u;
            }

            catch(Exception e)
            {
              //  u.Name = "empty";
                return u;
            }
          
        }



        



        // POST api/values
        public HttpResponseMessage Post([FromBody]UserDetails user)
        {
             try
              {
                  using (var context = new MyDbContext())
                  {
                      context.UserDetails.Add(user);
                      context.SaveChanges();
                  }
                  var message = Request.CreateResponse(HttpStatusCode.Created, user);
                  return message;
              }
              catch (Exception e)
              {
                  return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
              }
          }


        // PUT api/values/5
        public HttpResponseMessage Put([FromBody]UserDetails user)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var update =
                            from n in context.UserDetails
                            where n.UserId1 == user.UserId1
                            select n;

                    foreach (UserDetails u in update)
                    {
                        u.UserId1 = user.UserId1;
                        u.Name = user.Name;
                        u.Age = user.Age;
                        u.UserAdress = user.UserAdress;
                        u.UserPassword = user.UserPassword;
                        u.UserMobile = user.UserMobile;
                        u.UserEmail = user.UserEmail;

                    }

                    context.SaveChanges();
                    
                }

                var message = Request.CreateResponse(HttpStatusCode.Created, user);
                return message;
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                UserDetails um = new UserDetails();
                using (var context = new MyDbContext())
                {
                    um = (from n in context.UserDetails where n.UserId1 == id select n).Single();
                    context.UserDetails.Remove(um);
                    context.SaveChanges();
                } 
                var message = Request.CreateResponse(HttpStatusCode.Created, id);
                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
                
        }
    }
}

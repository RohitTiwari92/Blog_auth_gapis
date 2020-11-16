using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Blog_auth.AuthorizationFilter
{
   
        [AttributeUsage(AttributeTargets.Class)]
        public class CustomAuthorizationFilter : AuthorizeAttribute
        {
            protected override bool IsAuthorized(HttpActionContext actionContext)
            {
                //if (!base.IsAuthorized(actionContext)) return false;
                //var isAuthorized = true;
                //var username = HttpContext.Current.User.Identity.Name;
                //isAuthorized = username == "AValidUsername";
                //return isAuthorized;

                IEnumerable<string> values;

                actionContext.Request.Headers.TryGetValues("Authorization", out values);
                if (values == null)
                {
                    return false;
                }
                if (values.Count() != 0)
                {
                    try
                    {
                        var validPayload = check(values.First());

                        if (validPayload == null)
                        {
                            return false;
                        }

                        if (string.IsNullOrEmpty(validPayload.Email))
                            return false;

                    if (checklogin(validPayload.Email))
                        return true;
                    else
                        return false;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }

        private bool checklogin(string email)
        {
            //do something with email or other information 
            throw new NotImplementedException();
        }

        GoogleJsonWebSignature.Payload check(string token)
            {
                GoogleJsonWebSignature.Payload data = GoogleJsonWebSignature.ValidateAsync(token).Result;

                return data;
            }
        }
    }

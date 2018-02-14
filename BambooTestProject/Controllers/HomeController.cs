using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace BambooTestProject.Controllers
{
    public class HomeController : Controller
    {
        private string GetPlanName()
        {
            var Url = "http://localhost:8085/rest/api/latest/result.json?os_authType=basic";
            var webRequest = System.Net.WebRequest.Create(Url);
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("a.tarasenko:123123a"));
                webRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var jsonResponse = sr.ReadToEnd();
                        dynamic json = JsonConvert.DeserializeObject(jsonResponse);
                        return json.results.result.plan.key;
                    }
                }
            }
            return "TES-10";

        }

        public ActionResult Index()
        {
            var Url = "http://localhost:8085/rest/api/latest/result/BAM-PN/branch/development.json?expand=results[-10:-1].result?os_authType=basic";
            try
            {
                var webRequest = System.Net.WebRequest.Create(Url);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("a.tarasenko:123123a"));
                    webRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
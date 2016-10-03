using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestZuckerbergEditor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Databaseinitialiserings-strategi slås av - ellers vil entity prøve å lage database hver gang vi kjører
            //Database.SetInitializer<TestZuckerbergEditor.Models.WebsiteContext>(null);
            Database.SetInitializer<TestZuckerbergEditor.Models.WebsiteContext>(new DropCreateDatabaseIfModelChanges<TestZuckerbergEditor.Models.WebsiteContext>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestZuckerbergEditor.Startup))]
namespace TestZuckerbergEditor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

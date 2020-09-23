using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EducationManagementSystem.Startup))]
namespace EducationManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

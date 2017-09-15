using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InsuranceProgram.Startup))]
namespace InsuranceProgram
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

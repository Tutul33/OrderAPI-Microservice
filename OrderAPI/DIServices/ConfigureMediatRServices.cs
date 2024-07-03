

namespace AdminService.DIServices
{
    public static class ConfigureMediatRServices
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        }
    }
}

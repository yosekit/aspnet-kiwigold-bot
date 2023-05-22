namespace KiwigoldBot.Extensions.Di
{
    public static class EndpointRouteBuilderExtensions
    {
        public static ControllerActionEndpointConventionBuilder MapBotWebhook<TController>(
            this IEndpointRouteBuilder endpoints, string route)
        {
            var controllerName = typeof(TController).Name.Replace("Controller", "", StringComparison.Ordinal);
            var actionName = "Update";

            return endpoints.MapControllerRoute(
                name: "telegrambot_webhook",
                pattern: route,
                defaults: new { controller = controllerName, action = actionName });
        }
    }
}

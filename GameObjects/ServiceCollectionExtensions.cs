namespace MauiBrickBreak.GameObjects
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
            services
                .AddSingleton<Paddle>()
                .AddSingleton<Ball>()
                .AddSingleton<GameManager>();
    }
}

using DryIoc;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;

namespace Ed.Steamflix.Api
{
    public class CompositionRoot
    {
        public CompositionRoot(IRegistrator r)
        {
            // Services
            r.Register<BroadcastService, BroadcastService>(Reuse.Singleton);
            r.Register<GameService, GameService>(Reuse.Singleton);
            r.Register<UserService, UserService>(Reuse.Singleton);

            // Repositories
            r.Register<IApiRepository, ApiRepository>(Reuse.Singleton);
            r.Register<ICommunityRepository, CommunityRepository>(Reuse.Singleton);
        }
    }
}

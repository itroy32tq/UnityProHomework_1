using GameEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public sealed class ResourcesSaveLoader : SaveLoader<ResourceService, List<ResourceData>>
    {
        public ResourcesSaveLoader(ResourceService service, IGameRepository gameRepository) : base(service, gameRepository)
        {
        }

        protected override List<ResourceData> ConvertToData(ResourceService service)
        {
            return service.GetResources().Select(x => new ResourceData(x)).ToList();
        }

        protected override void SetupData(ResourceService service, List<ResourceData> data)
        {
            var result = data.Join(service.GetResources(),
                x => x.Id,
                y => y.ID,
                (x, y) =>
                {
                    y.Amount = x.Amount;
                    return y;
                }).ToList();
        }
    }
}

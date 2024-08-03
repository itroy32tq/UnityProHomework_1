using GameEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public sealed class UnitsSaveLoader : SaveLoader<UnitManager, List<UnitData>>
    {
        public UnitsSaveLoader(UnitManager service, IGameRepository gameRepository) : base(service, gameRepository)
        {
        }

        protected override List<UnitData> ConvertToData(UnitManager service)
        {
            return service.GetAllUnits().Select(x => new UnitData(x)).ToList();
        }

        protected override void SetupData(UnitManager service, List<UnitData> data)
        {
            //тут не очень понятно что с загрузкой, нет поля для идентефикации, а класс юнита менять нельзя
            //видимо надо переспавнить всех юнитов обратно ?
            //по хэшу префаба не получается

            var result = data.Join(service.GetAllUnits(),
                x => x.HashId,
                y => y.GetHashCode(),
                (x, y) => 
                { 
                    y.HitPoints = x.HitPoints;
                    return y;
                }).ToList();
        }
    }
}

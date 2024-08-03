using GameEngine;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts
{
    public struct UnitData
    {
        [JsonProperty]
        public string Type { get;private set; }

        [JsonProperty]
        public int HitPoints { get; private set; }

        [JsonProperty]
        public Vector3 Position { get; private set; }

        [JsonProperty]
        public Vector3 Rotation { get; private set; }

        [JsonProperty]
        public int HashId { get; private set; }

        public UnitData(Unit unit)
        {
            Type = unit.Type;
            HitPoints = unit.HitPoints;
            Position = unit.Position;
            Rotation = unit.Rotation;   
            HashId = unit.gameObject.GetHashCode();
        }
    }
}

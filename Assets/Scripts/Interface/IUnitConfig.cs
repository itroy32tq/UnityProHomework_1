using UnityEngine;

namespace ShootEmUp
{
    public interface IUnitConfig
    {
        Transform FirePoint { get; }
        int HitPoints { get; }
        bool IsPlayer { get; }
        GameObject Prefab { get; }
        float Speed { get; }
    }
}
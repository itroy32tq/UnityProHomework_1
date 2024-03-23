using ShootEmUp;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private BulletSystem _bulletSystem;

        public event Action<Enemy> OnEnemyDieEvent;

        public HitPointsComponent EnemyHitComponent => GetComponent<HitPointsComponent>();

        

        public void OnEnemyDieHandler(Enemy enemy)
        {
            
        }

        public void OnEnemyFireHandler(Enemy enemy, Vector2 position, Vector2 direction)
        {
            
        }
    }
}

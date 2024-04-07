using ShootEmUp;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : MonoBehaviour
    {

        public event Action<Enemy> OnEnemyDieEvent;
        public event Action<Enemy> OnEnemyFireEvent;

        public void OnEnemyDieHandler(Enemy enemy)
        {
            
        }
        public void OnEnemyFireHandler(Enemy enemy)
        {
            
        }
    }
}

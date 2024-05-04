using Assets.Scripts.InfroStructure;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Installers
{
    public sealed class EnemyInstaller : Installer
    {
        [SerializeField] EnemyConfig _enemyConfig;


        public override void Install(DiContainer container)
        {
            container.AddService<EnemyConfig>(_enemyConfig);
        }
    }
}

using Assets.Scripts.InfroStructure;
using ShootEmUp;

namespace Assets.Scripts.Installers
{
    public sealed class EnemyAgentsInstaller : Installer
    {
        [Listener] private readonly EnemyAttackAgent _enemyAttackAgent = new();
        [Listener] private readonly EnemyMoveAgent _enemyMoveAgent = new();

        public override void Install(DiContainer container)
        {
            container.AddService<EnemyAttackAgent>(_enemyAttackAgent);
            container.AddService<EnemyMoveAgent>(_enemyMoveAgent);

        }
    }
}

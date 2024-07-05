using Assets.Scripts.Level;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.InfroStructure
{
    public sealed class PrefabInsataller : Installer
    {
        [SerializeField] private LevelBoundsConfig _levelBoundsConfig;
        private LevelBounds _levelBounds;

        [SerializeField] private LevelBackgroundConfig _levelBackgroundConfig;
        [Listener] private LevelBackground _levelBackground;

        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        private EnemySpawnerPositions _enemySpawnerPositions;

        [SerializeField] private Transform _worldContainer;

        public override void Install(DiContainer container)
        {
            CreateBorder();
            container.AddService<LevelBounds>(_levelBounds);

            CreateLevelBackground();
            container.AddService<LevelBackground>(_levelBackground);

            CreateSpawnerPositions();
            container.AddService<EnemySpawnerPositions>(_enemySpawnerPositions);
        }

        private void CreateSpawnerPositions()
        {
            int len = _enemySpawnerConfig.SpawnPositions.Length;
            Transform[] spawnPositions = new Transform[len];

            for (int i = 0; i < len; i++)
            {
                var spawnPos = Instantiate(_enemySpawnerConfig.SpawnPositions[i], _worldContainer);
                spawnPositions[i] = spawnPos;
            }

            len = _enemySpawnerConfig.AttackPositions.Length;
            Transform[] attackPositions = new Transform[len];

            for (int i = 0; i < len; i++)
            {
                var attackPos = Instantiate(_enemySpawnerConfig.AttackPositions[i], _worldContainer);
                attackPositions[i] = attackPos;
            }

            _enemySpawnerPositions = new EnemySpawnerPositions(spawnPositions, attackPositions);
        }

        private void CreateLevelBackground()
        {
            Transform back = Instantiate(_levelBackgroundConfig.LevelBackground).transform;
            _levelBackground = new LevelBackground(_levelBackgroundConfig, back);
        }

        private void CreateBorder()
        {
            Transform left = Instantiate(_levelBoundsConfig.LeftBorder);
            Transform right = Instantiate(_levelBoundsConfig.RightBorder);
            Transform top = Instantiate(_levelBoundsConfig.DownBorder);
            Transform bottom = Instantiate(_levelBoundsConfig.TopBorder);


            _levelBounds = new LevelBounds(left, right, top, bottom);
        }
    }
}
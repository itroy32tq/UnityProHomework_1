using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _character; 
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;


        
        private bool _fireRequired;
        public bool FireRequired { get => _fireRequired; set => _fireRequired = value; }

        private void OnEnable()
        {
            _character.GetComponent<HitPointsComponent>().HpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _character.GetComponent<HitPointsComponent>().HpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();

        private void FixedUpdate()
        {
            if (_fireRequired)
            {
                OnFlyBullet();
                _fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = _character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) _bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }
    }
}
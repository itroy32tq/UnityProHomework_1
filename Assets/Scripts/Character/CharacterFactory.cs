using Assets.Scripts.Interface;
using Assets.Scripts.Inventary;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterFactory : IFactory<Character>
    {
        private readonly Transform _container;
        private readonly GameObject _prefab;
        private readonly MoveComponent _moveComponent;
        private readonly WeaponComponent _weaponComponent;
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly InputManager _inputManager;
        private readonly CharacterConfig _config;
        private Transform _firePoint;
        private Rigidbody2D _rigidbody;

        public Action<IGameListener> OnCreateListener;

        public CharacterFactory(Transform container,
                                MoveComponent moveComponent,
                                WeaponComponent weaponComponent,
                                HitPointsComponent hitPointsComponent,
                                InputManager inputManager,
                                CharacterConfig config)
        {
            _container = container;
            _prefab = config.Prefab;
            _moveComponent = moveComponent;
            _weaponComponent = weaponComponent;
            _hitPointsComponent = hitPointsComponent;
            _inputManager = inputManager;
            _config = config;
        }

        public Character Create()
        {
            GameObject newPrefab = UnityEngine.Object.Instantiate(_prefab, _container);

            _firePoint = newPrefab.GetComponentInChildren<Transform>();
            _rigidbody = newPrefab.GetComponent<Rigidbody2D>();

            Character character = new(_moveComponent,
                                      _weaponComponent,
                                      _hitPointsComponent,
                                      _inputManager,
                                      _config,
                                      newPrefab,
                                      _rigidbody,
                                      _firePoint);

            
            OnCreateListener(character);
            return character;
        }
    }
}

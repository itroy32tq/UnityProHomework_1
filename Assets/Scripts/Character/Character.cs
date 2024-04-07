using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Character : Unit
    {
        [SerializeField] MoveComponent _moveComponent;
        
        [SerializeField] WeaponComponent _weaponComponent;
        [SerializeField] HitPointsComponent _hitPointsComponent;
        [SerializeField] TeamComponent _teamComponent;

        //todo по умолчанию игрок стреляет только вверх
        [SerializeField] Vector2 _direction = Vector2.up;

        public Action<Character> OnCharacterDie;

        private void OnEnable()
        {
            _hitPointsComponent.HpEmpty += Die;
        }
        public override void Move(Vector2 vector)
        {
            _moveComponent.MoveByRigidbodyVelocity(vector);
        }
        public void Die(GameObject character)
        {
            _hitPointsComponent.HpEmpty -= Die;
            OnCharacterDie?.Invoke(this);
        }
        public override void Shoot()
        {
            _weaponComponent.Shoot(_teamComponent.IsPlayer, _direction);
        }
        public bool IsHitPointsExists()
        { 
            return _hitPointsComponent.IsHitPointsExists();
        }
    }
}

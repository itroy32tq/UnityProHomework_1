using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Character : MonoBehaviour
    {
        [SerializeField] MoveComponent _moveComponent;
        [SerializeField] WeaponComponent _weaponComponent;
        [SerializeField] HitPointsComponent _hitPointsComponent;
        [SerializeField] TeamComponent _teamComponent;
        [SerializeField] Vector2 _direction = Vector2.up;

        public Action<Character> OnCharacterDie;

        private void OnEnable()
        {
            _hitPointsComponent.HpEmpty += Die;
        }

        private void OnDisable()
        {
            _hitPointsComponent.HpEmpty -= Die;
        }

        public void Move(Vector2 vector)
        {
            _moveComponent.Move(vector);
        }

        public void Die(GameObject character)
        {
            _hitPointsComponent.HpEmpty -= Die;
            OnCharacterDie?.Invoke(this);
        }

        public void Shoot()
        {
            _weaponComponent.Shoot(_teamComponent.IsPlayer, _direction);
        }

        public bool IsHitPointsExists()
        { 
            return _hitPointsComponent.IsHitPointsExists();
        }
    }
}

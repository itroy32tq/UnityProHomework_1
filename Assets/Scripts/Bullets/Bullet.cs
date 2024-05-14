using Assets.Scripts.Interface;
using System;
using UnityEngine;
using static ShootEmUp.BulletSystem;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour, IGamePauseListener, IGameResumeListener
    {
        private bool _isPlayer;
        private int _damage;
        private Vector2 _cashedVelocity;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Action<Bullet, Collision2D> OnBulletDestroyHandler;

        public void SetArgsToBullet(Args args)
        { 
            transform.position = args.Position;
            _spriteRenderer.color = args.Color;
            gameObject.layer = args.PhysicsLayer;
            _damage = args.Damage;
            _isPlayer = args.IsPlayer;
            _rigidbody2D.velocity = args.Velocity;
        }

        public void OnPauseGame()
        {
            _cashedVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void OnResumeGame()
        {
            _rigidbody2D.velocity = _cashedVelocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (_isPlayer == team.IsPlayer)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(_damage);
            }

            OnBulletDestroyHandler?.Invoke(this, collision);
        }
    }
}
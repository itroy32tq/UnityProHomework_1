using System;
using UnityEngine;
using static ShootEmUp.BulletSystem;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        private bool _isPlayer;
        private int _damage;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Action<Bullet, Collision2D> OnDestoy;

        public bool IsPlayer => _isPlayer;

        public void Construct(Args args)
        { 
            transform.position = args.position;
            _spriteRenderer.color = args.color;
            gameObject.layer = args.physicsLayer;
            _damage = args.damage;
            _isPlayer = args.isPlayer;
            _rigidbody2D.velocity = args.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(_damage);
            }

            OnDestoy?.Invoke(this, collision);
        }
    }
}
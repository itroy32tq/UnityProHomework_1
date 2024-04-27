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
        private Vector2 _cashedvelocity;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Action<Bullet, Collision2D> OnDesrtoyHandler;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void Construct(Args args)
        { 
            transform.position = args.position;
            _spriteRenderer.color = args.color;
            gameObject.layer = args.physicsLayer;
            _damage = args.damage;
            _isPlayer = args.isPlayer;
            _rigidbody2D.velocity = args.velocity;
        }

        public void OnPauseGame()
        {
            _cashedvelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void OnResumeGame()
        {
            _rigidbody2D.velocity = _cashedvelocity;
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

            OnDesrtoyHandler?.Invoke(this, collision);
        }
    }
}
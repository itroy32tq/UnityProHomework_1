using System;
using System.Drawing;
using UnityEngine;
using static ShootEmUp.BulletSystem;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] private bool _isPlayer;
        [NonSerialized] public int _damage;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

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
            OnCollisionEntered?.Invoke(this, collision);
        }
    }
}
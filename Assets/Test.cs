using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using ShootEmUp;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _initialCount = 50;
    [SerializeField] private float _countdown;

    private float _currentTime;

    private Pool<Bullet> _bulletPool;
    private Factory<Bullet> _bulletFactory;

    private void Awake()
    {
        _bulletFactory = new Factory<Bullet>(_prefab, _container);
        _bulletPool = new Pool<Bullet>(_initialCount, _bulletFactory);

    }
    private void Update()
    {
       
        _currentTime -= Time.fixedDeltaTime;
        if (_currentTime <= 0)
        {
            var bullet = _bulletPool.TryGet();
            bullet.transform.position = Vector3.zero;
            _currentTime += _countdown;
        }
    }
}

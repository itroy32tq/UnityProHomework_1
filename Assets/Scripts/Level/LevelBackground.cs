using Assets.Scripts.Interface;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;
        private Transform _myTransform;

        private void Awake()
        {
            IGameListener.Register(this);
        }
        public void OnStartGame()
        {
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }


        private void FixedUpdate()
        {
            if (_myTransform == null)
            {
                return;
            }

            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }

        public void OnFinishGame()
        {
            _myTransform = null;
        }
    }
}
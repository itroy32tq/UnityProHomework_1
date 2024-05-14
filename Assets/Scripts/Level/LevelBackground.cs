using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using Assets.Scripts.Level;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : IGameStartListener, IGameFinishListener, IGameFixedUpdateListener
    {
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        private float _positionX;
        private float _positionZ;
        private Transform _myTransform;


        [Inject]
        public void Construct(LevelBackgroundConfig config)
        {
            _myTransform = config.LevelBackground.transform;
            _startPositionY = config.StartPositionY;
            _endPositionY = config.EndPositionY;
            _movingSpeedY = config.MovingSpeedY;
        }
        public void OnStartGame()
        {
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void OnFinishGame()
        {
            _myTransform = null;
        }

        public void OnFixedUpdate(float deltatime)
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
    }
}
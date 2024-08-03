using Assets.Scripts.Interface;
using Assets.Scripts.Level;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : IGameStartListener, IGameFinishListener, IGameFixedUpdateListener
    {
        private readonly float _startPositionY;
        private readonly float _endPositionY;
        private readonly float _movingSpeedY;

        private float _positionX;
        private float _positionZ;
        private Transform _myTransform;

        public LevelBackground(LevelBackgroundConfig config, Transform back)
        {
            
            _myTransform = back.transform;
            _startPositionY = config.StartPositionY;
            _endPositionY = config.EndPositionY;
            _movingSpeedY = config.MovingSpeedY;
        }

        public void OnStartGame()
        {
            Vector3 position = _myTransform.position;
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
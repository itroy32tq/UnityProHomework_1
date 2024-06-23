using Assets.Scripts.InfroStructure;
using Assets.Scripts.Level;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        private Transform _leftBorder;
        private Transform _rightBorder;
        private Transform _downBorder;
        private Transform _topBorder;

        [Inject]
        public void Construct(LevelBoundsConfig config)
        {
            
            _leftBorder = Object.Instantiate(config.LeftBorder);
            _rightBorder = Object.Instantiate(config.RightBorder);
            _downBorder = Object.Instantiate(config.DownBorder);
            _topBorder = Object.Instantiate(config.TopBorder);
        }

        public bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > _leftBorder.position.x
                   && positionX < _rightBorder.position.x
                   && positionY > _downBorder.position.y
                   && positionY < _topBorder.position.y;
        }
    }
}
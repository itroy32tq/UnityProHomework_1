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
            _leftBorder = config.LeftBorder;
            _rightBorder = config.RightBorder;
            _downBorder = config.DownBorder;
            _topBorder = config.TopBorder;
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
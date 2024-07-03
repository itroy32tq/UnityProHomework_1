using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        private readonly Transform _leftBorder;
        private readonly Transform _rightBorder;
        private readonly Transform _downBorder;
        private readonly Transform _topBorder;

        public LevelBounds(Transform leftBorder, Transform rightBorder, Transform downBorder, Transform topBorder)
        {
            _leftBorder = leftBorder;
            _rightBorder = rightBorder;
            _downBorder = downBorder;
            _topBorder = topBorder;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public abstract class Unit : MonoBehaviour, ICanBeShoot, ICanBeMove
    {
        public abstract void Move(Vector2 vector);

        public abstract void Shoot();
    }
}

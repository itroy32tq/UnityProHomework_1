using UnityEngine;

namespace Assets.Scripts.InfroStructure
{
    public abstract class Installer : MonoBehaviour
    {
        public abstract void Install(DiContainer container);
    }
}

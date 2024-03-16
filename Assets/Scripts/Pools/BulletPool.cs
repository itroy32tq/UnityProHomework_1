using ShootEmUp;

namespace Assets.Scripts.GenericPool
{
    public class BulletPool : Pool<Bullet>
    {
        public BulletPool(Bullet pref, int size) : base(pref, size)
        {
        }
    }
}

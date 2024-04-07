using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Conditions
{
    public class CompositeCondition
    {
        private readonly List<Func<bool>> _conditions = new();
        public void Append(Func<bool> func)
        { 
            _conditions.Add(func);
        }
        public bool IsTrue()
        { 
            return _conditions.All(x => true);
        }
    }
}

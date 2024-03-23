using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Conditions
{
    public class CompositeCondition
    {
        private List<Func<bool>> _conditions = new();
        public Func<> Append(Func<bool> func)
        { 
            Func t = _conditions.Add(func);
            return _conditions.Add(func);
        }
        public bool IsTrue()
        { 
            return _conditions.All(x => true);
        }
    }
}

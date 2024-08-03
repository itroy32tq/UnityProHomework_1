using JetBrains.Annotations;
using System;

namespace Assets.Scripts.InfroStructure
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
    public sealed class InjectAttribute : Attribute
    {

    }
    
}

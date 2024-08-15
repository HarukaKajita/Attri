using UnityEngine;

namespace Attri.Runtime
{
    public abstract class AttributeAsset : ScriptableObject
    {
        public abstract IAttribute GetAttribute();
    }
}

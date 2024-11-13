using System;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public abstract class ElementBase
    {
        // ElementBase
        public int ComponentCount() => Dimension();
        public virtual int[] ComponentsAsInt() => throw new NotImplementedException();
        public virtual float[] ComponentsAsFloat() => throw new NotImplementedException();
        public virtual string[] ComponentsAsString() => throw new NotImplementedException();
        
        // IDataProvider
        public virtual int Dimension()
        {
            return GetAttributeType() switch
            {
                AttributeDataType.Float => ComponentsAsFloat().Length,
                AttributeDataType.Int => ComponentsAsInt().Length,
                AttributeDataType.String => ComponentsAsString().Length,
                _ => throw new System.NotImplementedException()
            };
        }
        public virtual AttributeDataType GetAttributeType() => throw new System.NotImplementedException();
        // [frame][element][component]
        public virtual float[][][] AsFloat() => throw new System.NotImplementedException();
        public virtual int[][][] AsInt() => throw new System.NotImplementedException();
        public virtual string[][][] AsString() => throw new System.NotImplementedException();
    }
}

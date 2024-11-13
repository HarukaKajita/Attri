using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Attri.Runtime
{
    public class ContainerBase :ScriptableObject
    {
        [SerializeField, DisableOnInspector]
        protected AttributeDataType attributeDataType;
        public AttributeDataType GetAttributeType() => attributeDataType;
        
        public virtual int ElementCount() => throw new NotImplementedException();
        public virtual int[][] ElementsAsInt() => throw new NotImplementedException();
        public virtual float[][] ElementsAsFloat() => throw new NotImplementedException();
        public virtual string[][] ElementsAsString() => throw new NotImplementedException();
        
        public virtual float[][][] AsFloat() => throw new NotImplementedException();
        public virtual int[][][] AsInt() => throw new NotImplementedException();
        public virtual string[][][] AsString() => throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using Attri.Runtime;

namespace Attri.Editor
{
    [Serializable]
    public abstract class AttributeImportProcessor : ImportProcessor
    {
        internal List<IAttribute> attributes = new();
        
        public List<IAttribute> GetAttributes() => attributes;
        public void SetAttributes(List<IAttribute> attributes)
        {
            this.attributes = attributes;
        }
        protected AttributeImportProcessor(string prefix) : base(prefix) { }
        
        
    }
}
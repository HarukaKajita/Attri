using System;
using Attri.Runtime;

namespace Attri.Editor
{
    [Serializable]
    public abstract class AttributeImportProcessor : ImportProcessor
    {
        protected AttributeImportProcessor(string prefix) : base(prefix) { }
    }
}
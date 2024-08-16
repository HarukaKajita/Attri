using System;
using Attri.Runtime;

namespace Attri.Editor
{
    [Serializable]
    public abstract class CsvImportProcessor : ImportProcessor
    {
        protected CsvImportProcessor(string prefix) : base(prefix) { }
    }
}
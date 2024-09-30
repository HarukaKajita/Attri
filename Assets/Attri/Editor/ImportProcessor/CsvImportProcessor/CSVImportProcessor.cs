using System;
using Attri.Runtime;
using UnityEngine;

namespace Attri.Editor
{
    [Serializable]
    public abstract class CsvImportProcessor : ImportProcessor
    {
        [SerializeField] internal bool skipFirstLine = false;
        protected CsvImportProcessor(string prefix) : base(prefix) { }
    }
}
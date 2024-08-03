using System.Collections.Generic;
using UnityEngine;

namespace Attri.Runtime
{
    #region Common
    public class ArrayScriptableObject<T> : ScriptableObject
    {
        public List<T> values = new ();
    }
    public class Array2DScriptableObject<T> : ScriptableObject
    {
        public List<List<T>> values = new ();
    }
    #endregion
}

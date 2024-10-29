using UnityEngine;

namespace Attri.Runtime
{
    public interface IDataProvider
    {
        public int Dimension();
        public AttributeDataType GetAttributeType();
        public float[][] AsFloat();
        public int[][] AsInt();
        public string[][] AsString();
        public ScriptableObject GetScriptableObject();
    }
}

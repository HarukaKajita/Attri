using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Container), menuName = "Attri/CSV/Container", order = 204)]
    public class Container : ScriptableObject, IDataProvider
    {
        [SerializeReference, DisableOnInspector]
        private List<ElementBase> elements = new ();
        [SerializeField, DisableOnInspector]
        private AttributeDataType attributeDataType;
        
        public int ElementCount() => elements.Count;
        public void SetValues(float[][] values)
        {
            elements.Clear();
            foreach (var value in values)
                elements.Add((FloatElement)value);
            attributeDataType = AttributeDataType.Float;
        }

        public void SetValues(int[][] values)
        {
            elements.Clear();
            foreach (var value in values)
                elements.Add((IntElement)value);
            attributeDataType = AttributeDataType.Int;
        }

        public void SetValues(string[][] values)
        {
            elements.Clear();
            foreach (var value in values)
                elements.Add((StringElement)value);
            attributeDataType = AttributeDataType.String;
        }
        
        public int[][] ElementsAsInt() => elements.Select(e => e.ComponentsAsInt()).ToArray();
        public float[][] ElementsAsFloat() => elements.Select(e => e.ComponentsAsFloat()).ToArray();
        public string[][] ElementsAsString() => elements.Select(e => e.ComponentsAsString()).ToArray();
        
        
        #region IDataProvider
        public AttributeDataType GetAttributeType() => attributeDataType;
        public float[][][] AsFloat() => new []{elements.Select(e => e.ComponentsAsFloat()).ToArray()};
        public int[][][] AsInt() => new []{elements.Select(e => e.ComponentsAsInt()).ToArray()};
        public string[][][] AsString() => new []{elements.Select(e => e.ComponentsAsString()).ToArray()};
        public ScriptableObject GetScriptableObject() => this;
        #endregion
    }
}

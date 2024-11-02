using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Container), menuName = "Attri/CSV/Container", order = 204)]
    public class Container : ScriptableObject, IDataProvider
    {
        public string attributeName;
        
        [SerializeReference]
        private List<ElementBase> elements = new ();
        [SerializeField]
        private bool isVariableLengthAttribute;
        [SerializeField]
        private AttributeDataType attributeDataType;
        [SerializeField]
        private int size=0;

        public int ElementCount() => elements.Count;
        public void SetValues(float[][] values)
        {
            elements.Clear();
            var isVariableLength = false;
            var attributeDimension = values[0].Length;
            foreach (var value in values)
            {
                elements.Add((FloatElement)value);
                isVariableLength |= value.Length != attributeDimension;
            }
            attributeDataType = AttributeDataType.Float;
            if (isVariableLength) size = -1;
            else size = attributeDimension;
            if (isVariableLength) isVariableLengthAttribute = true;
        }

        public void SetValues(int[][] values)
        {
            elements.Clear();
            var isVariableLength = false;
            var attributeDimension = values[0].Length;
            foreach (var value in values)
            {
                elements.Add((IntElement)value);
                isVariableLength |= value.Length != attributeDimension;
            }
            attributeDataType = AttributeDataType.Int;
            if (isVariableLength) size = -1;
            else size = attributeDimension;
            if (isVariableLength) isVariableLengthAttribute = true;
        }

        public void SetValues(string[][] values)
        {
            elements.Clear();
            var isVariableLength = false;
            var attributeDimension = values[0].Length;
            foreach (var value in values)
            {
                elements.Add((StringElement)value);
                isVariableLength |= value.Length != attributeDimension;
            }
            attributeDataType = AttributeDataType.String;
            if (isVariableLength) size = -1;
            else size = attributeDimension;
            if (isVariableLength) isVariableLengthAttribute = true;
        }
        
        public int[][] ElementsAsInt() => elements.Select(e => e.ComponentsAsInt()).ToArray();
        public float[][] ElementsAsFloat() => elements.Select(e => e.ComponentsAsFloat()).ToArray();
        public string[][] ElementsAsString() => elements.Select(e => e.ComponentsAsString()).ToArray();
        
        
        #region IDataProvider
        public int Dimension() => size;
        public AttributeDataType GetAttributeType() => attributeDataType;
        public float[][][] AsFloat() => new []{elements.Select(e => e.ComponentsAsFloat()).ToArray()};
        public int[][][] AsInt() => new []{elements.Select(e => e.ComponentsAsInt()).ToArray()};
        public string[][][] AsString() => new []{elements.Select(e => e.ComponentsAsString()).ToArray()};
        public ScriptableObject GetScriptableObject() => this;
        #endregion
    }
}

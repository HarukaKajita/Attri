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
        private List<IDataProvider> elements = new ();
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
        
        #region IDataProvider
        public int Dimension() => size;
        public AttributeDataType GetAttributeType() => attributeDataType;
        public float[][] AsFloat() => elements.SelectMany(e => e.AsFloat()).ToArray();
        public int[][] AsInt() => elements.SelectMany(e => e.AsInt()).ToArray();
        public string[][] AsString() => elements.SelectMany(e => e.AsString()).ToArray();
        public ScriptableObject GetScriptableObject() => this;
        #endregion
    }
}

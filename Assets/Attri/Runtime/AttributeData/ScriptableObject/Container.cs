using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Attri.Runtime
{
    #region Common
    [CreateAssetMenu(fileName = nameof(Container), menuName = "Attri/CSV/Container", order = 204)]
    public class Container : ScriptableObject
    {
        public string attributeName;
        
        [SerializeReference]
        private List<IElement> elements = new ();
        [SerializeField]
        private bool isArrayAttribute;
        [SerializeField]
        private string attributeTypeName;
        [SerializeField]
        private int size=0;
        public int Size()
        {
            return size;
        }
        public int ElementCount()
        {
            return elements.Count;
        }
        public Type GetAttributeType()
        {
            if(attributeTypeName == typeof(float).ToString()) return typeof(float);
            if(attributeTypeName == typeof(int).ToString()) return typeof(int);
            if(attributeTypeName == typeof(string).ToString()) return typeof(string);
            else return null;
        }
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
            attributeTypeName = typeof(float).ToString();
            if (isVariableLength) size = -1;
            else size = attributeDimension;
            if (isVariableLength) isArrayAttribute = true;
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
            attributeTypeName = typeof(int).ToString();
            if (isVariableLength) size = -1;
            else size = attributeDimension;
            if (isVariableLength) isArrayAttribute = true;
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
            attributeTypeName = typeof(string).ToString();
            if (isVariableLength) size = -1;
            else size = attributeDimension;
            if (isVariableLength) isArrayAttribute = true;
        }
    }
    #endregion
}

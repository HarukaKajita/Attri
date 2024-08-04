using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Attri.Runtime
{
    public abstract class AttributeAssetBase : ScriptableObject
    {
        public AttributeType attributeType = AttributeType.String;
        public ushort dimension = 0;
        protected virtual void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            name = packedAttribute.name;
            attributeType = packedAttribute.attributeType;
            dimension = packedAttribute.dimension;
        }
        
        public static ScriptableObject CreateInstance(PackedAttribute packedAttribute)
        {
            var instance = CreateInstance(packedAttribute.attributeType, packedAttribute.dimension);
            instance.SetFromPackedAttribute(packedAttribute);
            return instance;
        }

        static AttributeAssetBase CreateInstance(AttributeType attributeType, ushort dimension)
        {
            var attributeAssetTypes = TypeCache.GetTypesWithAttribute<AttributeTypeAttribute>();
            var matchedType = attributeAssetTypes
                .Where(x => x.IsSubclassOf(typeof(AttributeAssetBase)))
                .FirstOrDefault(
                    x =>
                        x.GetCustomAttribute<AttributeTypeAttribute>().attributeType == attributeType &&
                        x.GetCustomAttribute<AttributeTypeAttribute>().dimension == dimension);
            return (AttributeAssetBase)CreateInstance(matchedType);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;
using MessagePack.Resolvers;
using MessagePack.Unity.Extension;

namespace Attri.Runtime
{
    public class AttributeSerializer
    {
        static bool isInitialized = false;
        private static MessagePackSerializerOptions options;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;
            var resolver = CompositeResolver.Create(
                Attri.Runtime.Resolvers.GeneratedResolver.Instance,
                UnityBlitResolver.Instance,
                StandardResolverAllowPrivate.Instance
            );
            options = MessagePackSerializerOptions.Standard.WithResolver(resolver);
        }
        
        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        static void EditorInitialize()
        {
            Initialize();
        }
        #endif
        
        public static byte[] Serialize(AttributeBase obj)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.Serialize(obj, options);
        }
        public static AttributeBase Deserialize(byte[] bytes)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.Deserialize<AttributeBase>(bytes, options);
        }
        public static AttributeBase[] DeserializeAsArray(byte[] bytes)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.Deserialize<AttributeBase[]>(bytes, options);
        }
        public static string ConvertToJson(byte[] bytes)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.ConvertToJson(bytes, options);
        }
        public static byte[] ConvertFromJson(string json)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.ConvertFromJson(json, options);
        }
    }
}

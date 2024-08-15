using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using MessagePack;
using MessagePack.Resolvers;
using MessagePack.Unity.Extension;

namespace Attri.Runtime
{
    public class AttributeSerializer
    {
        static bool isInitialized = false;
        internal static MessagePackSerializerOptions options;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;
            
            var resolver = CompositeResolver.Create(
                Resolvers.GeneratedResolver.Instance,
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
        
        public static byte[] Serialize(IAttribute obj)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.Serialize(obj, options);
        }
        public static byte[] Serialize(IEnumerable<IAttribute> obj)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.Serialize(obj, options);
        }
        public static IAttribute Deserialize(byte[] bytes)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.Deserialize<IAttribute>(bytes, options);
        }
        public static IEnumerable<IAttribute> DeserializeAsArray(byte[] bytes)
        {
            if (!isInitialized) Initialize();
            return MessagePackSerializer.Deserialize<IAttribute[]>(bytes, options);
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

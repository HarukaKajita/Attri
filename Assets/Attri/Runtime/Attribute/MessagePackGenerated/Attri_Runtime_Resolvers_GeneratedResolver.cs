// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1649 // File name should match first type name

namespace Attri.Runtime.Resolvers
{
    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        private GeneratedResolver()
        {
        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    Formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(35)
            {
                { typeof(global::Attri.Runtime.FrameData<bool>), 0 },
                { typeof(global::Attri.Runtime.FrameData<float>), 1 },
                { typeof(global::Attri.Runtime.FrameData<global::UnityEngine.Vector2>), 2 },
                { typeof(global::Attri.Runtime.FrameData<global::UnityEngine.Vector2Int>), 3 },
                { typeof(global::Attri.Runtime.FrameData<global::UnityEngine.Vector3>), 4 },
                { typeof(global::Attri.Runtime.FrameData<global::UnityEngine.Vector3Int>), 5 },
                { typeof(global::Attri.Runtime.FrameData<int>), 6 },
                { typeof(global::Attri.Runtime.FrameData<string>), 7 },
                { typeof(global::System.Collections.Generic.List<bool>), 8 },
                { typeof(global::System.Collections.Generic.List<float>), 9 },
                { typeof(global::System.Collections.Generic.List<global::Attri.Runtime.FrameData<bool>>), 10 },
                { typeof(global::System.Collections.Generic.List<global::Attri.Runtime.FrameData<float>>), 11 },
                { typeof(global::System.Collections.Generic.List<global::Attri.Runtime.FrameData<global::UnityEngine.Vector2>>), 12 },
                { typeof(global::System.Collections.Generic.List<global::Attri.Runtime.FrameData<global::UnityEngine.Vector2Int>>), 13 },
                { typeof(global::System.Collections.Generic.List<global::Attri.Runtime.FrameData<global::UnityEngine.Vector3>>), 14 },
                { typeof(global::System.Collections.Generic.List<global::Attri.Runtime.FrameData<global::UnityEngine.Vector3Int>>), 15 },
                { typeof(global::System.Collections.Generic.List<global::Attri.Runtime.FrameData<int>>), 16 },
                { typeof(global::System.Collections.Generic.List<global::Attri.Runtime.FrameData<string>>), 17 },
                { typeof(global::System.Collections.Generic.List<global::UnityEngine.Vector2>), 18 },
                { typeof(global::System.Collections.Generic.List<global::UnityEngine.Vector2Int>), 19 },
                { typeof(global::System.Collections.Generic.List<global::UnityEngine.Vector3>), 20 },
                { typeof(global::System.Collections.Generic.List<global::UnityEngine.Vector3Int>), 21 },
                { typeof(global::System.Collections.Generic.List<int>), 22 },
                { typeof(global::System.Collections.Generic.List<string>), 23 },
                { typeof(global::Attri.Runtime.AttributeType), 24 },
                { typeof(global::UnityEngine.HideFlags), 25 },
                { typeof(global::Attri.Runtime.AttributeAssetBase), 26 },
                { typeof(global::Attri.Runtime.BoolAttributeAssetBase), 27 },
                { typeof(global::Attri.Runtime.FloatAttributeAssetBase), 28 },
                { typeof(global::Attri.Runtime.IntegerAttributeAssetBase), 29 },
                { typeof(global::Attri.Runtime.StringAttributeAssetBase), 30 },
                { typeof(global::Attri.Runtime.Vector2AttributeAssetBase), 31 },
                { typeof(global::Attri.Runtime.Vector2IntAttributeAssetBase), 32 },
                { typeof(global::Attri.Runtime.Vector3AttributeAssetBase), 33 },
                { typeof(global::Attri.Runtime.Vector3IntAttributeAssetBase), 34 },
            };
        }

        internal static object GetFormatter(global::System.Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new Attri.Runtime.Formatters.Attri.Runtime.FrameDataFormatter<bool>();
                case 1: return new Attri.Runtime.Formatters.Attri.Runtime.FrameDataFormatter<float>();
                case 2: return new Attri.Runtime.Formatters.Attri.Runtime.FrameDataFormatter<global::UnityEngine.Vector2>();
                case 3: return new Attri.Runtime.Formatters.Attri.Runtime.FrameDataFormatter<global::UnityEngine.Vector2Int>();
                case 4: return new Attri.Runtime.Formatters.Attri.Runtime.FrameDataFormatter<global::UnityEngine.Vector3>();
                case 5: return new Attri.Runtime.Formatters.Attri.Runtime.FrameDataFormatter<global::UnityEngine.Vector3Int>();
                case 6: return new Attri.Runtime.Formatters.Attri.Runtime.FrameDataFormatter<int>();
                case 7: return new Attri.Runtime.Formatters.Attri.Runtime.FrameDataFormatter<string>();
                case 8: return new global::MessagePack.Formatters.ListFormatter<bool>();
                case 9: return new global::MessagePack.Formatters.ListFormatter<float>();
                case 10: return new global::MessagePack.Formatters.ListFormatter<global::Attri.Runtime.FrameData<bool>>();
                case 11: return new global::MessagePack.Formatters.ListFormatter<global::Attri.Runtime.FrameData<float>>();
                case 12: return new global::MessagePack.Formatters.ListFormatter<global::Attri.Runtime.FrameData<global::UnityEngine.Vector2>>();
                case 13: return new global::MessagePack.Formatters.ListFormatter<global::Attri.Runtime.FrameData<global::UnityEngine.Vector2Int>>();
                case 14: return new global::MessagePack.Formatters.ListFormatter<global::Attri.Runtime.FrameData<global::UnityEngine.Vector3>>();
                case 15: return new global::MessagePack.Formatters.ListFormatter<global::Attri.Runtime.FrameData<global::UnityEngine.Vector3Int>>();
                case 16: return new global::MessagePack.Formatters.ListFormatter<global::Attri.Runtime.FrameData<int>>();
                case 17: return new global::MessagePack.Formatters.ListFormatter<global::Attri.Runtime.FrameData<string>>();
                case 18: return new global::MessagePack.Formatters.ListFormatter<global::UnityEngine.Vector2>();
                case 19: return new global::MessagePack.Formatters.ListFormatter<global::UnityEngine.Vector2Int>();
                case 20: return new global::MessagePack.Formatters.ListFormatter<global::UnityEngine.Vector3>();
                case 21: return new global::MessagePack.Formatters.ListFormatter<global::UnityEngine.Vector3Int>();
                case 22: return new global::MessagePack.Formatters.ListFormatter<int>();
                case 23: return new global::MessagePack.Formatters.ListFormatter<string>();
                case 24: return new Attri.Runtime.Formatters.Attri.Runtime.AttributeTypeFormatter();
                case 25: return new Attri.Runtime.Formatters.UnityEngine.HideFlagsFormatter();
                case 26: return new Attri.Runtime.Formatters.Attri.Runtime.AttributeAssetBaseFormatter();
                case 27: return new Attri.Runtime.Formatters.Attri.Runtime.BoolAttributeAssetBaseFormatter();
                case 28: return new Attri.Runtime.Formatters.Attri.Runtime.FloatAttributeAssetBaseFormatter();
                case 29: return new Attri.Runtime.Formatters.Attri.Runtime.IntegerAttributeAssetBaseFormatter();
                case 30: return new Attri.Runtime.Formatters.Attri.Runtime.StringAttributeAssetBaseFormatter();
                case 31: return new Attri.Runtime.Formatters.Attri.Runtime.Vector2AttributeAssetBaseFormatter();
                case 32: return new Attri.Runtime.Formatters.Attri.Runtime.Vector2IntAttributeAssetBaseFormatter();
                case 33: return new Attri.Runtime.Formatters.Attri.Runtime.Vector3AttributeAssetBaseFormatter();
                case 34: return new Attri.Runtime.Formatters.Attri.Runtime.Vector3IntAttributeAssetBaseFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1649 // File name should match first type name
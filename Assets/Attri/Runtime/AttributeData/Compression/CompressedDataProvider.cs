using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Attri.Runtime
{
    public abstract class CompressedDataProvider : IDataProvider
    {
        // Decodeに必要な任意のパラメータを保持
        public Dictionary<string, int> _intParams = new ();
        protected Dictionary<string, float> _floatParams = new ();
        // 圧縮タイプ
        protected CompressionType _compressionType;
        // 圧縮されたデータ[frame][element][component]
        protected ValueByte[][][] _compressedValueBytes = null;
        // 元の値の型
        protected AttributeDataType _attributeType;
        
        public CompressedDataProvider(){}
        public CompressedDataProvider(AttributeDataType attributeType , CompressionType compressionType, ValueByte[][][] compressedValueBytes)
        {
            _compressedValueBytes = compressedValueBytes;
            _compressionType = compressionType;
            _attributeType = attributeType;
        }

        public void Init(AttributeDataType attributeType , CompressionType compressionType, ValueByte[][][] compressedValueBytes)
        {
            _compressedValueBytes = compressedValueBytes;
            _compressionType = compressionType;
            _attributeType = attributeType;
        }
        
        public virtual int Dimension()
        {
            // 雑な実装
            // 0番目のデータの成分の数を返す
            return _compressedValueBytes[0].Length;
        }

        public virtual AttributeDataType GetAttributeType() => _attributeType;

        public virtual float[][][] AsFloat()
        {
            throw new System.NotImplementedException();
        }

        public virtual int[][][] AsInt()
        {
            throw new System.NotImplementedException();
        }

        public virtual string[][][] AsString()
        {
            throw new System.NotImplementedException();
        }

        public virtual ScriptableObject GetScriptableObject()
        {
            throw new System.NotImplementedException();
        }
        
        
#if UNITY_EDITOR
        #region EditorOnly
        [Header("Editor Only Compression Setting")]
        [SerializeField] private DefaultAsset srcDataDirectory;
        [SerializeField] private CompressionType dstCompressionType;
        // 圧縮に必要な任意のパラメータを保持
        /*
         * 値列とパラメータを受け取り、圧縮した値列を生成する
         * 圧縮した値列をbyteで保持する
         * パラメータを辞書に追加する
         * 
         */
        #endregion

#endif
    }
}

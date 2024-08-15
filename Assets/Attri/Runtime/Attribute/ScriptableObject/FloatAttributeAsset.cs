using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(FloatAttributeAsset), menuName = "Attri/Attribute/Float", order = 0)]
    public class FloatAttributeAsset : ScriptableObject
    {
        [SerializeField] private FloatAttribute _attribute = new ();
        [ContextMenu("SerializeTest")]
        private void SerializeTest()
        {
            _attribute.SerializeTest();
        }
    }
}

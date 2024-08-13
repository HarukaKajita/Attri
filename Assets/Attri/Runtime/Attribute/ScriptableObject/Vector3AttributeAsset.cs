using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector3AttributeAsset), menuName = "Attri/Attribute/Vector3", order = 0)]
    public class Vector3AttributeAsset : ScriptableObject
    {
        [SerializeField] private Vector3Attribute _attribute = new ();
    }
}

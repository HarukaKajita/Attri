using System;
using UnityEngine;

namespace Attri.Editor
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ProcessorSelectorAttribute : PropertyAttribute { }
}
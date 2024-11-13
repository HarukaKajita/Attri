using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Attri.Runtime
{
	public class CompressedContainer : ContainerBase
	{
		[SerializeField,DisableOnInspector] internal CompressionType compressionType = CompressionType.UnCompressed;
		// [component]
		internal CompressionParams[] CompressionParams;
		
		[SerializeReference, DisableOnInspector]
		protected List<CompressedElementBase> elements = new ();

		private CompressorBase _compressor;
		
		public void SetCompressionParams(CompressionParams[] compressionParams)
		{
			CompressionParams = compressionParams;
			foreach (var e in elements)
			{
				e.SetCompressionParams(compressionParams);
			}
		}
		public int[][] ElementsAsInt()
		{
			var elementCount = elements.Count;
			for (var i = 0; i < elementCount; i++)
			{
				var e = elements[i];
				e.ComponentsAsInt();
			}
			
		}

		public float[][] ElementsAsFloat() => elements.Select(e => e.ComponentsAsFloat()).ToArray();
		public string[][] ElementsAsString() => elements.Select(e => e.ComponentsAsString()).ToArray();
	}
}
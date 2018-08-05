using System.Collections.Generic;
using System.Diagnostics;

namespace ZeroWorldStats.Modules
{
	public class ReqChunk
	{
		/// <summary>
		/// Name of the chunk.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Header byte align value.
		/// </summary>
		public string Align { get; set; }

		/// <summary>
		/// Chunk's platform designation.
		/// </summary>
		public string Platform { get; set; }

		/// <summary>
		/// List of files within the chunk.
		/// </summary>
		public List<string> Contents { get; set; }

		public void AddContents(string file)
		{
			if (Contents == null)
			{
				Contents = new List<string>();
			}
			Contents.Add(file);
		}

		/// <summary>
		/// Print all of the chunk's segments.
		/// </summary>
		public void PrintAll()
		{
			Debug.WriteLine("PrintAll: START OF CHUNK");
			Debug.WriteLine("PrintAll: Name        = " + Name);
			if (Align != null)
			{
				Debug.WriteLine("PrintAll: Align       = " + Align);
			}
			if (Platform != null)
			{
				Debug.WriteLine("PrintAll: Platform    = " + Platform);
			}
			if (Contents != null)
			{
				foreach (string file in Contents)
				{
					Debug.WriteLine("PrintAll: Contents    = " + file);
				}
			}
			Debug.WriteLine("PrintAll: END OF CHUNK");
		}
	}
}

using System.Collections.Generic;
using System.Diagnostics;

namespace ZeroWorldStats.Modules
{
	public class ObjectChunk
	{
		public string ClassName { get; set; }
		public string ObjectName { get; set; }
		public string ObjectId { get; set; }
		public Dictionary<string, string> Parameters { get; set; }

		public ObjectChunk()
		{
			Parameters = new Dictionary<string, string>();
		}
	}
}

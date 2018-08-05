using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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

		// TODO: eventually we should automatically get the file extensions based on the chunk name (ex: "texture" would use ".tga")
		/// <summary>
		/// Return a dictionary of file paths of the files required in this chunk.
		/// </summary>
		/// <param name="directory">Base directory in which to look for the files.</param>
		/// <param name="extension">File extension that should be resolved. 
		/// Ex: ".mrq" would try to resolve files with the ".mrq" extension.</param>
		/// <returns>Dictionary of file paths.</returns>
		public Dictionary<string, string> ResolveContentsAsFiles(string directory, string extension)
		{
			Dictionary<string, string> resolvedFiles = new Dictionary<string, string>();
			string platformDir = string.Concat(directory, "\\", "pc");

			// Override the platform directory if the req chunk has an explicit platform
			if (Platform != null)
			{
				platformDir = string.Concat(directory, "\\", Platform);
			}

			// Add the files
			foreach (string file in Contents)
			{
				string basePath = string.Concat(directory, "\\", file, extension);
				string platformPath = string.Concat(platformDir, "\\", file, extension);

				// First, try to find the files in the root directory - if not found there, look in the platform-specific directory
				if (File.Exists(basePath))
				{
					resolvedFiles.Add(file, basePath);
				}
				else if (File.Exists(platformPath))
				{
					resolvedFiles.Add(file, platformPath);
				}
			}

			return resolvedFiles;
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ZeroWorldStats.Modules;

namespace ZeroWorldStats.Modules
{
	public static class ObjectFileParser
	{
		enum ObjectFileParseState
		{
			CheckBegin,
			FileBegin,
			ObjectHeader,
			ObjectBegin,
			ObjectContents,
			ObjectEnd,
			FileEnd
		};


		/// <summary>
		/// Gets a list of all the objects in the specified layer file.
		/// </summary>
		/// <param name="layerFilePath">Path of LYR or WLD file to parse.</param>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="IOException"></exception>
		/// <exception cref="OutOfMemoryException"></exception>
		/// <returns>List of ObjectChunks.</returns>
		public static List<ObjectChunk> GetObjectChunks(string layerFilePath)
		{
			/// <summary>
			/// Remove whitespace and semicolons from the specified line.
			/// </summary>
			/// <returns>Trimmed line.</returns>
			string TrimLine(string line)
			{
				return line.Trim().Trim(';');
			}

			List<ObjectChunk> objectChunks = new List<ObjectChunk>();

			if (!File.Exists(layerFilePath))
			{
				var message = "ERROR! File does not exist at " + layerFilePath;
				Trace.WriteLine(message);
				return objectChunks;
			}

			FileInfo fileInfo = new FileInfo(layerFilePath);

			if (fileInfo.Extension.ToLower() != ".lyr" && fileInfo.Extension.ToLower() != ".wld")
			{
				var message = "ERROR! File extension is " + fileInfo.Extension;
				Trace.WriteLine(message);
				return objectChunks;
			}

			string curLine;
			int numObjects = 0;
			int curChunkIdx = 0;
			ObjectFileParseState curState = ObjectFileParseState.CheckBegin;
			bool startProcessing = false;
			ObjectChunk curObjectChunk = new ObjectChunk();

			try
			{
				StreamReader file = new StreamReader(layerFilePath);

				// Count the number of REQ chunks in the file
				while ((curLine = file.ReadLine()) != null)
				{
					//Debug.WriteLine("curLine: " + curLine);
					if (curLine.StartsWith("Object(\""))
					{
						numObjects++;
					}
				}

				Debug.WriteLine("numObjects: " + numObjects);

				// Scan the file line by line and extract the objects
				file = new StreamReader(layerFilePath);
				if (numObjects > 0)
				{
					while ((curLine = file.ReadLine()) != null)
					{
						var parsedLine = TrimLine(curLine);
						//Debug.WriteLine("curLine: " + curLine);

						// File Header
						if (curLine.StartsWith("NextSequence(") && curState == ObjectFileParseState.CheckBegin)
						{
							curState = ObjectFileParseState.FileBegin;
							startProcessing = true;
						}

						if (startProcessing)
						{
							// Chunk Header
							if (parsedLine.StartsWith("Object(\""))
							{
								curState = ObjectFileParseState.ObjectHeader;
								curChunkIdx++;

								curObjectChunk = new ObjectChunk();

								// Get only the values inside the header
								string headerStart = "Object(";
								int len = parsedLine.Length;
								string headerValues = parsedLine.Substring(headerStart.Length, parsedLine.Length - headerStart.Length - 1);

								// Extract the name, class, and id
								string[] splitHeader = headerValues.Split(',');
								curObjectChunk.ObjectName = splitHeader[0].Trim().Trim('\"');
								curObjectChunk.ClassName = splitHeader[1].Trim().Trim('\"');
								curObjectChunk.ObjectId = splitHeader[2].Trim().Trim('\"');

								//return objectChunks;
							}

							// Chunk Begin - opening bracket
							if (parsedLine.StartsWith("{") && curState == ObjectFileParseState.ObjectHeader)
							{
								curState = ObjectFileParseState.ObjectBegin;
							}

							// Chunk Contents - list of files within the chunk
							if (!parsedLine.StartsWith("Object(\"") && !parsedLine.StartsWith("{") && !parsedLine.StartsWith("}") &&
								(curState == ObjectFileParseState.ObjectContents || curState == ObjectFileParseState.ObjectBegin))
							{
								curState = ObjectFileParseState.ObjectContents;

								// Don't add blank lines!
								if (parsedLine.Length > 0)
								{
									Debug.WriteLine("Adding Contents: " + parsedLine);

									// Extract the parameter name and value
									string[] splitParam = parsedLine.Split(new char[] { '(' }, 2);
									string paramName = splitParam[0];
									string paramValue = splitParam[1].Substring(0, splitParam[1].Length - 1).Trim().Trim('\"');

									curObjectChunk.Parameters.Add(paramName, paramValue);
								}
							}

							// Chunk End - closing bracket
							if (curLine.StartsWith("}") && (curState == ObjectFileParseState.ObjectContents))
							{
								curState = ObjectFileParseState.ObjectEnd;

								objectChunks.Add(curObjectChunk);
							}

							// File End - no more objects
							if (curState == ObjectFileParseState.ObjectEnd && curChunkIdx == numObjects)
							{
								Debug.WriteLine("END OF FILE");
								curState = ObjectFileParseState.FileEnd;
							}
						}
					}
				}
				else
				{
					Trace.WriteLine("ERROR! There are no objects in the file @ " + layerFilePath);
				}

				file.Close();
			}
			catch (FileNotFoundException ex)
			{
				throw new FileNotFoundException(ex.Message, layerFilePath, ex);
			}
			catch (IOException ex)
			{
				throw new IOException(ex.Message, ex);
			}
			catch (OutOfMemoryException ex)
			{
				throw new OutOfMemoryException(ex.Message, ex);
			}

			return objectChunks;
		}
	}
}

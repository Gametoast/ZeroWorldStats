using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ZeroWorldStats.Modules;

namespace ZeroWorldStats
{
	public static class ReqParser
	{
		enum ReqChunkParseState
		{
			CheckBegin,
			FileHeader,
			FileBegin,
			ChunkHeader,
			ChunkBegin,
			ChunkName,
			ChunkAlign,
			ChunkPlatform,
			ChunkContents,
			ChunkEnd,
			FileEnd
		};


		/// <summary>
		/// Returns the contents of a given chunk name in a REQ (or MRQ) file.
		/// </summary>
		/// <param name="reqFilePath">File path of file to parse chunk from.</param>
		/// <param name="reqChunkName">Name of REQN chunk to parse.</param>
		/// <returns>Contents of parsed REQN chunk.</returns>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="IOException"></exception>
		/// <exception cref="OutOfMemoryException"></exception>
		public static ReqChunk ParseChunk(string reqFilePath, string reqChunkName)
		{
			string ParseLine(string line)
			{
				if (line.Contains("\""))
				{
					// TODO: should probably refactor this to use the Trim method
					// Get the contents in the quotation marks
					return line.Substring(line.IndexOf("\"") + 1, line.LastIndexOf("\"") - line.IndexOf("\"") - 1);
				}
				else
				{
					return line;
				}
			}

			bool CheckLine(string line, string match)
			{
				if (line.ToLower().Contains(match.ToLower()))
				{
					//Debug.WriteLine("match: " + match.ToLower());
				}
				return line.ToLower().Contains(match.ToLower());
			}

			ReqChunk reqChunk = new ReqChunk();
			reqChunk.Contents = new List<string>();

			if (!File.Exists(reqFilePath))
			{
				var message = "ERROR! File does not exist at " + reqFilePath;
				Trace.WriteLine(message);
				return reqChunk;
			}

			FileInfo fileInfo = new FileInfo(reqFilePath);

			if (fileInfo.Extension.ToLower() != ".req" && fileInfo.Extension.ToLower() != ".mrq")
			{
				var message = "ERROR! File extension is " + fileInfo.Extension;
				Trace.WriteLine(message);
				return reqChunk;
			}

			string curLine;
			int numChunks = 0;
			int curChunkIdx = 0;
			bool foundChunk = false;
			ReqChunkParseState curState = ReqChunkParseState.CheckBegin;

			try
			{
				StreamReader file = new StreamReader(reqFilePath);

				// Count the number of REQ chunks in the file
				while ((curLine = file.ReadLine()) != null)
				{
					//Debug.WriteLine("curLine: " + curLine);
					if (CheckLine(curLine, "REQN"))
					{
						numChunks++;
					}
				}

				Debug.WriteLine("numChunks: " + numChunks);

				// Scan the file line by line and extract the segments from the given REQ chunk
				Debug.WriteLine("Looking for chunk: " + reqChunkName);
				file = new StreamReader(reqFilePath);
				if (numChunks > 0)
				{
					while ((curLine = file.ReadLine()) != null)
					{
						var parsedLine = ParseLine(curLine);
						//Debug.WriteLine("curLine: " + curLine);

						// File Header
						if (CheckLine(parsedLine, "ucft") && curState == ReqChunkParseState.CheckBegin)
						{
							curState = ReqChunkParseState.FileHeader;
						}

						// File Begin - opening bracket
						if (CheckLine(parsedLine, "{") && curState == ReqChunkParseState.FileHeader)
						{
							curState = ReqChunkParseState.FileBegin;
						}

						// Chunk Header
						if (CheckLine(parsedLine, "REQN"))
						{
							curState = ReqChunkParseState.ChunkHeader;
							curChunkIdx++;
						}

						// Chunk Begin - opening bracket
						if (CheckLine(parsedLine, "{") && curState == ReqChunkParseState.ChunkHeader)
						{
							curState = ReqChunkParseState.ChunkBegin;
						}

						// Chunk Name - declaration of chunk type, e.g. "lvl", "class", "config", etc.
						if (!parsedLine.Contains("{") && curState == ReqChunkParseState.ChunkBegin)
						{
							curState = ReqChunkParseState.ChunkName;
							//Debug.WriteLine("Current chunk: " + parsedLine);

							if (parsedLine == reqChunkName)
							{
								Debug.WriteLine("Found chunk: " + parsedLine);
								Debug.WriteLine("Adding Name: " + parsedLine);
								reqChunk.Name = parsedLine;
								foundChunk = true;
							}
							else
							{
								Debug.WriteLine("Wrong chunk: " + parsedLine);
								foundChunk = false;
							}
						}

						// Start processing and storing the different chunk segments if we have the right chunk
						if (foundChunk && reqChunk.Name != null)
						{
							// Chunk Align - chunk header byte align value, pretty much almost always "align=2048"
							if (CheckLine(parsedLine, "align=") && (curState == ReqChunkParseState.ChunkName || curState == ReqChunkParseState.ChunkPlatform))
							{
								curState = ReqChunkParseState.ChunkAlign;
								Debug.WriteLine("Adding Align: " + parsedLine);
								reqChunk.Align = parsedLine;
							}

							// Chunk Platform - platform designation for the chunk, always "platform=" followed by "pc", "ps2" or "xbox", e.g. "platform=pc"
							if ((curState == ReqChunkParseState.ChunkName || curState == ReqChunkParseState.ChunkAlign) && CheckLine(parsedLine, "platform="))
							{
								curState = ReqChunkParseState.ChunkPlatform;
								Debug.WriteLine("Adding Platform: " + parsedLine);
								reqChunk.Platform = parsedLine;
							}

							// Chunk Contents - list of files within the chunk
							if (!CheckLine(parsedLine, "align=") && !CheckLine(parsedLine, "platform=") && !CheckLine(parsedLine, reqChunk.Name) && !CheckLine(parsedLine, "}") &&
								(curState == ReqChunkParseState.ChunkContents || curState == ReqChunkParseState.ChunkName || curState == ReqChunkParseState.ChunkAlign || curState == ReqChunkParseState.ChunkPlatform))
							{
								curState = ReqChunkParseState.ChunkContents;

								// Don't add blank lines!
								if (curLine.Contains("\""))
								{
									Debug.WriteLine("Adding Contents: " + parsedLine.ToLower());
									reqChunk.AddContents(parsedLine.ToLower());
								}
							}
						}

						// Chunk End - closing bracket
						if (CheckLine(parsedLine, "}") && ((curState == ReqChunkParseState.ChunkContents) || (curState == ReqChunkParseState.ChunkName && !foundChunk)))
						{
							curState = ReqChunkParseState.ChunkEnd;
						}

						// File End - closing bracket
						if (CheckLine(parsedLine, "}") && curState == ReqChunkParseState.ChunkEnd && curChunkIdx == numChunks)
						{
							Debug.WriteLine("END OF FILE");
							curState = ReqChunkParseState.FileEnd;
						}
					}
				}
				else
				{
					Trace.WriteLine("ERROR! There are no chunks in the REQ file @ " + @reqFilePath);
				}

				file.Close();
			}
			catch (FileNotFoundException ex)
			{
				throw new FileNotFoundException(ex.Message, reqFilePath, ex);
			}
			catch (IOException ex)
			{
				throw new IOException(ex.Message, ex);
			}
			catch (OutOfMemoryException ex)
			{
				throw new OutOfMemoryException(ex.Message, ex);
			}

			return reqChunk;
		}
	}
}

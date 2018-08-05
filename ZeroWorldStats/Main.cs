using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZeroWorldStats.Modules;

namespace ZeroWorldStats
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
		}

		public struct Counts
		{
			public int objectCount;
			public int regionCount;
			public int planConnectionCount;
			public int planHubCount;
		};

		public enum StringMatchType
		{
			StartsWith,
			EndsWith,
			Contains
		};

		public const string DROPDOWN_MODES_BASE = "[Base]";

		public Counts counts = new Counts();
		public string worldReqFilePath;
		public string worldDirectory;
		public Dictionary<string, string> worldModes = new Dictionary<string, string>();
		public Dictionary<string, string> worldLayers = new Dictionary<string, string>();
		public string selectedModeMrq = DROPDOWN_MODES_BASE;

		private void Main_Load(object sender, EventArgs e)
		{
			counts.objectCount = 0;
			counts.regionCount = 0;
			counts.planConnectionCount = 0;
			counts.planHubCount = 0;

			ResetCounts();
			SetCountLabels();
		}

		private void btn_Browse_Click(object sender, EventArgs e)
		{
			if (openDlg_WorldReqFile.ShowDialog() == DialogResult.OK)
			{
				worldReqFilePath = openDlg_WorldReqFile.FileName;
				txt_WorldReqFilePath.Text = worldReqFilePath;

				FileInfo fileInfo = new FileInfo(worldReqFilePath);
				worldDirectory = fileInfo.DirectoryName;

				PopulateModeList();
				ResetSelectedMode();
			}
		}

		private void btn_GetAllCounts_Click(object sender, EventArgs e)
		{
			GetCounts();
		}

		private void btn_GetObjectCnt_Click(object sender, EventArgs e)
		{
			GetObjectCount();
		}

		private void btn_GetRegionCnt_Click(object sender, EventArgs e)
		{
			GetRegionCount();
		}

		private void btn_GetPlanConnectionCnt_Click(object sender, EventArgs e)
		{

		}

		private void btn_GetPlanHubCnt_Click(object sender, EventArgs e)
		{

		}

		private void dd_ModeMrq_SelectionChangeCommitted(object sender, EventArgs e)
		{
			selectedModeMrq = (string)dd_ModeMrq.SelectedItem;
			Debug.WriteLine(selectedModeMrq);
		}

		private void ResetSelectedMode()
		{
			dd_ModeMrq.SelectedIndex = 0;
			selectedModeMrq = (string)dd_ModeMrq.SelectedItem;
		}

		private void PopulateModeList()
		{
			ReqChunk reqChunk = ReqParser.ParseChunk(worldReqFilePath, "lvl");
			List<string> dropdownModeNames = new List<string>();
			
			worldModes.Clear();
			worldModes.Add(DROPDOWN_MODES_BASE, DROPDOWN_MODES_BASE);

			var resolvedFiles = reqChunk.ResolveContentsAsFiles(worldDirectory, ".mrq");
			resolvedFiles.ToList().ForEach(x => worldModes[x.Key] = x.Value);

			dd_ModeMrq.Items.Clear();
			dd_ModeMrq.Items.AddRange(worldModes.Keys.ToArray());

			ResetSelectedMode();
		}

		private void ResetCounts()
		{
			ResetCount(ref counts.objectCount);
			ResetCount(ref counts.regionCount);
			ResetCount(ref counts.planConnectionCount);
			ResetCount(ref counts.planHubCount);
		}

		private void ResetCount(ref int count)
		{
			count = 0;
		}

		private void GetCounts()
		{
			GetObjectCount();
			GetRegionCount();
			GetPlanConnectionCount();
			GetPlanHubCount();
		}

		private void SetCountLabels()
		{
			SetCountLabel(lbl_ObjectCnt, counts.objectCount);
			SetCountLabel(lbl_RegionCnt, counts.regionCount);
			SetCountLabel(lbl_PlanConnectionCnt, counts.planConnectionCount);
			SetCountLabel(lbl_PlanHubCnt, counts.planHubCount);
		}

		private void SetCountLabel(Label label, int count)
		{
			label.Text = count.ToString();
		}

		/// <summary>
		/// Gets the sum of objects from the WLD file and all used LYR files.
		/// </summary>
		private void GetObjectCount()
		{
			List<string> objectFiles = GetWorldChunkFilePaths(worldReqFilePath, worldModes[selectedModeMrq], new string[] { ".wld", ".lyr" });

			ResetCount(ref counts.objectCount);
			
			// Count the objects in each object file
			foreach (string filePath in objectFiles)
			{
				Debug.WriteLine("Counting number of objects in file at path: " + filePath);
				counts.objectCount += GetNumberOfStringInstancesInFile(filePath, "Object(\"", StringMatchType.StartsWith);
			}

			SetCountLabel(lbl_ObjectCnt, counts.objectCount);
		}

		/// <summary>
		/// Gets the sum of regions from all used RGN files.
		/// </summary>
		private void GetRegionCount()
		{
			List<string> regionFiles = GetWorldChunkFilePaths(worldReqFilePath, worldModes[selectedModeMrq], new string[] { ".rgn" });

			ResetCount(ref counts.regionCount);

			// Count the regions in each region file
			foreach (string filePath in regionFiles)
			{
				Debug.WriteLine("Counting number of regions in file at path: " + filePath);
				counts.regionCount += GetNumberOfStringInstancesInFile(filePath, "Region(\"", StringMatchType.StartsWith);
			}

			SetCountLabel(lbl_RegionCnt, counts.regionCount);
		}

		/// <summary>
		/// Gets the sum of planning connections from the specified PLN file.
		/// </summary>
		private void GetPlanConnectionCount()
		{
			
		}

		/// <summary>
		/// Gets the sum of planning hubs from the specified PLN file.
		/// </summary>
		private void GetPlanHubCount()
		{

		}

		/// <summary>
		/// Count the number of instances of a string in the specified file. 
		/// Each line in the file is matched with the specified string and match type; if a match is made, the count is incremented.
		/// </summary>
		/// <param name="filePath">Path of file to evaluate.</param>
		/// <param name="match">String to count.</param>
		/// <param name="matchType">Method with which to match the string. Default is StringMatchType.StartsWith.</param>
		/// <returns>Number of string instances found. Negative value means an exception was raised.</returns>
		private int GetNumberOfStringInstancesInFile(string filePath, string match, StringMatchType matchType)
		{
			int count = 0;

			try
			{
				StreamReader file = new StreamReader(filePath);
				string curLine;

				while ((curLine = file.ReadLine()) != null)
				{
					switch (matchType)
					{
						case StringMatchType.StartsWith:
							if (curLine.StartsWith(match))
							{
								count++;
							}
							break;

						case StringMatchType.EndsWith:
							if (curLine.EndsWith(match))
							{
								count++;
							}
							break;

						case StringMatchType.Contains:
							if (curLine.Contains(match))
							{
								count++;
							}
							break;

						default:
							if (curLine.StartsWith(match))
							{
								count++;
							}
							break;
					}
				}
			}
			catch (FileNotFoundException ex)
			{
				var msg = string.Format("FileNotFoundException: File not found at path: {0}. Reason: {1}", filePath, ex.Message);
				Trace.WriteLine(msg);
				return -1;
			}
			catch (DirectoryNotFoundException ex)
			{
				var msg = string.Format("DirectoryNotFoundException: Directory not found at path: {0}. Reason: {1}", filePath, ex.Message);
				Trace.WriteLine(msg);
				return -2;
			}
			catch (IOException ex)
			{
				var msg = string.Format("IOException: Failed to read file at path: {0}. Reason: {1}", filePath, ex.Message);
				Trace.WriteLine(msg);
				return -3;
			}

			return count;
		}

		// Returns a list of file paths for files of the specified extensions from the specified world REQ file and mode MRQ.
		private List<string> GetWorldChunkFilePaths(string reqFile, string mrqFile, string[] extensions)
		{
			List<string> filePaths = new List<string>();
			bool mrq = (mrqFile != "" && mrqFile != DROPDOWN_MODES_BASE);

			// Retrieve the "world" REQN chunks
			ReqChunk baseWorldChunk = ReqParser.ParseChunk(reqFile, "world");
			ReqChunk modeWorldChunk = new ReqChunk();
			if (mrq)
			{
				modeWorldChunk = ReqParser.ParseChunk(mrqFile, "world");
			}

			// Resolve the file paths for each extension
			foreach (string extension in extensions)
			{
				filePaths.AddRange(baseWorldChunk.ResolveContentsAsFiles(worldDirectory, extension).Values);
				if (mrq)
				{
					filePaths.AddRange(modeWorldChunk.ResolveContentsAsFiles(worldDirectory, extension).Values);
				}
			}

			return filePaths;
		}
	}
}

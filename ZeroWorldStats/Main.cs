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

		public const string DROPDOWN_MODES_BASE = "[Base]";

		public Counts counts = new Counts();
		public string worldReqFilePath;
		public string worldDirectory;
		public Dictionary<string, string> worldModes = new Dictionary<string, string>();
		public string selectedModeMrq;

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
				dd_ModeMrq.SelectedIndex = 0;
			}
		}

		private void btn_GetObjectCnt_Click(object sender, EventArgs e)
		{
			ReqChunk reqChunk = ReqParser.ParseChunk(worldReqFilePath, "world");
			reqChunk.PrintAll();
		}

		private void btn_GetRegionCnt_Click(object sender, EventArgs e)
		{

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

		private void PopulateModeList()
		{
			ReqChunk reqChunk = ReqParser.ParseChunk(worldReqFilePath, "lvl");
			List<string> worldModeFiles = reqChunk.ResolveContentsAsFiles(worldDirectory, ".mrq");
			List<string> dropdownModeNames = new List<string>();

			worldModes.Clear();

			// Create a dictionary of modes where the key is the mode's name ("abc_conquest") and the value is the mrq file path
			foreach (string modeFile in worldModeFiles)
			{
				FileInfo fileInfo = new FileInfo(modeFile);
				string modeName = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);

				worldModes.Add(modeName, modeFile);
				dropdownModeNames.Add(modeName);
			}

			dd_ModeMrq.Items.Clear();
			dd_ModeMrq.Items.Add(DROPDOWN_MODES_BASE);
			dd_ModeMrq.Items.AddRange(dropdownModeNames.ToArray());
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

		private void SetCounts()
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
			List<string> worldFiles = GetWorldFiles(worldReqFilePath, selectedModeMrq);
			List<string> lyrFiles = new List<string>();
			DirectoryInfo directoryInfo = new DirectoryInfo(worldReqFilePath);
			
			foreach (string file in worldFiles)
			{
				string lyrPath = string.Concat(directoryInfo.Parent, "\\", file, ".lyr");
				string wldPath = string.Concat(directoryInfo.Parent, "\\", file, ".wld");

				// Try to find the LYR/WLD file and add it
				if (File.Exists(lyrPath))
				{
					Debug.WriteLine("Adding LYR file at path: " + lyrPath);
					lyrFiles.Add(lyrPath);
				}
				else if (File.Exists(wldPath))
				{
					Debug.WriteLine("Adding WLD file at path: " + wldPath);
					lyrFiles.Add(wldPath);
				}
				else
				{
					Debug.WriteLine("Failed to find LYR file at path: " + lyrPath);
					Debug.WriteLine("Failed to find WLD file at path: " + wldPath);
				}
			}

			// add '.lyr' to each file in lyrFiles
			// try to resolve file path for lyr files based on world req file path
			// count the instances of ' Object(" ' in each lyr file
			
		}

		/// <summary>
		/// Gets the sum of regions from all used RGN files.
		/// </summary>
		private void GetRegionCount()
		{
			List<string> worldFiles = GetWorldFiles(worldReqFilePath, selectedModeMrq);
			List<string> rgnFiles = new List<string>();
			DirectoryInfo directoryInfo = new DirectoryInfo(worldReqFilePath);

			foreach (string file in worldFiles)
			{
				string path = string.Concat(directoryInfo.Parent, "\\", file, ".rgn");

				if (File.Exists(path))
				{
					Debug.WriteLine("Adding RGN file at path: " + path);
					rgnFiles.Add(path);
				}
				else
				{
					Debug.WriteLine("Failed to find RGN file at path: " + path);
				}
			}

			// add '.rgn' to each file in rgnFiles
			// try to resolve file path for lyr files based on world req file path
			// count the instances of ' Region(" ' in each rgn file
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

		private List<string> GetWorldFiles(string reqFile, string modeName)
		{
			List<string> worldFiles = new List<string>();

			// add list of files from "world" section in req
			// add list of files from "world" section in selected mode mrq

			return worldFiles;
		}
	}
}

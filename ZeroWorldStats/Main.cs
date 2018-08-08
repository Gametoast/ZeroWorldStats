using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using System.Text.RegularExpressions;
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


		#region Custom data types

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

		#endregion Custom data types


		#region Fields

		public const string APP_NAME = "Zero World Stats";
		public const string APP_VERSION = "0.1.0";
		public const string DROPDOWN_MODES_BASE = "[Base]";

		public Counts counts = new Counts();
		public string worldReqFilePath;
		public string worldDirectory;
		public Dictionary<string, string> worldModes = new Dictionary<string, string>();
		public Dictionary<string, string> worldLayers = new Dictionary<string, string>();
		public Dictionary<string, string> worldPlans = new Dictionary<string, string>();
		public string selectedModeMrq = DROPDOWN_MODES_BASE;
		public string selectedPlanFile;

		#endregion Fields


		#region Form controls

		private void Main_Load(object sender, EventArgs e)
		{
			this.Text = string.Format("{0}", APP_NAME);
			lbl_AppVersion.Text = string.Format("v{0}", APP_VERSION);

			counts.objectCount = 0;
			counts.regionCount = 0;
			counts.planConnectionCount = 0;
			counts.planHubCount = 0;

			ResetCounts();
			SetCountLabels();
		}

		private void txt_WorldReqFilePath_TextChanged(object sender, EventArgs e)
		{
			txt_WorldReqFilePath.Text = worldReqFilePath;
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

				PopulatePlanList();
				ResetSelectedPlan();

				ResetCounts();
				SetCountLabels();
			}
		}

		private void dd_ModeMrq_SelectionChangeCommitted(object sender, EventArgs e)
		{
			selectedModeMrq = (string)dd_ModeMrq.SelectedItem;
			Debug.WriteLine(selectedModeMrq);
		}

		private void dd_PlanFile_SelectionChangeCommitted(object sender, EventArgs e)
		{
			selectedPlanFile = (string)dd_PlanFile.SelectedItem;
			Debug.WriteLine(selectedPlanFile);
		}

		private void btn_GetAllCounts_Click(object sender, EventArgs e)
		{
			if (File.Exists(worldReqFilePath))
			{
				GetCounts();
			}
		}

		private void btn_GetObjectCnt_Click(object sender, EventArgs e)
		{
			if (File.Exists(worldReqFilePath))
			{
				GetObjectCount();
			}
		}

		private void btn_GetRegionCnt_Click(object sender, EventArgs e)
		{
			if (File.Exists(worldReqFilePath))
			{
				GetRegionCount();
			}
		}

		private void btn_GetPlanConnectionCnt_Click(object sender, EventArgs e)
		{
			if (File.Exists(worldReqFilePath))
			{
				GetPlanConnectionCount();
			}
		}

		private void btn_GetPlanHubCnt_Click(object sender, EventArgs e)
		{
			if (File.Exists(worldReqFilePath))
			{
				GetPlanHubCount();
			}
		}

		#endregion Form controls


		#region Core logic

		/// <summary>
		/// Repopulates the mode list with the modes that are listed in world REQ.
		/// </summary>
		private void PopulateModeList()
		{
			try
			{
				try
				{
					ReqChunk reqChunk = ReqParser.ParseChunk(worldReqFilePath, "lvl");
					// Add the "[Base]" mode to the list
					worldModes.Clear();
					worldModes.Add(DROPDOWN_MODES_BASE, DROPDOWN_MODES_BASE);

					// Get the existing mrq files and merge the returned dictionary with the worldModes dictionary
					var resolvedFiles = reqChunk.ResolveContentsAsFiles(worldDirectory, ".mrq", true);
					resolvedFiles.ToList().ForEach(x => worldModes[x.Key] = x.Value);

					// Add the modes to the dropdown
					dd_ModeMrq.Items.Clear();
					dd_ModeMrq.Items.AddRange(worldModes.Keys.ToArray());
				}
				catch (FileNotFoundException ex)
				{
					Trace.WriteLine(ex.Message);
					throw;
				}
				catch (IOException ex)
				{
					Trace.WriteLine(ex.Message);
					throw;
				}
				catch (OutOfMemoryException ex)
				{
					Trace.WriteLine(ex.Message);
					throw;
				}
			}
			catch (ArgumentNullException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}

			ResetSelectedMode();
		}
		
		/// <summary>
		/// Resets the mode dropdown to the first item.
		/// </summary>
		private void ResetSelectedMode()
		{
			if (dd_ModeMrq.Items.Count > 0)
			{
				dd_ModeMrq.SelectedIndex = 0;
				selectedModeMrq = (string)dd_ModeMrq.SelectedItem;
			}
		}
		
		/// <summary>
		/// Repopulates the plan list with the plan files that are listed in the world REQ and mode MRQs.
		/// </summary>
		private void PopulatePlanList()
		{
			try
			{
				worldPlans.Clear();

				// Get the existing pln files that are listed in the world req and merge the returned dictionary with the worldPlans dictionary
				ReqChunk basePlnChunk = ReqParser.ParseChunk(worldReqFilePath, "congraph");

				var resolvedBasePlanFiles = basePlnChunk.ResolveContentsAsFiles(worldDirectory, ".pln", true);
				resolvedBasePlanFiles.ToList().ForEach(x => worldPlans[x.Key] = x.Value);

				// Get a list of all the existing pln files in each game mode mrq
				foreach (string modeFilePath in worldModes.Values)
				{
					if (modeFilePath != DROPDOWN_MODES_BASE)
					{
						ReqChunk modePlnChunk = ReqParser.ParseChunk(modeFilePath, "congraph");

						// Get the mrq's existing pln files and merge the returned dictionary with the worldPlans dictionary
						var resolvedModePlanFiles = modePlnChunk.ResolveContentsAsFiles(worldDirectory, ".pln", true);
						resolvedModePlanFiles.ToList().ForEach(x => worldPlans[x.Key] = x.Value);
					}
				}

				// Add the pln files to the dropdown
				dd_PlanFile.Items.Clear();
				dd_PlanFile.Items.AddRange(worldPlans.Keys.ToArray());
			}
			catch (ArgumentNullException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}
			catch (FileNotFoundException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}
			catch (IOException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}
			catch (OutOfMemoryException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}

			ResetSelectedPlan();
		}
		
		/// <summary>
		/// Resets the plan dropdown to the first item.
		/// </summary>
		private void ResetSelectedPlan()
		{
			if (dd_PlanFile.Items.Count > 0)
			{
				dd_PlanFile.SelectedIndex = 0;
				selectedPlanFile = (string)dd_PlanFile.SelectedItem;
			}
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
				int numFound = GetNumberOfStringInstancesInFile(filePath, "Object(\"", StringMatchType.StartsWith);
				if (numFound >= 0)
				{
					counts.objectCount += numFound;
				}
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
				int numFound = GetNumberOfStringInstancesInFile(filePath, "Region(\"", StringMatchType.StartsWith);
				if (numFound >= 0)
				{
					counts.regionCount += numFound;
				}
			}

			SetCountLabel(lbl_RegionCnt, counts.regionCount);
		}

		/// <summary>
		/// Gets the sum of planning connections from the specified PLN file.
		/// </summary>
		private void GetPlanConnectionCount()
		{
			string planFilePath = worldPlans[selectedPlanFile];

			ResetCount(ref counts.planConnectionCount);

			// Count the connections in the plan file
			Debug.WriteLine("Counting number of connections in file at path: " + planFilePath);
			int numFound = GetNumberOfStringInstancesInFile(planFilePath, "Connection(\"", StringMatchType.StartsWith);
			if (numFound >= 0)
			{
				counts.planConnectionCount += numFound;
			}

			SetCountLabel(lbl_PlanConnectionCnt, counts.planConnectionCount);
		}

		/// <summary>
		/// Gets the sum of planning hubs from the specified PLN file.
		/// </summary>
		private void GetPlanHubCount()
		{
			string planFilePath = worldPlans[selectedPlanFile];

			ResetCount(ref counts.planHubCount);

			// Count the hubs in the plan file
			Debug.WriteLine("Counting number of hubs in file at path: " + planFilePath);
			int numFound = GetNumberOfStringInstancesInFile(planFilePath, "Hub(\"", StringMatchType.StartsWith);
			if (numFound >= 0)
			{
				counts.planHubCount += numFound;
			}

			SetCountLabel(lbl_PlanHubCnt, counts.planHubCount);
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
				MessageBox.Show(this, "File not found at the specified path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			catch (DirectoryNotFoundException ex)
			{
				var msg = string.Format("DirectoryNotFoundException: Directory not found at path: {0}. Reason: {1}", filePath, ex.Message);
				Trace.WriteLine(msg);
				MessageBox.Show(this, "Directory not found at the specified path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -2;
			}
			catch (IOException ex)
			{
				var msg = string.Format("IOException: Failed to read file at path: {0}. Reason: {1}", filePath, ex.Message);
				Trace.WriteLine(msg);
				MessageBox.Show(this, "Failed to read file at path. Reason: \n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -3;
			}
			catch (ArgumentNullException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}

			return count;
		}
		
		/// <summary>
		/// Returns a list of file paths for files of the specified extensions from the specified world REQ file and mode MRQ.
		/// </summary>
		/// <param name="reqFile">Path of REQ file to parse.</param>
		/// <param name="mrqFile">Path of MRQ file to parse.</param>
		/// <param name="extensions">Array of file extensions to check for. (ex: ".rgn")</param>
		/// <returns></returns>
		private List<string> GetWorldChunkFilePaths(string reqFile, string mrqFile, string[] extensions)
		{
			List<string> filePaths = new List<string>();
			bool mrq = (mrqFile != "" && mrqFile != DROPDOWN_MODES_BASE);

			try
			{
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
			}
			catch (FileNotFoundException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}
			catch (IOException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}
			catch (OutOfMemoryException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}
			
			return filePaths;
		}

		#endregion Core logic
	}
}

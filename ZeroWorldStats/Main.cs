using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

		public Counts counts = new Counts();
		public string worldReqFilePath;

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
			}
		}

		private void btn_GetObjectCnt_Click(object sender, EventArgs e)
		{

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

		private void GetObjectCount()
		{

		}

		private void GetRegionCount()
		{

		}

		private void GetPlanConnectionCount()
		{

		}

		private void GetPlanHubCount()
		{

		}
	}
}

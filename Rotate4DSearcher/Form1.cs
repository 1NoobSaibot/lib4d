using Rotate4DSearcher.Genetic;
using System;
using System.Windows.Forms;

namespace Rotate4DSearcher
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			GeneticAlgorithm.Start();
			candidatesUpdater.Start();
		}


		private void onLoad(object sender, EventArgs e)
		{
			SamplesStore.GetSamples();
		}


		private void candidatesUpdater_Tick(object sender, EventArgs e)
		{
			Candidate[] array = GeneticAlgorithm.TheBest;
			candidates.Items.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					string[] strings = array[i].ToString().Split('\n');
					candidates.Items.AddRange(strings);
				}
			}

			logLabel.Text = "Generation: " + GeneticAlgorithm.GenerationCounter;
		}
	}
}

using Lib4D;
using Rotate4DSearcher.Genetic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Rotate4DSearcher
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			SamplesStore.SamplesChanged += _OnSamplesChanged;
			_OnSamplesChanged(null, null);
			GeneticAlgorithm.Start();
			candidatesUpdater.Start();
		}


		private void onLoad(object sender, EventArgs e)
		{
			SamplesStore.GetSamples();
		}

		private void addRotationSurfaceButton_Click(object sender, EventArgs e)
		{
			try
			{
				CustomBivector4D b = rotationSurfaceInput4D.GetBivector();
				SamplesStore.AddNewSample(b);
			}
			catch (Exception error)
			{
				logLabel.Text = error.Message;
			} 
		}


		private void _OnSamplesChanged(object sender, List<Sample> list)
		{
			List<Sample> samples = SamplesStore.GetSamples();
			int selectedSample = rotationSurfacesListBox.SelectedIndex;
			rotationSurfacesListBox.Items.Clear();

			for (int i = 0; i < samples.Count; i++)
			{
				rotationSurfacesListBox.Items.Add(samples[i]);
			}

			/*rotationSurfacesListBox.SelectedIndex = selectedSample;

			Sample activeSample = rotationSurfacesListBox.SelectedItem as Sample;
			if (activeSample == null)
			{
				return;
			}

			List<QuestionAnswerPair> pairs = activeSample.pairs;
			fromToPairsListBox.Items.Clear();

			for (int i = 0; i < pairs.Count; i++)
			{
				fromToPairsListBox.Items.Add(pairs[i]);
			}*/
		}


		private void rotationSurfacesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Sample sample = rotationSurfacesListBox.SelectedItem as Sample;
			if (sample == null)
			{
				return;
			}
			List<QuestionAnswerPair> pairs = sample.pairs;

			fromToPairsListBox.Items.Clear();
			for (int i = 0; i < pairs.Count; i++)
			{
				fromToPairsListBox.Items.Add(pairs[i]);
			}
		}


		private void addPairButton_Click(object sender, EventArgs e)
		{
			try
			{
				int sampleIndex = rotationSurfacesListBox.SelectedIndex;
				Vector4D from = vectorInput4DFrom.GetVector();
				Vector4D to = vectorInput4DTo.GetVector();
				SamplesStore.AddPair(sampleIndex, from, to);
			} catch (Exception error)
			{
				logLabel.Text = error.Message;
			}
		}

		private void candidatesUpdater_Tick(object sender, EventArgs e)
		{
			Candidate[] array = GeneticAlgorithm.TheBest;
			candidates.Items.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					candidates.Items.Add(array[i]);
				}
			}

			double[][] errors = GeneticAlgorithm.GetErrors();
			string[] errorStrings = new string[errors.Length];
			for (int i = 0; i < errors.Length; i++)
			{
				StringBuilder errorBuilder = new StringBuilder();
				double error = 0;

				if (errors[i].Length == 0)
				{
					errorStrings[i] = "No Data";
					continue;
				}
				for (int j = 0; j < errors[i].Length; j++)
				{
					error += errors[i][j];
					errorBuilder.Append(String.Format("{0:0.##}", errors[i][j]) + "  ");
				}
				errorBuilder.Insert(0, String.Format("{0:0.##}", error / errors[i].Length) + " | ");
				errorStrings[i] = errorBuilder.ToString();
			}

			samplesErrors.Items.Clear();
			for (int i = 0; i < errorStrings.Length; i++)
			{
				samplesErrors.Items.Add(errorStrings[i]);
			}
		}

		private void removeSelectedSurface_Click(object sender, EventArgs e)
		{
			Sample selected = rotationSurfacesListBox.SelectedItem as Sample;
			if (selected == null)
			{
				return;
			}

			SamplesStore.RemoveSample(selected);
		}

		private void removeSelectedPair_Click(object sender, EventArgs e)
		{
			Sample selected = rotationSurfacesListBox.SelectedItem as Sample;
			if (selected == null)
			{
				return;
			}

			QuestionAnswerPair pair = fromToPairsListBox.SelectedItem as QuestionAnswerPair;
			if (pair == null)
			{
				return;
			}

			SamplesStore.RemovePair(selected, pair);
		}
	}
}

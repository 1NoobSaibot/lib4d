﻿using Lib4D;
using System;
using System.Collections.Generic;
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

			rotationSurfacesListBox.SelectedIndex = selectedSample;

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
			}
		}


		private void rotationSurfacesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Sample sample = rotationSurfacesListBox.SelectedItem as Sample;
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
	}
}

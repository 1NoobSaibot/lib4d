using Lib4D;
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
				Vector4D a = new Vector4D();
				Vector4D b = new Vector4D();

				a.X = Double.Parse(aXInput.Text);
				a.Y = Double.Parse(aYInput.Text);
				a.Z = Double.Parse(aZInput.Text);
				a.Q = Double.Parse(aQInput.Text);

				b.X = Double.Parse(bXInput.Text);
				b.Y = Double.Parse(bYInput.Text);
				b.Z = Double.Parse(bZInput.Text);
				b.Q = Double.Parse(bQInput.Text);

				int angleInGrad = Int32.Parse(rotationAngleInput.Text);
				SamplesStore.AddNewSample(a, b, angleInGrad);
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

				Vector4D from = new Vector4D();
				Vector4D to = new Vector4D();

				from.X = Double.Parse(fromXInput.Text);
				from.Y = Double.Parse(fromYInput.Text);
				from.Z = Double.Parse(fromZInput.Text);
				from.Q = Double.Parse(fromQInput.Text);

				to.X = Double.Parse(toXInput.Text);
				to.Y = Double.Parse(toYInput.Text);
				to.Z = Double.Parse(toZInput.Text);
				to.Q = Double.Parse(toQInput.Text);

				SamplesStore.AddPair(sampleIndex, from, to);
			} catch (Exception error)
			{
				logLabel.Text = error.Message;
			}
			
		}
	}
}

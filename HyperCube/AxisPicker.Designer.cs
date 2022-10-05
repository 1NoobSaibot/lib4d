namespace HyperCube
{
	partial class AxisPicker
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.aVectorPicker = new HyperCube.Vector4DPicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.bVectorPicker = new HyperCube.Vector4DPicker();
			this.anglePicker = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.anglePicker)).BeginInit();
			this.SuspendLayout();
			// 
			// aVectorPicker
			// 
			this.aVectorPicker.Location = new System.Drawing.Point(0, 24);
			this.aVectorPicker.Name = "aVectorPicker";
			this.aVectorPicker.Size = new System.Drawing.Size(134, 198);
			this.aVectorPicker.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(45, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Axis A";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(185, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Axis B";
			// 
			// bVectorPicker
			// 
			this.bVectorPicker.Location = new System.Drawing.Point(140, 24);
			this.bVectorPicker.Name = "bVectorPicker";
			this.bVectorPicker.Size = new System.Drawing.Size(134, 198);
			this.bVectorPicker.TabIndex = 2;
			// 
			// anglePicker
			// 
			this.anglePicker.Location = new System.Drawing.Point(19, 229);
			this.anglePicker.Maximum = 180;
			this.anglePicker.Minimum = -180;
			this.anglePicker.Name = "anglePicker";
			this.anglePicker.Size = new System.Drawing.Size(252, 45);
			this.anglePicker.TabIndex = 4;
			// 
			// AxisPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.anglePicker);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.bVectorPicker);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.aVectorPicker);
			this.Name = "AxisPicker";
			this.Size = new System.Drawing.Size(274, 279);
			((System.ComponentModel.ISupportInitialize)(this.anglePicker)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Vector4DPicker aVectorPicker;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private Vector4DPicker bVectorPicker;
		private System.Windows.Forms.TrackBar anglePicker;
	}
}

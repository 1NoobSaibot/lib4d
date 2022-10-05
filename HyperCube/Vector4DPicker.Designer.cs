namespace HyperCube
{
	partial class Vector4DPicker
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
			this.label1 = new System.Windows.Forms.Label();
			this.xPicker = new System.Windows.Forms.TrackBar();
			this.yPicker = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.zPicker = new System.Windows.Forms.TrackBar();
			this.label3 = new System.Windows.Forms.Label();
			this.qPicker = new System.Windows.Forms.TrackBar();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.xPicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.yPicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.zPicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.qPicker)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "X";
			// 
			// xPicker
			// 
			this.xPicker.Location = new System.Drawing.Point(24, 3);
			this.xPicker.Maximum = 100;
			this.xPicker.Minimum = -100;
			this.xPicker.Name = "xPicker";
			this.xPicker.Size = new System.Drawing.Size(104, 45);
			this.xPicker.TabIndex = 1;
			this.xPicker.TickFrequency = 0;
			// 
			// yPicker
			// 
			this.yPicker.Location = new System.Drawing.Point(24, 54);
			this.yPicker.Maximum = 100;
			this.yPicker.Minimum = -100;
			this.yPicker.Name = "yPicker";
			this.yPicker.Size = new System.Drawing.Size(104, 45);
			this.yPicker.TabIndex = 3;
			this.yPicker.TickFrequency = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(14, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Y";
			// 
			// zPicker
			// 
			this.zPicker.Location = new System.Drawing.Point(24, 105);
			this.zPicker.Maximum = 100;
			this.zPicker.Minimum = -100;
			this.zPicker.Name = "zPicker";
			this.zPicker.Size = new System.Drawing.Size(104, 45);
			this.zPicker.TabIndex = 5;
			this.zPicker.TickFrequency = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 105);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(14, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Z";
			// 
			// qPicker
			// 
			this.qPicker.Location = new System.Drawing.Point(24, 156);
			this.qPicker.Maximum = 100;
			this.qPicker.Minimum = -100;
			this.qPicker.Name = "qPicker";
			this.qPicker.Size = new System.Drawing.Size(104, 45);
			this.qPicker.TabIndex = 7;
			this.qPicker.TickFrequency = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 156);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(15, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Q";
			// 
			// Vector4DPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.qPicker);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.zPicker);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.yPicker);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.xPicker);
			this.Controls.Add(this.label1);
			this.Name = "Vector4DPicker";
			this.Size = new System.Drawing.Size(134, 198);
			((System.ComponentModel.ISupportInitialize)(this.xPicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.yPicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.zPicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.qPicker)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar xPicker;
		private System.Windows.Forms.TrackBar yPicker;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar zPicker;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TrackBar qPicker;
		private System.Windows.Forms.Label label4;
	}
}

namespace Rotate4DSearcher.Components
{
	partial class VectorInput4D
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
			this.labelX = new System.Windows.Forms.Label();
			this.InputX = new System.Windows.Forms.TextBox();
			this.InputY = new System.Windows.Forms.TextBox();
			this.labelY = new System.Windows.Forms.Label();
			this.InputZ = new System.Windows.Forms.TextBox();
			this.labelZ = new System.Windows.Forms.Label();
			this.InputQ = new System.Windows.Forms.TextBox();
			this.labelQ = new System.Windows.Forms.Label();
			this.NormalizeButton = new System.Windows.Forms.Button();
			this.LengthLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelX
			// 
			this.labelX.AutoSize = true;
			this.labelX.Location = new System.Drawing.Point(3, 7);
			this.labelX.Name = "labelX";
			this.labelX.Size = new System.Drawing.Size(14, 13);
			this.labelX.TabIndex = 0;
			this.labelX.Text = "X";
			// 
			// InputX
			// 
			this.InputX.Location = new System.Drawing.Point(25, 4);
			this.InputX.Name = "InputX";
			this.InputX.Size = new System.Drawing.Size(122, 20);
			this.InputX.TabIndex = 1;
			this.InputX.Text = "0";
			this.InputX.TextChanged += new System.EventHandler(this.OnInputChanged);
			// 
			// InputY
			// 
			this.InputY.Location = new System.Drawing.Point(25, 30);
			this.InputY.Name = "InputY";
			this.InputY.Size = new System.Drawing.Size(122, 20);
			this.InputY.TabIndex = 3;
			this.InputY.Text = "0";
			this.InputY.TextChanged += new System.EventHandler(this.OnInputChanged);
			// 
			// labelY
			// 
			this.labelY.AutoSize = true;
			this.labelY.Location = new System.Drawing.Point(3, 33);
			this.labelY.Name = "labelY";
			this.labelY.Size = new System.Drawing.Size(14, 13);
			this.labelY.TabIndex = 2;
			this.labelY.Text = "Y";
			// 
			// InputZ
			// 
			this.InputZ.Location = new System.Drawing.Point(25, 56);
			this.InputZ.Name = "InputZ";
			this.InputZ.Size = new System.Drawing.Size(122, 20);
			this.InputZ.TabIndex = 5;
			this.InputZ.Text = "0";
			this.InputZ.TextChanged += new System.EventHandler(this.OnInputChanged);
			// 
			// labelZ
			// 
			this.labelZ.AutoSize = true;
			this.labelZ.Location = new System.Drawing.Point(3, 59);
			this.labelZ.Name = "labelZ";
			this.labelZ.Size = new System.Drawing.Size(14, 13);
			this.labelZ.TabIndex = 4;
			this.labelZ.Text = "Z";
			// 
			// InputQ
			// 
			this.InputQ.Location = new System.Drawing.Point(25, 82);
			this.InputQ.Name = "InputQ";
			this.InputQ.Size = new System.Drawing.Size(122, 20);
			this.InputQ.TabIndex = 7;
			this.InputQ.Text = "0";
			this.InputQ.TextChanged += new System.EventHandler(this.OnInputChanged);
			// 
			// labelQ
			// 
			this.labelQ.AutoSize = true;
			this.labelQ.Location = new System.Drawing.Point(3, 85);
			this.labelQ.Name = "labelQ";
			this.labelQ.Size = new System.Drawing.Size(15, 13);
			this.labelQ.TabIndex = 6;
			this.labelQ.Text = "Q";
			// 
			// NormalizeButton
			// 
			this.NormalizeButton.Location = new System.Drawing.Point(115, 109);
			this.NormalizeButton.Name = "NormalizeButton";
			this.NormalizeButton.Size = new System.Drawing.Size(31, 23);
			this.NormalizeButton.TabIndex = 8;
			this.NormalizeButton.Text = "N1";
			this.NormalizeButton.UseVisualStyleBackColor = true;
			this.NormalizeButton.Click += new System.EventHandler(this.NormalizeButton_Click);
			// 
			// LengthLabel
			// 
			this.LengthLabel.AutoSize = true;
			this.LengthLabel.Location = new System.Drawing.Point(3, 114);
			this.LengthLabel.MaximumSize = new System.Drawing.Size(110, 14);
			this.LengthLabel.Name = "LengthLabel";
			this.LengthLabel.Size = new System.Drawing.Size(106, 14);
			this.LengthLabel.TabIndex = 9;
			this.LengthLabel.Text = "Abs:0000000000000000000";
			// 
			// VectorInput4D2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LengthLabel);
			this.Controls.Add(this.NormalizeButton);
			this.Controls.Add(this.InputQ);
			this.Controls.Add(this.labelQ);
			this.Controls.Add(this.InputZ);
			this.Controls.Add(this.labelZ);
			this.Controls.Add(this.InputY);
			this.Controls.Add(this.labelY);
			this.Controls.Add(this.InputX);
			this.Controls.Add(this.labelX);
			this.Name = "VectorInput4D2";
			this.Size = new System.Drawing.Size(150, 139);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelX;
		private System.Windows.Forms.TextBox InputX;
		private System.Windows.Forms.TextBox InputY;
		private System.Windows.Forms.Label labelY;
		private System.Windows.Forms.TextBox InputZ;
		private System.Windows.Forms.Label labelZ;
		private System.Windows.Forms.TextBox InputQ;
		private System.Windows.Forms.Label labelQ;
		private System.Windows.Forms.Button NormalizeButton;
		private System.Windows.Forms.Label LengthLabel;
	}
}

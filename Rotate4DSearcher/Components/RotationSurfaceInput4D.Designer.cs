namespace Rotate4DSearcher.Components
{
	partial class RotationSurfaceInput4D
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
			this.vectorAInput = new Rotate4DSearcher.Components.VectorInput4D();
			this.vectorBInput = new Rotate4DSearcher.Components.VectorInput4D();
			this.labelA = new System.Windows.Forms.Label();
			this.labelB = new System.Windows.Forms.Label();
			this.labelAngle = new System.Windows.Forms.Label();
			this.AngleGradInput = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// vectorAInput
			// 
			this.vectorAInput.Location = new System.Drawing.Point(0, 20);
			this.vectorAInput.Name = "vectorAInput";
			this.vectorAInput.Size = new System.Drawing.Size(150, 139);
			this.vectorAInput.TabIndex = 0;
			// 
			// vectorBInput
			// 
			this.vectorBInput.Location = new System.Drawing.Point(156, 20);
			this.vectorBInput.Name = "vectorBInput";
			this.vectorBInput.Size = new System.Drawing.Size(150, 139);
			this.vectorBInput.TabIndex = 1;
			// 
			// labelA
			// 
			this.labelA.AutoSize = true;
			this.labelA.Location = new System.Drawing.Point(26, 4);
			this.labelA.Name = "labelA";
			this.labelA.Size = new System.Drawing.Size(48, 13);
			this.labelA.TabIndex = 2;
			this.labelA.Text = "Vector A";
			// 
			// labelB
			// 
			this.labelB.AutoSize = true;
			this.labelB.Location = new System.Drawing.Point(182, 4);
			this.labelB.Name = "labelB";
			this.labelB.Size = new System.Drawing.Size(48, 13);
			this.labelB.TabIndex = 3;
			this.labelB.Text = "Vector B";
			// 
			// labelAngle
			// 
			this.labelAngle.AutoSize = true;
			this.labelAngle.Location = new System.Drawing.Point(26, 162);
			this.labelAngle.Name = "labelAngle";
			this.labelAngle.Size = new System.Drawing.Size(69, 13);
			this.labelAngle.TabIndex = 4;
			this.labelAngle.Text = "Angle (Grad):";
			// 
			// AngleGradInput
			// 
			this.AngleGradInput.Location = new System.Drawing.Point(101, 159);
			this.AngleGradInput.Name = "AngleGradInput";
			this.AngleGradInput.Size = new System.Drawing.Size(49, 20);
			this.AngleGradInput.TabIndex = 5;
			this.AngleGradInput.Text = "90";
			// 
			// RotationSurfaceInput4D
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.Controls.Add(this.AngleGradInput);
			this.Controls.Add(this.labelAngle);
			this.Controls.Add(this.labelB);
			this.Controls.Add(this.labelA);
			this.Controls.Add(this.vectorBInput);
			this.Controls.Add(this.vectorAInput);
			this.Name = "RotationSurfaceInput4D";
			this.Size = new System.Drawing.Size(310, 188);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private VectorInput4D vectorAInput;
		private VectorInput4D vectorBInput;
		private System.Windows.Forms.Label labelA;
		private System.Windows.Forms.Label labelB;
		private System.Windows.Forms.Label labelAngle;
		private System.Windows.Forms.TextBox AngleGradInput;
	}
}

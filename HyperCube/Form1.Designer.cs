namespace HyperCube
{
	partial class Form1
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

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.canvas = new System.Windows.Forms.PictureBox();
			this.looper = new System.Windows.Forms.Timer(this.components);
			this.axisPicker = new HyperCube.AxisPicker();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.Dock = System.Windows.Forms.DockStyle.Left;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(685, 450);
			this.canvas.TabIndex = 0;
			this.canvas.TabStop = false;
			// 
			// looper
			// 
			this.looper.Interval = 5;
			this.looper.Tick += new System.EventHandler(this.looper_Tick);
			// 
			// axisPicker
			// 
			this.axisPicker.Location = new System.Drawing.Point(691, 12);
			this.axisPicker.Name = "axisPicker";
			this.axisPicker.Size = new System.Drawing.Size(274, 279);
			this.axisPicker.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(977, 450);
			this.Controls.Add(this.axisPicker);
			this.Controls.Add(this.canvas);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.Timer looper;
		private AxisPicker axisPicker;
	}
}


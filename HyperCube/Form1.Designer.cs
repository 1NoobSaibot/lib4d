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
			components = new System.ComponentModel.Container();
			canvas = new PictureBox();
			looper = new System.Windows.Forms.Timer(components);
			shapePicker = new ListBox();
			axisPicker = new AxisPicker();
			((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
			SuspendLayout();
			// 
			// canvas
			// 
			canvas.Dock = DockStyle.Fill;
			canvas.Location = new Point(0, 0);
			canvas.Margin = new Padding(4, 3, 4, 3);
			canvas.Name = "canvas";
			canvas.Size = new Size(1140, 519);
			canvas.TabIndex = 0;
			canvas.TabStop = false;
			// 
			// looper
			// 
			looper.Interval = 1;
			looper.Tick += Looper_Tick;
			// 
			// shapePicker
			// 
			shapePicker.FormattingEnabled = true;
			shapePicker.ItemHeight = 15;
			shapePicker.Location = new Point(0, 329);
			shapePicker.Margin = new Padding(4, 3, 4, 3);
			shapePicker.Name = "shapePicker";
			shapePicker.Size = new Size(139, 94);
			shapePicker.TabIndex = 2;
			// 
			// axisPicker
			// 
			axisPicker.Location = new Point(0, 0);
			axisPicker.Margin = new Padding(5, 3, 5, 3);
			axisPicker.Name = "axisPicker";
			axisPicker.Size = new Size(320, 322);
			axisPicker.TabIndex = 1;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1140, 519);
			Controls.Add(shapePicker);
			Controls.Add(axisPicker);
			Controls.Add(canvas);
			FormBorderStyle = FormBorderStyle.None;
			Margin = new Padding(4, 3, 4, 3);
			Name = "Form1";
			Text = "Form1";
			WindowState = FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)canvas).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.Timer looper;
		private AxisPicker axisPicker;
		private System.Windows.Forms.ListBox shapePicker;
	}
}


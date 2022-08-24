namespace Rotate4DSearcher
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
			this.candidates = new System.Windows.Forms.ListBox();
			this.candidatesUpdater = new System.Windows.Forms.Timer(this.components);
			this.logLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// candidates
			// 
			this.candidates.FormattingEnabled = true;
			this.candidates.Location = new System.Drawing.Point(12, 25);
			this.candidates.Name = "candidates";
			this.candidates.Size = new System.Drawing.Size(1180, 576);
			this.candidates.TabIndex = 56;
			// 
			// candidatesUpdater
			// 
			this.candidatesUpdater.Tick += new System.EventHandler(this.candidatesUpdater_Tick);
			// 
			// logLabel
			// 
			this.logLabel.AutoSize = true;
			this.logLabel.Location = new System.Drawing.Point(9, 608);
			this.logLabel.Name = "logLabel";
			this.logLabel.Size = new System.Drawing.Size(41, 13);
			this.logLabel.TabIndex = 51;
			this.logLabel.Text = "label22";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1204, 630);
			this.Controls.Add(this.candidates);
			this.Controls.Add(this.logLabel);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion
		private System.Windows.Forms.ListBox candidates;
		private System.Windows.Forms.Timer candidatesUpdater;
		private System.Windows.Forms.Label logLabel;
	}
}


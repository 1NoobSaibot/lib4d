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
			this.addRotationSurfaceButton = new System.Windows.Forms.Button();
			this.addPairButton = new System.Windows.Forms.Button();
			this.rotationSurfacesListBox = new System.Windows.Forms.ListBox();
			this.label17 = new System.Windows.Forms.Label();
			this.fromToPairsListBox = new System.Windows.Forms.ListBox();
			this.removeSelectedSurface = new System.Windows.Forms.Button();
			this.label18 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.logLabel = new System.Windows.Forms.Label();
			this.candidates = new System.Windows.Forms.ListBox();
			this.SelectedFormulas = new System.Windows.Forms.TextBox();
			this.candidatesUpdater = new System.Windows.Forms.Timer(this.components);
			this.vectorInput4DTo = new Rotate4DSearcher.Components.VectorInput4D();
			this.rotationSurfaceInput4D = new Rotate4DSearcher.Components.RotationSurfaceInput4D();
			this.vectorInput4DFrom = new Rotate4DSearcher.Components.VectorInput4D();
			this.SuspendLayout();
			// 
			// addRotationSurfaceButton
			// 
			this.addRotationSurfaceButton.Location = new System.Drawing.Point(204, 569);
			this.addRotationSurfaceButton.Name = "addRotationSurfaceButton";
			this.addRotationSurfaceButton.Size = new System.Drawing.Size(121, 23);
			this.addRotationSurfaceButton.TabIndex = 21;
			this.addRotationSurfaceButton.Text = "Add Rotation Surface";
			this.addRotationSurfaceButton.UseVisualStyleBackColor = true;
			this.addRotationSurfaceButton.Click += new System.EventHandler(this.addRotationSurfaceButton_Click);
			// 
			// addPairButton
			// 
			this.addPairButton.Location = new System.Drawing.Point(399, 569);
			this.addPairButton.Name = "addPairButton";
			this.addPairButton.Size = new System.Drawing.Size(210, 23);
			this.addPairButton.TabIndex = 40;
			this.addPairButton.Text = "Add Pair";
			this.addPairButton.UseVisualStyleBackColor = true;
			this.addPairButton.Click += new System.EventHandler(this.addPairButton_Click);
			// 
			// rotationSurfacesListBox
			// 
			this.rotationSurfacesListBox.FormattingEnabled = true;
			this.rotationSurfacesListBox.Location = new System.Drawing.Point(12, 25);
			this.rotationSurfacesListBox.Name = "rotationSurfacesListBox";
			this.rotationSurfacesListBox.Size = new System.Drawing.Size(313, 316);
			this.rotationSurfacesListBox.TabIndex = 42;
			this.rotationSurfacesListBox.SelectedIndexChanged += new System.EventHandler(this.rotationSurfacesListBox_SelectedIndexChanged);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(12, 9);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(92, 13);
			this.label17.TabIndex = 43;
			this.label17.Text = "Rotation Surfaces";
			// 
			// fromToPairsListBox
			// 
			this.fromToPairsListBox.FormattingEnabled = true;
			this.fromToPairsListBox.Location = new System.Drawing.Point(353, 25);
			this.fromToPairsListBox.Name = "fromToPairsListBox";
			this.fromToPairsListBox.Size = new System.Drawing.Size(306, 316);
			this.fromToPairsListBox.TabIndex = 44;
			// 
			// removeSelectedSurface
			// 
			this.removeSelectedSurface.Location = new System.Drawing.Point(242, 346);
			this.removeSelectedSurface.Name = "removeSelectedSurface";
			this.removeSelectedSurface.Size = new System.Drawing.Size(83, 23);
			this.removeSelectedSurface.TabIndex = 45;
			this.removeSelectedSurface.Text = "Remove";
			this.removeSelectedSurface.UseVisualStyleBackColor = true;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(396, 9);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(72, 13);
			this.label18.TabIndex = 46;
			this.label18.Text = "From-To Pairs";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(381, 380);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(38, 13);
			this.label20.TabIndex = 49;
			this.label20.Text = "FROM";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(535, 380);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(22, 13);
			this.label21.TabIndex = 50;
			this.label21.Text = "TO";
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
			// candidates
			// 
			this.candidates.FormattingEnabled = true;
			this.candidates.Location = new System.Drawing.Point(681, 25);
			this.candidates.Name = "candidates";
			this.candidates.Size = new System.Drawing.Size(511, 316);
			this.candidates.TabIndex = 56;
			// 
			// SelectedFormulas
			// 
			this.SelectedFormulas.Location = new System.Drawing.Point(681, 359);
			this.SelectedFormulas.Multiline = true;
			this.SelectedFormulas.Name = "SelectedFormulas";
			this.SelectedFormulas.Size = new System.Drawing.Size(511, 259);
			this.SelectedFormulas.TabIndex = 57;
			// 
			// candidatesUpdater
			// 
			this.candidatesUpdater.Interval = 3000;
			this.candidatesUpdater.Tick += new System.EventHandler(this.candidatesUpdater_Tick);
			// 
			// vectorInput4DTo
			// 
			this.vectorInput4DTo.Location = new System.Drawing.Point(509, 396);
			this.vectorInput4DTo.Name = "vectorInput4DTo";
			this.vectorInput4DTo.Size = new System.Drawing.Size(150, 139);
			this.vectorInput4DTo.TabIndex = 55;
			// 
			// rotationSurfaceInput4D
			// 
			this.rotationSurfaceInput4D.BackColor = System.Drawing.SystemColors.ControlLight;
			this.rotationSurfaceInput4D.Location = new System.Drawing.Point(15, 375);
			this.rotationSurfaceInput4D.Name = "rotationSurfaceInput4D";
			this.rotationSurfaceInput4D.Size = new System.Drawing.Size(310, 188);
			this.rotationSurfaceInput4D.TabIndex = 54;
			// 
			// vectorInput4DFrom
			// 
			this.vectorInput4DFrom.Location = new System.Drawing.Point(353, 396);
			this.vectorInput4DFrom.Name = "vectorInput4DFrom";
			this.vectorInput4DFrom.Size = new System.Drawing.Size(150, 139);
			this.vectorInput4DFrom.TabIndex = 53;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1204, 630);
			this.Controls.Add(this.SelectedFormulas);
			this.Controls.Add(this.candidates);
			this.Controls.Add(this.vectorInput4DTo);
			this.Controls.Add(this.rotationSurfaceInput4D);
			this.Controls.Add(this.vectorInput4DFrom);
			this.Controls.Add(this.logLabel);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.removeSelectedSurface);
			this.Controls.Add(this.fromToPairsListBox);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.rotationSurfacesListBox);
			this.Controls.Add(this.addPairButton);
			this.Controls.Add(this.addRotationSurfaceButton);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion
		private System.Windows.Forms.Button addRotationSurfaceButton;
		private System.Windows.Forms.Button addPairButton;
		private System.Windows.Forms.ListBox rotationSurfacesListBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ListBox fromToPairsListBox;
		private System.Windows.Forms.Button removeSelectedSurface;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label logLabel;
		private Components.VectorInput4D vectorInput4DFrom;
		private Components.RotationSurfaceInput4D rotationSurfaceInput4D;
		private Components.VectorInput4D vectorInput4DTo;
		private System.Windows.Forms.ListBox candidates;
		private System.Windows.Forms.TextBox SelectedFormulas;
		private System.Windows.Forms.Timer candidatesUpdater;
	}
}


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
			this.loadButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.aXInput = new System.Windows.Forms.TextBox();
			this.aYInput = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.aZInput = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.aQInput = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.bQInput = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.bZInput = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.bYInput = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.bXInput = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.normalizeAButton = new System.Windows.Forms.Button();
			this.normalizeBButton = new System.Windows.Forms.Button();
			this.addRotationSurfaceButton = new System.Windows.Forms.Button();
			this.addPairButton = new System.Windows.Forms.Button();
			this.normalizeToButton = new System.Windows.Forms.Button();
			this.normalizeFromButton = new System.Windows.Forms.Button();
			this.textBox10 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox11 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox12 = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBox13 = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.fromQInput = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.fromZInput = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.fromYInput = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.fromXInput = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBox18 = new System.Windows.Forms.TextBox();
			this.rotationSurfacesListBox = new System.Windows.Forms.ListBox();
			this.label17 = new System.Windows.Forms.Label();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.removeSelectedSurface = new System.Windows.Forms.Button();
			this.label18 = new System.Windows.Forms.Label();
			this.rotationAngleInput = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.logLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// loadButton
			// 
			this.loadButton.Location = new System.Drawing.Point(1097, 561);
			this.loadButton.Name = "loadButton";
			this.loadButton.Size = new System.Drawing.Size(95, 23);
			this.loadButton.TabIndex = 1;
			this.loadButton.Text = "Load Samples";
			this.loadButton.UseVisualStyleBackColor = true;
			this.loadButton.Click += new System.EventHandler(this.onLoad);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 348);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "A.X";
			// 
			// aXInput
			// 
			this.aXInput.Location = new System.Drawing.Point(41, 345);
			this.aXInput.Name = "aXInput";
			this.aXInput.Size = new System.Drawing.Size(121, 20);
			this.aXInput.TabIndex = 4;
			// 
			// aYInput
			// 
			this.aYInput.Location = new System.Drawing.Point(41, 371);
			this.aYInput.Name = "aYInput";
			this.aYInput.Size = new System.Drawing.Size(121, 20);
			this.aYInput.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 374);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "A.Y";
			// 
			// aZInput
			// 
			this.aZInput.Location = new System.Drawing.Point(41, 397);
			this.aZInput.Name = "aZInput";
			this.aZInput.Size = new System.Drawing.Size(121, 20);
			this.aZInput.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 400);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "A.Z";
			// 
			// aQInput
			// 
			this.aQInput.Location = new System.Drawing.Point(41, 423);
			this.aQInput.Name = "aQInput";
			this.aQInput.Size = new System.Drawing.Size(121, 20);
			this.aQInput.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 426);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "A.Q";
			// 
			// bQInput
			// 
			this.bQInput.Location = new System.Drawing.Point(41, 543);
			this.bQInput.Name = "bQInput";
			this.bQInput.Size = new System.Drawing.Size(121, 20);
			this.bQInput.TabIndex = 18;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(11, 546);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(25, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "B.Q";
			// 
			// bZInput
			// 
			this.bZInput.Location = new System.Drawing.Point(41, 517);
			this.bZInput.Name = "bZInput";
			this.bZInput.Size = new System.Drawing.Size(121, 20);
			this.bZInput.TabIndex = 16;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(11, 520);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(24, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "B.Z";
			// 
			// bYInput
			// 
			this.bYInput.Location = new System.Drawing.Point(41, 491);
			this.bYInput.Name = "bYInput";
			this.bYInput.Size = new System.Drawing.Size(121, 20);
			this.bYInput.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(11, 494);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "B.Y";
			// 
			// bXInput
			// 
			this.bXInput.Location = new System.Drawing.Point(41, 465);
			this.bXInput.Name = "bXInput";
			this.bXInput.Size = new System.Drawing.Size(121, 20);
			this.bXInput.TabIndex = 12;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(11, 468);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(24, 13);
			this.label8.TabIndex = 11;
			this.label8.Text = "B.X";
			// 
			// normalizeAButton
			// 
			this.normalizeAButton.Location = new System.Drawing.Point(168, 345);
			this.normalizeAButton.Name = "normalizeAButton";
			this.normalizeAButton.Size = new System.Drawing.Size(83, 23);
			this.normalizeAButton.TabIndex = 19;
			this.normalizeAButton.Text = "Normalize A";
			this.normalizeAButton.UseVisualStyleBackColor = true;
			// 
			// normalizeBButton
			// 
			this.normalizeBButton.Location = new System.Drawing.Point(168, 465);
			this.normalizeBButton.Name = "normalizeBButton";
			this.normalizeBButton.Size = new System.Drawing.Size(83, 23);
			this.normalizeBButton.TabIndex = 20;
			this.normalizeBButton.Text = "Normalize B";
			this.normalizeBButton.UseVisualStyleBackColor = true;
			// 
			// addRotationSurfaceButton
			// 
			this.addRotationSurfaceButton.Location = new System.Drawing.Point(41, 569);
			this.addRotationSurfaceButton.Name = "addRotationSurfaceButton";
			this.addRotationSurfaceButton.Size = new System.Drawing.Size(121, 23);
			this.addRotationSurfaceButton.TabIndex = 21;
			this.addRotationSurfaceButton.Text = "Add Rotation Surface";
			this.addRotationSurfaceButton.UseVisualStyleBackColor = true;
			this.addRotationSurfaceButton.Click += new System.EventHandler(this.addRotationSurfaceButton_Click);
			// 
			// addPairButton
			// 
			this.addPairButton.Location = new System.Drawing.Point(331, 571);
			this.addPairButton.Name = "addPairButton";
			this.addPairButton.Size = new System.Drawing.Size(210, 23);
			this.addPairButton.TabIndex = 40;
			this.addPairButton.Text = "Add Pair";
			this.addPairButton.UseVisualStyleBackColor = true;
			// 
			// normalizeToButton
			// 
			this.normalizeToButton.Location = new System.Drawing.Point(458, 467);
			this.normalizeToButton.Name = "normalizeToButton";
			this.normalizeToButton.Size = new System.Drawing.Size(83, 23);
			this.normalizeToButton.TabIndex = 39;
			this.normalizeToButton.Text = "Normalize To";
			this.normalizeToButton.UseVisualStyleBackColor = true;
			// 
			// normalizeFromButton
			// 
			this.normalizeFromButton.Location = new System.Drawing.Point(458, 347);
			this.normalizeFromButton.Name = "normalizeFromButton";
			this.normalizeFromButton.Size = new System.Drawing.Size(96, 23);
			this.normalizeFromButton.TabIndex = 38;
			this.normalizeFromButton.Text = "Normalize From";
			this.normalizeFromButton.UseVisualStyleBackColor = true;
			// 
			// textBox10
			// 
			this.textBox10.Location = new System.Drawing.Point(331, 545);
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new System.Drawing.Size(121, 20);
			this.textBox10.TabIndex = 37;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(301, 548);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(15, 13);
			this.label9.TabIndex = 36;
			this.label9.Text = "Q";
			// 
			// textBox11
			// 
			this.textBox11.Location = new System.Drawing.Point(331, 519);
			this.textBox11.Name = "textBox11";
			this.textBox11.Size = new System.Drawing.Size(121, 20);
			this.textBox11.TabIndex = 35;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(301, 522);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(14, 13);
			this.label10.TabIndex = 34;
			this.label10.Text = "Z";
			// 
			// textBox12
			// 
			this.textBox12.Location = new System.Drawing.Point(331, 493);
			this.textBox12.Name = "textBox12";
			this.textBox12.Size = new System.Drawing.Size(121, 20);
			this.textBox12.TabIndex = 33;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(301, 496);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(14, 13);
			this.label11.TabIndex = 32;
			this.label11.Text = "Y";
			// 
			// textBox13
			// 
			this.textBox13.Location = new System.Drawing.Point(331, 467);
			this.textBox13.Name = "textBox13";
			this.textBox13.Size = new System.Drawing.Size(121, 20);
			this.textBox13.TabIndex = 31;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(301, 470);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(14, 13);
			this.label12.TabIndex = 30;
			this.label12.Text = "X";
			// 
			// fromQInput
			// 
			this.fromQInput.Location = new System.Drawing.Point(331, 425);
			this.fromQInput.Name = "fromQInput";
			this.fromQInput.Size = new System.Drawing.Size(121, 20);
			this.fromQInput.TabIndex = 29;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(301, 428);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(15, 13);
			this.label13.TabIndex = 28;
			this.label13.Text = "Q";
			// 
			// fromZInput
			// 
			this.fromZInput.Location = new System.Drawing.Point(331, 399);
			this.fromZInput.Name = "fromZInput";
			this.fromZInput.Size = new System.Drawing.Size(121, 20);
			this.fromZInput.TabIndex = 27;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(301, 402);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(14, 13);
			this.label14.TabIndex = 26;
			this.label14.Text = "Z";
			// 
			// fromYInput
			// 
			this.fromYInput.Location = new System.Drawing.Point(331, 373);
			this.fromYInput.Name = "fromYInput";
			this.fromYInput.Size = new System.Drawing.Size(121, 20);
			this.fromYInput.TabIndex = 25;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(301, 376);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(14, 13);
			this.label15.TabIndex = 24;
			this.label15.Text = "Y";
			// 
			// fromXInput
			// 
			this.fromXInput.Location = new System.Drawing.Point(331, 347);
			this.fromXInput.Name = "fromXInput";
			this.fromXInput.Size = new System.Drawing.Size(121, 20);
			this.fromXInput.TabIndex = 23;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(301, 350);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(14, 13);
			this.label16.TabIndex = 22;
			this.label16.Text = "X";
			// 
			// textBox18
			// 
			this.textBox18.Location = new System.Drawing.Point(695, 25);
			this.textBox18.Multiline = true;
			this.textBox18.Name = "textBox18";
			this.textBox18.Size = new System.Drawing.Size(239, 310);
			this.textBox18.TabIndex = 41;
			// 
			// rotationSurfacesListBox
			// 
			this.rotationSurfacesListBox.FormattingEnabled = true;
			this.rotationSurfacesListBox.Location = new System.Drawing.Point(12, 25);
			this.rotationSurfacesListBox.Name = "rotationSurfacesListBox";
			this.rotationSurfacesListBox.Size = new System.Drawing.Size(239, 264);
			this.rotationSurfacesListBox.TabIndex = 42;
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
			// listBox2
			// 
			this.listBox2.FormattingEnabled = true;
			this.listBox2.Location = new System.Drawing.Point(304, 25);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(239, 277);
			this.listBox2.TabIndex = 44;
			// 
			// removeSelectedSurface
			// 
			this.removeSelectedSurface.Location = new System.Drawing.Point(168, 569);
			this.removeSelectedSurface.Name = "removeSelectedSurface";
			this.removeSelectedSurface.Size = new System.Drawing.Size(83, 23);
			this.removeSelectedSurface.TabIndex = 45;
			this.removeSelectedSurface.Text = "Remove";
			this.removeSelectedSurface.UseVisualStyleBackColor = true;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(301, 9);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(72, 13);
			this.label18.TabIndex = 46;
			this.label18.Text = "From-To Pairs";
			// 
			// rotationAngleInput
			// 
			this.rotationAngleInput.Location = new System.Drawing.Point(90, 308);
			this.rotationAngleInput.Name = "rotationAngleInput";
			this.rotationAngleInput.Size = new System.Drawing.Size(72, 20);
			this.rotationAngleInput.TabIndex = 47;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(12, 311);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(66, 13);
			this.label19.TabIndex = 48;
			this.label19.Text = "Angle (Grad)";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(328, 322);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(38, 13);
			this.label20.TabIndex = 49;
			this.label20.Text = "FROM";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(328, 448);
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1204, 630);
			this.Controls.Add(this.logLabel);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.rotationAngleInput);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.removeSelectedSurface);
			this.Controls.Add(this.listBox2);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.rotationSurfacesListBox);
			this.Controls.Add(this.textBox18);
			this.Controls.Add(this.addPairButton);
			this.Controls.Add(this.normalizeToButton);
			this.Controls.Add(this.normalizeFromButton);
			this.Controls.Add(this.textBox10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textBox11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textBox12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textBox13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.fromQInput);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.fromZInput);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.fromYInput);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.fromXInput);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.addRotationSurfaceButton);
			this.Controls.Add(this.normalizeBButton);
			this.Controls.Add(this.normalizeAButton);
			this.Controls.Add(this.bQInput);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.bZInput);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.bYInput);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.bXInput);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.aQInput);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.aZInput);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.aYInput);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.aXInput);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.loadButton);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion
		private System.Windows.Forms.Button loadButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox aXInput;
		private System.Windows.Forms.TextBox aYInput;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox aZInput;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox aQInput;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox bQInput;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox bZInput;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox bYInput;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox bXInput;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button normalizeAButton;
		private System.Windows.Forms.Button normalizeBButton;
		private System.Windows.Forms.Button addRotationSurfaceButton;
		private System.Windows.Forms.Button addPairButton;
		private System.Windows.Forms.Button normalizeToButton;
		private System.Windows.Forms.Button normalizeFromButton;
		private System.Windows.Forms.TextBox textBox10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBox12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBox13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox fromQInput;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox fromZInput;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox fromYInput;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox fromXInput;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textBox18;
		private System.Windows.Forms.ListBox rotationSurfacesListBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.Button removeSelectedSurface;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox rotationAngleInput;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label logLabel;
	}
}


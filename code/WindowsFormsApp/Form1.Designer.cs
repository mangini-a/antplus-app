namespace WindowsFormsApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.startButton = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.desiredResistanceLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.cadenceLabel = new System.Windows.Forms.Label();
            this.heartRateLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startButton.FlatAppearance.BorderSize = 0;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.startButton.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(4, 155);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startButton.Name = "startButton";
            this.tableLayoutPanel1.SetRowSpan(this.startButton, 3);
            this.startButton.Size = new System.Drawing.Size(282, 219);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // desiredResistanceLabel
            // 
            this.desiredResistanceLabel.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desiredResistanceLabel.ForeColor = System.Drawing.Color.Navy;
            this.desiredResistanceLabel.Location = new System.Drawing.Point(293, 1);
            this.desiredResistanceLabel.Name = "desiredResistanceLabel";
            this.desiredResistanceLabel.Size = new System.Drawing.Size(861, 149);
            this.desiredResistanceLabel.TabIndex = 1;
            this.desiredResistanceLabel.Text = "The desired resistance will be shown here...";
            this.desiredResistanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.timeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.startButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.desiredResistanceLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.speedLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cadenceLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.heartRateLabel, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1158, 379);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // timeLabel
            // 
            this.timeLabel.Font = new System.Drawing.Font("Rockwell", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(4, 1);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(282, 149);
            this.timeLabel.TabIndex = 2;
            this.timeLabel.Text = "00:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // speedLabel
            // 
            this.speedLabel.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(293, 151);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(861, 74);
            this.speedLabel.TabIndex = 3;
            this.speedLabel.Text = "The speed will be shown here...";
            this.speedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cadenceLabel
            // 
            this.cadenceLabel.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cadenceLabel.Location = new System.Drawing.Point(293, 226);
            this.cadenceLabel.Name = "cadenceLabel";
            this.cadenceLabel.Size = new System.Drawing.Size(861, 74);
            this.cadenceLabel.TabIndex = 4;
            this.cadenceLabel.Text = "The cadence will be shown here...";
            this.cadenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // heartRateLabel
            // 
            this.heartRateLabel.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heartRateLabel.Location = new System.Drawing.Point(293, 301);
            this.heartRateLabel.Name = "heartRateLabel";
            this.heartRateLabel.Size = new System.Drawing.Size(861, 77);
            this.heartRateLabel.TabIndex = 5;
            this.heartRateLabel.Text = "The HR will be shown here...";
            this.heartRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(1182, 403);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Heart Rate Display";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Label desiredResistanceLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label cadenceLabel;
        private System.Windows.Forms.Label heartRateLabel;
    }
}


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
            this.tableLayoutPanelGlobal = new System.Windows.Forms.TableLayoutPanel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.heartRateLabel = new System.Windows.Forms.Label();
            this.powerLabel = new System.Windows.Forms.Label();
            this.cadenceLabel = new System.Windows.Forms.Label();
            this.groupBoxWorkout = new System.Windows.Forms.GroupBox();
            this.groupBoxGarmin = new System.Windows.Forms.GroupBox();
            this.groupBoxPolar = new System.Windows.Forms.GroupBox();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelWorkout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelGarmin = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelPolar = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelControl = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelGlobal.SuspendLayout();
            this.groupBoxWorkout.SuspendLayout();
            this.groupBoxGarmin.SuspendLayout();
            this.groupBoxPolar.SuspendLayout();
            this.groupBoxControl.SuspendLayout();
            this.tableLayoutPanelWorkout.SuspendLayout();
            this.tableLayoutPanelGarmin.SuspendLayout();
            this.tableLayoutPanelPolar.SuspendLayout();
            this.tableLayoutPanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startButton.FlatAppearance.BorderSize = 0;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.startButton.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(3, 4);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(400, 114);
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
            this.desiredResistanceLabel.Font = new System.Drawing.Font("Nirmala UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desiredResistanceLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.desiredResistanceLabel.Location = new System.Drawing.Point(873, 0);
            this.desiredResistanceLabel.Name = "desiredResistanceLabel";
            this.desiredResistanceLabel.Size = new System.Drawing.Size(176, 122);
            this.desiredResistanceLabel.TabIndex = 1;
            this.desiredResistanceLabel.Text = "8 %";
            this.desiredResistanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelGlobal
            // 
            this.tableLayoutPanelGlobal.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelGlobal.ColumnCount = 1;
            this.tableLayoutPanelGlobal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGlobal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelGlobal.Controls.Add(this.groupBoxWorkout, 0, 0);
            this.tableLayoutPanelGlobal.Controls.Add(this.groupBoxGarmin, 0, 1);
            this.tableLayoutPanelGlobal.Controls.Add(this.groupBoxPolar, 0, 2);
            this.tableLayoutPanelGlobal.Controls.Add(this.groupBoxControl, 0, 3);
            this.tableLayoutPanelGlobal.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanelGlobal.Name = "tableLayoutPanelGlobal";
            this.tableLayoutPanelGlobal.RowCount = 4;
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelGlobal.Size = new System.Drawing.Size(1238, 649);
            this.tableLayoutPanelGlobal.TabIndex = 2;
            // 
            // timeLabel
            // 
            this.timeLabel.Font = new System.Drawing.Font("Rockwell", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(409, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(400, 122);
            this.timeLabel.TabIndex = 2;
            this.timeLabel.Text = "00:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // speedLabel
            // 
            this.speedLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.speedLabel.Location = new System.Drawing.Point(1033, 0);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(182, 122);
            this.speedLabel.TabIndex = 3;
            this.speedLabel.Text = "27 km/h";
            this.speedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // heartRateLabel
            // 
            this.heartRateLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heartRateLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.heartRateLabel.Location = new System.Drawing.Point(611, 0);
            this.heartRateLabel.Name = "heartRateLabel";
            this.heartRateLabel.Size = new System.Drawing.Size(176, 122);
            this.heartRateLabel.TabIndex = 5;
            this.heartRateLabel.Text = "88 bpm";
            this.heartRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // powerLabel
            // 
            this.powerLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.powerLabel.Location = new System.Drawing.Point(609, 0);
            this.powerLabel.Name = "powerLabel";
            this.powerLabel.Size = new System.Drawing.Size(176, 122);
            this.powerLabel.TabIndex = 6;
            this.powerLabel.Text = "40 W";
            this.powerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cadenceLabel
            // 
            this.cadenceLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cadenceLabel.Location = new System.Drawing.Point(185, 0);
            this.cadenceLabel.Name = "cadenceLabel";
            this.cadenceLabel.Size = new System.Drawing.Size(176, 122);
            this.cadenceLabel.TabIndex = 7;
            this.cadenceLabel.Text = "0 rpm";
            this.cadenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxWorkout
            // 
            this.groupBoxWorkout.Controls.Add(this.tableLayoutPanelWorkout);
            this.groupBoxWorkout.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxWorkout.Location = new System.Drawing.Point(4, 4);
            this.groupBoxWorkout.Name = "groupBoxWorkout";
            this.groupBoxWorkout.Size = new System.Drawing.Size(1230, 155);
            this.groupBoxWorkout.TabIndex = 0;
            this.groupBoxWorkout.TabStop = false;
            this.groupBoxWorkout.Text = "Workout - Actions and Progress";
            // 
            // groupBoxGarmin
            // 
            this.groupBoxGarmin.Controls.Add(this.tableLayoutPanelGarmin);
            this.groupBoxGarmin.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxGarmin.Location = new System.Drawing.Point(4, 166);
            this.groupBoxGarmin.Name = "groupBoxGarmin";
            this.groupBoxGarmin.Size = new System.Drawing.Size(1230, 155);
            this.groupBoxGarmin.TabIndex = 1;
            this.groupBoxGarmin.TabStop = false;
            this.groupBoxGarmin.Text = "Garmin Tacx NEO 3M Smart Trainer - Incoming Data";
            // 
            // groupBoxPolar
            // 
            this.groupBoxPolar.Controls.Add(this.tableLayoutPanelPolar);
            this.groupBoxPolar.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPolar.Location = new System.Drawing.Point(4, 328);
            this.groupBoxPolar.Name = "groupBoxPolar";
            this.groupBoxPolar.Size = new System.Drawing.Size(1230, 155);
            this.groupBoxPolar.TabIndex = 2;
            this.groupBoxPolar.TabStop = false;
            this.groupBoxPolar.Text = "Polar H10 Heart Rate Sensor - Incoming Data";
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.tableLayoutPanelControl);
            this.groupBoxControl.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxControl.Location = new System.Drawing.Point(4, 490);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Size = new System.Drawing.Size(1230, 155);
            this.groupBoxControl.TabIndex = 3;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "Control - Setpoint Definition (bpm) and Applied Resistance";
            // 
            // tableLayoutPanelWorkout
            // 
            this.tableLayoutPanelWorkout.ColumnCount = 3;
            this.tableLayoutPanelWorkout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelWorkout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelWorkout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelWorkout.Controls.Add(this.timeLabel, 1, 0);
            this.tableLayoutPanelWorkout.Controls.Add(this.startButton, 0, 0);
            this.tableLayoutPanelWorkout.Location = new System.Drawing.Point(6, 27);
            this.tableLayoutPanelWorkout.Name = "tableLayoutPanelWorkout";
            this.tableLayoutPanelWorkout.RowCount = 1;
            this.tableLayoutPanelWorkout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelWorkout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanelWorkout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanelWorkout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanelWorkout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanelWorkout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanelWorkout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanelWorkout.Size = new System.Drawing.Size(1218, 122);
            this.tableLayoutPanelWorkout.TabIndex = 0;
            // 
            // tableLayoutPanelGarmin
            // 
            this.tableLayoutPanelGarmin.ColumnCount = 8;
            this.tableLayoutPanelGarmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelGarmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelGarmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelGarmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelGarmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelGarmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelGarmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelGarmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelGarmin.Controls.Add(this.cadenceLabel, 1, 0);
            this.tableLayoutPanelGarmin.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanelGarmin.Controls.Add(this.speedLabel, 7, 0);
            this.tableLayoutPanelGarmin.Controls.Add(this.pictureBox3, 6, 0);
            this.tableLayoutPanelGarmin.Controls.Add(this.powerLabel, 4, 0);
            this.tableLayoutPanelGarmin.Controls.Add(this.pictureBox2, 3, 0);
            this.tableLayoutPanelGarmin.Location = new System.Drawing.Point(6, 27);
            this.tableLayoutPanelGarmin.Name = "tableLayoutPanelGarmin";
            this.tableLayoutPanelGarmin.RowCount = 1;
            this.tableLayoutPanelGarmin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGarmin.Size = new System.Drawing.Size(1218, 122);
            this.tableLayoutPanelGarmin.TabIndex = 0;
            // 
            // tableLayoutPanelPolar
            // 
            this.tableLayoutPanelPolar.ColumnCount = 4;
            this.tableLayoutPanelPolar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelPolar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelPolar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelPolar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelPolar.Controls.Add(this.heartRateLabel, 2, 0);
            this.tableLayoutPanelPolar.Controls.Add(this.pictureBox4, 1, 0);
            this.tableLayoutPanelPolar.Location = new System.Drawing.Point(6, 27);
            this.tableLayoutPanelPolar.Name = "tableLayoutPanelPolar";
            this.tableLayoutPanelPolar.RowCount = 1;
            this.tableLayoutPanelPolar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPolar.Size = new System.Drawing.Size(1218, 122);
            this.tableLayoutPanelPolar.TabIndex = 0;
            // 
            // tableLayoutPanelControl
            // 
            this.tableLayoutPanelControl.ColumnCount = 7;
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33133F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0015F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0015F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33133F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0015F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0015F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33133F));
            this.tableLayoutPanelControl.Controls.Add(this.desiredResistanceLabel, 5, 0);
            this.tableLayoutPanelControl.Controls.Add(this.pictureBox5, 1, 0);
            this.tableLayoutPanelControl.Controls.Add(this.numericUpDown, 2, 0);
            this.tableLayoutPanelControl.Controls.Add(this.pictureBox6, 4, 0);
            this.tableLayoutPanelControl.Location = new System.Drawing.Point(6, 27);
            this.tableLayoutPanelControl.Name = "tableLayoutPanelControl";
            this.tableLayoutPanelControl.RowCount = 1;
            this.tableLayoutPanelControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelControl.Size = new System.Drawing.Size(1218, 122);
            this.tableLayoutPanelControl.TabIndex = 0;
            // 
            // numericUpDown
            // 
            this.numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown.Font = new System.Drawing.Font("Nirmala UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown.Location = new System.Drawing.Point(347, 35);
            this.numericUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(176, 52);
            this.numericUpDown.TabIndex = 2;
            this.numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WindowsFormsApp.Properties.Resources.cadence1;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::WindowsFormsApp.Properties.Resources.speed;
            this.pictureBox3.Location = new System.Drawing.Point(851, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(176, 116);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::WindowsFormsApp.Properties.Resources.power1;
            this.pictureBox2.Location = new System.Drawing.Point(427, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(176, 116);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::WindowsFormsApp.Properties.Resources.heartbeat;
            this.pictureBox4.Location = new System.Drawing.Point(429, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(176, 116);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::WindowsFormsApp.Properties.Resources.technology;
            this.pictureBox5.Location = new System.Drawing.Point(165, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(176, 116);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 3;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::WindowsFormsApp.Properties.Resources.setting;
            this.pictureBox6.Location = new System.Drawing.Point(691, 3);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(176, 116);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 4;
            this.pictureBox6.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.tableLayoutPanelGlobal);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Heart Rate Controller";
            this.tableLayoutPanelGlobal.ResumeLayout(false);
            this.groupBoxWorkout.ResumeLayout(false);
            this.groupBoxGarmin.ResumeLayout(false);
            this.groupBoxPolar.ResumeLayout(false);
            this.groupBoxControl.ResumeLayout(false);
            this.tableLayoutPanelWorkout.ResumeLayout(false);
            this.tableLayoutPanelGarmin.ResumeLayout(false);
            this.tableLayoutPanelPolar.ResumeLayout(false);
            this.tableLayoutPanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Label desiredResistanceLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGlobal;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label heartRateLabel;
        private System.Windows.Forms.Label powerLabel;
        private System.Windows.Forms.Label cadenceLabel;
        private System.Windows.Forms.GroupBox groupBoxWorkout;
        private System.Windows.Forms.GroupBox groupBoxGarmin;
        private System.Windows.Forms.GroupBox groupBoxPolar;
        private System.Windows.Forms.GroupBox groupBoxControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWorkout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGarmin;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPolar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelControl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
    }
}


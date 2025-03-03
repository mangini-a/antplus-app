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
            this.targetResistanceLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanelGlobal = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxWorkout = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelWorkout = new System.Windows.Forms.TableLayoutPanel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelData = new System.Windows.Forms.TableLayoutPanel();
            this.cadenceLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.powerLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.heartRateLabel = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.speedLabel = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelControl = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.actualResistanceLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bpmLabel = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.controlTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelGlobal.SuspendLayout();
            this.groupBoxWorkout.SuspendLayout();
            this.tableLayoutPanelWorkout.SuspendLayout();
            this.groupBoxData.SuspendLayout();
            this.tableLayoutPanelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBoxControl.SuspendLayout();
            this.tableLayoutPanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.LightGreen;
            this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startButton.FlatAppearance.BorderSize = 0;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.startButton.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(124, 4);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(358, 103);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // targetResistanceLabel
            // 
            this.targetResistanceLabel.Font = new System.Drawing.Font("Nirmala UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.targetResistanceLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.targetResistanceLabel.Location = new System.Drawing.Point(3, 0);
            this.targetResistanceLabel.Name = "targetResistanceLabel";
            this.targetResistanceLabel.Size = new System.Drawing.Size(206, 53);
            this.targetResistanceLabel.TabIndex = 1;
            this.targetResistanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelGlobal
            // 
            this.tableLayoutPanelGlobal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanelGlobal.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelGlobal.ColumnCount = 1;
            this.tableLayoutPanelGlobal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGlobal.Controls.Add(this.groupBoxWorkout, 0, 0);
            this.tableLayoutPanelGlobal.Controls.Add(this.groupBoxData, 0, 2);
            this.tableLayoutPanelGlobal.Controls.Add(this.groupBoxControl, 0, 4);
            this.tableLayoutPanelGlobal.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanelGlobal.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanelGlobal.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanelGlobal.Name = "tableLayoutPanelGlobal";
            this.tableLayoutPanelGlobal.RowCount = 5;
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanelGlobal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelGlobal.Size = new System.Drawing.Size(1238, 649);
            this.tableLayoutPanelGlobal.TabIndex = 2;
            // 
            // groupBoxWorkout
            // 
            this.groupBoxWorkout.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxWorkout.Controls.Add(this.tableLayoutPanelWorkout);
            this.groupBoxWorkout.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxWorkout.Location = new System.Drawing.Point(6, 6);
            this.groupBoxWorkout.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxWorkout.Name = "groupBoxWorkout";
            this.groupBoxWorkout.Size = new System.Drawing.Size(1226, 144);
            this.groupBoxWorkout.TabIndex = 0;
            this.groupBoxWorkout.TabStop = false;
            this.groupBoxWorkout.Text = "Workout progress";
            // 
            // tableLayoutPanelWorkout
            // 
            this.tableLayoutPanelWorkout.ColumnCount = 5;
            this.tableLayoutPanelWorkout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelWorkout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelWorkout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelWorkout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelWorkout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelWorkout.Controls.Add(this.timeLabel, 2, 0);
            this.tableLayoutPanelWorkout.Controls.Add(this.startButton, 1, 0);
            this.tableLayoutPanelWorkout.Controls.Add(this.stopButton, 3, 0);
            this.tableLayoutPanelWorkout.Location = new System.Drawing.Point(6, 27);
            this.tableLayoutPanelWorkout.Name = "tableLayoutPanelWorkout";
            this.tableLayoutPanelWorkout.RowCount = 1;
            this.tableLayoutPanelWorkout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelWorkout.Size = new System.Drawing.Size(1214, 111);
            this.tableLayoutPanelWorkout.TabIndex = 0;
            // 
            // timeLabel
            // 
            this.timeLabel.Font = new System.Drawing.Font("Rockwell", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(488, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(236, 111);
            this.timeLabel.TabIndex = 2;
            this.timeLabel.Text = "00:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.Tomato;
            this.stopButton.Enabled = false;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stopButton.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopButton.Location = new System.Drawing.Point(730, 3);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(358, 105);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // groupBoxData
            // 
            this.groupBoxData.Controls.Add(this.tableLayoutPanelData);
            this.groupBoxData.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxData.Location = new System.Drawing.Point(6, 174);
            this.groupBoxData.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.Size = new System.Drawing.Size(1226, 299);
            this.groupBoxData.TabIndex = 1;
            this.groupBoxData.TabStop = false;
            this.groupBoxData.Text = "Real-time data";
            // 
            // tableLayoutPanelData
            // 
            this.tableLayoutPanelData.ColumnCount = 7;
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelData.Controls.Add(this.cadenceLabel, 2, 0);
            this.tableLayoutPanelData.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanelData.Controls.Add(this.powerLabel, 5, 0);
            this.tableLayoutPanelData.Controls.Add(this.pictureBox2, 4, 0);
            this.tableLayoutPanelData.Controls.Add(this.heartRateLabel, 5, 1);
            this.tableLayoutPanelData.Controls.Add(this.pictureBox4, 4, 1);
            this.tableLayoutPanelData.Controls.Add(this.speedLabel, 2, 1);
            this.tableLayoutPanelData.Controls.Add(this.pictureBox3, 1, 1);
            this.tableLayoutPanelData.Location = new System.Drawing.Point(6, 27);
            this.tableLayoutPanelData.Name = "tableLayoutPanelData";
            this.tableLayoutPanelData.RowCount = 2;
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelData.Size = new System.Drawing.Size(1214, 266);
            this.tableLayoutPanelData.TabIndex = 0;
            // 
            // cadenceLabel
            // 
            this.cadenceLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cadenceLabel.Location = new System.Drawing.Point(306, 0);
            this.cadenceLabel.Name = "cadenceLabel";
            this.cadenceLabel.Size = new System.Drawing.Size(176, 133);
            this.cadenceLabel.TabIndex = 7;
            this.cadenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WindowsFormsApp.Properties.Resources.cadence1;
            this.pictureBox1.Location = new System.Drawing.Point(124, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 127);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // powerLabel
            // 
            this.powerLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.powerLabel.Location = new System.Drawing.Point(912, 0);
            this.powerLabel.Name = "powerLabel";
            this.powerLabel.Size = new System.Drawing.Size(176, 133);
            this.powerLabel.TabIndex = 6;
            this.powerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::WindowsFormsApp.Properties.Resources.power1;
            this.pictureBox2.Location = new System.Drawing.Point(730, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(176, 127);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // heartRateLabel
            // 
            this.heartRateLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heartRateLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.heartRateLabel.Location = new System.Drawing.Point(912, 133);
            this.heartRateLabel.Name = "heartRateLabel";
            this.heartRateLabel.Size = new System.Drawing.Size(176, 133);
            this.heartRateLabel.TabIndex = 5;
            this.heartRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::WindowsFormsApp.Properties.Resources.heartbeat;
            this.pictureBox4.Location = new System.Drawing.Point(730, 136);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(176, 127);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // speedLabel
            // 
            this.speedLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.speedLabel.Location = new System.Drawing.Point(306, 133);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(176, 133);
            this.speedLabel.TabIndex = 3;
            this.speedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::WindowsFormsApp.Properties.Resources.speed;
            this.pictureBox3.Location = new System.Drawing.Point(124, 136);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(176, 127);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.tableLayoutPanelControl);
            this.groupBoxControl.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxControl.Location = new System.Drawing.Point(6, 497);
            this.groupBoxControl.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Size = new System.Drawing.Size(1226, 146);
            this.groupBoxControl.TabIndex = 3;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "Control setting";
            // 
            // tableLayoutPanelControl
            // 
            this.tableLayoutPanelControl.ColumnCount = 7;
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0006F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9988F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0006F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanelControl.Controls.Add(this.pictureBox5, 1, 0);
            this.tableLayoutPanelControl.Controls.Add(this.pictureBox6, 4, 0);
            this.tableLayoutPanelControl.Controls.Add(this.tableLayoutPanel1, 5, 0);
            this.tableLayoutPanelControl.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanelControl.Location = new System.Drawing.Point(6, 27);
            this.tableLayoutPanelControl.Name = "tableLayoutPanelControl";
            this.tableLayoutPanelControl.RowCount = 1;
            this.tableLayoutPanelControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelControl.Size = new System.Drawing.Size(1214, 113);
            this.tableLayoutPanelControl.TabIndex = 0;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::WindowsFormsApp.Properties.Resources.technology;
            this.pictureBox5.Location = new System.Drawing.Point(87, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(176, 107);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 3;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::WindowsFormsApp.Properties.Resources.setting;
            this.pictureBox6.Location = new System.Drawing.Point(729, 3);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(176, 107);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 4;
            this.pictureBox6.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.targetResistanceLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.actualResistanceLabel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(911, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(212, 107);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // actualResistanceLabel
            // 
            this.actualResistanceLabel.Font = new System.Drawing.Font("Nirmala UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actualResistanceLabel.Location = new System.Drawing.Point(3, 53);
            this.actualResistanceLabel.Name = "actualResistanceLabel";
            this.actualResistanceLabel.Size = new System.Drawing.Size(206, 54);
            this.actualResistanceLabel.TabIndex = 2;
            this.actualResistanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.bpmLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(269, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(212, 107);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // bpmLabel
            // 
            this.bpmLabel.Font = new System.Drawing.Font("Nirmala UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bpmLabel.Location = new System.Drawing.Point(3, 53);
            this.bpmLabel.Name = "bpmLabel";
            this.bpmLabel.Size = new System.Drawing.Size(206, 54);
            this.bpmLabel.TabIndex = 0;
            this.bpmLabel.Text = "bpm";
            this.bpmLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numericUpDown
            // 
            this.numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown.Font = new System.Drawing.Font("Nirmala UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown.Location = new System.Drawing.Point(3, 5);
            this.numericUpDown.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(206, 43);
            this.numericUpDown.TabIndex = 2;
            this.numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(1, 156);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1236, 12);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(1, 479);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1236, 12);
            this.panel2.TabIndex = 5;
            // 
            // controlTimer
            // 
            this.controlTimer.Interval = 5000;
            this.controlTimer.Tick += new System.EventHandler(this.ControlTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.tableLayoutPanelGlobal);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Heartbeat controller";
            this.tableLayoutPanelGlobal.ResumeLayout(false);
            this.groupBoxWorkout.ResumeLayout(false);
            this.tableLayoutPanelWorkout.ResumeLayout(false);
            this.groupBoxData.ResumeLayout(false);
            this.tableLayoutPanelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBoxControl.ResumeLayout(false);
            this.tableLayoutPanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Label targetResistanceLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGlobal;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label heartRateLabel;
        private System.Windows.Forms.Label powerLabel;
        private System.Windows.Forms.Label cadenceLabel;
        private System.Windows.Forms.GroupBox groupBoxWorkout;
        private System.Windows.Forms.GroupBox groupBoxData;
        private System.Windows.Forms.GroupBox groupBoxControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWorkout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelControl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Timer controlTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label actualResistanceLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label bpmLabel;
    }
}


namespace HalconModle
{
    partial class ShapeModle
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLP_Layer1 = new System.Windows.Forms.TableLayoutPanel();
            this.hDisplay1 = new HalWindow.HDisplay();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_ParamCreateModle = new System.Windows.Forms.TabPage();
            this.cmB_TrainMinContrast = new System.Windows.Forms.ComboBox();
            this.cmB_TrainContrast = new System.Windows.Forms.ComboBox();
            this.cmB_TrainMetric = new System.Windows.Forms.ComboBox();
            this.cmB_TrainOptimization = new System.Windows.Forms.ComboBox();
            this.nmUD_TrainAngleStep = new System.Windows.Forms.NumericUpDown();
            this.nmUD_TrainAngleExtent = new System.Windows.Forms.NumericUpDown();
            this.nmUD_TrainAngleStart = new System.Windows.Forms.NumericUpDown();
            this.nmUD_TrainNumLevels = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage_ParamFindModle = new System.Windows.Forms.TabPage();
            this.cmB_FindSubpixel = new System.Windows.Forms.ComboBox();
            this.nmUD_FindGreediness = new System.Windows.Forms.NumericUpDown();
            this.nmUD_FindNumLevels = new System.Windows.Forms.NumericUpDown();
            this.nmUD_FindMaxOverLap = new System.Windows.Forms.NumericUpDown();
            this.nmUD_FindNumMachs = new System.Windows.Forms.NumericUpDown();
            this.nmUD_FindMinScore = new System.Windows.Forms.NumericUpDown();
            this.nmUD_FindAngleStep = new System.Windows.Forms.NumericUpDown();
            this.nmUD_FindAngleExtent = new System.Windows.Forms.NumericUpDown();
            this.nmUD_FindAngleStart = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_SaveModleInfo = new System.Windows.Forms.Button();
            this.btn_TrainModle = new System.Windows.Forms.Button();
            this.btn_GrabTraget = new System.Windows.Forms.Button();
            this.btn_AddModleRegion = new System.Windows.Forms.Button();
            this.btn_LoadModleIInfo = new System.Windows.Forms.Button();
            this.btn_LoadImage = new System.Windows.Forms.Button();
            this.tableLP_Layer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_ParamCreateModle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_TrainAngleStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_TrainAngleExtent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_TrainAngleStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_TrainNumLevels)).BeginInit();
            this.tabPage_ParamFindModle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindGreediness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindNumLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindMaxOverLap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindNumMachs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindAngleStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindAngleExtent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindAngleStart)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLP_Layer1
            // 
            this.tableLP_Layer1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLP_Layer1.ColumnCount = 2;
            this.tableLP_Layer1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tableLP_Layer1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLP_Layer1.Controls.Add(this.hDisplay1, 1, 0);
            this.tableLP_Layer1.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLP_Layer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLP_Layer1.Location = new System.Drawing.Point(0, 0);
            this.tableLP_Layer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLP_Layer1.Name = "tableLP_Layer1";
            this.tableLP_Layer1.RowCount = 1;
            this.tableLP_Layer1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLP_Layer1.Size = new System.Drawing.Size(620, 417);
            this.tableLP_Layer1.TabIndex = 0;
            // 
            // hDisplay1
            // 
            this.hDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hDisplay1.HImageX = null;
            this.hDisplay1.HRegionXList = null;
            this.hDisplay1.IsCancelImageMove = false;
            this.hDisplay1.Location = new System.Drawing.Point(230, 4);
            this.hDisplay1.Name = "hDisplay1";
            this.hDisplay1.Size = new System.Drawing.Size(386, 409);
            this.hDisplay1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(221, 411);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_ParamCreateModle);
            this.tabControl1.Controls.Add(this.tabPage_ParamFindModle);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(2, 122);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(217, 287);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_ParamCreateModle
            // 
            this.tabPage_ParamCreateModle.Controls.Add(this.cmB_TrainMinContrast);
            this.tabPage_ParamCreateModle.Controls.Add(this.cmB_TrainContrast);
            this.tabPage_ParamCreateModle.Controls.Add(this.cmB_TrainMetric);
            this.tabPage_ParamCreateModle.Controls.Add(this.cmB_TrainOptimization);
            this.tabPage_ParamCreateModle.Controls.Add(this.nmUD_TrainAngleStep);
            this.tabPage_ParamCreateModle.Controls.Add(this.nmUD_TrainAngleExtent);
            this.tabPage_ParamCreateModle.Controls.Add(this.nmUD_TrainAngleStart);
            this.tabPage_ParamCreateModle.Controls.Add(this.nmUD_TrainNumLevels);
            this.tabPage_ParamCreateModle.Controls.Add(this.label9);
            this.tabPage_ParamCreateModle.Controls.Add(this.label8);
            this.tabPage_ParamCreateModle.Controls.Add(this.label7);
            this.tabPage_ParamCreateModle.Controls.Add(this.label6);
            this.tabPage_ParamCreateModle.Controls.Add(this.label5);
            this.tabPage_ParamCreateModle.Controls.Add(this.label4);
            this.tabPage_ParamCreateModle.Controls.Add(this.label3);
            this.tabPage_ParamCreateModle.Controls.Add(this.label2);
            this.tabPage_ParamCreateModle.Location = new System.Drawing.Point(4, 22);
            this.tabPage_ParamCreateModle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_ParamCreateModle.Name = "tabPage_ParamCreateModle";
            this.tabPage_ParamCreateModle.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_ParamCreateModle.Size = new System.Drawing.Size(209, 261);
            this.tabPage_ParamCreateModle.TabIndex = 0;
            this.tabPage_ParamCreateModle.Text = "训练模板参数";
            this.tabPage_ParamCreateModle.UseVisualStyleBackColor = true;
            // 
            // cmB_TrainMinContrast
            // 
            this.cmB_TrainMinContrast.FormattingEnabled = true;
            this.cmB_TrainMinContrast.Location = new System.Drawing.Point(90, 228);
            this.cmB_TrainMinContrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmB_TrainMinContrast.Name = "cmB_TrainMinContrast";
            this.cmB_TrainMinContrast.Size = new System.Drawing.Size(116, 20);
            this.cmB_TrainMinContrast.TabIndex = 3;
            this.cmB_TrainMinContrast.Text = "auto";
            // 
            // cmB_TrainContrast
            // 
            this.cmB_TrainContrast.FormattingEnabled = true;
            this.cmB_TrainContrast.Location = new System.Drawing.Point(90, 194);
            this.cmB_TrainContrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmB_TrainContrast.Name = "cmB_TrainContrast";
            this.cmB_TrainContrast.Size = new System.Drawing.Size(116, 20);
            this.cmB_TrainContrast.TabIndex = 3;
            // 
            // cmB_TrainMetric
            // 
            this.cmB_TrainMetric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_TrainMetric.FormattingEnabled = true;
            this.cmB_TrainMetric.Location = new System.Drawing.Point(90, 166);
            this.cmB_TrainMetric.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmB_TrainMetric.Name = "cmB_TrainMetric";
            this.cmB_TrainMetric.Size = new System.Drawing.Size(116, 20);
            this.cmB_TrainMetric.TabIndex = 3;
            // 
            // cmB_TrainOptimization
            // 
            this.cmB_TrainOptimization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_TrainOptimization.FormattingEnabled = true;
            this.cmB_TrainOptimization.Location = new System.Drawing.Point(90, 137);
            this.cmB_TrainOptimization.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmB_TrainOptimization.Name = "cmB_TrainOptimization";
            this.cmB_TrainOptimization.Size = new System.Drawing.Size(116, 20);
            this.cmB_TrainOptimization.TabIndex = 2;
            // 
            // nmUD_TrainAngleStep
            // 
            this.nmUD_TrainAngleStep.DecimalPlaces = 1;
            this.nmUD_TrainAngleStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_TrainAngleStep.Location = new System.Drawing.Point(104, 104);
            this.nmUD_TrainAngleStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_TrainAngleStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_TrainAngleStep.Name = "nmUD_TrainAngleStep";
            this.nmUD_TrainAngleStep.Size = new System.Drawing.Size(90, 21);
            this.nmUD_TrainAngleStep.TabIndex = 1;
            this.nmUD_TrainAngleStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmUD_TrainAngleStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // nmUD_TrainAngleExtent
            // 
            this.nmUD_TrainAngleExtent.Location = new System.Drawing.Point(104, 73);
            this.nmUD_TrainAngleExtent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_TrainAngleExtent.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nmUD_TrainAngleExtent.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.nmUD_TrainAngleExtent.Name = "nmUD_TrainAngleExtent";
            this.nmUD_TrainAngleExtent.Size = new System.Drawing.Size(90, 21);
            this.nmUD_TrainAngleExtent.TabIndex = 1;
            this.nmUD_TrainAngleExtent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmUD_TrainAngleStart
            // 
            this.nmUD_TrainAngleStart.Location = new System.Drawing.Point(104, 42);
            this.nmUD_TrainAngleStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_TrainAngleStart.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nmUD_TrainAngleStart.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.nmUD_TrainAngleStart.Name = "nmUD_TrainAngleStart";
            this.nmUD_TrainAngleStart.Size = new System.Drawing.Size(90, 21);
            this.nmUD_TrainAngleStart.TabIndex = 1;
            this.nmUD_TrainAngleStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmUD_TrainNumLevels
            // 
            this.nmUD_TrainNumLevels.Location = new System.Drawing.Point(104, 10);
            this.nmUD_TrainNumLevels.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_TrainNumLevels.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmUD_TrainNumLevels.Name = "nmUD_TrainNumLevels";
            this.nmUD_TrainNumLevels.Size = new System.Drawing.Size(90, 21);
            this.nmUD_TrainNumLevels.TabIndex = 1;
            this.nmUD_TrainNumLevels.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 230);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "MinContrast:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 199);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "Contrast:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 168);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "Metric:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 137);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "Optimization:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 106);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "AngleStep:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 74);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "AngleExtent:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "AngleStart:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "NumLevels:";
            // 
            // tabPage_ParamFindModle
            // 
            this.tabPage_ParamFindModle.Controls.Add(this.cmB_FindSubpixel);
            this.tabPage_ParamFindModle.Controls.Add(this.nmUD_FindGreediness);
            this.tabPage_ParamFindModle.Controls.Add(this.nmUD_FindNumLevels);
            this.tabPage_ParamFindModle.Controls.Add(this.nmUD_FindMaxOverLap);
            this.tabPage_ParamFindModle.Controls.Add(this.nmUD_FindNumMachs);
            this.tabPage_ParamFindModle.Controls.Add(this.nmUD_FindMinScore);
            this.tabPage_ParamFindModle.Controls.Add(this.nmUD_FindAngleStep);
            this.tabPage_ParamFindModle.Controls.Add(this.nmUD_FindAngleExtent);
            this.tabPage_ParamFindModle.Controls.Add(this.nmUD_FindAngleStart);
            this.tabPage_ParamFindModle.Controls.Add(this.label17);
            this.tabPage_ParamFindModle.Controls.Add(this.label16);
            this.tabPage_ParamFindModle.Controls.Add(this.label15);
            this.tabPage_ParamFindModle.Controls.Add(this.label14);
            this.tabPage_ParamFindModle.Controls.Add(this.label13);
            this.tabPage_ParamFindModle.Controls.Add(this.label12);
            this.tabPage_ParamFindModle.Controls.Add(this.label1);
            this.tabPage_ParamFindModle.Controls.Add(this.label10);
            this.tabPage_ParamFindModle.Controls.Add(this.label11);
            this.tabPage_ParamFindModle.Location = new System.Drawing.Point(4, 22);
            this.tabPage_ParamFindModle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_ParamFindModle.Name = "tabPage_ParamFindModle";
            this.tabPage_ParamFindModle.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage_ParamFindModle.Size = new System.Drawing.Size(208, 260);
            this.tabPage_ParamFindModle.TabIndex = 1;
            this.tabPage_ParamFindModle.Text = "查找模板参数";
            this.tabPage_ParamFindModle.UseVisualStyleBackColor = true;
            // 
            // cmB_FindSubpixel
            // 
            this.cmB_FindSubpixel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_FindSubpixel.FormattingEnabled = true;
            this.cmB_FindSubpixel.Location = new System.Drawing.Point(88, 178);
            this.cmB_FindSubpixel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmB_FindSubpixel.Name = "cmB_FindSubpixel";
            this.cmB_FindSubpixel.Size = new System.Drawing.Size(107, 20);
            this.cmB_FindSubpixel.TabIndex = 5;
            // 
            // nmUD_FindGreediness
            // 
            this.nmUD_FindGreediness.DecimalPlaces = 1;
            this.nmUD_FindGreediness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_FindGreediness.Location = new System.Drawing.Point(88, 231);
            this.nmUD_FindGreediness.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_FindGreediness.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmUD_FindGreediness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_FindGreediness.Name = "nmUD_FindGreediness";
            this.nmUD_FindGreediness.Size = new System.Drawing.Size(106, 21);
            this.nmUD_FindGreediness.TabIndex = 4;
            this.nmUD_FindGreediness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmUD_FindGreediness.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // nmUD_FindNumLevels
            // 
            this.nmUD_FindNumLevels.Location = new System.Drawing.Point(88, 206);
            this.nmUD_FindNumLevels.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_FindNumLevels.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmUD_FindNumLevels.Name = "nmUD_FindNumLevels";
            this.nmUD_FindNumLevels.Size = new System.Drawing.Size(106, 21);
            this.nmUD_FindNumLevels.TabIndex = 4;
            this.nmUD_FindNumLevels.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmUD_FindMaxOverLap
            // 
            this.nmUD_FindMaxOverLap.DecimalPlaces = 1;
            this.nmUD_FindMaxOverLap.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_FindMaxOverLap.Location = new System.Drawing.Point(88, 150);
            this.nmUD_FindMaxOverLap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_FindMaxOverLap.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmUD_FindMaxOverLap.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_FindMaxOverLap.Name = "nmUD_FindMaxOverLap";
            this.nmUD_FindMaxOverLap.Size = new System.Drawing.Size(106, 21);
            this.nmUD_FindMaxOverLap.TabIndex = 4;
            this.nmUD_FindMaxOverLap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmUD_FindMaxOverLap.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // nmUD_FindNumMachs
            // 
            this.nmUD_FindNumMachs.Location = new System.Drawing.Point(88, 119);
            this.nmUD_FindNumMachs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_FindNumMachs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmUD_FindNumMachs.Name = "nmUD_FindNumMachs";
            this.nmUD_FindNumMachs.Size = new System.Drawing.Size(106, 21);
            this.nmUD_FindNumMachs.TabIndex = 4;
            this.nmUD_FindNumMachs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmUD_FindNumMachs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nmUD_FindMinScore
            // 
            this.nmUD_FindMinScore.DecimalPlaces = 1;
            this.nmUD_FindMinScore.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_FindMinScore.Location = new System.Drawing.Point(88, 94);
            this.nmUD_FindMinScore.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_FindMinScore.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmUD_FindMinScore.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_FindMinScore.Name = "nmUD_FindMinScore";
            this.nmUD_FindMinScore.Size = new System.Drawing.Size(106, 21);
            this.nmUD_FindMinScore.TabIndex = 4;
            this.nmUD_FindMinScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmUD_FindMinScore.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // nmUD_FindAngleStep
            // 
            this.nmUD_FindAngleStep.DecimalPlaces = 1;
            this.nmUD_FindAngleStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_FindAngleStep.Location = new System.Drawing.Point(88, 63);
            this.nmUD_FindAngleStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_FindAngleStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmUD_FindAngleStep.Name = "nmUD_FindAngleStep";
            this.nmUD_FindAngleStep.Size = new System.Drawing.Size(106, 21);
            this.nmUD_FindAngleStep.TabIndex = 4;
            this.nmUD_FindAngleStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmUD_FindAngleStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // nmUD_FindAngleExtent
            // 
            this.nmUD_FindAngleExtent.Location = new System.Drawing.Point(88, 38);
            this.nmUD_FindAngleExtent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_FindAngleExtent.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nmUD_FindAngleExtent.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.nmUD_FindAngleExtent.Name = "nmUD_FindAngleExtent";
            this.nmUD_FindAngleExtent.Size = new System.Drawing.Size(106, 21);
            this.nmUD_FindAngleExtent.TabIndex = 4;
            this.nmUD_FindAngleExtent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmUD_FindAngleStart
            // 
            this.nmUD_FindAngleStart.Location = new System.Drawing.Point(88, 10);
            this.nmUD_FindAngleStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmUD_FindAngleStart.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nmUD_FindAngleStart.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.nmUD_FindAngleStart.Name = "nmUD_FindAngleStart";
            this.nmUD_FindAngleStart.Size = new System.Drawing.Size(106, 21);
            this.nmUD_FindAngleStart.TabIndex = 4;
            this.nmUD_FindAngleStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 236);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 12);
            this.label17.TabIndex = 1;
            this.label17.Text = "Greediness:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 208);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 1;
            this.label16.Text = "NumLevels:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 180);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "SubPixel:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 152);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "MaxOverLap:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 124);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "NumMatchs:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 96);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "MinScore:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "AngleStep:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 40);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "AngleExtent:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 12);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "AngleStart:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_SaveModleInfo);
            this.panel1.Controls.Add(this.btn_TrainModle);
            this.panel1.Controls.Add(this.btn_GrabTraget);
            this.panel1.Controls.Add(this.btn_AddModleRegion);
            this.panel1.Controls.Add(this.btn_LoadModleIInfo);
            this.panel1.Controls.Add(this.btn_LoadImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 116);
            this.panel1.TabIndex = 1;
            // 
            // btn_SaveModleInfo
            // 
            this.btn_SaveModleInfo.Location = new System.Drawing.Point(112, 78);
            this.btn_SaveModleInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_SaveModleInfo.Name = "btn_SaveModleInfo";
            this.btn_SaveModleInfo.Size = new System.Drawing.Size(84, 32);
            this.btn_SaveModleInfo.TabIndex = 0;
            this.btn_SaveModleInfo.Text = "保存模板";
            this.btn_SaveModleInfo.UseVisualStyleBackColor = true;
            this.btn_SaveModleInfo.Click += new System.EventHandler(this.btn_SaveModleInfo_Click);
            // 
            // btn_TrainModle
            // 
            this.btn_TrainModle.Location = new System.Drawing.Point(112, 42);
            this.btn_TrainModle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_TrainModle.Name = "btn_TrainModle";
            this.btn_TrainModle.Size = new System.Drawing.Size(84, 32);
            this.btn_TrainModle.TabIndex = 0;
            this.btn_TrainModle.Text = "训练模板";
            this.btn_TrainModle.UseVisualStyleBackColor = true;
            this.btn_TrainModle.Click += new System.EventHandler(this.btn_TrainModle_Click);
            // 
            // btn_GrabTraget
            // 
            this.btn_GrabTraget.Location = new System.Drawing.Point(14, 78);
            this.btn_GrabTraget.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_GrabTraget.Name = "btn_GrabTraget";
            this.btn_GrabTraget.Size = new System.Drawing.Size(84, 32);
            this.btn_GrabTraget.TabIndex = 0;
            this.btn_GrabTraget.Text = "抓取目标";
            this.btn_GrabTraget.UseVisualStyleBackColor = true;
            this.btn_GrabTraget.Click += new System.EventHandler(this.btn_GrabTraget_Click);
            // 
            // btn_AddModleRegion
            // 
            this.btn_AddModleRegion.Location = new System.Drawing.Point(14, 42);
            this.btn_AddModleRegion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_AddModleRegion.Name = "btn_AddModleRegion";
            this.btn_AddModleRegion.Size = new System.Drawing.Size(84, 32);
            this.btn_AddModleRegion.TabIndex = 0;
            this.btn_AddModleRegion.Text = "添加模板区域";
            this.btn_AddModleRegion.UseVisualStyleBackColor = true;
            this.btn_AddModleRegion.Click += new System.EventHandler(this.btn_AddModleRegion_Click);
            // 
            // btn_LoadModleIInfo
            // 
            this.btn_LoadModleIInfo.Location = new System.Drawing.Point(112, 5);
            this.btn_LoadModleIInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_LoadModleIInfo.Name = "btn_LoadModleIInfo";
            this.btn_LoadModleIInfo.Size = new System.Drawing.Size(84, 32);
            this.btn_LoadModleIInfo.TabIndex = 0;
            this.btn_LoadModleIInfo.Text = "读取模板";
            this.btn_LoadModleIInfo.UseVisualStyleBackColor = true;
            this.btn_LoadModleIInfo.Click += new System.EventHandler(this.btn_LoadModleIInfo_Click);
            // 
            // btn_LoadImage
            // 
            this.btn_LoadImage.Location = new System.Drawing.Point(14, 5);
            this.btn_LoadImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_LoadImage.Name = "btn_LoadImage";
            this.btn_LoadImage.Size = new System.Drawing.Size(84, 32);
            this.btn_LoadImage.TabIndex = 0;
            this.btn_LoadImage.Text = "加载图片";
            this.btn_LoadImage.UseVisualStyleBackColor = true;
            this.btn_LoadImage.Click += new System.EventHandler(this.btn_LoadImage_Click);
            // 
            // ShapeModle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLP_Layer1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ShapeModle";
            this.Size = new System.Drawing.Size(620, 417);
            this.tableLP_Layer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_ParamCreateModle.ResumeLayout(false);
            this.tabPage_ParamCreateModle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_TrainAngleStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_TrainAngleExtent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_TrainAngleStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_TrainNumLevels)).EndInit();
            this.tabPage_ParamFindModle.ResumeLayout(false);
            this.tabPage_ParamFindModle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindGreediness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindNumLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindMaxOverLap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindNumMachs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindAngleStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindAngleExtent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_FindAngleStart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLP_Layer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_ParamCreateModle;
        private System.Windows.Forms.ComboBox cmB_TrainMinContrast;
        private System.Windows.Forms.ComboBox cmB_TrainContrast;
        private System.Windows.Forms.ComboBox cmB_TrainMetric;
        private System.Windows.Forms.ComboBox cmB_TrainOptimization;
        private System.Windows.Forms.NumericUpDown nmUD_TrainAngleStep;
        private System.Windows.Forms.NumericUpDown nmUD_TrainAngleExtent;
        private System.Windows.Forms.NumericUpDown nmUD_TrainAngleStart;
        private System.Windows.Forms.NumericUpDown nmUD_TrainNumLevels;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage_ParamFindModle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_SaveModleInfo;
        private System.Windows.Forms.Button btn_TrainModle;
        private System.Windows.Forms.Button btn_GrabTraget;
        private System.Windows.Forms.Button btn_AddModleRegion;
        private System.Windows.Forms.Button btn_LoadModleIInfo;
        private System.Windows.Forms.Button btn_LoadImage;
        private System.Windows.Forms.ComboBox cmB_FindSubpixel;
        private System.Windows.Forms.NumericUpDown nmUD_FindGreediness;
        private System.Windows.Forms.NumericUpDown nmUD_FindNumLevels;
        private System.Windows.Forms.NumericUpDown nmUD_FindMaxOverLap;
        private System.Windows.Forms.NumericUpDown nmUD_FindNumMachs;
        private System.Windows.Forms.NumericUpDown nmUD_FindMinScore;
        private System.Windows.Forms.NumericUpDown nmUD_FindAngleStep;
        private System.Windows.Forms.NumericUpDown nmUD_FindAngleExtent;
        private System.Windows.Forms.NumericUpDown nmUD_FindAngleStart;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private HalWindow.HDisplay hDisplay1;
    }
}

namespace Morphing
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.destinationImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bothImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.source_pb = new System.Windows.Forms.PictureBox();
            this.morphed_pb = new System.Windows.Forms.PictureBox();
            this.dest_pb = new System.Windows.Forms.PictureBox();
            this.source_label = new System.Windows.Forms.Label();
            this.morphed_label = new System.Windows.Forms.Label();
            this.dest_label = new System.Windows.Forms.Label();
            this.morph_btn = new System.Windows.Forms.Button();
            this.frames_label = new System.Windows.Forms.Label();
            this.frames_num = new System.Windows.Forms.NumericUpDown();
            this.frames_trackbar = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.resetLines_btn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.source_pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.morphed_pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dest_pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frames_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frames_trackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1318, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceImageToolStripMenuItem,
            this.destinationImageToolStripMenuItem,
            this.bothImagesToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // sourceImageToolStripMenuItem
            // 
            this.sourceImageToolStripMenuItem.Name = "sourceImageToolStripMenuItem";
            this.sourceImageToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.sourceImageToolStripMenuItem.Text = "&Source image";
            this.sourceImageToolStripMenuItem.Click += new System.EventHandler(this.sourceImageToolStripMenuItem_Click);
            // 
            // destinationImageToolStripMenuItem
            // 
            this.destinationImageToolStripMenuItem.Name = "destinationImageToolStripMenuItem";
            this.destinationImageToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.destinationImageToolStripMenuItem.Text = "&Destination image";
            this.destinationImageToolStripMenuItem.Click += new System.EventHandler(this.destinationImageToolStripMenuItem_Click);
            // 
            // bothImagesToolStripMenuItem
            // 
            this.bothImagesToolStripMenuItem.Name = "bothImagesToolStripMenuItem";
            this.bothImagesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bothImagesToolStripMenuItem.Text = "Both images";
            this.bothImagesToolStripMenuItem.Click += new System.EventHandler(this.bothImagesToolStripMenuItem_Click);
            // 
            // source_pb
            // 
            this.source_pb.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.source_pb.Location = new System.Drawing.Point(12, 50);
            this.source_pb.Name = "source_pb";
            this.source_pb.Size = new System.Drawing.Size(401, 371);
            this.source_pb.TabIndex = 1;
            this.source_pb.TabStop = false;
            this.source_pb.Paint += new System.Windows.Forms.PaintEventHandler(this.source_pb_Paint);
            this.source_pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.source_pb_MouseDown);
            this.source_pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.source_pb_MouseMove);
            this.source_pb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.source_pb_MouseUp);
            // 
            // morphed_pb
            // 
            this.morphed_pb.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.morphed_pb.Location = new System.Drawing.Point(468, 50);
            this.morphed_pb.Name = "morphed_pb";
            this.morphed_pb.Size = new System.Drawing.Size(401, 371);
            this.morphed_pb.TabIndex = 2;
            this.morphed_pb.TabStop = false;
            // 
            // dest_pb
            // 
            this.dest_pb.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.dest_pb.Location = new System.Drawing.Point(905, 50);
            this.dest_pb.Name = "dest_pb";
            this.dest_pb.Size = new System.Drawing.Size(401, 371);
            this.dest_pb.TabIndex = 3;
            this.dest_pb.TabStop = false;
            this.dest_pb.Paint += new System.Windows.Forms.PaintEventHandler(this.dest_pb_Paint);
            this.dest_pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dest_pb_MouseDown);
            this.dest_pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dest_pb_MouseMove);
            this.dest_pb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dest_pb_MouseUp);
            // 
            // source_label
            // 
            this.source_label.AutoSize = true;
            this.source_label.Location = new System.Drawing.Point(12, 34);
            this.source_label.Name = "source_label";
            this.source_label.Size = new System.Drawing.Size(73, 13);
            this.source_label.TabIndex = 4;
            this.source_label.Text = "Source Image";
            // 
            // morphed_label
            // 
            this.morphed_label.AutoSize = true;
            this.morphed_label.Location = new System.Drawing.Point(465, 34);
            this.morphed_label.Name = "morphed_label";
            this.morphed_label.Size = new System.Drawing.Size(81, 13);
            this.morphed_label.TabIndex = 5;
            this.morphed_label.Text = "Morphed Image";
            // 
            // dest_label
            // 
            this.dest_label.AutoSize = true;
            this.dest_label.Location = new System.Drawing.Point(902, 34);
            this.dest_label.Name = "dest_label";
            this.dest_label.Size = new System.Drawing.Size(92, 13);
            this.dest_label.TabIndex = 6;
            this.dest_label.Text = "Destination Image";
            // 
            // morph_btn
            // 
            this.morph_btn.Location = new System.Drawing.Point(468, 427);
            this.morph_btn.Name = "morph_btn";
            this.morph_btn.Size = new System.Drawing.Size(75, 23);
            this.morph_btn.TabIndex = 7;
            this.morph_btn.Text = "Morph";
            this.morph_btn.UseVisualStyleBackColor = true;
            this.morph_btn.Click += new System.EventHandler(this.morph_btn_Click);
            // 
            // frames_label
            // 
            this.frames_label.AutoSize = true;
            this.frames_label.Location = new System.Drawing.Point(468, 476);
            this.frames_label.Name = "frames_label";
            this.frames_label.Size = new System.Drawing.Size(41, 13);
            this.frames_label.TabIndex = 8;
            this.frames_label.Text = "Frames";
            // 
            // frames_num
            // 
            this.frames_num.Location = new System.Drawing.Point(515, 469);
            this.frames_num.Name = "frames_num";
            this.frames_num.Size = new System.Drawing.Size(120, 20);
            this.frames_num.TabIndex = 9;
            this.frames_num.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.frames_num.ValueChanged += new System.EventHandler(this.frames_num_ValueChanged);
            // 
            // frames_trackbar
            // 
            this.frames_trackbar.Location = new System.Drawing.Point(468, 508);
            this.frames_trackbar.Maximum = 0;
            this.frames_trackbar.Name = "frames_trackbar";
            this.frames_trackbar.Size = new System.Drawing.Size(401, 45);
            this.frames_trackbar.TabIndex = 10;
            this.frames_trackbar.ValueChanged += new System.EventHandler(this.frames_trackbar_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1197, 549);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // resetLines_btn
            // 
            this.resetLines_btn.Location = new System.Drawing.Point(12, 443);
            this.resetLines_btn.Name = "resetLines_btn";
            this.resetLines_btn.Size = new System.Drawing.Size(75, 23);
            this.resetLines_btn.TabIndex = 12;
            this.resetLines_btn.Text = "Reset";
            this.resetLines_btn.UseVisualStyleBackColor = true;
            this.resetLines_btn.Click += new System.EventHandler(this.resetLines_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 592);
            this.Controls.Add(this.resetLines_btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.frames_trackbar);
            this.Controls.Add(this.frames_num);
            this.Controls.Add(this.frames_label);
            this.Controls.Add(this.morph_btn);
            this.Controls.Add(this.dest_label);
            this.Controls.Add(this.morphed_label);
            this.Controls.Add(this.source_label);
            this.Controls.Add(this.dest_pb);
            this.Controls.Add(this.morphed_pb);
            this.Controls.Add(this.source_pb);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.source_pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.morphed_pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dest_pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frames_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frames_trackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem destinationImageToolStripMenuItem;
        private System.Windows.Forms.PictureBox source_pb;
        private System.Windows.Forms.PictureBox morphed_pb;
        private System.Windows.Forms.PictureBox dest_pb;
        private System.Windows.Forms.Label source_label;
        private System.Windows.Forms.Label morphed_label;
        private System.Windows.Forms.Label dest_label;
        private System.Windows.Forms.Button morph_btn;
        private System.Windows.Forms.Label frames_label;
        private System.Windows.Forms.NumericUpDown frames_num;
        private System.Windows.Forms.TrackBar frames_trackbar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem bothImagesToolStripMenuItem;
        private System.Windows.Forms.Button resetLines_btn;
    }
}


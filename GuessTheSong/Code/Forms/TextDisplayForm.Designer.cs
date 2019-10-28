namespace GuessTheSong.Forms
{
    partial class TextDisplayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextDisplayForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.txtText = new System.Windows.Forms.TextBox();
            this.btnDummy = new System.Windows.Forms.Button();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 5;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel.Controls.Add(this.rightPanel, 4, 0);
            this.tableLayoutPanel.Controls.Add(this.txtText, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.btnDummy, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.leftPanel, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(284, 387);
            this.tableLayoutPanel.TabIndex = 2;
            this.tableLayoutPanel.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            // 
            // txtText
            // 
            this.txtText.BackColor = System.Drawing.Color.White;
            this.txtText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtText.Location = new System.Drawing.Point(73, 3);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.ReadOnly = true;
            this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtText.Size = new System.Drawing.Size(136, 381);
            this.txtText.TabIndex = 2;
            this.txtText.WordWrap = false;
            this.txtText.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            this.txtText.Enter += new System.EventHandler(this.txtText_Enter);
            // 
            // btnDummy
            // 
            this.btnDummy.Location = new System.Drawing.Point(215, 3);
            this.btnDummy.Name = "btnDummy";
            this.btnDummy.Size = new System.Drawing.Size(2, 23);
            this.btnDummy.TabIndex = 3;
            this.btnDummy.Text = "Dummy";
            this.btnDummy.UseVisualStyleBackColor = true;
            this.btnDummy.Visible = false;
            // 
            // leftPanel
            // 
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(3, 3);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(56, 381);
            this.leftPanel.TabIndex = 4;
            this.leftPanel.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            // 
            // rightPanel
            // 
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(223, 3);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(58, 381);
            this.rightPanel.TabIndex = 5;
            this.rightPanel.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            // 
            // TextDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 387);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextDisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TextDisplayForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        internal System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Button btnDummy;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightPanel;

    }
}
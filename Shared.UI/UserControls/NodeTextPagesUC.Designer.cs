﻿namespace Shared.UI.UserControls
{
    partial class NodeTextPagesUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeTextPagesUC));
            this.btnKillPage = new System.Windows.Forms.Button();
            this.brnAddPage = new System.Windows.Forms.Button();
            this.txtPageText = new System.Windows.Forms.TextBox();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.WorkspaceText = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openNodeByIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKillPage
            // 
            this.btnKillPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnKillPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKillPage.BackgroundImage")));
            this.btnKillPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnKillPage.FlatAppearance.BorderSize = 0;
            this.btnKillPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKillPage.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKillPage.Location = new System.Drawing.Point(199, 3);
            this.btnKillPage.Name = "btnKillPage";
            this.btnKillPage.Size = new System.Drawing.Size(44, 20);
            this.btnKillPage.TabIndex = 25;
            this.btnKillPage.TabStop = false;
            this.btnKillPage.UseVisualStyleBackColor = false;
            this.btnKillPage.Click += new System.EventHandler(this.btnKillPage_Click);
            // 
            // brnAddPage
            // 
            this.brnAddPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.brnAddPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("brnAddPage.BackgroundImage")));
            this.brnAddPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.brnAddPage.FlatAppearance.BorderSize = 0;
            this.brnAddPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.brnAddPage.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brnAddPage.Location = new System.Drawing.Point(153, 3);
            this.brnAddPage.Name = "brnAddPage";
            this.brnAddPage.Size = new System.Drawing.Size(44, 20);
            this.brnAddPage.TabIndex = 24;
            this.brnAddPage.TabStop = false;
            this.brnAddPage.UseVisualStyleBackColor = false;
            this.brnAddPage.Click += new System.EventHandler(this.brnAddPage_Click);
            // 
            // txtPageText
            // 
            this.txtPageText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(58)))));
            this.txtPageText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPageText.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageText.ForeColor = System.Drawing.Color.White;
            this.txtPageText.Location = new System.Drawing.Point(47, 4);
            this.txtPageText.Name = "txtPageText";
            this.txtPageText.ReadOnly = true;
            this.txtPageText.Size = new System.Drawing.Size(58, 19);
            this.txtPageText.TabIndex = 23;
            this.txtPageText.TabStop = false;
            this.txtPageText.Tag = "";
            this.txtPageText.Text = "20/20";
            this.txtPageText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnPrevPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrevPage.BackgroundImage")));
            this.btnPrevPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrevPage.FlatAppearance.BorderSize = 0;
            this.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevPage.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevPage.Location = new System.Drawing.Point(1, 3);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(44, 20);
            this.btnPrevPage.TabIndex = 22;
            this.btnPrevPage.TabStop = false;
            this.btnPrevPage.UseVisualStyleBackColor = false;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnNextPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNextPage.BackgroundImage")));
            this.btnNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNextPage.FlatAppearance.BorderSize = 0;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextPage.Location = new System.Drawing.Point(107, 3);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(44, 20);
            this.btnNextPage.TabIndex = 21;
            this.btnNextPage.TabStop = false;
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // WorkspaceText
            // 
            this.WorkspaceText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkspaceText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(58)))));
            this.WorkspaceText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WorkspaceText.ContextMenuStrip = this.contextMenuStrip1;
            this.WorkspaceText.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WorkspaceText.ForeColor = System.Drawing.Color.Yellow;
            this.WorkspaceText.Location = new System.Drawing.Point(-1, 29);
            this.WorkspaceText.Name = "WorkspaceText";
            this.WorkspaceText.Size = new System.Drawing.Size(766, 416);
            this.WorkspaceText.TabIndex = 0;
            this.WorkspaceText.Text = resources.GetString("WorkspaceText.Text");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openNodeByIdToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 28);
            // 
            // openNodeByIdToolStripMenuItem
            // 
            this.openNodeByIdToolStripMenuItem.Name = "openNodeByIdToolStripMenuItem";
            this.openNodeByIdToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.openNodeByIdToolStripMenuItem.Text = "Open node by id";
            this.openNodeByIdToolStripMenuItem.Click += new System.EventHandler(this.openNodeByIdToolStripMenuItem_Click);
            // 
            // NodeTextPagesUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.Controls.Add(this.WorkspaceText);
            this.Controls.Add(this.btnKillPage);
            this.Controls.Add(this.brnAddPage);
            this.Controls.Add(this.txtPageText);
            this.Controls.Add(this.btnPrevPage);
            this.Controls.Add(this.btnNextPage);
            this.Name = "NodeTextPagesUC";
            this.Size = new System.Drawing.Size(768, 448);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKillPage;
        private System.Windows.Forms.Button brnAddPage;
        private System.Windows.Forms.TextBox txtPageText;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.RichTextBox WorkspaceText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openNodeByIdToolStripMenuItem;
    }
}

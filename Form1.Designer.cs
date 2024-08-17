namespace PrintifyAutomation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblSourceStore = new Label();
            lblTargetStore = new Label();
            lblBulletPoint = new Label();
            txtBulletPoint = new TextBox();
            btnCopy = new Button();
            cmbSourceStore = new ComboBox();
            cmbTargetStore = new ComboBox();
            label1 = new Label();
            txtPath = new TextBox();
            btnBrowse = new Button();
            progressBar1 = new ProgressBar();
            progressLabel = new Label();
            btnPublish = new Button();
            btnCopyAndPublish = new Button();
            SuspendLayout();
            // 
            // lblSourceStore
            // 
            lblSourceStore.AutoSize = true;
            lblSourceStore.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSourceStore.Location = new Point(153, 103);
            lblSourceStore.Name = "lblSourceStore";
            lblSourceStore.Size = new Size(86, 17);
            lblSourceStore.TabIndex = 0;
            lblSourceStore.Text = "Source Store:";
            // 
            // lblTargetStore
            // 
            lblTargetStore.AutoSize = true;
            lblTargetStore.Font = new Font("Segoe UI", 9.75F);
            lblTargetStore.Location = new Point(156, 134);
            lblTargetStore.Name = "lblTargetStore";
            lblTargetStore.Size = new Size(83, 17);
            lblTargetStore.TabIndex = 2;
            lblTargetStore.Text = "Target Store:";
            // 
            // lblBulletPoint
            // 
            lblBulletPoint.AutoSize = true;
            lblBulletPoint.Font = new Font("Segoe UI", 9.75F);
            lblBulletPoint.Location = new Point(164, 196);
            lblBulletPoint.Name = "lblBulletPoint";
            lblBulletPoint.Size = new Size(75, 17);
            lblBulletPoint.TabIndex = 3;
            lblBulletPoint.Text = "Bullet Point:";
            // 
            // txtBulletPoint
            // 
            txtBulletPoint.Location = new Point(245, 193);
            txtBulletPoint.Multiline = true;
            txtBulletPoint.Name = "txtBulletPoint";
            txtBulletPoint.Size = new Size(311, 108);
            txtBulletPoint.TabIndex = 7;
            txtBulletPoint.Text = "Text...";
            txtBulletPoint.TextChanged += txtBulletPoint_TextChanged;
            // 
            // btnCopy
            // 
            btnCopy.Location = new Point(253, 321);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(75, 26);
            btnCopy.TabIndex = 8;
            btnCopy.Text = "Copy";
            btnCopy.UseVisualStyleBackColor = true;
            // 
            // cmbSourceStore
            // 
            cmbSourceStore.FormattingEnabled = true;
            cmbSourceStore.Location = new Point(245, 100);
            cmbSourceStore.Name = "cmbSourceStore";
            cmbSourceStore.Size = new Size(311, 25);
            cmbSourceStore.TabIndex = 11;
            // 
            // cmbTargetStore
            // 
            cmbTargetStore.FormattingEnabled = true;
            cmbTargetStore.Location = new Point(245, 131);
            cmbTargetStore.Name = "cmbTargetStore";
            cmbTargetStore.Size = new Size(311, 25);
            cmbTargetStore.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F);
            label1.Location = new Point(203, 165);
            label1.Name = "label1";
            label1.Size = new Size(36, 17);
            label1.TabIndex = 13;
            label1.Text = "Path:";
            // 
            // txtPath
            // 
            txtPath.Location = new Point(245, 162);
            txtPath.Name = "txtPath";
            txtPath.ReadOnly = true;
            txtPath.Size = new Size(311, 25);
            txtPath.TabIndex = 14;
            // 
            // btnBrowse
            // 
            btnBrowse.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBrowse.Location = new Point(562, 160);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 26);
            btnBrowse.TabIndex = 15;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(0, 398);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(800, 28);
            progressBar1.TabIndex = 16;
            // 
            // progressLabel
            // 
            progressLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            progressLabel.AutoSize = true;
            progressLabel.BackColor = Color.Transparent;
            progressLabel.Location = new Point(392, 378);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(17, 17);
            progressLabel.TabIndex = 17;
            progressLabel.Text = "...";
            progressLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPublish
            // 
            btnPublish.Location = new Point(334, 321);
            btnPublish.Name = "btnPublish";
            btnPublish.Size = new Size(75, 26);
            btnPublish.TabIndex = 18;
            btnPublish.Text = "Publish";
            btnPublish.UseVisualStyleBackColor = true;
            // 
            // btnCopyAndPublish
            // 
            btnCopyAndPublish.Location = new Point(415, 321);
            btnCopyAndPublish.Name = "btnCopyAndPublish";
            btnCopyAndPublish.Size = new Size(141, 26);
            btnCopyAndPublish.TabIndex = 19;
            btnCopyAndPublish.Text = "Copy and Publish";
            btnCopyAndPublish.UseVisualStyleBackColor = true;
            btnCopyAndPublish.Click += btnCopyAndPublish_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 426);
            Controls.Add(btnCopyAndPublish);
            Controls.Add(btnPublish);
            Controls.Add(progressLabel);
            Controls.Add(progressBar1);
            Controls.Add(btnBrowse);
            Controls.Add(txtPath);
            Controls.Add(label1);
            Controls.Add(cmbTargetStore);
            Controls.Add(cmbSourceStore);
            Controls.Add(btnCopy);
            Controls.Add(txtBulletPoint);
            Controls.Add(lblBulletPoint);
            Controls.Add(lblTargetStore);
            Controls.Add(lblSourceStore);
            Font = new Font("Segoe UI", 9.75F);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSourceStore;
        private Label lblTargetStore;
        private Label lblBulletPoint;
        private TextBox txtBulletPoint;
        private Button btnCopy;
        private ComboBox cmbSourceStore;
        private ComboBox cmbTargetStore;
        private Label label1;
        private TextBox txtPath;
        private Button btnBrowse;
        private ProgressBar progressBar1;
        private Label progressLabel;
        private Button btnPublish;
        private Button btnCopyAndPublish;
    }
}

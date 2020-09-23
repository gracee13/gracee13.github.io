namespace KSU.CIS300.WordSearch
{
    partial class TextPrompt
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
            this.uxTextBox_UserText = new System.Windows.Forms.TextBox();
            this.uxOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uxTextBox_UserText
            // 
            this.uxTextBox_UserText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxTextBox_UserText.Location = new System.Drawing.Point(12, 32);
            this.uxTextBox_UserText.MaxLength = 24;
            this.uxTextBox_UserText.Name = "uxTextBox_UserText";
            this.uxTextBox_UserText.Size = new System.Drawing.Size(570, 44);
            this.uxTextBox_UserText.TabIndex = 0;
            this.uxTextBox_UserText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uxOk
            // 
            this.uxOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uxOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxOk.Location = new System.Drawing.Point(218, 101);
            this.uxOk.Name = "uxOk";
            this.uxOk.Size = new System.Drawing.Size(150, 53);
            this.uxOk.TabIndex = 1;
            this.uxOk.Text = "OK";
            this.uxOk.UseVisualStyleBackColor = true;
            // 
            // TextPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 179);
            this.Controls.Add(this.uxOk);
            this.Controls.Add(this.uxTextBox_UserText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TextPrompt";
            this.Text = "TextPrompt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uxTextBox_UserText;
        private System.Windows.Forms.Button uxOk;
    }
}
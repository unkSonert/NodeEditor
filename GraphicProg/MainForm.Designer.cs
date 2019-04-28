using System.Drawing;

namespace GraphicProg
{
    partial class MainForm
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.UpControlePanel = new System.Windows.Forms.Panel();
            this.ResizeButton = new System.Windows.Forms.Button();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.ElPanel = new System.Windows.Forms.Panel();
            this.ResizeElPanelButton = new System.Windows.Forms.Button();
            this.CrutchPanel = new System.Windows.Forms.Panel();
            this.ElementContainer = new System.Windows.Forms.Panel();
            this.WorkPlace = new System.Windows.Forms.Panel();
            this.UpControlePanel.SuspendLayout();
            this.ElPanel.SuspendLayout();
            this.CrutchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.BackColor = System.Drawing.Color.Transparent;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.CloseButton.Location = new System.Drawing.Point(966, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(50, 30);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "❌";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.Close);
            this.CloseButton.MouseEnter += new System.EventHandler(this.MouseEnterChangeColor);
            this.CloseButton.MouseLeave += new System.EventHandler(this.MouseLeaveChangeColor);
            // 
            // UpControlePanel
            // 
            this.UpControlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.UpControlePanel.Controls.Add(this.ResizeButton);
            this.UpControlePanel.Controls.Add(this.MinimizeButton);
            this.UpControlePanel.Controls.Add(this.CloseButton);
            this.UpControlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.UpControlePanel.Location = new System.Drawing.Point(0, 0);
            this.UpControlePanel.Name = "UpControlePanel";
            this.UpControlePanel.Size = new System.Drawing.Size(1016, 30);
            this.UpControlePanel.TabIndex = 1;
            this.UpControlePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UpPanelDownMouse);
            this.UpControlePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMove);
            this.UpControlePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UpPanelUpMouse);
            // 
            // ResizeButton
            // 
            this.ResizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResizeButton.BackColor = System.Drawing.Color.Transparent;
            this.ResizeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.ResizeButton.FlatAppearance.BorderSize = 0;
            this.ResizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ResizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ResizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.ResizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.ResizeButton.Location = new System.Drawing.Point(916, -5);
            this.ResizeButton.Name = "ResizeButton";
            this.ResizeButton.Size = new System.Drawing.Size(50, 40);
            this.ResizeButton.TabIndex = 2;
            this.ResizeButton.Text = "🗖";
            this.ResizeButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ResizeButton.UseVisualStyleBackColor = false;
            this.ResizeButton.Click += new System.EventHandler(this.Resize);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeButton.BackColor = System.Drawing.Color.Transparent;
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.MinimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.MinimizeButton.FlatAppearance.BorderSize = 0;
            this.MinimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.MinimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.MinimizeButton.Location = new System.Drawing.Point(866, 0);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(50, 30);
            this.MinimizeButton.TabIndex = 1;
            this.MinimizeButton.Text = "—";
            this.MinimizeButton.UseVisualStyleBackColor = false;
            this.MinimizeButton.Click += new System.EventHandler(this.HideForm);
            // 
            // ElPanel
            // 
            this.ElPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.ElPanel.Controls.Add(this.ResizeElPanelButton);
            this.ElPanel.Controls.Add(this.CrutchPanel);
            this.ElPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ElPanel.Location = new System.Drawing.Point(0, 30);
            this.ElPanel.Name = "ElPanel";
            this.ElPanel.Size = new System.Drawing.Size(60, 574);
            this.ElPanel.TabIndex = 2;
            // 
            // ResizeElPanelButton
            // 
            this.ResizeElPanelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResizeElPanelButton.BackColor = System.Drawing.Color.Transparent;
            this.ResizeElPanelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ResizeElPanelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.ResizeElPanelButton.FlatAppearance.BorderSize = 0;
            this.ResizeElPanelButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.MenuText;
            this.ResizeElPanelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.ResizeElPanelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResizeElPanelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.ResizeElPanelButton.ForeColor = System.Drawing.SystemColors.Control;
            this.ResizeElPanelButton.Location = new System.Drawing.Point(0, 0);
            this.ResizeElPanelButton.Name = "ResizeElPanelButton";
            this.ResizeElPanelButton.Size = new System.Drawing.Size(60, 60);
            this.ResizeElPanelButton.TabIndex = 3;
            this.ResizeElPanelButton.Text = "☰";
            this.ResizeElPanelButton.UseVisualStyleBackColor = false;
            this.ResizeElPanelButton.Click += new System.EventHandler(this.ResizeElPanel);
            // 
            // CrutchPanel
            // 
            this.CrutchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrutchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.CrutchPanel.Controls.Add(this.ElementContainer);
            this.CrutchPanel.Location = new System.Drawing.Point(-278, 8);
            this.CrutchPanel.Name = "CrutchPanel";
            this.CrutchPanel.Size = new System.Drawing.Size(272, 554);
            this.CrutchPanel.TabIndex = 4;
            // 
            // ElementContainer
            // 
            this.ElementContainer.AutoScroll = true;
            this.ElementContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.ElementContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.ElementContainer.Location = new System.Drawing.Point(0, 0);
            this.ElementContainer.Name = "ElementContainer";
            this.ElementContainer.Size = new System.Drawing.Size(294, 554);
            this.ElementContainer.TabIndex = 5;
            // 
            // WorkPlace
            // 
            this.WorkPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkPlace.BackColor = System.Drawing.Color.Gray;
            this.WorkPlace.Location = new System.Drawing.Point(66, 36);
            this.WorkPlace.Name = "WorkPlace";
            this.WorkPlace.Size = new System.Drawing.Size(945, 564);
            this.WorkPlace.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1016, 604);
            this.Controls.Add(this.ElPanel);
            this.Controls.Add(this.UpControlePanel);
            this.Controls.Add(this.WorkPlace);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.SizeChanged += new System.EventHandler(this.OnResize);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMoveInForm);
            this.UpControlePanel.ResumeLayout(false);
            this.ElPanel.ResumeLayout(false);
            this.CrutchPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel UpControlePanel;
        private System.Windows.Forms.Button MinimizeButton;
        private System.Windows.Forms.Button ResizeButton;
        private System.Windows.Forms.Panel ElPanel;
        private System.Windows.Forms.Button ResizeElPanelButton;
        private System.Windows.Forms.Panel WorkPlace;
        private System.Windows.Forms.Panel CrutchPanel;
        private System.Windows.Forms.Panel ElementContainer;
    }
}


namespace SkyBattle_CourseProject
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxAnimation = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarBox = new System.Windows.Forms.TrackBar();
            this.DrawWindow = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1080, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 38);
            this.button2.TabIndex = 13;
            this.button2.Text = "Автор";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(959, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 38);
            this.button1.TabIndex = 12;
            this.button1.Text = "Справка";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // checkBoxAnimation
            // 
            this.checkBoxAnimation.AutoSize = true;
            this.checkBoxAnimation.Location = new System.Drawing.Point(956, 23);
            this.checkBoxAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAnimation.Name = "checkBoxAnimation";
            this.checkBoxAnimation.Size = new System.Drawing.Size(227, 21);
            this.checkBoxAnimation.TabIndex = 11;
            this.checkBoxAnimation.Text = "Неконтролируемая анимация";
            this.checkBoxAnimation.UseVisualStyleBackColor = true;
            this.checkBoxAnimation.CheckedChanged += new System.EventHandler(this.CheckBoxAnimation_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(956, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Вращение космического корабля";
            // 
            // trackBarBox
            // 
            this.trackBarBox.Location = new System.Drawing.Point(950, 88);
            this.trackBarBox.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarBox.Maximum = 360;
            this.trackBarBox.Name = "trackBarBox";
            this.trackBarBox.Size = new System.Drawing.Size(233, 56);
            this.trackBarBox.TabIndex = 9;
            this.trackBarBox.ValueChanged += new System.EventHandler(this.TrackBarBox_ValueChanged);
            // 
            // DrawWindow
            // 
            this.DrawWindow.AccumBits = ((byte)(0));
            this.DrawWindow.AutoCheckErrors = false;
            this.DrawWindow.AutoFinish = false;
            this.DrawWindow.AutoMakeCurrent = true;
            this.DrawWindow.AutoSwapBuffers = true;
            this.DrawWindow.BackColor = System.Drawing.Color.Black;
            this.DrawWindow.ColorBits = ((byte)(32));
            this.DrawWindow.DepthBits = ((byte)(16));
            this.DrawWindow.Location = new System.Drawing.Point(13, 22);
            this.DrawWindow.Margin = new System.Windows.Forms.Padding(4);
            this.DrawWindow.Name = "DrawWindow";
            this.DrawWindow.Size = new System.Drawing.Size(931, 583);
            this.DrawWindow.StencilBits = ((byte)(0));
            this.DrawWindow.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 634);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxAnimation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarBox);
            this.Controls.Add(this.DrawWindow);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Sky battle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxAnimation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarBox;
        private Tao.Platform.Windows.SimpleOpenGlControl DrawWindow;
        private System.Windows.Forms.Timer timer1;
    }
}


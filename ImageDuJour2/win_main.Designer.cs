namespace ImageDuJour2
{
    partial class WinMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_proxy_setup = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.chk_proxy = new System.Windows.Forms.CheckBox();
            this.bt_quit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pict_bing_today = new System.Windows.Forms.PictureBox();
            this.lab_bing_desc = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict_bing_today)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_proxy_setup);
            this.panel1.Controls.Add(this.chk_proxy);
            this.panel1.Controls.Add(this.bt_quit);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 464);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(7);
            this.panel1.Size = new System.Drawing.Size(718, 46);
            this.panel1.TabIndex = 0;
            // 
            // bt_proxy_setup
            // 
            this.bt_proxy_setup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_proxy_setup.ImageIndex = 0;
            this.bt_proxy_setup.ImageList = this.imageList1;
            this.bt_proxy_setup.Location = new System.Drawing.Point(273, 10);
            this.bt_proxy_setup.Name = "bt_proxy_setup";
            this.bt_proxy_setup.Size = new System.Drawing.Size(26, 26);
            this.bt_proxy_setup.TabIndex = 3;
            this.bt_proxy_setup.UseVisualStyleBackColor = true;
            this.bt_proxy_setup.Click += new System.EventHandler(this.bt_proxy_setup_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "WebConfiguration.png");
            // 
            // chk_proxy
            // 
            this.chk_proxy.Location = new System.Drawing.Point(305, 10);
            this.chk_proxy.Name = "chk_proxy";
            this.chk_proxy.Size = new System.Drawing.Size(104, 26);
            this.chk_proxy.TabIndex = 2;
            this.chk_proxy.Text = "Utiliser un proxy";
            this.chk_proxy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chk_proxy.UseVisualStyleBackColor = true;
            this.chk_proxy.CheckedChanged += new System.EventHandler(this.chk_proxy_CheckedChanged);
            // 
            // bt_quit
            // 
            this.bt_quit.Location = new System.Drawing.Point(10, 10);
            this.bt_quit.Name = "bt_quit";
            this.bt_quit.Size = new System.Drawing.Size(132, 26);
            this.bt_quit.TabIndex = 1;
            this.bt_quit.Text = "Annuler";
            this.bt_quit.UseVisualStyleBackColor = true;
            this.bt_quit.Click += new System.EventHandler(this.bt_quit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(528, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Utiliser comme papier-peint";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(718, 464);
            this.panel2.TabIndex = 1;
            // 
            // pict_bing_today
            // 
            this.pict_bing_today.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pict_bing_today.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pict_bing_today.Dock = System.Windows.Forms.DockStyle.Top;
            this.pict_bing_today.Location = new System.Drawing.Point(0, 0);
            this.pict_bing_today.Name = "pict_bing_today";
            this.pict_bing_today.Size = new System.Drawing.Size(718, 411);
            this.pict_bing_today.TabIndex = 2;
            this.pict_bing_today.TabStop = false;
            // 
            // lab_bing_desc
            // 
            this.lab_bing_desc.BackColor = System.Drawing.Color.Transparent;
            this.lab_bing_desc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lab_bing_desc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_bing_desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_bing_desc.Location = new System.Drawing.Point(0, 417);
            this.lab_bing_desc.Name = "lab_bing_desc";
            this.lab_bing_desc.Size = new System.Drawing.Size(718, 47);
            this.lab_bing_desc.TabIndex = 3;
            this.lab_bing_desc.Text = "label1";
            // 
            // WinMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 510);
            this.ControlBox = false;
            this.Controls.Add(this.lab_bing_desc);
            this.Controls.Add(this.pict_bing_today);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinMain";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "L\'image du jour";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pict_bing_today)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_quit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chk_proxy;
        private System.Windows.Forms.Button bt_proxy_setup;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pict_bing_today;
        private System.Windows.Forms.Label lab_bing_desc;
    }
}


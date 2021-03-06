﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser.Model;


namespace ImageDuJour2
{
    public partial class WinConfig : Form
    {
        
        
        public WinConfig()
        {
            InitializeComponent();
            ed_url.Text = WinMain._userproxy.Url.Trim();
            ed_port.Text = WinMain._userproxy.Port.Trim();
            ed_login.Text = WinMain._userproxy.Login.Trim();
            ed_password.Text = WinMain._userproxy.Password.Trim();
            chk_auth.Checked = WinMain._userproxy.useAuth;
            ed_login.Enabled = chk_auth.Checked;
            ed_password.Enabled = chk_auth.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WinConfig.ActiveForm != null) WinConfig.ActiveForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // On stocke les paramètres du proxy
            WinMain._userproxy.Url      = ed_url.Text.Trim();
            WinMain._userproxy.Port     = ed_port.Text.Trim();
            WinMain._userproxy.Login    = ed_login.Text.Trim();
            WinMain._userproxy.Password = ed_password.Text.Trim();

            if (WinConfig.ActiveForm != null)
            {
                // Save proxy parameters

                WinMain.iniData["PROXY"]["host"] = ed_url.Text.Trim();
                WinMain.iniData["PROXY"]["port"] = ed_port.Text.Trim();
                WinMain.iniData["PROXY"]["user"] = ed_login.Text.Trim();
                WinMain.iniData["PROXY"]["pwd"]  = ed_password.Text.Trim();
                
                WinConfig.ActiveForm.Close();
            }
        }
    }
}


﻿using System;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI_Form_Config : Form
    {
        public GUI_Form_Config()
        {
            InitializeComponent();
        }
        private void GUI_Form_Config_Load_1(object sender, EventArgs e)
        {
            if (ckW.Checked == true)
            {
                txtuid.ReadOnly = true;
                txtPass.ReadOnly = true;
            }
            else
            {
                txtuid.ReadOnly = false;
                txtPass.ReadOnly = false;
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("config.txt");
            if (ckW.Checked == true)
            {
                sw.WriteLine("windows");
                sw.WriteLine(txtserver.Text);
                sw.WriteLine(txtdb.Text);


            }
            else
            {
                sw.WriteLine("sql");
                sw.WriteLine(txtserver.Text);
                sw.WriteLine(txtdb.Text);
                sw.WriteLine(txtuid.Text);
                sw.WriteLine(txtPass.Text);
            }
            sw.Close();
            MessageBox.Show("Lưu thành công");
            this.Close();
        }

        private void ckW_CheckedChanged(object sender, EventArgs e)
        {
            if (ckW.Checked == true)
            {
                txtuid.ReadOnly = true;
                txtPass.ReadOnly = true;
            }
            else
            {
                txtuid.ReadOnly = false;
                txtPass.ReadOnly = false;
            }
        }

        private void bCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

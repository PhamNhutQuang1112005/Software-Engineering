﻿using System.Drawing;
using System.Windows.Forms;
namespace GUI
{
    partial class GUI_Form_Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI_Form_Config));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtserver = new System.Windows.Forms.TextBox();
            this.txtdb = new System.Windows.Forms.TextBox();
            this.txtuid = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.ckW = new System.Windows.Forms.CheckBox();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.bSave = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "UID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            // 
            // txtserver
            // 
            this.txtserver.Location = new System.Drawing.Point(105, 6);
            this.txtserver.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtserver.Name = "txtserver";
            this.txtserver.Size = new System.Drawing.Size(189, 22);
            this.txtserver.TabIndex = 4;
            // 
            // txtdb
            // 
            this.txtdb.Location = new System.Drawing.Point(105, 33);
            this.txtdb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtdb.Name = "txtdb";
            this.txtdb.Size = new System.Drawing.Size(189, 22);
            this.txtdb.TabIndex = 5;
            // 
            // txtuid
            // 
            this.txtuid.Location = new System.Drawing.Point(105, 59);
            this.txtuid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtuid.Name = "txtuid";
            this.txtuid.Size = new System.Drawing.Size(189, 22);
            this.txtuid.TabIndex = 6;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(105, 86);
            this.txtPass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(189, 22);
            this.txtPass.TabIndex = 7;
            // 
            // ckW
            // 
            this.ckW.AutoSize = true;
            this.ckW.Location = new System.Drawing.Point(14, 112);
            this.ckW.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckW.Name = "ckW";
            this.ckW.Size = new System.Drawing.Size(163, 20);
            this.ckW.TabIndex = 8;
            this.ckW.Text = "Window Authentication";
            this.ckW.UseVisualStyleBackColor = true;
            this.ckW.CheckedChanged += new System.EventHandler(this.ckW_CheckedChanged);
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(50, 137);
            this.bSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(94, 23);
            this.bSave.TabIndex = 9;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(165, 136);
            this.bCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(94, 23);
            this.bCancel.TabIndex = 10;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click_1);
            // 
            // GUI_Form_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 170);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.ckW);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtuid);
            this.Controls.Add(this.txtdb);
            this.Controls.Add(this.txtserver);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GUI_Form_Config";
            this.Text = "frmConfig";
            this.Load += new System.EventHandler(this.GUI_Form_Config_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtserver;
        private TextBox txtdb;
        private TextBox txtuid;
        private TextBox txtPass;
        private CheckBox ckW;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Button bCancel;
        private Button bSave;
    }
}
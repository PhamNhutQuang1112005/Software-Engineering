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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI_Form_Config));
            this.label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtserver = new TextBox();
            txtdb = new TextBox();
            txtuid = new TextBox();
            txtPass = new TextBox();
            ckW = new CheckBox();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            bSave = new Button();
            bCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 11);
            label1.Name = "label1";
            label1.Size = new Size(53, 20);
            label1.TabIndex = 0;
            label1.Text = "Server:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 1;
            label2.Text = "Database:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 81);
            label3.Name = "label3";
            label3.Size = new Size(37, 20);
            label3.TabIndex = 2;
            label3.Text = "UID:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 114);
            label4.Name = "label4";
            label4.Size = new Size(73, 20);
            label4.TabIndex = 3;
            label4.Text = "Password:";
            // 
            // txtserver
            // 
            txtserver.Location = new Point(105, 8);
            txtserver.Name = "txtserver";
            txtserver.Size = new Size(189, 27);
            txtserver.TabIndex = 4;
            // 
            // txtdb
            // 
            txtdb.Location = new Point(105, 41);
            txtdb.Name = "txtdb";
            txtdb.Size = new Size(189, 27);
            txtdb.TabIndex = 5;
            // 
            // txtuid
            // 
            txtuid.Location = new Point(105, 74);
            txtuid.Name = "txtuid";
            txtuid.Size = new Size(189, 27);
            txtuid.TabIndex = 6;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(105, 107);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(189, 27);
            txtPass.TabIndex = 7;
            // 
            // ckW
            // 
            ckW.AutoSize = true;
            ckW.Location = new Point(14, 140);
            ckW.Name = "ckW";
            ckW.Size = new Size(187, 24);
            ckW.TabIndex = 8;
            ckW.Text = "Window Authentication";
            ckW.UseVisualStyleBackColor = true;
            ckW.CheckedChanged += ckW_CheckedChanged_1;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // bSave
            // 
            bSave.Location = new Point(50, 171);
            bSave.Name = "bSave";
            bSave.Size = new Size(94, 29);
            bSave.TabIndex = 9;
            bSave.Text = "Save";
            bSave.UseVisualStyleBackColor = true;
            bSave.Click += bSave_Click_1;
            // 
            // bCancel
            // 
            bCancel.Location = new Point(165, 170);
            bCancel.Name = "bCancel";
            bCancel.Size = new Size(94, 29);
            bCancel.TabIndex = 10;
            bCancel.Text = "Cancel";
            bCancel.UseVisualStyleBackColor = true;
            bCancel.Click += bCancel_Click;
            // 
            // frmConfig
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(306, 212);
            Controls.Add(bCancel);
            Controls.Add(bSave);
            Controls.Add(ckW);
            Controls.Add(txtPass);
            Controls.Add(txtuid);
            Controls.Add(txtdb);
            Controls.Add(txtserver);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmConfig";
            Text = "frmConfig";
            ResumeLayout(false);
            PerformLayout();
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
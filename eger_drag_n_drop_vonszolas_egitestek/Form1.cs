using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eger_drag_n_drop_vonszolas_egitestek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Label[] helyek = new Label[10];
        Label[] egitestek = new Label[10];
        private string[] egitestNevek = {"Fold", "Jupiter", "Mars", "Merkur", "Nap", "Neptunusz", "Pluto", "Szaturnusz", "Uranusz", "Venusz"};
        private bool moving = false;
        int aktX, aktY = 0;
        private Label lbDragged;

        private void Form1_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < 10; i++)
            {
                var ujCimke = new Label
                {
                    Width = 127,
                    Height = 27,
                    Left = 803,
                    Top = 10 + i*30,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.Gold,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = egitestNevek[i]
                };

                egitestek[i] = ujCimke;
                this.Controls.Add(ujCimke);

                ujCimke.BringToFront();
                ujCimke.MouseDown += egitest_Mousedown;
                ujCimke.MouseUp += egitest_Mouseup;
                ujCimke.MouseMove += egitest_Mousemove;
            }

            helyek[0] = label1;
            helyek[1] = label2;
            helyek[2] = label3;
            helyek[3] = label4;
            helyek[4] = label5;
            helyek[5] = label6;
            helyek[6] = label7;
            helyek[7] = label8;
            helyek[8] = label9;
            helyek[9] = label10;
        }

        private void egitest_Mousemove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                lbDragged.Left += e.X - aktX;
                lbDragged.Top += e.Y - aktY;
                lbDragged.Tag = null;
                lbDragged.BackColor = Color.White;
            }
        }

        private void egitest_Mouseup(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            moving = false;
            for (int i = 0; i < helyek.Length; i++)
            {
                if (Tavolsag(lbDragged, helyek[i]) < 20)
                {
                    lbDragged.Left = helyek[i].Left;
                    lbDragged.Top = helyek[i].Top;
                    lbDragged.Tag = i;
                    break;
                }
            }
            lbDragged = null;
        }

        private void egitest_Mousedown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            moving = true;
            lbDragged = sender as Label;
            aktX = e.X;
            aktY = e.Y;
            lbDragged?.BringToFront();
        }

        private void Ellenorzes_Click(object sender, EventArgs e)
        {
            foreach (var label in egitestek)
            {
                if (label.Tag != null && label.Text == helyek[Convert.ToInt32(label.Tag)].Tag.ToString())
                {
                    label.BackColor = Color.Lime;
                }
                else
                {
                    label.BackColor = Color.Red;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < egitestek.Length; i++)
            {
                egitestek[i].Left = 803;
                egitestek[i].Top = 10 + i*30;
                egitestek[i].Tag = null;
                egitestek[i].BackColor = Color.White;
            }
        }

        private double Tavolsag(Label cimke1, Label cimke2)
        {
            return Math.Sqrt(Math.Pow(cimke2.Left - cimke1.Left, 2) + Math.Pow(cimke2.Top - cimke1.Top, 2));
        }
    }
}

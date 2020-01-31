using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace ACAD_NewDrawingSectionSelect
{
    public partial class SectionSelectForm : Form
    {
        Dictionary<string, List<string>> m_dict;

        public CurrentSectionDepartment result = null;

        #region Draggable form fields and methods
        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion

        public SectionSelectForm(Dictionary<string, List<string>> dict, CurrentSectionDepartment preSelected = null)
        {
            m_dict = dict;

            InitializeComponent();

            MouseDown += Form1_MouseDown;

            MouseUp += Form1_MouseUp;

            MouseMove += Form1_MouseMove;

            comboBox1.Items.AddRange(dict.Keys.ToArray());

            if (preSelected != null)
            {
                if (comboBox1.Items.Contains(preSelected.department))
                {
                    int comb1index = comboBox1.Items.IndexOf(preSelected.department);

                    comboBox1.SelectedIndex = comb1index;

                    List<string> sectsList = m_dict[m_dict.Keys.ToArray()[comb1index]];

                    SectionsFill(sectsList);

                    if (comboBox2.Items.Contains(preSelected.section))
                    {
                        int comb2index = comboBox2.Items.IndexOf(preSelected.section);

                        comboBox2.SelectedIndex = comb2index;
                    }
                    else
                    {
                        int comb2index = comboBox2.Items.Add(preSelected.section);

                        comboBox2.SelectedIndex = comb2index;
                    }
                }
                else
                {
                    comboBox1.SelectedIndex = 0;

                    SectionsFill(m_dict[m_dict.Keys.ToArray()[0]]);
                }
            }
            else
            {
                comboBox1.SelectedIndex = 0;

                SectionsFill(m_dict[m_dict.Keys.ToArray()[0]]);
            }

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SectionsFill(m_dict[comboBox1.SelectedItem.ToString()]);
        }

        private void SectionsFill(List<string> sects)
        {
            comboBox2.Items.Clear();

            comboBox2.Items.AddRange(sects.ToArray());

            comboBox2.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = new CurrentSectionDepartment();

            result.department = comboBox1.SelectedItem.ToString();

            result.section = comboBox2.SelectedItem.ToString();

            DialogResult = DialogResult.OK;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NewSection sectForm = new NewSection();

            DialogResult res = sectForm.ShowDialog();

            if (res != DialogResult.OK) return;

            int itemIndex = comboBox2.Items.Add(sectForm.result);

            comboBox2.SelectedIndex = itemIndex;
        }
    }
}

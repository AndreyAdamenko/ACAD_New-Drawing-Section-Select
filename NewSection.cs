using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACAD_NewDrawingSectionSelect
{
    public partial class NewSection : Form
    {
        public string result { private set; get; }
        
        public NewSection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = textBox1.Text;

            DialogResult = DialogResult.OK;
        }
    }
}

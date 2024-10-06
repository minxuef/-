using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CI
{
    public partial class Dialogue : Form
    {
        private string password = "123";
        public Dialogue()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim().Equals(this.password))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }

        }
    }
}

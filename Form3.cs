using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GogInstaller
{
    public partial class TextHelpDialog : Form
    {
        public TextHelpDialog(string text)
        {
            InitializeComponent();

            textBox1.Text = text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void TextHelpDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }
        }
    }
}

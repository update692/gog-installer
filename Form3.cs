using System;
using System.Windows.Forms;

namespace GogInstaller
{
    public partial class TextHelpDialog : Form
    {
        private FormWindowState _windowState = FormWindowState.Normal;

        public TextHelpDialog(string text)
        {
            InitializeComponent();

            textBox1.Text = text;
        }

        public void MyShow()
        {
            this.Show();
            this.Activate();
            this.WindowState = _windowState;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void TextHelpDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }

        private void TextHelpDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void TextHelpDialog_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != _windowState && (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized))
            {
                _windowState = this.WindowState;
            }
        }
    }
}

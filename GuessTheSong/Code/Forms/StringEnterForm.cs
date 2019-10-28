using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuessTheSong
{
    public delegate void GetEnteredValue(string value);

    public partial class StringEnterForm : Form
    {
        public event GetEnteredValue OnFormClosingWithValue;

        public StringEnterForm(string label, string value)
        {
            InitializeComponent();
            lblMessage.Text = label;
            txtValue.Text = value;
        }

        private void StringEnterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            if (OnFormClosingWithValue != null)
            {
                OnFormClosingWithValue(txtValue.Text);
            }
            this.Close();
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CloseForm();
            }
        }

    }
}

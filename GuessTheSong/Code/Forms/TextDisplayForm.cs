using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuessTheSong.Forms
{
    public partial class TextDisplayForm : Form
    {
        public static Font TextFont=null;
        public static Image BgImage = null;

        public TextDisplayForm()
        {
            InitializeComponent();
        }      

        private void txtText_Enter(object sender, EventArgs e)
        {
            btnDummy.Select();
        }

        private void TextDisplayForm_Load(object sender, EventArgs e)
        {
            txtText.Font = TextFont;
            leftPanel.BackgroundImage = BgImage;
            rightPanel.BackgroundImage = BgImage;
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }               
    }
}

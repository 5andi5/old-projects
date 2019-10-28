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
    public partial class ImageDisplayForm : Form
    {
        private Image m_Image = null;

        public ImageDisplayForm(Image image, Image backgroundImage)
        {
            InitializeComponent();
            m_Image = image;
            pbxPicture.BackgroundImage = backgroundImage;
        }

        private void ImageDisplayForm_Load(object sender, EventArgs e)
        {
            pbxPicture.Image = m_Image;
        }

        private void pbxPicture_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = e as MouseEventArgs;
            if (args != null)
            {
                if (args.Button == MouseButtons.Left)
                {
                    this.Close();
                }
                else if (args.Button == MouseButtons.Right)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
            }      
        }
    }
}

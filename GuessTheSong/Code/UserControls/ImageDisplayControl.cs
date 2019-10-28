using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuessTheSong.UserControls
{
    public partial class ImageDisplayControl: RoundUserControlBase
    {
        //from designer
        private readonly Font c_WordFont=new Font("Microsoft Sans Serif",  16F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(186)));

        private ImageRoundSettings m_Settings;


        public ImageDisplayControl()
            :base(true)
        {
            InitializeComponent();
            m_Settings = null;
        }

        private void ImageDisplayControl_Load(object sender, EventArgs e)
        {
            if (m_Settings == null)
            {
            }
            else
            {
                GenerateButtons();
            }
        }

        public override void LoadSettings(RoundSettingsBase settings)
        {
            if (!(settings is ImageRoundSettings))
            {
                throw new Exception("ImageDisplayControl accepts only ImageRoundSettings class as parameter");
            }
            LoadSettings((ImageRoundSettings)settings);
        }

        public void LoadSettings(ImageRoundSettings settings)
        {
            m_Settings = settings;
            if (settings != null)
            {
                GenerateButtons();
            }
        }

        private void GenerateButtons()
        {
            for (int index = tblLayout.Controls.Count-1; index >=0 ; index--)
            {
                tblLayout.Controls.RemoveAt(index);
            }

            int columnCount=m_Settings.ImageNames.Length;
            int columnWidth = 100 / columnCount;

            tblLayout.ColumnCount = columnCount;
            for (int index = 0; index < columnCount; index++)
            {
                tblLayout.ColumnStyles[index].SizeType = SizeType.Percent;
                tblLayout.ColumnStyles[index].Width = columnWidth;

                tblLayout.Controls.Add(CreatePictureButton(index, m_Settings.ImageNames[index]), index, 0);
            }
        }

        private Button CreatePictureButton(int index, string imagePath)
        {
            Button button = new Button();
            button.Name = "btnPicture" + index.ToString();
            button.BackgroundImage = TryLoadImage(imagePath);
            button.Dock = DockStyle.Fill;
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.Text = "";
            button.Click += pictureButton_Click;
            return button;
        }

        private Image TryLoadImage(string imagePath)
        {
            Image image = null;
            try
            {
                image = Image.FromFile(imagePath);
            }
            catch
            {
                image = GuessTheSong.Properties.Resources.X;
            }
            return image;
        }

        private void pictureButton_Click(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            Image image = senderButton.BackgroundImage;
            if (image != null)
            {
                ImageDisplayForm displayForm = new ImageDisplayForm(image, this.BackgroundImage);
                displayForm.ShowDialog();
            }
        }
    }
}

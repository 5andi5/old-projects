using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GuessTheSong.Forms;

namespace GuessTheSong.UserControls
{
    public partial class WordGuessingControl : RoundUserControlBase
    {
        private WordGuessingRoundSettings m_Settings;
        public static Font DisplayFont = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(186)));
        private Font m_Font = DisplayFont;
        private bool m_ShowSongTextButtonAdded = false;


        public WordGuessingControl()
            : base(true)
        {
            InitializeComponent();
            m_Settings = null;
        }

        private void WordGuessingControl_Load(object sender, EventArgs e)
        {
            if (m_Settings != null)
            {
                GenerateButtons();
            }
        }

        public void LoadSettings(WordGuessingRoundSettings settings)
        {
            m_Settings = settings;
            if (settings != null)
            {
                GenerateButtons();
            }
        }

        private void GenerateButtons()
        {
            this.SuspendLayout();
            for (int index = tblLayout.Controls.Count - 1; index >= 0; index--)
            {
                tblLayout.Controls.RemoveAt(index);
            }

            int columnCount = m_Settings.Words.Length;
            int columnWidth = 100 / columnCount;

            CreateColumns(columnCount);
            for (int index = 0; index < columnCount; index++)
            {
                tblLayout.Controls.Add(CreateImageButton(index), index, 0);
            }
            this.ResumeLayout();
        }

        private void CreateColumns(int columnCount)
        {
            tblLayout.ColumnCount = columnCount;
            int columnWidht = 100 / columnCount;
            int index;
            for (index = 0; index < tblLayout.ColumnStyles.Count; index++)
            {
                tblLayout.ColumnStyles[index].SizeType = SizeType.Percent;
                tblLayout.ColumnStyles[index].Width = columnWidht;
            }
            for (index = tblLayout.ColumnStyles.Count; index < columnCount; index++)
            {
                tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnWidht));
            }
            for (index = tblLayout.ColumnStyles.Count - 1; index >= columnCount; index--)
            {
                tblLayout.ColumnStyles.RemoveAt(index);
            }
        }

        private Button CreateImageButton(int index)
        {
            Button button = new Button();
            button.Name = "btnImage" + index.ToString();
            button.BackgroundImage = GuessTheSong.Properties.Resources.Question;
            button.Click += imageButton_Click;
            button.Dock = DockStyle.Fill;
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.Font = m_Font;
            button.Text = (index + 1).ToString();
            return button;
        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            int position = tblLayout.GetColumn(senderButton);
            WordInfo wordInfo = m_Settings.Words[position];
            if (wordInfo.IsHidden)
            {
                wordInfo.IsHidden = false;
                if (wordInfo.IsBadWord)
                {
                    //senderButton.BackgroundImage = GuessTheSong.Properties.Resources.Minus;
                    senderButton.BackgroundImage = GuessTheSong.Properties.Resources.MinusTall;
                }
                else
                {
                    //senderButton.BackgroundImage = GuessTheSong.Properties.Resources.Plus;
                    senderButton.BackgroundImage = GuessTheSong.Properties.Resources.PlusTall;
                }
                senderButton.Text = wordInfo.Word;
            }
            ShowSongTextIfGuessed();
        }

        private void ShowSongTextIfGuessed()
        {
            if (!m_ShowSongTextButtonAdded && m_Settings.HasFullSongText)
            {
                bool allGuessed = true;
                foreach (WordInfo word in m_Settings.Words)
                {
                    if (word.IsHidden)
                    {
                        allGuessed = false;
                        break;
                    }
                }
                if (allGuessed)
                {
                    AddShowSongFullTextButton();
                    m_ShowSongTextButtonAdded = true;
                }                
            }
        }

        private void AddShowSongFullTextButton()
        {
            this.SuspendLayout();
            this.Visible = false;
            Button button = new Button();
            button.Name = "btnShowSongFullText";
            button.BackgroundImage = GuessTheSong.Properties.Resources.Note;
            button.Click += showFullTextButton_Click;
            button.Dock = DockStyle.Fill;
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.Font = m_Font;
            button.Text = "";

            int totalButtonCount = m_Settings.Words.Length + 1;
            int buttonIndex = totalButtonCount - 1;
            int columnWidth = 100 / totalButtonCount;
            CreateColumns(totalButtonCount);
            tblLayout.Controls.Add(button, buttonIndex, 0);
            this.Visible = true;
            this.ResumeLayout();
        }

        private void showFullTextButton_Click(object sender, EventArgs e)
        {
            TextDisplayForm textForm = new TextDisplayForm();
            textForm.txtText.Text = m_Settings.FullSongText;
            textForm.ShowDialog();
        }

        public override Font Font
        {
            get
            {
                return m_Font;
            }
            set
            {
                m_Font = value;
                foreach (Control control in tblLayout.Controls)
                {
                    control.Font = m_Font;
                }
            }
        }

        public override void LoadSettings(RoundSettingsBase settings)
        {
            if (!(settings is WordGuessingRoundSettings))
            {
                throw new Exception("WordGuessingControl accepts only WordGuessingRoundSettings class as parameter");
            }
            LoadSettings((WordGuessingRoundSettings)settings);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using GuessTheSong.Properties;

namespace GuessTheSong.UserControls
{

    public class RoundUserControlBase : UserControl
    {
        public static Image RoundBackgroundImage=Resources.Pattern;

        private bool m_HasBackgroundImage;

        public RoundUserControlBase(bool hasBackgroundImage)
        {
            m_HasBackgroundImage = hasBackgroundImage;
            if (m_HasBackgroundImage)
            {
                this.BackgroundImage = RoundBackgroundImage;
            }
        }

        /// <summary>
        /// Default constructor for form designer.
        /// </summary>
        public RoundUserControlBase()
        {
            m_HasBackgroundImage = true;
        }

        virtual public void LoadSettings(RoundSettingsBase settings)
        {
            throw new NotImplementedException("LoadSettings method should be overwritten");
        }

        public virtual void SetBackgroundImage()
        {
            if (m_HasBackgroundImage)
            {
                this.BackgroundImage = RoundBackgroundImage;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RoundUserControlBase
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Name = "RoundUserControlBase";
            this.ResumeLayout(false);

        }
    }
}

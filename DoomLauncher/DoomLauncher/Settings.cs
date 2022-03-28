// Andrew Maney 2022


// Required Libraries
using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DoomLauncher
{
    public partial class Settings : Form
    {
        // Variables
        RegistryKey SettingsKey;


        public Settings()
        {
            InitializeComponent();
            SettingsKey = Registry.CurrentUser.OpenSubKey("SETTINGS", true);
            if (SettingsKey != null)
            {
                if((string)SettingsKey.GetValue("FullScreen") == "1")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if ((string)SettingsKey.GetValue("ShowConsole") == "1")
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    SettingsKey.SetValue("FullScreen", "1");
                }
                else
                {
                    SettingsKey.SetValue("FullScreen", "0");
                }
                if (checkBox2.Checked)
                {
                    SettingsKey.SetValue("ShowConsole", "1");
                }
                else
                {
                    SettingsKey.SetValue("ShowConsole", "0");
                }
                SettingsKey.Close();
                Close();
            }
            catch
            {
                MessageBox.Show("EEFEGHGUDJKGSHFDS");
            }
        }
    }
}

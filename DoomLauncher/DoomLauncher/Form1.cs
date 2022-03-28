// Andrew Maney 2022


// Required Libraries
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DoomLauncher
{
    public partial class Form1 : Form
    {
		// Variables
		public string CWD = Directory.GetCurrentDirectory();
		public bool GameRunning = false;
		public string ERROR_DATA = "";
		public bool ERROR = false;
		public bool ShowConsole = false;
		public bool FullScreen = false;
		RegistryKey Fscreen;


		// Functions
        public Form1()
        {
            InitializeComponent();
			Fscreen = Registry.CurrentUser.OpenSubKey("SETTINGS", true);
			if (Fscreen == null)
			{
				Fscreen = Registry.CurrentUser.CreateSubKey("SETTINGS", true);
				Fscreen.SetValue("FullScreen", "0");
				Fscreen.SetValue("ShowConsole", "0");
				Fscreen.Close();
            }
            else
            {

            }
        }


        // Run Doom
        public void RunDoom()
        {
			if((string)Fscreen.GetValue("ShowConsole") == "1")
            {
				ShowConsole = true;
            }
            else
            {
				ShowConsole = false;
            }
			if ((string)Fscreen.GetValue("FullScreen") == "1")
			{
				FullScreen = true;
			}
			else
			{
				FullScreen = false;
			}
			GameRunning = true;
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = false;
			startInfo.FileName = CWD + "/bin/DosBox/DOSBox.exe";
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.Arguments = "-conf \"" + CWD + "/bin/DosBox/dosbox_doom.conf\"";
            if (FullScreen)
            {
				startInfo.Arguments += " -fullscreen ";
            }
            if (!ShowConsole)
            {
				startInfo.Arguments += " -noconsole ";
			}
			try
			{
				using (Process exeProcess = Process.Start(startInfo))
				{
					exeProcess.WaitForExit();
				}
			}
			catch (Exception EX)
			{
				ERROR_DATA = EX.Message;
				ERROR = true;
			}
		}


		// Run DosBox
		public void RunDosBox()
		{
			if ((string)Fscreen.GetValue("ShowConsole") == "1")
			{
				ShowConsole = true;
			}
			else
			{
				ShowConsole = false;
			}
			if ((string)Fscreen.GetValue("FullScreen") == "1")
			{
				FullScreen = true;
			}
			else
			{
				FullScreen = false;
			}
			GameRunning = true;
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = false;
			startInfo.FileName = CWD + "/bin/DosBox/DOSBox.exe";
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.Arguments = "-conf \"" + CWD + "/bin/DosBox/dosbox.conf\"";
			if (FullScreen)
			{
				startInfo.Arguments += " -fullscreen ";
			}
			if (!ShowConsole)
			{
				startInfo.Arguments += " -noconsole ";
			}
			try
			{
				using (Process exeProcess = Process.Start(startInfo))
				{
					exeProcess.WaitForExit();
				}
			}
			catch (Exception EX)
			{
				ERROR_DATA = EX.Message;
				ERROR = true;
			}
		}


		// Edit DosBox options
		public void EditOptions()
		{
			GameRunning = true;
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = false;
			startInfo.FileName = CWD + "/bin/DosBox/Options.bat";
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.Arguments = CWD + "/bin/DosBox/";
			try
			{
				using (Process exeProcess = Process.Start(startInfo))
				{
					exeProcess.WaitForExit();
				}
			}
			catch (Exception EX)
			{
				ERROR_DATA = EX.Message;
				ERROR = true;
			}
		}


		// Open DOOM Folder
		public void OpenDOOMFolder()
		{
			GameRunning = true;
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = false;
			startInfo.FileName = CWD + "/bin/DOOM/CURDIR.bat";
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.Arguments = CWD + "/bin/DOOM";
			try
			{
				using (Process exeProcess = Process.Start(startInfo))
				{
					exeProcess.WaitForExit();
				}
			}
			catch (Exception EX)
			{
				ERROR_DATA = EX.Message;
				ERROR = true;
			}
		}

		private void button1_Click(object sender, EventArgs e)
        {
			Thread thread = new Thread(() =>
			{
				RunDoom();
				GameRunning = false;
			});
			thread.Start();
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
			if(ERROR == true)
            {
				ERROR = false;
				MessageBox.Show(ERROR_DATA, "An Error Occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (GameRunning)
            {
				button1.Text = "Game is running";
				button2.Text = "Game is running";
				button3.Text = "Game is running";
				button4.Text = "Game is running";
				button1.Enabled = false;
				button2.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
            }
            else
            {
				button1.Text = "Launch DOOM";
				button2.Text = "Launch DosBox";
				button3.Text = "Edit DosBox options";
				button4.Text = "Open DOOM folder";
				button1.Enabled = true;
				button2.Enabled = true;
				button3.Enabled = true;
				button4.Enabled = true;
			}
        }

        private void button2_Click(object sender, EventArgs e)
        {
			Thread thread = new Thread(() =>
			{
				RunDosBox();
				GameRunning = false;
			});
			thread.Start();
		}

        private void button3_Click(object sender, EventArgs e)
        {
			Thread thread = new Thread(() =>
			{
				EditOptions();
				GameRunning = false;
			});
			thread.Start();
		}

        private void button4_Click(object sender, EventArgs e)
        {
			Thread thread = new Thread(() =>
			{
				OpenDOOMFolder();
				GameRunning = false;
			});
			thread.Start();
		}

        private void button5_Click(object sender, EventArgs e)
        {
			Environment.Exit(0);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Settings settings = new Settings();
			settings.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Environment.Exit(0);
        }
    }
}

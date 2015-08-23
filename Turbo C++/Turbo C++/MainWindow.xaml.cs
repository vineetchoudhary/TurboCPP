using System;
using System.Diagnostics;
using System.Text;
using System.Timers;
using System.Windows;


namespace Turbo_C__
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String CurrentDirectory = Environment.CurrentDirectory;
        String DoxBoxPath = Environment.CurrentDirectory + "\\DOSBox-0.74\\DOSBox.exe";
        String TCPath = Environment.SystemDirectory.Substring(0, 2) + "\\TC\\BIN";
        String ProjectDirectory = Environment.SystemDirectory.Substring(0, 2) + "\\TC\\Projects";

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                LoadStartupSetting();
                SetRecentItems();
				
                //memory monitor
                Timer memoryMoniter = new Timer(1000);
                memoryMoniter.Enabled = true;
                memoryMoniter.Elapsed += Timer_Elapsed;
            }
            catch {}
        }
		
        #region Button Click Event

        private void turboc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://turbocpp.codeplex.com/");
            }
            catch {}
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveSetting();
                Environment.Exit(Environment.ExitCode);
            }
            catch {}
        }

        private void buttonNewProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.DefaultExt = ".prj";
                dlg.Title = "Project Name...";
                dlg.Filter = "Project Files (*.prj)|*.prj";
                dlg.InitialDirectory = ProjectDirectory;
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true && ValidatePath(dlg.FileName))
                {
                    AddRecentItems(dlg.FileName, dlg.SafeFileName);
                    StartTurbo((bool)checkBoxFullScreen.IsChecked, true, dlg.FileName, (bool)checkBoxKeepWindowOpen.IsChecked);
                }
            }
            catch {}
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.DefaultExt = "*.c";
                dlg.Title = "create a source code file...";
                dlg.Filter = "C++ Files (*.cpp)|*.cpp|C Files (*.c)|*.c|Header Files (*.h)|*.h|All Files (*.*)|*.*";
                dlg.InitialDirectory = ProjectDirectory;
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true && ValidatePath(dlg.FileName))
                {
                    AddRecentItems(dlg.FileName, dlg.SafeFileName);
                    StartTurbo((bool)checkBoxFullScreen.IsChecked, true, dlg.FileName, (bool)checkBoxKeepWindowOpen.IsChecked);
                }
            }
            catch {}
        }

        private void buttonOpenProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".prj";
                dlg.Title = "Select a project...";
                dlg.Multiselect = false;
                dlg.Filter = "Project Files (*.prj)|*.prj";
                dlg.InitialDirectory = ProjectDirectory;
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true && ValidatePath(dlg.FileName))
                {
                    AddRecentItems(dlg.FileName, dlg.SafeFileName);
                    StartTurbo((bool)checkBoxFullScreen.IsChecked, true, dlg.FileName, (bool)checkBoxKeepWindowOpen.IsChecked);
                }
            }
            catch {}
        }

        private void buttonOpenSourceFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = "*.*";
                dlg.Title = "Select a source code file...";
                dlg.Multiselect = false;
                dlg.Filter = "All Files (*.*)|*.*|C++ Files (*.cpp)|*.cpp|C Files (*.c)|*.c|Header Files (*.h)|*.h";
                dlg.InitialDirectory = ProjectDirectory;
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true && ValidatePath(dlg.FileName))
                {
                    AddRecentItems(dlg.FileName, dlg.SafeFileName);
                    StartTurbo((bool)checkBoxFullScreen.IsChecked, true, dlg.FileName, (bool)checkBoxKeepWindowOpen.IsChecked);
                }
            }
            catch {}
        }

        private void buttonRecent1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddRecentItems(Properties.Settings.Default.Recent1, Properties.Settings.Default.Recent1Name);
                StartTurbo((bool)checkBoxFullScreen.IsChecked, true, Properties.Settings.Default.Recent1, (bool)checkBoxKeepWindowOpen.IsChecked);
            }
            catch {}
        }

        private void buttonRecent2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddRecentItems(Properties.Settings.Default.Recent2, Properties.Settings.Default.Recent2Name);
                StartTurbo((bool)checkBoxFullScreen.IsChecked, true, Properties.Settings.Default.Recent2, (bool)checkBoxKeepWindowOpen.IsChecked);
            }
            catch {}
        }

        private void buttonRecent3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddRecentItems(Properties.Settings.Default.Recent3, Properties.Settings.Default.Recent3Name);
                StartTurbo((bool)checkBoxFullScreen.IsChecked, true, Properties.Settings.Default.Recent3, (bool)checkBoxKeepWindowOpen.IsChecked);
            }
            catch {}
        }

        private void buttonLearnCPP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://turbocpp.codeplex.com/wikipage?title=CPPDocumentationHome");
            }
            catch {}
        }

        private void buttonLearnC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://turbocpp.codeplex.com/wikipage?title=CDocumentationHome");
            }
            catch {}
        }

        private void buttonDocumentation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://turbocpp.codeplex.com/documentation");
            }
            catch {}
        }

        private void buttonReportABug_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String subject = "Turbo C++ ver3.0 bug ";
                String body = "\n\n\nOSVersion : " + Environment.OSVersion + "\n.NET Version : " + Environment.Version + "\nIs 64-Bit OS : " + Environment.Is64BitOperatingSystem + "\n";
                Process.Start("mailto:vineetchoudhary291@gmail.com?subject=" + subject + "&body=" + body);
            }
            catch {}
        }

        private void buttonSubmitAnIdea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String subject = "An idea for Turbo C++";
                Process.Start("mailto:vineetchoudhary291@gmail.com?subject=" + subject + "&body=");
            }
            catch {}
        }

        private void buttonLicense_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://turbocpp.codeplex.com/license");
            }
            catch {}
        }

        private void buttonStartC_Click(object sender, RoutedEventArgs e)
        {
            StartTurbo((bool)checkBoxFullScreen.IsChecked, false, null, (bool)checkBoxKeepWindowOpen.IsChecked);
        }

        #endregion

        #region Run Turbo C++
        private void StartTurbo(bool fullScreen, bool openFile, string filePath, bool keepWindowOpen)
        {
            try
            {
                StringBuilder doxBoxArguments = new StringBuilder();
                string dir = Environment.SystemDirectory.Substring(0, 1);
                doxBoxArguments.Append("-c \"mount " + dir + " " + dir + ":\\ \"");
                doxBoxArguments.Append(" -c \"" + dir + ":\"");
                doxBoxArguments.Append(" -c \"cd TURBOC3\"");
                doxBoxArguments.Append(" -c \"cd bin\"");
                doxBoxArguments.Append(" -c \"cls\"");
                doxBoxArguments.Append(" -c \"TC");
                if (openFile) doxBoxArguments.Append(" " + filePath);
                doxBoxArguments.Append("\"");
                if (fullScreen) doxBoxArguments.Append(" -fullscreen");
                doxBoxArguments.Append(" -userconf");
                doxBoxArguments.Append(" -c \"exit\"");

                Process bat = new Process();
                bat.StartInfo.FileName = DoxBoxPath;
                bat.StartInfo.Arguments = doxBoxArguments.ToString();
                bat.StartInfo.UseShellExecute = false;
                bat.StartInfo.RedirectStandardOutput = false;
                bat.StartInfo.CreateNoWindow = true;
                bat.Start();

                if (!(bool)checkBoxKeepWindowOpen.IsChecked)
                {
                    SaveSetting();
                    Environment.Exit(Environment.ExitCode);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message + "\nPlease run Turbo C++ as administrator.", @"ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region update memory info info
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
            {
                try
                {
                    tbPysicalMemory.Text = "Physical Memory : " + currentProcess.WorkingSet64 / (1024 * 1024) + " MB";
                    tbVirtualMemory.Text = "Virtual Memory : " + currentProcess.VirtualMemorySize64 / (1024 * 1024) + " MB";
                }
                catch {}
            }));

        }
        #endregion

        #region StartUp Setting
        private void LoadStartupSetting()
        {
            checkBoxKeepWindowOpen.IsChecked = Properties.Settings.Default.KeepWindowOpen;
            checkBoxFullScreen.IsChecked = Properties.Settings.Default.FullScreenMode;
        }
        
        #endregion

        #region Save Setting
        private void SaveSetting()
        {
            Properties.Settings.Default.KeepWindowOpen = (bool)checkBoxKeepWindowOpen.IsChecked;
            Properties.Settings.Default.FullScreenMode = (bool)checkBoxFullScreen.IsChecked;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Recent
        private void AddRecentItems(string newItem, string newItemName)
        {
            if (Properties.Settings.Default.Recent1 == newItem)
            {
            }
            else if (Properties.Settings.Default.Recent2 == newItem)
            {
                Properties.Settings.Default.Recent2 = Properties.Settings.Default.Recent1;
                Properties.Settings.Default.Recent1 = newItem;

                Properties.Settings.Default.Recent2Name = Properties.Settings.Default.Recent1Name;
                Properties.Settings.Default.Recent1Name = newItemName;
            }
            else
            {
                Properties.Settings.Default.Recent3 = Properties.Settings.Default.Recent2;
                Properties.Settings.Default.Recent2 = Properties.Settings.Default.Recent1;
                Properties.Settings.Default.Recent1 = newItem;

                Properties.Settings.Default.Recent3Name = Properties.Settings.Default.Recent2Name;
                Properties.Settings.Default.Recent2Name = Properties.Settings.Default.Recent1Name;
                Properties.Settings.Default.Recent1Name = newItemName;
            }
            Properties.Settings.Default.Save();

            SetRecentItems();
        }

        private void SetRecentItems()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Recent1Name))
            {
                buttonRecent1.Content = Properties.Settings.Default.Recent1Name;
                buttonRecent1.Visibility = Visibility.Visible;
            }
            else
            {
                buttonRecent1.Visibility = Visibility.Hidden;
            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Recent2Name))
            {
                buttonRecent2.Content = Properties.Settings.Default.Recent2Name;
                buttonRecent2.Visibility = Visibility.Visible;
            }
            else
            {
                buttonRecent2.Visibility = Visibility.Hidden;
            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Recent3Name))
            {
                buttonRecent3.Content = Properties.Settings.Default.Recent3Name;
                buttonRecent3.Visibility = Visibility.Visible;
            }
            else
            {
                buttonRecent3.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Validation
        private bool ValidatePath(string path)
        {
            if (path.Contains(ProjectDirectory))
            {
                if (path.Contains(@" "))
                {
                    MessageBox.Show("Project/Filename or directory cannot contain spaces.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("You can only open/save the project/file in " + ProjectDirectory + " directory.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
        #endregion
    }
}

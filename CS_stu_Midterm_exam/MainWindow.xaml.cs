using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace CS_stu_Midterm_exam
{
    public partial class MainWindow : Window
    {
        public bool main_Bold = false;
        public bool main_Ltalic = false;
        public bool main_Line = false;
        public int size_in = 10;
        public FontFamily main_FontFamily_put;

        string app_loc;
        int iWidth = new int();
        int iHeight = new int();
        bool is_Save=false;//如果有檔案
        bool need_seve = false;//有輸入過
        string Save_path="";//儲存點


        public MainWindow()
        {
            InitializeComponent();
            app_loc = this.GetType().Assembly.Location;
            iWidth = (int)this.Width;
            iHeight = (int)this.Height;
            Text_in.AcceptsReturn = true;
        }
        //當主視窗更新大小時，TextBox更新大小
        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Width == 0 || Height == 0) return;
            iWidth = (int)this.Width;
            iHeight = (int)this.Height;
            Text_in.Height = iHeight - 75;
            Text_in.Width = iWidth - 15;
        }
        //說明->傳送意見反應
        private void Some_idear(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("不可以有意見","想要提供意見嗎?");
        }
        //儲存檔案內容
        private void Save_file()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "*.txt";
            saveFileDialog.Filter = "Text file|*.txt|All file|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(Text_in.Text);
                }
                Save_path = path;
                is_Save = true;
                window.Title = saveFileDialog.SafeFileName + " - 記事本";
                need_seve = false;
            }
        }
        //開新檔案內容
        public void ExitApp()

        {
            if (need_seve)
            {
                string messageBoxText = "Do you want to save changes?";
                string caption = "Word Processor";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = (MessageBoxResult)MessageBox.Show(messageBoxText, caption, (MessageBoxButtons)button, (MessageBoxIcon)icon);

                // Process message box results
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Save_file();
                        break;
                    case MessageBoxResult.No:
                        is_Save = false;
                        Save_path = "";
                        Text_in.Text = "";
                        window.Title = "未命名 - 記事本";
                        need_seve = false;
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }
        //如果使用者有輸入過
        private void Text_in_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
            if (need_seve == false)
            {
                string title = window.Title;
                window.Title = "* " + title;
            }
            need_seve = true;
        }
        //編輯menu刷新
        private void Edit_Item_Refresh(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Text_in.SelectionLength > 0)
            {
                Cut_item.IsEnabled = true;
                Remove_item.IsEnabled = true;
                Copy_item.IsEnabled = true;
            }
            else
            {
                Cut_item.IsEnabled = false;
                Remove_item.IsEnabled = false;
                Copy_item.IsEnabled = false;
            }
        }


        /*menu檔案類功能
         *      |--開新檔案(O)
         *      |--開新視窗(O)
         *      |--開啟舊檔(O)
         *      |--儲存檔案(O)
         *      |--另存為....(O)
         *      |--結束(O)
         */
        //檔案->開新檔案 功能
        private void New_File_Click(object sender, RoutedEventArgs e)
        {
            ExitApp();
        }
        //檔案->開啟新視窗 功能
        private void New_windows_Click(object sender, RoutedEventArgs e)
        {
            using (Process myProcess = new Process())
            {
                myProcess.StartInfo.UseShellExecute = false;
                // You can start any process, HelloWorld is a do-nothing example.
                myProcess.StartInfo.FileName = app_loc;
                myProcess.Start();
            }
        }
        //檔案->開啟舊檔 功能
        private void Loading_Click(object sender, RoutedEventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".txt";
            openfile.Filter = "文字文件|*.txt|全部文件|*.*";
            if (openfile.ShowDialog() == true)
            {
                string path = openfile.FileName;
                var reader = new StreamReader(path, Encoding.GetEncoding("big5"));//var不明確的宣告

                Text_in.Text = File.ReadAllText(path);

                Save_path = path;
                is_Save = true;
                window.Title = openfile.SafeFileName + " - 記事本";
            }

        }
        //檔案->儲存檔案 按鈕
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (is_Save == false)
            {
                Save_file();
            }
            else
            {
                using (StreamWriter sw = File.CreateText(Save_path))
                {
                    sw.WriteLine(Text_in.Text);
                }
            }
        }
        //檔案->另存為.... 功能
        private void Other_Save_Click(object sender, RoutedEventArgs e)
        {
            Save_file();
        }
        //檔案->結束 功能
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (need_seve)
            {
                ExitApp();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }


        /*menu編輯類功能
         *      |--復原(O)
         *      |--剪下(O)
         *      |--複製(O)
         *      |--貼上(O)
         *      |--刪除(O)
         *      |--全選(O)
         *      |--時間/日期(O)
         */
        //編輯->復原 功能
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            Text_in.Undo();
        }
        //編輯->剪下 功能
        private void cut_Click(object sender, RoutedEventArgs e)
        {
            if (Text_in.SelectedText != "")
            {
                Text_in.Cut();
            }
        }
        //編輯->複製 功能
        private void copy_Click(object sender, RoutedEventArgs e)
        {
            if (Text_in.SelectionLength > 0)
            {
                Text_in.Copy();
            }
        }
        //編輯->貼上 功能
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            Text_in.Paste();
        }
        //編輯->刪除 功能
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (Text_in.SelectionLength > 0)
            {
                int start = Text_in.SelectionStart;
                int end = start + Text_in.SelectionLength;
                string index = Text_in.Text;

                Text_in.Text = index.Remove(start, end);
            }
        }
        //編輯->全選 功能
        private void Select_all_Click(object sender, RoutedEventArgs e)
        {
            Text_in.Select(0, Text_in.Text.Length);
        }
        //編輯->時間/日期 功能
        private void add_time(object sender, RoutedEventArgs e)
        {
            DateTime localDate = DateTime.Now;
            int test = Text_in.SelectionStart;
            Text_in.Text = Text_in.Text.Insert(test, localDate.ToString());
        }

        private void Auto_return_item_Click(object sender, RoutedEventArgs e)
        {
            if (Auto_return_item.IsChecked)
            {
                Auto_return_item.IsChecked = false;
                Text_in.TextWrapping = TextWrapping.NoWrap;
            }
            else
            {
                Auto_return_item.IsChecked = true;
                Text_in.TextWrapping = TextWrapping.Wrap;
            }

        }

        private void Form_Click(object sender, RoutedEventArgs e)
        {
            Window1 mywindow = new Window1(this);
            mywindow.Show();
        }

        private void BGColor_Click(object sender, RoutedEventArgs e)
        {
            Color BG_color = GetDialog();
            Brush currentsolidBush = new System.Windows.Media.SolidColorBrush(BG_color);
            Text_in.Background = currentsolidBush;
        }
        private Color GetDialog()
        {
            ColorDialog colorDialog = new ColorDialog();
            
            colorDialog.ShowDialog();
            System.Drawing.Color color_input = colorDialog.Color;
            return Color.FromArgb(color_input.A, color_input.R, color_input.G, color_input.B);

        }
        public void forn_return(Window1 in_put)
        {
            main_Bold = in_put.Bold;
            main_Ltalic = in_put.Ltalic;
            main_Line = in_put.Line;
            size_in = in_put.size_out;
            main_FontFamily_put=in_put.FontFamily_put;
    }
    }
}

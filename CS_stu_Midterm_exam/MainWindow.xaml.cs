using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CS_stu_Midterm_exam
{
    public partial class MainWindow : Window
    {
        int iWidth = new int();
        int iHeight = new int();
        bool is_Save=false;//如果有檔案
        string Save_path="";//儲存點
        public MainWindow()
        {
            InitializeComponent();
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
        //開啟檔案功能
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
        //儲存檔案功能
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (is_Save == false)
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
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(Save_path))
                {
                    sw.WriteLine(Text_in.Text);
                }
            }
        }
        //另存新檔功能
        private void Other_Save_Click(object sender, RoutedEventArgs e)
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
            }
        }
        //傳送意見反應
        private void Some_idear(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("不可以有意見","想要提供意見嗎?");
        }
    }
}

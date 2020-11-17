using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FontFamily = System.Windows.Media.FontFamily;

namespace CS_stu_Midterm_exam
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow frm1;
        public bool Bold = false;
        public bool Ltalic = false;
        public bool Line = false;
        public int size_out = 10;
        public FontFamily FontFamily_put;

        public Window1(MainWindow form)
        {
            InitializeComponent();
            frm1 = form;
            windows_loading(frm1);
            PopulateListBoxWithFonts();
            set_size();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frm1.forn_return(this);
            this.Close();
        }
        private void windows_loading(MainWindow farm_in_put)
        {
            Bold = farm_in_put.main_Bold;
            Ltalic = farm_in_put.main_Ltalic;
            Line = farm_in_put.main_Ltalic;
            size_out = (int)farm_in_put.Text_in.FontSize;
            FontFamily_put = farm_in_put.Text_in.FontFamily;
            font_show.Text = FontFamily_put.ToString();
            Size_show.Text = size_out.ToString();

        }
        private void PopulateListBoxWithFonts()
        {
            foreach (FontFamily oneFontFamily in Fonts.SystemFontFamilies)
            {
                Font_listBox.Items.Add(oneFontFamily.Source);
                
            }
        }
        private void set_size() {
            int[] size = new int[] { 8, 9, 10, 11, 12, 14, 16 ,18,20,22,24,26,28,36,48,72};
            foreach (int size_item in size)
            {
                size_listBox.Items.Add(size_item);
            }
        }
        private void Font_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string curItem = Font_listBox.SelectedItem.ToString();
            font_show.Text = curItem;
            foreach (FontFamily oneFontFamily in Fonts.SystemFontFamilies)
            {
                if (oneFontFamily.Source == Font_listBox.SelectedItem.ToString())
                {
                    FontFamily_put = oneFontFamily;
                    Test_Block.FontFamily = oneFontFamily;
                }
            }
        }
        private void size_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int curItem = Convert.ToInt32(size_listBox.SelectedValue.ToString());
            Size_show.Text = curItem.ToString();
            size_out = curItem;
            Test_Block.FontSize = curItem;
        }
        private void Bold_BUT_Click(object sender, RoutedEventArgs e)
        {
            Bold = !Bold;
            if (Bold)
            {
                Test_Block.FontWeight = FontWeights.Bold;
            }
            else
            {
                Test_Block.FontWeight = FontWeights.Normal;
            }

        }
        private void Ltalic_BUT_Click(object sender, RoutedEventArgs e)
        {
            Ltalic = !Ltalic;
            if (Ltalic)
            {
                Test_Block.FontStyle = FontStyles.Italic;
            }
            else
            {
                Test_Block.FontStyle = FontStyles.Normal;
            }

        }
        private void Line_BUT_Click(object sender, RoutedEventArgs e)
        {
            Line = !Line;
            if (Line)
            {
                Test_Block.TextDecorations = TextDecorations.Underline;
            }
            else
            {
                Test_Block.TextDecorations = null;
            }
        }

        private void cas_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

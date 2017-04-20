using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace provodnik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog provodnik = new OpenFileDialog();
            provodnik.AddExtension = false;
            provodnik.CheckFileExists = true;
            provodnik.CheckPathExists = true;
            provodnik.Multiselect = false;
            provodnik.ReadOnlyChecked = true;
            provodnik.Title = "Проводник";
            provodnik.Filter = "rtf файлы(*.rtf)|*.rtf|txt файлы(*.txt)|*.txt";
            provodnik.FilterIndex = 1;
            provodnik.ShowDialog();

            FolderBrowserDialog provodnik2 = new FolderBrowserDialog();
            provodnik2.ShowNewFolderButton = true;
            provodnik2.Description = "Выбор деректории";
            provodnik2.ShowDialog();

            File file = new File();
            
        }
    }
}

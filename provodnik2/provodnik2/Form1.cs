using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace provodnik2
{
    public partial class Form1 : Form
    {
        private int iFiles = 0;

        public Form1()
        {
            InitializeComponent();

            treeView1.BeforeSelect += treeView1_BeforeSelect;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            // заполняем дерево дисками
            FillDriveNodes();
        }
        // событие перед раскрытием узла
        void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        // событие перед выделением узла
        void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            AddFiles(e.Node.FullPath.ToString());
        }

        // получаем все диски на компьютере
        private void FillDriveNodes()
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeNode driveNode = new TreeNode { Text = drive.Name };
                    FillTreeNode(driveNode, drive.Name);
                    treeView1.Nodes.Add(driveNode);
                }
            }
            catch (Exception ex) { }
        }
        // получаем дочерние узлы для определенного узла
        private void FillTreeNode(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                }
            }
            catch (Exception ex) { }
        }
        //добавление файлов в listView
        private void AddFiles(string strPath)
        {
            listView1.BeginUpdate();

            listView1.Items.Clear();
            iFiles = 0;
            try
            {
                DirectoryInfo di = new DirectoryInfo(strPath + "\\");
                FileInfo[] theFiles = di.GetFiles();
                foreach (FileInfo theFile in theFiles)
                {
                    iFiles++;
                    System.Windows.Forms.ListViewItem lvItem = new System.Windows.Forms.ListViewItem(theFile.Name);
                    lvItem.SubItems.Add(theFile.Length.ToString());
                    lvItem.SubItems.Add(theFile.LastWriteTime.ToShortDateString());
                    lvItem.SubItems.Add(theFile.LastWriteTime.ToShortTimeString());
                    listView1.Items.Add(lvItem);
                }
            }
            catch (Exception Exc) {}

            listView1.EndUpdate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

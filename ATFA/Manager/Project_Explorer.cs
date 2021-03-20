using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ATFA.Manager
{
    class Project_Explorer
    {
        private static string PATH = "D:/Projets/wpf/ATFA/ATFA/data/";
        public static void PopulateTreeView(TreeViewItem root_tvi)
        {
            DirectoryInfo root = new DirectoryInfo(PATH);

            WalkDirectoryTree(root_tvi, 0, root);
        }

        private static void WalkDirectoryTree(TreeViewItem item, int depth, DirectoryInfo root)
        {
            FileInfo[] files = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    // add node in treeview here
                    Label label = new();
                    label.Content = fi.Name;
                    item.Items.Add(label);
                }

                // Now find all the subdirectories under this directory.
                DirectoryInfo[] subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    TreeViewItem item_n = new();
                    item_n.Header = dirInfo.Name;
                    int index = item.Items.Add(item_n);
                    WalkDirectoryTree((TreeViewItem)item.Items[index], depth+1, dirInfo);
                }
            }
        }
    }
}

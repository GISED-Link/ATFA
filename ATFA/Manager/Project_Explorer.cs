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
        private static string PATH_FB = "D:/Projets/wpf/ATFA/ATFA/data/FB_def";
        private static string PATH_PARAM = "D:/Projets/wpf/ATFA/ATFA/data/FB_param";

        private static List<FB_class.FB_Param_Valve_2_Pos> ParamFiles = new();

       public static List<FB_class.FB_Param_Valve_2_Pos> GetParamList()
        {
            return ParamFiles;
        }

        public static void LoadParamFiles(TreeViewItem root_tvi)
        {
            DirectoryInfo root = new DirectoryInfo(PATH_PARAM);
            WalkDirectoryTree(root_tvi, root);

            GetParamFilesRecursive(ParamFiles, root_tvi);
        }

        public static List<FB_view.View_Valve_2_Pos> GetFB_Files()
        {
            List<FB_view.View_Valve_2_Pos> ret = new();
            // The object TreeViewItem is just too big here. Since the method to walk into dir
            // uses TreeViewItem to store the full tree files, we use it as well
            TreeViewItem root_tvi = new();

            DirectoryInfo root = new(PATH_FB);
            WalkDirectoryTree(root_tvi, root);

            GetFB_FilesRecursive(ret, root_tvi);

            return ret;
        }

        public static void GetParamFilesRecursive(List<FB_class.FB_Param_Valve_2_Pos> param_list, TreeViewItem item_tv)
        {
            foreach (var item in item_tv.Items)
            {
                if (item is Label)
                {
                    var param = FB_class.FB_Param_Valve_2_Pos.Open(((Label)item).Content as string);

                    if (null != param)
                    {
                        param_list.Add((FB_class.FB_Param_Valve_2_Pos)param);
                    }
                    else
                    {
                        Manager.Tool.Log("Unable to create an instance for FB " + ((Label)item).Content);
                    }
                }
                else if (item is TreeViewItem)
                {
                    GetParamFilesRecursive(param_list, item_tv);
                }
                else
                {
                    // We never should be here. Please adapt your code you dumbass
                    throw new InvalidOperationException();
                }
            }
        }

        private static void GetFB_FilesRecursive(List<FB_view.View_Valve_2_Pos> fb_list, TreeViewItem item_tv)
        {
            foreach (var item in item_tv.Items)
            {
                if (item is Label)
                {
                    var fb = FB_view.View_Valve_2_Pos.Open(((Label)item).Content as string);

                    if (null != fb)
                    {
                        fb_list.Add((FB_view.View_Valve_2_Pos)fb);
                    }
                    else
                    {
                        Manager.Tool.Log("Unable to create an instance for FB " + ((Label)item).Content);
                    }
                }
                else if (item is TreeViewItem)
                {
                    GetFB_FilesRecursive(fb_list, item_tv);
                }
                else
                {
                    // We never should be here. Please adapt your code you dumbass
                    throw new InvalidOperationException();
                }
            }
        }


        private static void WalkDirectoryTree(TreeViewItem item, DirectoryInfo root)
        {
            FileInfo[] files = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (Exception e)
            {
                Tool.Log(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
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
                    WalkDirectoryTree((TreeViewItem)item.Items[index], dirInfo);
                }
            }
        }
    }
}

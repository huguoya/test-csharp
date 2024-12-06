using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TestApp
{
    public partial class FrmRename : Form
    {
        public FrmRename()
        {
            InitializeComponent();
        }

        private void FrmRename_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                folderBrowserDialog1.SelectedPath = @"E:\v_files\video\A\";
            }
            folderBrowserDialog1.ShowDialog();
            Ref();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {

            var dir = folderBrowserDialog1.SelectedPath;
            var name = txtName.Text;
            if (string.IsNullOrEmpty(dir) || string.IsNullOrEmpty(name))
            {
                return;
            }

            var di = new DirectoryInfo(dir);

            foreach (var file in di.GetFiles())
            {
                var fileName = file.Name.Replace(name, "").Replace(".", " .").Replace("  ", " ");
                var strs = fileName.Split(' ');
                if (strs.Length > 1 && strs[0].Contains('-'))
                {
                    strs[0] = strs[0].Substring(strs[0].IndexOf(")") + 1) + " " + name + " ";
                    fileName = string.Join(" ", strs).Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Trim();
                    file.MoveTo(dir + "\\" + fileName);
                }
                else if (strs.Length > 2 && strs[1].Contains('-'))
                {
                    strs[0] = strs[1] + " " + name + " ";
                    strs[1] = "";
                    fileName = string.Join(" ", strs).Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Trim();
                    file.MoveTo(dir + "\\" + fileName);
                }
                //if (strs.Length > 1 && strs[1].Contains("-"))
                //{

                //    //strs[0] = strs[1];
                //    //strs[1] = name;
                //    //fileName = string.Join(" ", strs).Replace("  ", " ").Trim();
                //    //file.MoveTo(dir + "\\" + fileName);
                //}
            }
            txtList.Text = string.Empty;
            foreach (var file in di.GetFiles())
            {
                txtList.Text += file.Name + "\r\n";
            }
            lbCount.Text = di.GetFiles().Length.ToString();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            var dirList = Directory.GetDirectories(@"E:\v_files\video\A").OrderBy(q => q).ToArray();
            var dirThis = folderBrowserDialog1.SelectedPath;
            for (int i = 1; i < dirList.Length; i++)
            {
                if (dirList[i] == dirThis)
                {
                    folderBrowserDialog1.SelectedPath = dirList[i - 1];
                    break;
                }
            }
            Ref();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            var dirList = Directory.GetDirectories(@"E:\v_files\video\A").OrderBy(q=>q).ToArray();
            var dirThis = folderBrowserDialog1.SelectedPath;
            for (int i = 0; i < dirList.Length - 1; i++)
            {
                if (dirList[i] == dirThis)
                {
                    folderBrowserDialog1.SelectedPath = dirList[i + 1];
                    break;
                }
            }
            Ref();
        }

        private void Ref()
        {
            var dir = folderBrowserDialog1.SelectedPath;
            txtDir.Text = dir;
            var name = dir.Substring(dir.LastIndexOf('\\') + 1);
            var index = name.IndexOf('(');
            if (index > 0)
            {
                name = name.Substring(index + 1, name.IndexOf(')') - index - 1);
            }
            txtName.Text = name;


            var di = new DirectoryInfo(dir);

            txtList.Text = string.Empty;
            foreach (var file in di.GetFiles())
            {
                txtList.Text += file.Name + "\r\n";
            }
            lbCount.Text = di.GetFiles().Length.ToString();
        }


    }
}

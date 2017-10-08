using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ANN_GUI_SEM5_BINUS
{
    public partial class Form1 : Form
    {
        OpenFileDialog openfile = new OpenFileDialog();
        public List<String> filelist = new List<string>();
        public Form1()
        {
            InitializeComponent();
            this.listView_selectedimage.View = View.LargeIcon;
            this.imagelist_selected.ImageSize = new Size(80, 80);

            this.listView_uploaded.View = View.LargeIcon;
            this.imageList_uploaded.ImageSize = new Size(80, 80);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_browseimages_Click(object sender, EventArgs e)
        {
            filelist.Clear();
            openfile.Multiselect = true;
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach(string filename in openfile.FileNames)
                {
                    try
                    {
                        this.imagelist_selected.Images.Add(Image.FromFile(filename));
                        filelist.Add(filename);
                    }
                    catch
                    {
                        MessageBox.Show("File type must be “jpg”, “jpeg”, or “png”");
                    }
                }
                //    MessageBox.Show(openfile.FileName);
                this.listView_selectedimage.LargeImageList = this.imagelist_selected;
                for (int i = 0; i < this.imagelist_selected.Images.Count;i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = i;
                    this.listView_selectedimage.Items.Add(item);
                }
            }
        }

        private void btn_removeimages_Click(object sender, EventArgs e)
        {
            filelist.Clear();
            listView_selectedimage.Clear();
            imagelist_selected.Dispose();
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (filelist.Count() <= 0)
            {
                MessageBox.Show("select image to upload first");
            }else if (cb_type.SelectedIndex==-1)
            {
                MessageBox.Show("please choose item type first");
                ////////////////////////belum bisa categoriin
                ////////////////////////baru bisa pindahin image dari bawah keatas
            }
            else
            {
                for(int i = 0; i < filelist.Count(); i++)
                {
                    this.imageList_uploaded.Images.Add(Image.FromFile(filelist[i]));
                }
                this.listView_uploaded.LargeImageList = this.imageList_uploaded;
                for (int i = 0; i < this.imageList_uploaded.Images.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = i;
                    this.listView_uploaded.Items.Add(item);
                }
                filelist.Clear();
                listView_selectedimage.Clear();
                imagelist_selected.Dispose();
            }
        }
    }
}

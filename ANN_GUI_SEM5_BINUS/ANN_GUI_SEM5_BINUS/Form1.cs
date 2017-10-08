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
        public Form1()
        {
            InitializeComponent();
            this.listView_selectedimage.View = View.LargeIcon;
            this.imagelist_selected.ImageSize = new Size(80, 80);

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
            openfile.Multiselect = true;
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach(string filename in openfile.FileNames)
                {
                    try
                    {
                        this.imagelist_selected.Images.Add(Image.FromFile(filename));
                        //harus ada list buat capture filename
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
    }
}

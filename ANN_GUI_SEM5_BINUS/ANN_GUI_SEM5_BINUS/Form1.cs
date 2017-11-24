using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Imaging.Filters;

namespace ANN_GUI_SEM5_BINUS
{
    public partial class Form1 : Form
    {
        OpenFileDialog openfile = new OpenFileDialog();
        public List<String> filelist = new List<string>();
        private Bitmap originalImage;
        private Bitmap modifiedImage;
        private List<KeyValuePair<Image, string>> fileCat;
        private List<KeyValuePair<int, string>> fileGroup;
        ///variabel buat kohonen som
        private List<KeyValuePair<Bitmap, string>> dbtrainingdata = new List<KeyValuePair<Bitmap, string>>();
        List<string> dbimageclass = new List<string>();
        som_form som_form1;
        ///end variabel buat kohonen som

        public Form1()
        {
            InitializeComponent();
            this.listView_selectedimage.View = View.LargeIcon;
            this.imagelist_selected.ImageSize = new Size(80, 80);

            this.listView_uploaded.View = View.LargeIcon;
            this.imageList_uploaded.ImageSize = new Size(80, 80);
            fileCat = new List<KeyValuePair<Image, string>>();
            fileGroup = new List<KeyValuePair<int, string>>();
        }

        private Bitmap makeGreyScale(Bitmap image)
        {
            return Grayscale.CommonAlgorithms.RMY.Apply(image);
        }

        private Bitmap threshold(Bitmap img, int thresholdVal)
        {
            return new Threshold(thresholdVal).Apply(img);
        }

        private Bitmap EdgeDetection(Bitmap image)
        {
            return new HomogenityEdgeDetector().Apply(image);
        }

        private Bitmap doResize(Bitmap img, int width, int height)
        {
            return new ResizeBilinear(width, height).Apply(img);
        }

        private void procesImage ()
        {
            modifiedImage = makeGreyScale(originalImage);
            modifiedImage = threshold(modifiedImage, 127);
            modifiedImage = EdgeDetection(modifiedImage);
            modifiedImage = doCrop(modifiedImage);
            modifiedImage = doResize(modifiedImage, 10, 10);
        }

        private Bitmap doCrop(Bitmap img)
        {
            var leftBoundary = img.Width;
            var rightBoundary = 0;
            var upperBoundary = img.Height;
            var lowerBoundary = 0;

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    //RGB sama aja kalo greyscale
                    if (img.GetPixel(j, i).R > 210) //== 255
                    {
                        leftBoundary = Math.Min(leftBoundary, j);
                        rightBoundary = Math.Max(rightBoundary, j);
                        upperBoundary = Math.Min(upperBoundary, i);
                        lowerBoundary = Math.Max(lowerBoundary, i);
                    }
                }
            }

            return img.Clone(new Rectangle(leftBoundary, upperBoundary, rightBoundary - leftBoundary, lowerBoundary - upperBoundary), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }

        private double[] normalizeInput(Bitmap image)
        {
            double[] temp = new double[100];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //formula to get normalize input 
                    temp[i * 10 + j] = image.GetPixel(i, j).R / 255;
                }

            }
            return temp;
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
            openfile.Filter = "Img files(*.jpg,*.jpeg,*.png)|*.jpg;*.jpeg;*.png;";
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
            if(listView_selectedimage.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an Item First");
                return;
            }

            filelist.Clear();
            listView_selectedimage.Clear();
            imagelist_selected.Dispose();
            dbtrainingdata.Clear();
            dbimageclass.Clear();
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (filelist.Count() <= 0)
            {
                MessageBox.Show("select image to upload first");
            }else if (cb_type.SelectedIndex==-1)
            {
                MessageBox.Show("please choose item type first");
            }
            else
            {
                //Clear the imageList and listview Uploaded every upload, and load from key value list
                this.imageList_uploaded.Dispose();
                this.listView_uploaded.Clear();
                this.listView_uploaded.Groups.Clear();

                //Get the user selected group
                string group = cb_type.SelectedItem.ToString();

                //Add the newly added images to key value pair
                for (int i = 0; i < filelist.Count(); i++)
                {
                    this.fileCat.Add(new KeyValuePair<Image, string>(Image.FromFile(filelist[i]),group));
                }

                //Add the imagelist uploaded from the key value array
                foreach (var single in this.fileCat)
                {
                    this.imageList_uploaded.Images.Add(single.Key);
                }

                this.listView_uploaded.LargeImageList = this.imageList_uploaded;

                //Recreate group based on uploaded files
                foreach (var singleGroup in fileGroup)
                {
                    this.listView_uploaded.Groups.Add(new ListViewGroup(singleGroup.Value, HorizontalAlignment.Left));
                    Console.WriteLine($"Image :{ singleGroup }");
                }

                Console.WriteLine("END2");
                Console.WriteLine(this.listView_uploaded.Groups.Count);

                for (int i = 0; i < this.imageList_uploaded.Images.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    bool group_exists = false;

                    //Check if group exists
                    foreach(ListViewGroup existedGroup in this.listView_uploaded.Groups)
                    {
                        if (existedGroup.Header == this.fileCat.ElementAt(i).Value)
                        {
                            // Add to previous group if group existed
                            Console.WriteLine($"{existedGroup.Header}");
                            this.listView_uploaded.Items.Add(item).Group = existedGroup;
                            group_exists = true;
                        }
                    }

                    if(!group_exists)
                    {
                        int theGroup = this.listView_uploaded.Groups.Add(new ListViewGroup(group, HorizontalAlignment.Left));
                        this.listView_uploaded.Items.Add(item).Group = this.listView_uploaded.Groups[theGroup];
                        //Save the new created group
                        this.fileGroup.Add(new KeyValuePair<int, string>(theGroup, group));
                    }

                    //Next Image in the next iteration
                    item.ImageIndex = i;
                }
                ///kode buat db kohone
                foreach (var loadedimage in filelist)
                {
                    var bitmap = new Bitmap(loadedimage);
                    dbtrainingdata.Add(new KeyValuePair<Bitmap, string>(bitmap, loadedimage));
                    dbimageclass.Add(cb_type.SelectedItem.ToString());
                }
                ///end kode buat db kohonen
                filelist.Clear();
                listView_selectedimage.Clear();
                imagelist_selected.Dispose();
            }
        }

        private void btn_searchsimilar_Click(object sender, EventArgs e)
        {
            if (dbtrainingdata.Count < 1)
            {
                MessageBox.Show("Upload some image first");
            }
            else {
                if (som_form1 == null)
                {
                    som_form1 = new som_form(dbtrainingdata, dbimageclass);
                    som_form1.Show();
                }
                else
                {
                    som_form1.Dispose();
                    som_form1 = new som_form(dbtrainingdata, dbimageclass);
                    som_form1.Show();
                }
            }
        }
    }
}

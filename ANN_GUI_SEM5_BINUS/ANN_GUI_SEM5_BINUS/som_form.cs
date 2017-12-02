using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Imaging.Filters;
using Accord.Imaging.Converters;
using Accord.Statistics.Analysis;

namespace ANN_GUI_SEM5_BINUS
{
    public partial class som_form : Form
    {
        private List<KeyValuePair<Bitmap, string>> dbtrainingdata;
        
        List<string> dbclassname;
        static int numberofclasses = 4;
        public som_form(List<KeyValuePair<Bitmap, string>> DBtrainingdata, List<string> DBclassname)
        {
            InitializeComponent();
            dbtrainingdata = DBtrainingdata;    
            dbclassname = DBclassname;
            listView1.LargeImageList = imageList1;
            this.listView1.View = View.LargeIcon;
            this.imageList1.ImageSize = new Size(80, 80);
            listView1.LargeImageList = imageList1;
            //////training pas ngeload form
            train();
        }

        PrincipalComponentAnalysis pca;
        DistanceNetwork dn;
        SOMLearning som;
        Bitmap imagetopredict;
        private List<KeyValuePair<Bitmap, string>>[] classes;
        private void train()
        {
            double[][] trainingData = new double[dbtrainingdata.Count][];
            ImageToArray imagetoarray = new ImageToArray(0,1);
            for (int i = 0; i < dbtrainingdata.Count; i++)
            {
                var image = Form1.preprocess(dbtrainingdata[i].Key);
                double[] output;
                imagetoarray.Convert(image, out output);
                trainingData[i] = output;
            }
            pca = new PrincipalComponentAnalysis();
            pca.Learn(trainingData);
            var pcaresult = pca.Transform(trainingData);
            int numberofepoch = 100000;
            double minimumerror = 0.001;
            int sqrt = (int)Math.Sqrt(dbtrainingdata.Count);
            int numofneuron = (int)(sqrt > 1 ? Math.Pow(sqrt, 2) : numberofclasses);
            dn = new DistanceNetwork(pcaresult[0].Length,numofneuron);
            classes = new List<KeyValuePair<Bitmap, string>>[numofneuron];
            som = new SOMLearning(dn);
            som.LearningRadius = 0;
            for (int i = 0; i < numberofepoch; i++)
            {
                var error = som.RunEpoch(pcaresult);
                if (error < minimumerror) break;
            }
            for (int i = 0; i < numofneuron; i++)
            {
                classes[i] = new List<KeyValuePair<Bitmap, string>>();
            }
            //////////////////////training done
            //////////////////bawah biar pas predict cpet
            for (int i = 0; i < dbtrainingdata.Count; i++)
            {
                var item = dbtrainingdata[i];
                Bitmap tempimage = new Bitmap(item.Key);
                tempimage = Form1.preprocess(tempimage);
                double[] outputimg;
                imagetoarray.Convert(tempimage, out outputimg);
                outputimg = pca.Transform(outputimg);
                dn.Compute(outputimg);
                int currwinner = dn.GetWinner();
                if (!classes[currwinner].Contains(dbtrainingdata[i]))
                {
                    classes[currwinner].Add(dbtrainingdata[i]);
                }
            }
            //////end
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            //clear
            pictureBox1.Image = null;
            imagetopredict = null;
            listView1.Items.Clear();
            imageList1.Images.Clear();
            var ofd1 = new OpenFileDialog();
            ofd1.Filter = "Img files(*.jpg,*.jpeg,*.png)|*.jpg;*.jpeg;*.png;";
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                imagetopredict = new Bitmap(ofd1.FileName);
                pictureBox1.Image = imagetopredict;
            }
            if (imagetopredict != null)
            {
                ImageToArray imagetoarray = new ImageToArray(0, 1);
                //mulai predict data
                //predict input
                Bitmap targetimage = new Bitmap(imagetopredict);
                targetimage = Form1.preprocess(targetimage);
                double[] targetoutput;
                imagetoarray.Convert(targetimage, out targetoutput);
                targetoutput = pca.Transform(targetoutput);
                dn.Compute(targetoutput);
                int winner = dn.GetWinner();
                int j = 0;
                foreach (var item in classes[winner])
                {
                    var name = System.IO.Path.GetFileName(item.Value);
                    imageList1.Images.Add(item.Value, new Bitmap(item.Key));
                    ListViewItem temp = new ListViewItem();
                    temp.ImageIndex = j;
                    this.listView1.Items.Add(temp);
                    j++;
                }
                //int j = 0; ///pakai kode diatas, pas predict speed naik 30-50 detikan per predict :vvvv, kode ini w move ke bagian training
                //for (int i = 0; i < dbtrainingdata.Count; i++)
                //{
                //    var item = dbtrainingdata[i];
                //    Bitmap tempimage = new Bitmap(item.Key);
                //    tempimage = Form1.preprocess(tempimage);
                //    double[] outputimg;
                //    imagetoarray.Convert(tempimage, out outputimg);
                //    outputimg = pca.Transform(outputimg);
                //    dn.Compute(outputimg);
                //    int currwinner = dn.GetWinner();
                //    if (currwinner == winner)
                //    {
                //        var name = System.IO.Path.GetFileName(item.Value);
                //        imageList1.Images.Add(item.Value,new Bitmap(item.Key));
                //        ListViewItem temp = new ListViewItem();
                //        temp.ImageIndex = j;
                //        this.listView1.Items.Add(temp);
                //        j++;
                //    }
                //}
                //for (int i = 0; i < this.imageList1.Images.Count; i++)
                //{
                //    ListViewItem item = new ListViewItem();
                //    item.ImageIndex = i;
                //    this.listView1.Items.Add(item);
                //}
                MessageBox.Show("Find Similar Items Finished");
            }
        }

        private void som_form_Load(object sender, EventArgs e)
        {

        }
    }
}


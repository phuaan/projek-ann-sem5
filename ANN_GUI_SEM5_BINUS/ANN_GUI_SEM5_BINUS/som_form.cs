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
        private Boolean trained = false;
        public som_form(List<KeyValuePair<Bitmap, string>> DBtrainingdata, List<string> DBclassname)
        {
            InitializeComponent();
            dbtrainingdata = DBtrainingdata;
            dbclassname = DBclassname;
            listView1.LargeImageList = imageList1;
            this.listView1.View = View.LargeIcon;
            this.imageList1.ImageSize = new Size(80, 80);
            listView1.LargeImageList = imageList1;
        }
        
        PrincipalComponentAnalysis pca;
        DistanceNetwork dn;
        SOMLearning som;
        Bitmap imagetopredict;
        private void button1_Click(object sender, EventArgs e)
        {
            //clear
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
                double[][] trainingData = new double[dbtrainingdata.Count][];
                ImageToArray imagetoarray = new ImageToArray();
                for (int i = 0; i < dbtrainingdata.Count; i++)
                {
                    var image = Form1.preprocess(dbtrainingdata[i].Key);
                    double[] output;
                    imagetoarray.Convert(image, out output);
                    trainingData[i] = output;
                }
                if (!trained)
                {
                    pca = new PrincipalComponentAnalysis();
                    pca.Learn(trainingData);
                    var pcaresult = pca.Transform(trainingData);
                    int numberofepoch = 100000;
                    double minimumerror = 0.001;
                    dn = new DistanceNetwork(pcaresult[0].Length, numberofclasses);
                    som = new SOMLearning(dn);
                    som.LearningRadius = 0;
                    for (int i = 0; i < numberofepoch; i++)
                    {
                        var error = som.RunEpoch(pcaresult);
                        if (error < minimumerror) break;
                    }
                    //////////////////////training done
                    trained = true;
                }
                //mulai predict data
                //predict input
                Bitmap targetimage = new Bitmap(imagetopredict);
                targetimage = Form1.preprocess(targetimage);
                double[] targetoutput;
                imagetoarray.Convert(targetimage, out targetoutput);
                targetoutput = pca.Transform(targetoutput);
                dn.Compute(targetoutput);
                int winner = dn.GetWinner();

                foreach (var item in dbtrainingdata)
                {
                    Bitmap tempimage = new Bitmap(item.Key);
                    tempimage = Form1.preprocess(tempimage);
                    double[] outputimg;
                    imagetoarray.Convert(tempimage, out outputimg);
                    outputimg = pca.Transform(outputimg);
                    dn.Compute(outputimg);
                    int currwinner = dn.GetWinner();
                    if (currwinner == winner)
                    {
                        var name = System.IO.Path.GetFileName(item.Value);
                        imageList1.Images.Add(new Bitmap(item.Key));
                    }
                }
                for (int i = 0; i < this.imageList1.Images.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = i;
                    this.listView1.Items.Add(item);
                }
                MessageBox.Show("Find Similar Items Finished");
            }
        }
    }
}

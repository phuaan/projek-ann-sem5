using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Accord.Neuro.Learning;
using Accord.Neuro;
using Accord.Imaging.Filters;

namespace ANN_GUI_SEM5_BINUS
{
    public partial class bpnn_form : Form
    {

        ActivationNetwork an = null;
        BackPropagationLearning bpl = null;

        int MAX_EPOCH = 1000000;
        double desiredError = 0.000001;

        List<KeyValuePair<Image, string>> dbtrainingdata;
        List<KeyValuePair<int, string>> dbimageclass;
        string path = "bpn.bin";

        public bpnn_form(List<KeyValuePair<Image, string>> dbtrainingdata, List<KeyValuePair<int, string>> dbimageclass)
        {
            InitializeComponent();

            this.dbtrainingdata = dbtrainingdata;
            this.dbimageclass = dbimageclass;

            if (File.Exists(path))
            {
                an = (ActivationNetwork)ActivationNetwork.Load(path);
            }

            foreach (KeyValuePair<Image, string> item in dbtrainingdata)
            {
                Console.Write(item.Key.ToString());
                Console.Write(":");
                Console.WriteLine(item.Value);
            }
        }

        private double[] normalize(Bitmap image)
        {
            double[] temp = new double[100];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    temp[i * 10 + j] = (double)image.GetPixel(i, j).R / 255;
                }
            }

            return temp;
        }

        private Bitmap crop(Bitmap image)
        {

            var leftBoundary = image.Width;
            var rightBoundary = 0;
            var upperBoundary = image.Height;
            var lowerBoundary = 0;

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if (image.GetPixel(j, i).R > 127)
                    {

                        leftBoundary = Math.Min(leftBoundary, j);
                        rightBoundary = Math.Max(rightBoundary, j);
                        upperBoundary = Math.Min(upperBoundary, i);
                        lowerBoundary = Math.Max(lowerBoundary, i);
                    }
                }
            }

            return image.Clone(new Rectangle(leftBoundary, upperBoundary, rightBoundary - leftBoundary, lowerBoundary - upperBoundary),

                System.Drawing.Imaging.PixelFormat.Format24bppRgb
                );
        }

        private Bitmap preprocess(Bitmap image)
        {
            Bitmap preprocessed = image.Clone(new Rectangle(0, 0, image.Width, image.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            preprocessed = Grayscale.CommonAlgorithms.BT709.Apply(preprocessed);
            preprocessed = new Threshold(100).Apply(preprocessed);
            preprocessed = new HomogenityEdgeDetector().Apply(preprocessed);
            preprocessed = this.crop(preprocessed);
            preprocessed = new ResizeBilinear(10, 10).Apply(preprocessed);

            return preprocessed;
        }

        private int normalizeOutput(double[] output)
        {
            int prediction = (int)Math.Round(output[0]) * dbimageclass.Count;

            return prediction;
        }

        private void trainBtn_Click(object sender, EventArgs e)
        {
            if (an != null && bpl != null)
            {
                var confirmation = MessageBox.Show("Are you sure you want to retrain the classification network?", "Confirmation", MessageBoxButtons.YesNo);

                if (confirmation == DialogResult.No)
                    return;
            }
            else
            {
                an = new ActivationNetwork(new SigmoidFunction(), 100, dbimageclass.Count, 1);
                bpl = new BackPropagationLearning(an);
            }

            List<double[]> input_data = new List<double[]> ();
            List<double[]> output_data = new List<double[]> ();
            double error = 0;

            foreach (KeyValuePair<Image, string> image in dbtrainingdata) {
                input_data.Add(normalize(preprocess((Bitmap)image.Key)));

                foreach (KeyValuePair<int, string> imageCat in dbimageclass)
                {
                    if (imageCat.Value.Equals(image.Value))
                    {
                        double[] temp = new double[1] { imageCat.Key };
                        output_data.Add(temp);
                    }
                }
            }

            double[][] input_training = input_data.ToArray();
            double[][] output_training = output_data.ToArray();

            for (int i = 0; i < MAX_EPOCH; i++)
            {
                error = bpl.RunEpoch(input_training, output_training);
                if (error <= desiredError)
                    break;
            }

            Console.WriteLine(error);

            an.Save(path);

            MessageBox.Show("Computing BPNN Finished");
            predictBtn.Enabled = true;
        }

        private void predictBtn_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Img files(*.jpg,*.jpeg,*.png)|*.jpg;*.jpeg;*.png;";
            Bitmap toBePredicted = null;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    toBePredicted = (Bitmap)Image.FromFile(ofd.FileName);
                    pictureBox1.Image = toBePredicted;
                    
                    double[] predicted = normalize(preprocess(toBePredicted));

                    double[] res = an.Compute(predicted);
                    string prediction = "";

                    Console.WriteLine(res[0]);
                    Console.WriteLine(normalizeOutput(res));

                    foreach (KeyValuePair<int, string> item in dbimageclass)
                    {
                        if ((item.Key+1) == normalizeOutput(res))
                            prediction = item.Value;
                    }

                    predictionBox.Text = prediction;
                    MessageBox.Show("Prediction Finished!");
                }
                catch
                {
                    MessageBox.Show("File type must be “jpg”, “jpeg”, or “png”");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace MonolingualDictionary
{
    public partial class FormMain : Form
    {
        private string text = "";

        public FormMain()
        {
            InitializeComponent();
        }

        SpeechSynthesizer synthesizer;

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbSearch.Text != String.Empty && cbLanguages.GetItemText(cbLanguages.SelectedItem) != "")
                {
                    string jsonFilePath = "";

                    //if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Русский") jsonFilePath = "JSON/russian.json";
                    if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Русский") jsonFilePath = "russian.json";
                    //else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Узбекский") jsonFilePath = "JSON/uzbekskiy.json";
                    else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Узбекский") jsonFilePath = "uzbekskiy.json";
                    //else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Английский") jsonFilePath = "JSON/english.json";
                    else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Английский") jsonFilePath = "english.json";
                    //else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Каракалпакский") jsonFilePath = "JSON/karakalpakskiy.json";
                    else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Каракалпакский") jsonFilePath = "karakalpakskiy.json";

                    bool isFind = false;
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var words = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Words>>(jsonContent);
             
                    foreach (var word in words)
                    {
                        if (word.Word == tbSearch.Text)
                        {
                            listBox1.SelectedItem = word.Word;
                            rtbShowDefine.Text = word.Word + " \n" + word.Description;
                            isFind = true;
                            break;
                        }
                    }
                    if (!isFind)
                    {
                        MessageBox.Show("We haven't got any result!", "Informing message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        rtbShowDefine.Text = "";
                        tbSearch.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("We have nothing to search!","Informing message",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
                }
            }
            catch { }
        }

        private void cbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            string jsonFilePath = "";
            tbSearch.Text = "";
            rtbShowDefine.Text = "";
            listBox1.Items.Clear();
            switch (cbLanguages.GetItemText(cbLanguages.SelectedItem))
            {
                case "Русский":
                    //jsonFilePath = "JSON/russian.json"; break;
                    jsonFilePath = "russian.json"; break;
                case "Английский":
                    //jsonFilePath = "JSON/english.json"; break;
                    jsonFilePath = "english.json"; break;
                case "Каракалпакский":
                    //jsonFilePath = "JSON/karakalpakskiy.json"; break;
                    jsonFilePath = "karakalpakskiy.json"; break;
                case "Узбекский":
                    //jsonFilePath = "JSON/uzbekskiy.json"; break;
                    jsonFilePath = "uzbekskiy.json"; break;
            }
            string jsonContent = File.ReadAllText(jsonFilePath);
            var words = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Words>>(jsonContent);
            var sortedWords = words.OrderBy(elem => elem.Word).ToList();
            foreach (var word in sortedWords)
            {
                listBox1.Items.Add(word.Word);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSearch.Text = listBox1.GetItemText(listBox1.SelectedItem);
            string jsonFilePath = "";

            //if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Русский") jsonFilePath = "JSON/russian.json";
            if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Русский") jsonFilePath = "russian.json";
            //else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Узбекский") jsonFilePath = "JSON/uzbekskiy.json";
            else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Узбекский") jsonFilePath = "uzbekskiy.json";
            //else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Английский") jsonFilePath = "JSON/english.json";
            else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Английский") jsonFilePath = "english.json";
            //else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Каракалпакский") jsonFilePath = "JSON/karakalpakskiy.json";
            else if (cbLanguages.GetItemText(cbLanguages.SelectedItem) == "Каракалпакский") jsonFilePath = "karakalpakskiy.json";

            string jsonContent = File.ReadAllText(jsonFilePath);
            var words = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Words>>(jsonContent);
            foreach (var word in words)
            {
                if (word.Word == tbSearch.Text)
                {
                    rtbShowDefine.Text = word.Word + " \n" + word.Description; 
                    break;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (rtbShowDefine.Text != String.Empty)
            {
                text = rtbShowDefine.Text;
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += PrintPageHandler;
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDialog.Document.Print();
                }
            }
        }

        void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(text,new Font("Century Ghotic", 14), Brushes.Black, 0, 0);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (rtbShowDefine.Text != "")
                MessageBox.Show(rtbShowDefine.Text, "Description message", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SpeakAsync(rtbShowDefine.Text);
        }
    }
}
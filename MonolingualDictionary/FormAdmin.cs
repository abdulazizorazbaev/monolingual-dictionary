using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MonolingualDictionary
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbLanguages.SelectedItem != null && tbWord.Text != "" && rtbDescription.Text != "")
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

                    bool isTrue = false;
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var words = JsonConvert.DeserializeObject<List<Words>>(jsonContent);
                    foreach (var word in words)
                    {
                        if (tbWord.Text == word.Word)
                        {
                            isTrue = true;
                        }
                    }
                    if (!isTrue)
                    {
                        Words word = new Words();
                        word.Word = tbWord.Text;
                        word.Description = rtbDescription.Text;
                        words.Add(word);
                        string jsonAddContent = JsonConvert.SerializeObject(words);
                        File.WriteAllText(jsonFilePath, jsonAddContent);
                        MessageBox.Show("The new word has been added successfully!", "Informing message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        rtbDescription.Text = "";
                        tbWord.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("The word exists in our system!", "Informing message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        rtbDescription.Text = "";
                        tbWord.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Please, fill all the fields!", "Informing message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbLanguages.SelectedItem != null && tbWord.Text != "")
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

                    bool isTrue = false;
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var words = JsonConvert.DeserializeObject<List<Words>>(jsonContent);
                    foreach (var word in words)
                    {
                        if (tbWord.Text == word.Word)
                        {
                            words.Remove(word);
                            string jsonDeleteContent = JsonConvert.SerializeObject(words);
                            File.WriteAllText(jsonFilePath, jsonDeleteContent);
                            MessageBox.Show("The word has been removed successfully!", "Informing message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            rtbDescription.Text = "";
                            tbWord.Text = "";
                            isTrue = true;
                        }
                    }
                    if (!isTrue)
                    {
                        MessageBox.Show("The word doesn't exist in our system!", "Informing message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show("Please, fill all the fields!", "Informing message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch { }
        }
    }
}

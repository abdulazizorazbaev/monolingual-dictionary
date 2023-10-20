using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MonolingualDictionary
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlRegister.Height = pnl.Height;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlRegister.Height = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbUsernameSU.Text != "" && tbEmail.Text != "" && tbPasswordSU.Text != "" && tbConfirmPasswordSU.Text != "")
                {
                    //string jsonFilePath = "JSON/users.json";
                    string jsonFilePath = "users.json";
                    string jsonReadContent = File.ReadAllText(jsonFilePath);
                    var users = JsonConvert.DeserializeObject<List<Users>>(jsonReadContent);
                    if (users.Count == 0 && tbUsernameSU.Text != "")
                    {
                        Users user = new Users();
                        user.Username = tbUsernameSU.Text;
                        user.Email = tbEmail.Text;
                        user.Password = tbPasswordSU.Text;
                        user.ConfirmPassword = tbConfirmPasswordSU.Text;

                        users.Add(user);

                        string jsonContent = JsonConvert.SerializeObject(users);
                        File.WriteAllText(jsonFilePath, jsonContent);
                        MessageBox.Show("You've successfully registered!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else if (users.Count >= 1)
                    {
                        bool isCheck = true;
                        foreach (var item in users)
                        {
                            if (item.Username == tbUsernameSU.Text && tbUsernameSU.Text != "")
                            {
                                isCheck = false;    
                            }
                        }
                        if (!isCheck)
                        {
                            DialogResult result = MessageBox.Show("This username has already exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            if (result == DialogResult.OK)
                            {
                                tbUsernameSU.Text = "";
                                tbEmail.Text = "";
                                tbPasswordSU.Text = "";
                                tbConfirmPasswordSU.Text = "";
                            }
                        }
                        else
                        {
                            Users user = new Users();
                            user.Username = tbUsernameSU.Text;
                            user.Email = tbEmail.Text;
                            user.Password = tbPasswordSU.Text;
                            user.ConfirmPassword = tbConfirmPasswordSU.Text;
                            if (user.Email.Contains("@"))
                            {
                                users.Add(user);
                                string jsonContent = JsonConvert.SerializeObject(users);
                                File.WriteAllText(jsonFilePath, jsonContent);
                                MessageBox.Show("You've successfully registered!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            }
                            else
                            {
                                MessageBox.Show("Please, fill all the fields correctly!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                tbUsernameSU.Text = "";
                                tbEmail.Text = "";
                                tbPasswordSU.Text = "";
                                tbConfirmPasswordSU.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please, fill all the fields!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch { }
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            pnlRegister.Height = 0;
        }

        private void chbPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPassword.Checked)
                tbPassword.PasswordChar = '\0';
            else
                tbPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLoginAdmin.Hide();
            try
            {
                //string jsonFilePath = "JSON/users.json";
                string jsonFilePath = "users.json";
                string jsonReadContent = File.ReadAllText(jsonFilePath);
                var users = JsonConvert.DeserializeObject<List<Users>>(jsonReadContent);
                bool isMatch = false;
                if (users.Count >= 1)
                {
                    foreach (var item in users)
                    {
                        if (item.Username == tbUsername.Text && tbUsername.Text != "" && item.Password == tbPassword.Text && tbPassword.Text != "")
                        {
                            if (tbUsername.Text.Length >= 8 && tbUsername.Text.Length <= 16 && tbPassword.Text != String.Empty)
                            {
                                isMatch = true;
                                this.Close();
                            }
                        }
                    }
                    if (!isMatch)
                    {
                        DialogResult result = MessageBox.Show("The username or passoword doesn't match!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.OK)
                        {
                            tbUsername.Text = String.Empty;
                            tbPassword.Text = String.Empty;
                        }
                    }
                }
            }
            catch { }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pnlChoose.Height = 443;
            pnl.Height = 0;
            pnlMain.Height = 0;
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlMain.Height = 440;
            pnl.Height = 440;
            linkRegister.Show();
            pnlChoose.Height = 0;
            btnLoginAdmin.Hide();
            label3.Text = "Don't have an Account ?";
            btnLogin.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlMain.Height = 440;
            pnl.Height = 440;
            pnlChoose.Height = 0;
            linkRegister.Hide();
            label3.Text = String.Empty;
            btnLogin.Hide();
            btnLoginAdmin.Show();
        }
        private bool CheckForAdmin(string username, string password)
        {
            if (tbUsername.Text == "Jake1976" && tbPassword.Text == "12345")
            {
                return true;
            }
            return false;
        }

        private void btnLoginAdmin_Click(object sender, EventArgs e)
        {
            if (CheckForAdmin(tbUsername.Text, tbPassword.Text))
            {
                FormAdmin formAdmin = new FormAdmin();
                this.Hide();
                formAdmin.ShowDialog();
            }
            else
            {
                DialogResult result = MessageBox.Show("The username or passoword doesn't match!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    tbUsername.Text = String.Empty;
                    tbPassword.Text = String.Empty;
                }
            }
        }
    }
}

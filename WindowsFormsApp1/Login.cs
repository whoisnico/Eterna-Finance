using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace WindowsFormsApp1
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public partial class Login : Form
    {
        private const string JsonFilePath = "userdata.json"; // JSON-Speicherort

        public Login()
        {
            InitializeComponent();

            // Username Box
            userTextBox.Text = "Enter your username...";
            userTextBox.ForeColor = Color.Gray;

            // Event-Handler verbinden
            userTextBox.Enter += userTextBox_Enter;
            userTextBox.Leave += userTextBox_Leave;

            // Password Box
            passwordTextBox.Text = "Enter your password...";
            passwordTextBox.ForeColor = Color.Gray;
            passwordTextBox.UseSystemPasswordChar = false;

            // Event-Handler verbinden
            passwordTextBox.Enter += passwordTextBox_Enter;
            passwordTextBox.Leave += passwordTextBox_Leave;

            // Login-Button Event verbinden
            loginButton.Click += loginButton_Click;
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void userTextBox_Enter(object sender, EventArgs e)
        {
            if (userTextBox.Text == "Enter your username...")
            {
                userTextBox.Text = "";
                userTextBox.ForeColor = Color.Black;
            }
        }

        private void userTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userTextBox.Text))
            {
                userTextBox.Text = "Enter your username...";
                userTextBox.ForeColor = Color.Gray;
            }
        }

        public void SetUserText(string text)
        {
            userTextBox.Text = text;
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "Enter your password...")
            {
                passwordTextBox.Text = "";
                passwordTextBox.ForeColor = Color.Black;
                passwordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                passwordTextBox.Text = "Enter your password...";
                passwordTextBox.ForeColor = Color.Gray;
                passwordTextBox.UseSystemPasswordChar = false;
            }
        }

        public void SetPasswordText(string text)
        {
            passwordTextBox.Text = text;
        }

        public event EventHandler LoginButtonClicked;

        private void loginButton_Click(object sender, EventArgs e)
        {
            // Überprüfung der Eingaben
            if (string.IsNullOrWhiteSpace(userTextBox.Text) || userTextBox.Text == "Enter your username...")
            {
                MessageBox.Show("Please enter your user name.", "Missing input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text) || passwordTextBox.Text == "Enter your password...")
            {
                MessageBox.Show("Please enter your password.", "Missing entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Benutzerobjekt erstellen
            var userData = new User
            {
                Username = userTextBox.Text,
                Password = passwordTextBox.Text, // Fix: Korrekte Zuweisung
                LastLogin = DateTime.Now
            };

            // Benutzer speichern
            SaveUserToJson(userData);

            MessageBox.Show($"Successfully logged in as {userData.Username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Falls ein Event-Listener existiert, wird dieser ausgelöst
            LoginButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void SaveUserToJson(User user)
        {
            List<User> users = new List<User>();

            // Falls die Datei existiert, laden wir die vorhandenen Daten
            if (File.Exists(JsonFilePath))
            {
                string existingJson = File.ReadAllText(JsonFilePath);
                users = JsonSerializer.Deserialize<List<User>>(existingJson) ?? new List<User>();
            }

            // Neuen Benutzer hinzufügen
            users.Add(user);

            // JSON speichern
            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFilePath, jsonString);
        }
    }
}

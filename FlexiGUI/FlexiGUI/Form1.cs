using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace FlexiGUI
{
    public partial class Form1 : Form
    {
        private HubConnection _connection;
        public Form1()
        {
            InitializeComponent();
        }



        private async void Form1_Load(object sender, EventArgs e)
        {

            _connection = new HubConnectionBuilder()
           .WithUrl("http://localhost:5161/chatHub") // URL of your SignalR hub
           .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                // Handle incoming message
                MessageBox.Show($"Received message from {user}: {message}");
            });


            try
            {
                await _connection.StartAsync(); // Start the connection
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnSend_Click(object sender, EventArgs e)
        {

            string user = txtUser.Text;
            string message = txtMessage.Text;

            // Call server method
            await _connection.SendAsync("SendMessageMainFunction", user, message);


        }
    }
}

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
                MessageBox.Show($" Sender =>: {user} \n\n Message =>: {message}");
            });


            _connection.On<List<string>>("ConnectionList", (connectionIds) =>
            {
                UpdateConnectionList(connectionIds);
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

        private void UpdateConnectionList(List<string> connectionIds)
        {
            if (connectionIdComboBox.InvokeRequired)
            {
                connectionIdComboBox.Invoke(new Action<List<string>>(UpdateConnectionList), new object[] { connectionIds });
                return;
            }

            // Clear existing items
            connectionIdComboBox.Items.Clear();

            // Add new items
            foreach (var connectionId in connectionIds)
            {
                connectionIdComboBox.Items.Add(connectionId);
            }
        }




        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnSend_Click(object sender, EventArgs e)
{
    try
    {
        string user = txtUser.Text;
        string message = txtMessage.Text;
        string selectedConnectionId = connectionIdComboBox.SelectedItem?.ToString(); // Get the selected connection ID from the ComboBox

        // Check if a connection ID is selected
        if (string.IsNullOrWhiteSpace(selectedConnectionId))
        {
            MessageBox.Show("Select a connection Id");
            return;
        }

        // Call server method with the selected connection ID
        await _connection.SendAsync("SendMessageToConnectionId", selectedConnectionId, user, message);
    }
    catch (Exception ex)
    {
        // Handle any exceptions here
        MessageBox.Show($"An error occurred: {ex.Message}");
    }
}




    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WordFilteringTryIt
{
    
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            // added text for required documentation A B C
            descFun.Text = "Part of required services, enter in string of text and it will filter out hard coded words that were given";
            serviceURL.Text = "Swagger URL: https://localhost:7046/swagger/index.html , runs off of: https://localhost:7046/api/wordfilter/filter ";
            methodOP.Text = "FilterTextAsync(string) , pass in text and outputs a string of filtered text";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;
            if (string.IsNullOrEmpty(inputText)) //check if box is empty 
            {
                MessageBox.Show("please enter text.");
                return;
            }

            string filteredText = await FilterTextAsync(inputText); //send in our input text to get filtered 
            if(filteredText != null)
            {
                label1.Text = filteredText; //print filtered text to our label
            }
            else
            {
                MessageBox.Show("Failed to find filtering service to filter text. Please check that it's running.");
            }

        }

        private async Task<string> FilterTextAsync(string text)
        {
            string apiURL = "https://localhost:7012/api/wordfilter/filter"; //api URL for our filtering service

            var body = new {Text = text}; 
            string jsonContent = JsonConvert.SerializeObject(body); //convert to Json object 
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"); //create the HTTP request content 

            try
            {
                HttpResponseMessage response = await client.PostAsync(apiURL, content); //send the request to our API URL w/ our text to filter 
                response.EnsureSuccessStatusCode(); //throw exception if theres an error (like 400 or sum ) 
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
        }

        private void serviceURL_Click(object sender, EventArgs e) //ignore this, i accidentally double clicked one of the things and I'm too lazy to remove it.
        {

        }
    }
}

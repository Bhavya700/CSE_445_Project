using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace CoinGeckoService
{
    public partial class Form1 : Form
    {

        private readonly HttpClient client = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true //ensure theres no issues with certification on Http
        });

        public Form1()
        {
            InitializeComponent();
            //add comments for the required documentation A B C
            serviceDesc.Text = "Takes in a Crypto name and currency (just enter usd) and saves a .json file with info on that coin";
            serviceURL.Text = "URL of Swagger: https://localhost:7012/swagger/index.html URL of request: https://localhost:7012/api/Crypto/price?cryptoId=bitcoin&currency=usd";
            methodOpReturn.Text = "Method: GetCryptoPrice(string , string), Returns .json file in curr. directory";
        }


        private async void pressToFetch_Click(object sender, EventArgs e)
        {
            string coinName = txtCoinName.Text;
            string currency = txtCurrency.Text;

            var result = await GetCryptoPrice(coinName, currency); //call for crypto price passing in variables from text box 

            if (result != null) //if reult is not null from call/service, save to json file
            {
                string jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented); //json result formatted with indents (otherwise it's just a long line)

                // Save the file in the project directory
                string projectPath = AppDomain.CurrentDomain.BaseDirectory; //get the project path of current computer (usually hides in debug folder but i can't hardcode a path so just working with it 
                string filePath = Path.Combine(projectPath, "crypto_price.json"); //add our json file to that path 


                File.WriteAllText(filePath, jsonResult); //write the result into our path
                MessageBox.Show($"Price data saved to: {filePath}"); //display the path where file was saved so consumer can go to location
            }
            else
            {
                MessageBox.Show("Failed to fetch data from the API.");
            }
        }


        public async Task<string> GetCryptoPrice(string coinName, string currency)
        {
            try
            {
                string apiUrl = $"https://localhost:7012/api/Crypto/price?cryptoId={coinName}&currency={currency}"; //api URL to our service, passing in requirements. Our RESTful service will handle the authentication and all that
                HttpResponseMessage response = await client.GetAsync(apiUrl); //get the response from the url
                response.EnsureSuccessStatusCode(); //throw error on response if error

                string jsonResponse = await response.Content.ReadAsStringAsync(); //initialize a variable with the response to return
                return jsonResponse; // Return raw json string (which will be formatted upon return)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
        }

    
    }
}

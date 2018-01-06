/*
 * Segundo Trabalho pratico de Integração de Sistemas de Informação (ESI)
 * 
 * Rúben Guimarães - nº11156 - a11156@alunos.ipca.pt
 * Engenharia de Sistemas Informaticos
 * 
 * Professor Luis Ferreira
 * 
 * Cliente
 * 
 */

using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Windows.Controls;

namespace Trabalho2_ISI_Cliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class JanelaPrincipal : Window
    {
        public JanelaPrincipal()
        {
            InitializeComponent();


        }



        #region Botoes


        /// <summary>
        /// Método que vai consultar uma API externa para saber qual o IP da minha ligação a internet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotaoGetIP(object sender, RoutedEventArgs e)
        {
            #region Variaveis

            string url;
            string enderecoIP, enderecoIPCorrigido;
            WebClient webClient = new WebClient();

            #endregion


            url = "http://localhost:6418/Service.svc/MyIp";

            // Recebe o endereço
            enderecoIP = webClient.DownloadString(url);

            // retira as aspas do ip
            enderecoIPCorrigido = enderecoIP.Substring(1, enderecoIP.Length - 2);

            // prenche o ip
            TextBoxIP.Text = enderecoIPCorrigido;
        }


        /// <summary>
        /// Método que vai consultar uma API externa e saber informação o ip que foi prenchido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotaoGetIPInfo(object sender, RoutedEventArgs e)
        {
            #region Variaveis

            HttpWebRequest request;
            StringBuilder uri;
            string url;
            string enderecoIP;
            EnderecoIPInfo respostaJson;
            object respostaObjecto;
            DataContractJsonSerializer jsonSerializer;

            #endregion


            // Alterar quando publicar o serviço
            url = "http://localhost:6418/Service.svc/GetIPInfo/";
            enderecoIP = TextBoxIP.Text;


            if (enderecoIP == "")
            {
                MessageBox.Show("Tem que prencher um endereço IP!", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Construi URI
            uri = new StringBuilder();
            uri.Append(url);
            uri.Append(HttpUtility.UrlEncode(enderecoIP));



            // Prepara e envia pedido
            request = WebRequest.Create(uri.ToString()) as HttpWebRequest;


            // Analisa a resposta
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                //Serializa de JSON para Objecto
                jsonSerializer = new DataContractJsonSerializer(typeof(EnderecoIPInfo));
                respostaObjecto = jsonSerializer.ReadObject(response.GetResponseStream());
                respostaJson = (EnderecoIPInfo)respostaObjecto;
            }
            ListView listView = new ListView();

         

            ApresentaJson.ItemsSource = (IEnumerable)respostaJson;

            /*
            #region Salvaguarda dados 

            // Guarda apenas os campos necessarios
            respotaLimpa.ip = respostaJson.ip;
            respotaLimpa.city = respostaJson.city;
            respotaLimpa.region = respostaJson.region;
            respotaLimpa.country = respostaJson.country;
            respotaLimpa.country_name = respostaJson.country_name;
            respotaLimpa.postal = respostaJson.postal;
            respotaLimpa.latitude = respostaJson.latitude;
            respotaLimpa.longitude = respostaJson.longitude;
            respotaLimpa.timezone = respostaJson.timezone;
            respotaLimpa.org = respostaJson.org;

            #endregion
            */

        }

        #endregion


    }

    #region Aux

    public class EnderecoIPInfo
    {
        public string city { get; set; }
        public string country { get; set; }
        public string country_name { get; set; }
        public string ip { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
        public string region { get; set; }
        public string timezone { get; set; }
    }

    #endregion
}

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


            #region Mostra os dados

   
            CidadeIP.Text = respostaJson.city;
            PaisIndicativoIP.Text = respostaJson.city;
            PaisIndicativoIP.Text = respostaJson.region;
            PaisIndicativoIP.Text = respostaJson.country;
            PaisIP.Text = respostaJson.country_name;
            CodigoPostalIP.Text = respostaJson.postal;
            Latitude.Text = respostaJson.latitude.ToString();
            Longitude.Text = respostaJson.longitude.ToString();
            TimezoneIP.Text = respostaJson.timezone;
            ISP.Text = respostaJson.org;
            RegiaoIP.Text = respostaJson.region;

            #endregion
            

        }


        #endregion

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBoxIP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
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

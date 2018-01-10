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
using System.IO;
using System.Collections.Generic;

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


            url = "http://wcfrest20180109101801.azurewebsites.net/Service.svc/MyIp";

            // Recebe o endereço
            enderecoIP = webClient.DownloadString(url);

            // retira as aspas do ip
            enderecoIPCorrigido = enderecoIP.Substring(1, enderecoIP.Length - 2);

            // prenche o ip
            TextBoxIP.Text = enderecoIPCorrigido;
        }


        /// <summary>
        /// Método que vai consultar uma API externa e saber informação do ip que foi prenchido
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
            url = "http://wcfrest20180109101801.azurewebsites.net//Service.svc/GetIPInfo/";
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


        /// <summary>
        /// Método que vai enviar um tweet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotaoEnviaTweet(object sender, RoutedEventArgs e)
        {
            #region Variaveis

            string mensagem;
            DataContractJsonSerializer jsonSerializer;
            MemoryStream memoria;
            WebClient webClient;
            string uriServico;
            string objectoSerializadoNumaString;

            #endregion

            // Recebe o texto da textbox
            mensagem = MessagemTweet.Text;

            // Inicializa o serialziador e stream de memoria
            jsonSerializer = new DataContractJsonSerializer(typeof(string));
            memoria = new MemoryStream();
            webClient = new WebClient();
            uriServico = "http://wcfrest20180109101801.azurewebsites.net//Service.svc/Tweet"; // atualizar quando publicar 


            // Converte o objecto para uma string data (já está pronto para o envio de objectos)
            jsonSerializer.WriteObject(memoria, mensagem);
            objectoSerializadoNumaString = Encoding.UTF8.GetString(memoria.ToArray(), 0, (int)memoria.Length);


            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString(uriServico, "POST", objectoSerializadoNumaString);

        }


        /// <summary>
        /// Botão que vai atualizar o browser da APP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotaoMostraBrowser(object sender, RoutedEventArgs e)
        {
            string urlTwitter = "https://twitter.com/trabalhoisi";
            System.Uri uri = new Uri(urlTwitter);

            Browser.Source = uri;
        }


        /// <summary>
        /// Botão que vai consultar a informação meteoriologica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotaoInformacaoMeteo(object sender, RoutedEventArgs e)
        {
            #region Variaveis

            HttpWebRequest request;
            StringBuilder uri;
            string url;
            string cidade;
            Tempo respostaJson;
            object respostaObjecto;
            DataContractJsonSerializer jsonSerializer;

            #endregion


            // Alterar quando publicar o serviço
            url = "http://wcfrest20180109101801.azurewebsites.net/Service.svc/Weather/";

            cidade = NomeCidadeWeather.Text;



            if (cidade == "")
            {
                MessageBox.Show("Tem que prencher uma Cidade!", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Construi URI
            uri = new StringBuilder();
            uri.Append(url);
            uri.Append(HttpUtility.UrlEncode(cidade));



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
                jsonSerializer = new DataContractJsonSerializer(typeof(Tempo));
                respostaObjecto = jsonSerializer.ReadObject(response.GetResponseStream());
                respostaJson = (Tempo)respostaObjecto;
            }


            #region Mostra os dados

            CidadeWeather.Text = cidade;
            PaisWeather.Text = respostaJson.sys.country;
            DescricaoWeather.Text = respostaJson.weather[0].description;
            Temperatura.Text = respostaJson.main.temp.ToString();
            HumidadeWeather.Text = respostaJson.main.humidity.ToString();
            VentoWeather.Text = respostaJson.wind.speed.ToString();
            PressaoWeather.Text = respostaJson.main.pressure.ToString();

            #endregion
        }


        /// <summary>
        /// Botão que envia um tweet com informação do tempo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnviaTweet(object sender, RoutedEventArgs e)
        {
            #region Variaveis

            string mensagem;
            DataContractJsonSerializer jsonSerializer;
            MemoryStream memoria;
            WebClient webClient;
            string uriServico;
            string objectoSerializadoNumaString;

            #endregion

            if (CidadeWeather.Text == "" && PaisWeather.Text == "")
            {
                MessageBox.Show("Tem receber a informação primeiro!", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            mensagem = ("Tempo atual de " + CidadeWeather.Text.ToString() + ": " + Temperatura.Text + "°C " + HumidadeWeather.Text + "% " + VentoWeather.Text + "m/s " + PressaoWeather.Text + "hPa"); 


            // Inicializa o serialziador e stream de memoria
            jsonSerializer = new DataContractJsonSerializer(typeof(string));
            memoria = new MemoryStream();
            webClient = new WebClient();
            uriServico = "http://wcfrest20180109101801.azurewebsites.net/Service.svc/Tweet"; // atualizar quando publicar 


            // Converte o objecto para uma string data (já está pronto para o envio de objectos)
            jsonSerializer.WriteObject(memoria, mensagem);
            objectoSerializadoNumaString = Encoding.UTF8.GetString(memoria.ToArray(), 0, (int)memoria.Length);


            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString(uriServico, "POST", objectoSerializadoNumaString);
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



    #region Objectos da Weather API





    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Tempo
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    #endregion


    #endregion
}

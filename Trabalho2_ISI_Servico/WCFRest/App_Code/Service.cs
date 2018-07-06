/* Segundo Trabalho pratico de Integração de Sistemas de Informação (ESI)
 * Professor Luis Ferreira
 * 
 * Rúben Guimarães - nº11156 - a11156@alunos.ipca.pt
 * Engenharia de Sistemas Informaticos
 * 
 *       Implementação do serviço
 * 
 */

using System;
using System.Text;
using System.Net;
using System.Web;
using System.Runtime.Serialization.Json;
using TinyTwitter;



/// <summary>
/// Classe que implenta o serviço
/// </summary>
public class Service : IService
{

    #region Aux

    // substituir apiKey pelas chaves correspondentes
    private string consumerKey = "apiKey";
    private string consumerKeySecret = "apiKey";
    private string accessToken = "apiKey";
    private string accessTokenSecret = "apiKey";
    private string weatherKey = "apiKey";

    #endregion


    /// <summary>
    /// Método que consulta uma api externa (https://ipapi.co/) recebe os dados em json sobre o ip e por fim devolve em json apenas os dados necessarios
    /// </summary>
    /// <param name="ip"> Endereço ip </param>
    /// <returns> informação sobre o endereço ip </returns>
    public InformacaoIPLimpa GetIPInfo(string ip)
    {
        #region Variaveis

        HttpWebRequest request;
        StringBuilder uri;
        string url;
        string formatoDados;
        string enderecoIP = ip;
        InformacaoIPBruta respostaJson;
        InformacaoIPLimpa respotaLimpa = new InformacaoIPLimpa();
        object respostaObjecto;
        DataContractJsonSerializer jsonSerializer;

        #endregion


        url = "https://ipapi.co/";
        formatoDados = "/json/";


        // Construi URI
        uri = new StringBuilder();
        uri.Append(url);
        uri.Append(HttpUtility.UrlEncode(enderecoIP));
        uri.Append(HttpUtility.UrlEncode(formatoDados));


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
            jsonSerializer = new DataContractJsonSerializer(typeof(InformacaoIPBruta));
            respostaObjecto = jsonSerializer.ReadObject(response.GetResponseStream());
            respostaJson = (InformacaoIPBruta)respostaObjecto;
        }

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

        return respotaLimpa;

    }


    /// <summary>
    /// Método que consulta uma api externa (https://www.ipify.org/) e recebe o nosso endereço ip em texto
    /// </summary>
    /// <returns></returns>
    public string GetMyIp()
    {
        #region Variaveis

        string url;
        string publicIp;
        WebClient webClient = new WebClient();

        #endregion


        url = "https://api.ipify.org";

        // Recebe o endereço
        publicIp = webClient.DownloadString(url);


        return publicIp;
    }


    /// <summary>
    /// Método que recebe um mensagem do cliente e faz um tweet dessa mensagem
    /// </summary>
    /// <param name="mensagem"> Texto da mensagem </param>
    /// <returns>Bool a indicar o sucesso da operação</returns>
    public bool Tweet(string mensagem)
    {

        // Atribui a informação necessaria para o OAuth
        var oauth = new OAuthInfo
        {
            AccessToken = accessToken,
            AccessSecret = accessTokenSecret,
            ConsumerKey = consumerKey,
            ConsumerSecret = consumerKeySecret
        };

        try
        {
            // Utiliza o protocolo oauth para se ligar a api do twitter
            var twitter = new TinyTwitter.TinyTwitter(oauth);

            // Envia o Tweet
            twitter.UpdateStatus(mensagem);


        }
        catch (Exception e)
        {
            return false;
            throw e;
        }



        return true;
    }

    
    /// <summary>
    /// Método que vai consultar a informação do tempo de uma dada cidade
    /// </summary>
    /// <param name="nomeCidade">nome da cidade</param>
    /// <returns>Objecto com a informação do tempo</returns>
    public Tempo GetWeatherInfo(string nomeCidade)
    {

        #region Variaveis

        HttpWebRequest request;
        StringBuilder uri;
        string url;
        string parametrosUri;
        Tempo respostaJson;
        object respostaObjecto;
        DataContractJsonSerializer jsonSerializer;

        #endregion

        parametrosUri = "&mode=json&units=metric&APPID=";
        url = "http://api.openweathermap.org/data/2.5/weather?q=" + nomeCidade + parametrosUri + weatherKey;

     


        // Construi URI
        uri = new StringBuilder();
        uri.Append(url);


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


        return respostaJson;

    }
}






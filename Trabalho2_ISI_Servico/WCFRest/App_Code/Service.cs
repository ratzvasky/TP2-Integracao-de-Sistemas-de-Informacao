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


/// <summary>
/// Classe que implenta o serviço
/// </summary>
public class Service : IService
{


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


}

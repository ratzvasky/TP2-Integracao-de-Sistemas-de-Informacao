/* Segundo Trabalho pratico de Integração de Sistemas de Informação (ESI)
 * Professor Luis Ferreira
 * 
 * Rúben Guimarães - nº11156 - a11156@alunos.ipca.pt
 * Engenharia de Sistemas Informaticos
 * 

 *      Interface do serviço
 * 
 */

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

[ServiceContract]
public interface IService
{


    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetIPInfo/{enderecoIP}")]
    InformacaoIPLimpa GetIPInfo(string enderecoIP);



    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MyIp")]
    string GetMyIp();





}


/// <summary>
/// Objecto que vai receber o json da API
/// </summary>
[DataContract]
public class InformacaoIPBruta
{
    [DataMember(Name = "ip")]
    public string ip { get; set; }

    [DataMember(Name = "city")]
    public string city { get; set; }

    [DataMember(Name = "region")]
    public string region { get; set; }

    [DataMember(Name = "region_code")]
    public string region_code { get; set; }

    [DataMember(Name = "country")]
    public string country { get; set; }

    [DataMember(Name = "country_name")]
    public string country_name { get; set; }

    [DataMember(Name = "postal")]
    public string postal { get; set; }

    [DataMember(Name = "latitude")]
    public double latitude { get; set; }

    [DataMember(Name = "longitude")]
    public double longitude { get; set; }

    [DataMember(Name = "timezone")]
    public string timezone { get; set; }

    [DataMember(Name = "asn")]
    public string asn { get; set; }

    [DataMember(Name = "org")]
    public string org { get; set; }
}


/// <summary>
/// Objecto que vai receber o json da API
/// </summary>
[DataContract]
public class InformacaoIPLimpa
{
    [DataMember(Name = "ip")]
    public string ip { get; set; }

    [DataMember(Name = "city")]
    public string city { get; set; }

    [DataMember(Name = "region")]
    public string region { get; set; }

    [DataMember(Name = "country")]
    public string country { get; set; }

    [DataMember(Name = "country_name")]
    public string country_name { get; set; }

    [DataMember(Name = "postal")]
    public string postal { get; set; }

    [DataMember(Name = "latitude")]
    public double latitude { get; set; }

    [DataMember(Name = "longitude")]
    public double longitude { get; set; }

    [DataMember(Name = "timezone")]
    public string timezone { get; set; }

    [DataMember(Name = "org")]
    public string org { get; set; }
}



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


    [OperationContract]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Tweet")]
    bool Tweet(string message);


    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Weather/{nomeCidade}")]
    Tempo GetWeatherInfo(string nomeCidade);


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

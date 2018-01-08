using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DT = System.Data;
using QC = System.Data.SqlClient;


/// <summary>
/// Summary description for Class1
/// </summary>

public class LigacaoDB
{
    /*
    #region Estado






    #endregion


    #region Operadores



    #endregion


    #region Outros

    /*
    static public string Connection()
    {
        /* using (var connection = new QC.SqlConnection(
                         "Server=tcp:trabalhoisi.database.windows.net,1433;" +
                         "Database=Trabalho_ISI_BD;" +
                         "UserID=a11156;" +
                         "Password=lufer.2017.isi;" +
                         "Encrypt=True;" +
                         "TrustServerCertificate=False;" +
                         "Connection Timeout=30;"
                         ))
                         
        //   connection.Open();


        using (var connection = new QC.SqlConnection("Server = tcp:trabalhoisi.database.windows.net,1433; Initial Catalog = Trabalho_ISI_BD; Persist Security Info = False; User ID = a11156; Password =lufer.2017.isi; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"))

            return SelectRows(connection);

           // return connection;
        
    }


    static public void InsertRows(SqlConnection connection)
    {
        SqlParameter parameter;

        

        using (var command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"  
    INSERT INTO dbo.moedas  
            (Quantidade
            )  
        VALUES  
            (@Quantidade  
            ); ";

            parameter = new SqlParameter("@Quantidade", SqlDbType.Int);
            parameter.Value = 120;
            command.Parameters.Add(parameter);


        }

        connection.Close();
    }


    static public string SelectRows(SqlConnection connection)
    {
        string teste = " ";

        connection.Open();

        using (var command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandType = DT.CommandType.Text;
            command.CommandText = @"SELECT  * FROM  dbo.Moedas; ";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                teste = reader.GetInt32(0).ToString() + " " + reader.GetInt32(1).ToString() + " " + reader.GetSqlDateTime(3).ToString();

            }
        }

        connection.Close();

        return teste;


    }



        #endregion

*/
    }
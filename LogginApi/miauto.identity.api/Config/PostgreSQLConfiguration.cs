using Npgsql;
using System;


namespace miauto.identity.api.Config
{
    public class PostgreSQLConfiguration
    {
        public string ConnectionString { get; set; }

        //Inicia la conexion
        public PostgreSQLConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
           
        }
        

     
    }
}

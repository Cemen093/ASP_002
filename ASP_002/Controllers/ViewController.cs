using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ASP_002.Controllers
{
    /*    Создать API для сайта кибер-команд по CS.
        Сделать несколько контроллеров разделяя логику просмотра и менеджмента командой / игроком / матчем.
        В ответ - ссылку на гит проекта*/

    [ApiController]
    [Route("api/view/[controller]")]
    public class RegistrationController : ControllerBase
    {
        string connStr = new string("Server=tcp:my-server93-2.database.windows.net,1433;Initial Catalog=ASP_003;Persist Security Info=False;User ID=admin93;Password=PASSworld93;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        [HttpGet("{nameTable}")]
        public IEnumerable<string> Get(string nameTable)
        {
            List<string> transactions = new List<string>();
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM [{nameTable}]", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        transactions.Add(reader.GetValue(1).ToString());
                    }
                }
                return transactions;
            };
        }

        [HttpGet("{nameTable}, {id}")]
        public string Get(string nameTable, int id)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM [dbo].[{nameTable}] WHERE id = {id}", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    if (!reader.HasRows)
                    {
                        return "no matches found";
                    }
                    return reader.GetValue(1).ToString(); 
                }
            };
        }
    }
}
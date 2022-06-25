using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ASP_002.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
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

        [HttpPost("{nameTable}")]
        public StatusCodeResult Post(string nameTable, string name, string description, int points_team_one, int points_team_tho, int command_one, int command_two)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                string str = "";
                if (nameTable.Equals("Commands"))
                {
                    str = new string($"INSERT INTO [dbo].[Commands] VALUES (\'{name}\', \'{description}\')");
                } else if (nameTable.Equals("Players"))
                {
                    str = new string($"INSERT INTO [dbo].[Players] VALUES (\'{name}\', \'{description}\', \'{id}\')");
                }
                else if (nameTable.Equals("Match"))
                {
                    str = new string($"INSERT INTO [dbo].[Match] VALUES (\'{name}\', \'{description}\', \'{points_team_one}\', \'{points_team_tho}\', \'{command_one}\', \'{command_two}\')");
                }

                using (SqlCommand command = new SqlCommand(str, connection))
                {
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        return StatusCode(405);
                    }
                }
                return StatusCode(200);
            };
        }

        [HttpDelete("{nameTable}, {id}")]
        public StatusCodeResult Delete(string nameTable, int id)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"DELETE FROM [dbo].[{nameTable}] WHERE id = {id}", connection))
                {
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        return StatusCode(405);
                    }
                }
                return StatusCode(200);
            };
        }
    }
}

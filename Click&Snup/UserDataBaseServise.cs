using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Click_Snup
{
    internal class UserDatabaseService
    {
        private string connectionString = @"Server=localhost;Integrated Security=True;";

        // Метод для проверки и создания базы данных, если она не существует
        public void InitializeDatabase()
        {
            string dbCheckQuery = "SELECT database_id FROM sys.databases WHERE Name = 'UserDB'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(dbCheckQuery, connection);
                var result = command.ExecuteScalar();

                // Если база данных не существует, создаем ее
                if (result == DBNull.Value)
                {
                    string createDbQuery = "CREATE DATABASE UserDB";
                    SqlCommand createDbCommand = new SqlCommand(createDbQuery, connection);
                    createDbCommand.ExecuteNonQuery();
                    Console.WriteLine("База данных создана.");

                    // После создания базы данных, создаем таблицу пользователей
                    CreateUsersTable();
                }
                else
                {
                    Console.WriteLine("База данных уже существует.");
                }
            }
        }

        // Метод для проверки и создания таблицы Users, если она не существует
        private void CreateUsersTable()
        {
            string createTableQuery = @"
            CREATE TABLE Users (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Username NVARCHAR(100) NOT NULL,
                Password NVARCHAR(100) NOT NULL,
                Role NVARCHAR(50) NOT NULL
            );";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Таблица Users создана.");
            }
        }

        // Метод для проверки учетных данных пользователя
        public bool ValidateUser(string username, string password, string role)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password AND Role = @Role";

            using (SqlConnection connection = new SqlConnection(connectionString + "Database=UserDB"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // Для реального проекта пароли должны быть зашифрованы
                    command.Parameters.AddWithValue("@Role", role);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Метод для регистрации пользователя
        public bool RegisterUser(string username, string password, string role)
        {
            // Проверка на существование пользователя с таким же именем
            if (IsUsernameExist(username))
            {
                return false; // Если пользователь существует, не разрешаем регистрацию
            }

            string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";

            using (SqlConnection connection = new SqlConnection(connectionString + "Database=UserDB"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // Используйте хеширование паролей для реальной безопасности
                    command.Parameters.AddWithValue("@Role", role);

                    int result = command.ExecuteNonQuery();
                    return result > 0; // Возвращаем true, если пользователь был успешно добавлен
                }
            }
        }

        // Метод для проверки существования пользователя
        private bool IsUsernameExist(string username)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString + "Database=UserDB"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}

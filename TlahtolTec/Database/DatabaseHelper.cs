using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

public static class DatabaseHelper
{
    private static string connectionString = "Data Source=Database\\Glosario.db;Version=3;";

    public static void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            connection.Execute(
                @"CREATE TABLE IF NOT EXISTS Palabras (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Espanol TEXT NOT NULL,
                    Nahuatl TEXT NOT NULL
                )");
        }
    }

    public static void AgregarPalabra(string espanol, string nahuatl)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Execute(
                "INSERT INTO Palabras (Espanol, Nahuatl) VALUES (@Espanol, @Nahuatl)",
                new { Espanol = espanol, Nahuatl = nahuatl });
        }
    }

    public static string Traducir(string palabra, bool esEspanol = true)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            var query = esEspanol
                ? "SELECT Nahuatl FROM Palabras WHERE Espanol = @Palabra"
                : "SELECT Espanol FROM Palabras WHERE Nahuatl = @Palabra";

            return connection.QueryFirstOrDefault<string>(query, new { Palabra = palabra });
        }
    }

    public static List<dynamic> ObtenerTodas()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            return connection.Query("SELECT * FROM Palabras").ToList();
        }
    }
}
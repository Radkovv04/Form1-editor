using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public static class DatabaseSettings
    {
        public static bool UseSQLite = true;
        public static string SqliteConnection = "Data Source=KnowledgeBase.db";
        public static string SqlServerConnection = "Server=(localdb)\\mssqllocaldb;Database=AppDb;Trusted_Connection=True;";
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.Models
{
    public static class DbConfig
    {
        // If set to 'false' it will switch the whole app to SQL Server
        public static bool UseSQLite = true;
        public static string SQLitePath = "Data Source=KnowledgeBase.db";
        public static string SqlServerPath = "Server=(localdb)\\mssqllocaldb;Database=AppDb;Trusted_Connection=True;";
    }
}
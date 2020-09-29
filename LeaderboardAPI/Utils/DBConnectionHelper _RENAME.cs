using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardAPI.Utils
{
    public class DBConnectionHelper_RENAME
    {
        private const string SERVER = "PostgreSQL 13"; // 13 released on 2020/09/24
        private const string HOST = "localhost";
        private const string PORT = "5432";
        private const string USERNAME = "<fill in>";
        private const string PASSWORD = "<fill in>";
        private const string DATABASE = "<fill in>";

        public static string GetConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}

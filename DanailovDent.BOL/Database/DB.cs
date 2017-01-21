using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL
{
    public static class DB
    {
        public static string ConnectionString { get; set; }
        private static string SqlFormat = "Server={0}; Port={1}; Database={2}; User Id={3}; Password={4}; Charset=utf8; AllowUserVariables=True;";

        static DB()
        {
            string server = string.Empty;
            string port = string.Empty;
            string user = string.Empty;
            string pass = string.Empty;
            string database = string.Empty;

            #region parse

            string[] lines = File.ReadAllLines("conn.txt");
            foreach (string l in lines)
            {
                string[] temp = l.Split('=');
                string key = temp[0].Trim().ToUpperInvariant();
                string value = temp[1].Trim();

                switch (key)
                {
                    case "SERVER":
                        {
                            server = value;
                            break;
                        }
                    case "PORT":
                        {
                            port = value;
                            break;
                        }
                    case "USERNAME":
                        {
                            user = value;
                            break;
                        }
                    case "PASSWORD":
                        {
                            pass = value;
                            break;
                        }
                    case "DATABASE":
                        {
                            database = value;
                            break;
                        }
                }
            #endregion

                DB.ConnectionString = string.Format(DB.SqlFormat,
                    server, port, database, user, pass);
            }
        }

        public static DentDb Create()
        {
            return new DentDb(DB.ConnectionString);
        }
    }
}

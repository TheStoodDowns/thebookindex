﻿namespace TheBookIndex.Data.BaseTypes
{
    public class ConnectionString
    {
        public ConnectionString(string db)
        {
            Db = db;
        }

        public string Db { get; private set; }
    }
}

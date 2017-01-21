using LinqToDB.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL.Pocos
{
    public partial class danailovDB : LinqToDB.Data.DataConnection
    {
        public danailovDB(IDataProvider provider, string connectionString)
            : base(provider, connectionString)
        { }
    }
}

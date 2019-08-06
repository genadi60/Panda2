using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Panda2.Data.Common;

namespace Panda2.Data
{
    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(Panda2DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Panda2DbContext Context { get; set; }

        public Task RunQueryAsync(string query, params object[] parameters)
        {
            return this.Context.Database.ExecuteSqlCommandAsync(query, parameters);
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}

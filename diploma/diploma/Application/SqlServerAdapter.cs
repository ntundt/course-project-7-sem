﻿using System.Data.Common;

namespace diploma.Application;

public class SqlServerAdapter : DbmsAdapter
{
    public SqlServerAdapter(DbConnection connection) : base(connection)
    {
    }

    private static async Task<string> GetDropCurrentSchemaSqlAsync()
    {
        return await File.ReadAllTextAsync("Assets/Scripts/MSSQLServer/DropSchema.sql");
    }
    
    public override async Task DropCurrentSchemaAsync(CancellationToken cancellationToken)
    {   
        var command = _connection.CreateCommand();
        command.CommandText = await GetDropCurrentSchemaSqlAsync();
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}

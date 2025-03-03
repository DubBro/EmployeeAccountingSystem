using System.Data.Common;

namespace EmployeeAccountingSystem.Utils.Extensions;

public static class DbExtensions
{
    public static void AddParameter(this IDbCommand command, string parameterName, object value)
    {
        var paramId = new SqlParameter(parameterName, value);
        command.Parameters.Add(paramId);
    }

    public static async Task<int> ExecuteDbCommandScalarSafetyAsync(this DbCommand command, DbTransaction transaction)
    {
        command.Transaction = transaction;

        try
        {
            var result = await command.ExecuteScalarAsync();

            await transaction.CommitAsync();

            return Convert.ToInt32(result);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

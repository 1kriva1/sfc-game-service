using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Extensions;

public static class ExceptionExtensions
{
    public static bool IsUniqueConstraintViolation(this DbUpdateException ex)
        => ex.InnerException is SqlException sqlEx && DatabaseConstants.UniqueViolationErrorNumbers.Contains(sqlEx.Number);
}
using MukSoft.Core.Domain;
using System;

namespace MukSoft.Core.Extensions
{
    public static class IEntityExtensions
    {
        public static bool IsObjectNull(this BaseEntity entity)
        {
            return entity == null;
        }

        public static bool IsEmptyObject(this BaseEntity entity)
        {
            return entity.Id.Equals(Guid.Empty);
        }
    }
}

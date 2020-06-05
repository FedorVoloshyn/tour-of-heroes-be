using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace TourOfHeroes
{
    public static class SoftDeletionExtension
    {
        public static void AddSoftDeletion(this ModelBuilder modelBuilder)
        {
            var entities = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in entities)
            {
                modelBuilder.Entity(entity.ClrType).Property<DateTime?>("Removed").IsRequired(false).HasDefaultValueSql(null);

                LambdaExpression filter = CreateDynamicLambdaExpression(entity.ClrType);
                modelBuilder.Entity(entity.ClrType).HasQueryFilter(filter);
            }
        }

        private static LambdaExpression CreateDynamicLambdaExpression(Type type)
        {
            var genericType = typeof(FilterExpressionCreator<>).MakeGenericType(type);
            var expressionCreator = Activator.CreateInstance(genericType);

            return (LambdaExpression)genericType.InvokeMember("GetExpression", System.Reflection.BindingFlags.InvokeMethod, null, expressionCreator, null);
        }

        private class FilterExpressionCreator<T> where T : class
        {
            public Expression<Func<T, bool>> GetExpression()
            {
                return (m) => EF.Property<DateTime?>(m, "Removed") == null;
            }
        }
    }
}

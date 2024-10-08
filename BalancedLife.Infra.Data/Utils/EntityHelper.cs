using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BalancedLife.Infra.Data.Utils {
    public static class EntityHelper {
        public static async Task LoadNavigationPropertyAsync<TEntity, TProperty>(
                TEntity entity,
                Expression<Func<TEntity, TProperty>> navigationProperty,
                object foreignKey,
                DbSet<TProperty> dbSet) where TEntity : class where TProperty : class {
            var navigation = typeof(TEntity).GetProperty(navigationProperty.GetMemberName());
            var value = navigation.GetValue(entity);
            if ( value == null ) {
                var relatedEntity = await dbSet.FindAsync(foreignKey);
                navigation.SetValue(entity, relatedEntity);
            }
        }

        public static string GetMemberName(this LambdaExpression expression) {
            if ( expression.Body is MemberExpression member ) {
                return member.Member.Name;
            }
            throw new ArgumentException("Expression is not a member access", nameof(expression));
        }

        public static int CalculateAge(DateTime birthdate) {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if ( birthdate.Date > today.AddYears(-age) )
                age--;

            return age;
        }

        internal static async Task LoadNavigationPropertyAsync<T>(ApplicationDbContext context, T user) {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Linq.Expressions;
using System.Reflection;
using ValidationServices.Fluent.Rules;

namespace ValidationServices.Fluent.Service
{
    public class ValidationServiceBuilder
    {
        private readonly CustomValidationService service;

        public ValidationServiceBuilder(bool isRecursiveValidationService = true)
        {
            this.service = new CustomValidationService(isRecursiveValidationService);
        }

        public PropertyValidationRule<TOwner, TProperty> RuleFor<TOwner, TProperty>(
            Expression<Func<TOwner, TProperty>> propertyLambda) where TOwner : class
        {
            MemberExpression memberExpression = propertyLambda.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' does not refer to a property");
            }
            PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' refers to a field, not a property");
            }
            if (memberExpression.Expression != null)
            {
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' does not refer to a property of an argument");
            }

            PropertyValidationRule<TOwner, TProperty> validationRule = new PropertyValidationRule<TOwner, TProperty>();
            this.service.SetRule(typeof(TOwner), propertyInfo.Name, validationRule);

            return validationRule;
        }

        public CustomValidationService Build()
        {
            return this.service;
        }
    }
}

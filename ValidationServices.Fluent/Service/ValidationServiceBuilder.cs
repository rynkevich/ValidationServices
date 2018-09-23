using System;
using System.Linq.Expressions;
using System.Reflection;
using ValidationServices.Fluent.Rules;

namespace ValidationServices.Fluent.Service
{
    public class ValidationServiceBuilder
    {
        private readonly CustomValidationService _service;

        public ValidationServiceBuilder(bool isRecursiveValidationService = true)
        {
            this._service = new CustomValidationService(isRecursiveValidationService);
        }

        public PropertyValidationRule<TOwner, TProperty> RuleFor<TOwner, TProperty>(
            Expression<Func<TOwner, TProperty>> propertyLambda) where TOwner : class
        {
            if (!(propertyLambda.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' does not refer to a property");
            }
            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' refers to a field, not a property");
            }
            if (memberExpression.Expression is MemberExpression)
            {
                throw new ArgumentException($"Expression '{propertyLambda.ToString()}' does not refer to a property of an argument");
            }

            var validationRule = new PropertyValidationRule<TOwner, TProperty>();
            this._service.SetRule(typeof(TOwner), propertyInfo.Name, validationRule);

            return validationRule;
        }

        public CustomValidationService Build()
        {
            return this._service;
        }
    }
}

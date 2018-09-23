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
                throw new ArgumentException(
                    Resources.Service.ArgumentExceptionNotAProperty.Replace("{expression}", propertyLambda.ToString()));
            }
            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    Resources.Service.ArgumentExceptionIsAField.Replace("{expression}", propertyLambda.ToString()));
            }
            if (memberExpression.Expression is MemberExpression)
            {
                throw new ArgumentException(
                    Resources.Service.ArgumentExceptionAlienProperty.Replace("{expression}", propertyLambda.ToString()));
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

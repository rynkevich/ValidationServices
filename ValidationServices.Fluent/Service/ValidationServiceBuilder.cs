using System;
using System.Linq.Expressions;
using System.Reflection;
using ValidationServices.Fluent.Rules;

namespace ValidationServices.Fluent.Service
{
    /// <summary>
    /// Builder for the <see cref="CustomValidationService"/> class.
    /// </summary>
    public class ValidationServiceBuilder
    {
        /// <summary>
        /// The service being built.
        /// </summary>
        private readonly CustomValidationService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationServiceBuilder"/> class.
        /// </summary>
        /// <param name="isRecursiveValidationService"></param>
        public ValidationServiceBuilder(bool isRecursiveValidationService = true)
        {
            this._service = new CustomValidationService(isRecursiveValidationService);
        }

        /// <summary>
        /// Creates a new validation rule and pushes it to service cache.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The property being validated</typeparam>
        /// <param name="propertyLambda">The lambda expression to provide property to validate</param>
        /// <returns>New instance of <see cref="PropertyValidationRule{TOwner, TProperty}"/></returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="propertyLambda"/> provides invalid property</exception>
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

        /// <summary>
        /// Returns an instance of service that was configured using 
        /// <see cref="RuleFor{TOwner, TProperty}(Expression{Func{TOwner, TProperty}})"/> method.
        /// </summary>
        /// <returns>Configured instance of <see cref="CustomValidationService"/></returns>
        public CustomValidationService Build()
        {
            return this._service;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Serilog;
using ThirdDrawer.Extensions.CollectionExtensionMethods;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace DemoWebApp.Infrastructure
{
    public static class ValidationExtensions
    {
        public static string[] ValidationErrors<T>(this T o)
        {
            if (o == null || o.GetType().IsValueType) return new string[0];

            var flattenedObjectGraph = new[] {new Tuple<string, object>(o.GetType().Name, o)}
                .DepthFirst(GetChildPropertyValues)
                .ToArray();

            var validationResults = flattenedObjectGraph
                .SelectMany(ValidationResultsFor)
                .ToArray();

            return validationResults
                .Where(vr => vr != ValidationResult.Success)
                .Select(vr => "{0}: {1}".FormatWith(vr.MemberNames.Join(","), vr.ErrorMessage))
                .ToArray();
        }

        private static IEnumerable<Tuple<string, object>> GetChildPropertyValues(Tuple<string, object> tuple)
        {
            if (tuple.Item2 == null) return new Tuple<string, object>[0];

            var properties = tuple.Item2.GetType().GetProperties()
                .Where(ShouldValidateChildPropertiesOf)
                .ToArray();
            var propertyValues = properties
                .Select(p => new Tuple<string, object>(tuple.Item1 + "." + p.Name, p.GetValue(tuple.Item2)))
                .Where(t => t.Item2 != null)
                .ToArray();
            return propertyValues;
        }

        private static bool ShouldValidateChildPropertiesOf(PropertyInfo property)
        {
            if (property.PropertyType == typeof (object)) return false;
            if (property.PropertyType == typeof (string)) return false;
            if (property.PropertyType.IsValueType) return false;
            if (property.GetMethod == null) return false;
            if (property.GetMethod.GetParameters().Any()) return false;

            return true;
        }

        private static IEnumerable<ValidationResult> ValidationResultsFor(Tuple<string, object> tuple)
        {
            var validationResults = new List<ValidationResult>();
            try
            {
                var properties = tuple.Item2.GetType()
                    .GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.CanRead)
                    .ToArray();
                foreach (var property in properties)
                {
                    var validationAttributes = property.GetCustomAttributes(true).OfType<ValidationAttribute>().ToArray();
                    foreach (var attr in validationAttributes)
                    {
                        var propertyValue = property.GetValue(tuple.Item2);
                        var propertyValidationContext = new ValidationContext(tuple.Item1) {MemberName = tuple.Item1 + "." + property.Name};
                        var propertyValidationResult = attr.GetValidationResult(propertyValue, propertyValidationContext);
                        if (propertyValidationResult != null) validationResults.Add(propertyValidationResult);

                        var propertyValidatableObject = propertyValue as IValidatableObject;
                        if (propertyValidatableObject == null) continue;

                        var propertyAdditionalResults = propertyValidatableObject.Validate(propertyValidationContext);
                        validationResults.AddRange(propertyAdditionalResults);
                    }
                }

                var validatableObject = tuple.Item2 as IValidatableObject;
                if (validatableObject != null)
                {
                    var validationContext = new ValidationContext(tuple.Item1) {MemberName = tuple.Item1};
                    var validatableObjectResults = validatableObject.Validate(validationContext);
                    validationResults.AddRange(validatableObjectResults);
                }
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Validation threw an exception: {Message}", exc.Message);
                validationResults.Add(new ValidationResult("Validation threw an exception", new[] {tuple.Item1}));
            }

            return validationResults;
        }
    }
}
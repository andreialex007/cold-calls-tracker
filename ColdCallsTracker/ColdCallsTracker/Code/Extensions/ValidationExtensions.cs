using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ColdCallsTracker.Code.Data.ViewModels._Common;
using ValidationException = ColdCallsTracker.Code.Exceptions.ValidationException;

namespace ColdCallsTracker.Code.Extensions
{
    /// <summary>
    /// Validation error. Can be either entity or property level validation error.
    /// </summary>
    [Serializable]
    public class DbValidationError
    {
        private readonly string _propertyName;
        private readonly string _errorMessage;
        public DbValidationError(string propertyName, string errorMessage)
        {
            this._propertyName = propertyName;
            this._errorMessage = errorMessage;
        }
        public string PropertyName => this._propertyName;
        public string ErrorMessage => this._errorMessage;
    }

    public static class ValidationExtensions
    {

        public static List<DbValidationError> GetValidationErrors<T>(this T entityBase)
            where T : ViewModelBase
        {
            var validationErrors = new List<DbValidationError>();
            var validationContext = new ValidationContext(entityBase);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(entityBase, validationContext, results);
            foreach (var validationResult in results)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    validationErrors.Add(new DbValidationError(memberName, validationResult.ErrorMessage));
                }
            }
            return validationErrors;
        }

        public static void ThrowIfHasErrors(this IEnumerable<DbValidationError> errors)
        {
            var arrorsList = errors.ToList();
            if (arrorsList.Any())
                throw new ValidationException(arrorsList);
        }
    }
}
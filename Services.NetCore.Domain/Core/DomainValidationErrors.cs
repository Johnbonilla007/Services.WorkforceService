namespace Services.NetCore.Domain.Core
{
    /// <summary>
    /// Class to construct Domain Validation Errors
    /// </summary>
    public class DomainValidationErrors
    {
        private readonly string _validationErrorCode;
        private readonly object[] _validationErrorArguments;

        public string ValidationErrorCode
        {
            get { return _validationErrorCode; }
        }

        public object[] ValidationErrorArguments
        {
            get { return _validationErrorArguments; }
        }

        /// <summary>
        /// Create new instance of Domain Validation Errors.
        /// </summary>
        /// <param name="validationErrorCode">The validation error code.</param>
        /// <param name="validationErrorArguments">The validation error arguments.</param>
        public DomainValidationErrors(string validationErrorCode, params object[] validationErrorArguments)
        {
            _validationErrorCode = validationErrorCode;
            _validationErrorArguments = validationErrorArguments;
        }

        /// <summary>
        /// Create new instance of Domain Validation Errors.
        /// </summary>
        /// <param name="validationErrorCode">The validation error code.</param>
        public DomainValidationErrors(string validationErrorCode)
        {
            _validationErrorCode = validationErrorCode;
            _validationErrorArguments = null;
        }

    }

    public static class DomainValidationErrorsExtensions
    {
        public static void AddIfNotExists(this List<DomainValidationErrors> collection, DomainValidationErrors newItem)
        {
            if (!collection.Any(c => c.ValidationErrorCode == newItem.ValidationErrorCode && ArgumentsMatch(c.ValidationErrorArguments, newItem.ValidationErrorArguments)))
            {
                collection.Add(newItem);
            }
        }

        public static void AddUniqueRange(this List<DomainValidationErrors> collection, IEnumerable<DomainValidationErrors> range)
        {
            foreach (var item in range)
            {
                collection.AddIfNotExists(item);
            }
        }

        private static bool ArgumentsMatch(object[] args1, object[] args2)
        {
            if (args1.Count() != args2.Count()) return false;
            for (int i = 0; i < args1.Count(); i++)
            {
                if (args1[i] == args2[i]) return false;
            }

            return true;
        }
    }
}

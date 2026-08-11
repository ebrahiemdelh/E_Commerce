namespace E_Commerce.Application.Common
{

    public class Error
    {
        public string Code { get; } // Product Not found 

        public string Description { get; }

        public ErrorType Type { get; }

        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        // Static factory methods to create errors

        public static Error Failure(string description = "General.Failure",
            string code = "A failure has occurred.")
            => new Error(code, description, ErrorType.Failure);

        public static Error Validation(string description = "General.Validation",
            string code = "A validation error has occurred.") =>
            new Error(code, description, ErrorType.Validation);

        public static Error NotFound(string description = "General.NotFound",
            string code = "A 'Not Found' error has occurred.") =>
            new Error(code, description, ErrorType.NotFound);

        public static Error Conflict(string description = "General.Conflict",
            string code = "A conflict error has occurred.") =>
            new Error(code, description, ErrorType.Conflict);

        public static Error Unauthorized(string description = "General.Unauthorized",
            string code = "An unauthorized error has occurred.") =>
            new Error(code, description, ErrorType.Unauthorized);
    }
}



public enum ErrorType
{
    Failure = 0,

    Validation = 1,

    NotFound = 2,

    Conflict = 3,

    Unauthorized = 4
}


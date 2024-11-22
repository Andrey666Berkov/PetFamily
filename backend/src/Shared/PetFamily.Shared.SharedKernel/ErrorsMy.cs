namespace PetFamily.Shared.SharedKernel;

public static class ErrorsMy
{
    public static class General
    {
        public static ErrorMy ValueIsInavalid(string? name = null)
        {
            var label = name ?? "value";
            return ErrorMy.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static ErrorMy NotFound(Guid? id = null)
        {
            var forId = id == null ? "value" : $" for id '{id}'";
            return ErrorMy.Validation("record.is.found", $"Record not found id:{forId}");
        }

        public static ErrorMy ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return ErrorMy.Validation("lenght.is.invalid", $"Invalid{label}length");
        }

        public static ErrorMy AllReadyExist()
        {
            return ErrorMy.Validation("record.already.exist", $"Volunteer already exist");
        }
    }

    public static class User
    {
        public static ErrorMy InvalidCredentials()
        {
            return ErrorMy.Validation("credentials.is.invalid", $"Credentials is invalid");
        }
    }

    public static class Tokens
    {
        public static ErrorMy ExpiredToken()
        {
            return ErrorMy.Validation("token.is.invalid", $"Token is expired");
        }
        public static ErrorMy InvalidToken()
        {
            return ErrorMy.Validation("token.is.invalid", $"Token is invalid");
        }
        
        public static ErrorMy Failure()
        {
            return ErrorMy.Validation("failure.is.invalid", $"Failure is invalid");
        }
    }
}
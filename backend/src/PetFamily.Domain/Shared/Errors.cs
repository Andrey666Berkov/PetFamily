namespace PetFamily.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInavalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }
        
        public static Error NotFound(Guid? id=null)
        {
            var forId = id==null ? "value" : $" for id '{id}'";
            return Error.Validation("record.is.found", $"Record not found id:{forId}");
        }
        
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " "+name+ " ";
            return Error.Validation("lenght.is.invalid", $"Invalid{label}length");
        }
        
        public static Error AllReadyExist()
        {
            return Error.Validation("record.already.exist", $"Volunteer already exist");
        }
    }
    
    public static class User
    {
        public static Error InvalidCredentials()
        {
            
            return Error.Validation("credentials.is.invalid", $"Credentials is invalid");
        }
        
    }
    
    

   
}
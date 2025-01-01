namespace PetFamily.Shared.Framework.Authorization;

public static class Permission
{
    public static class Accounts
    {
        public const string CreatePet = "pet.create";
        public const string UpdatePet = "pet.updatew";
        public const string DeletePet = "pet.delete";
        public const string ReadPet = "pet.read";
    }
    public static class Pets
    {
        public const string CreatePet = "pet.create";
        
    }
}
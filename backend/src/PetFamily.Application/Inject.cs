using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Database;
using PetFamily.Application.Massaging;
using PetFamily.Application.PetManagment.UseCases.AddPet;
using PetFamily.Application.PetManagment.UseCases.CreateVolunteer;
using PetFamily.Application.PetManagment.UseCases.DeletePet;
using PetFamily.Application.PetManagment.UseCases.DeleteVolunteer;
using PetFamily.Application.PetManagment.UseCases.GetPet;
using PetFamily.Application.PetManagment.UseCases.UpdateVolunteerMainInfo;
using PetFamily.Application.PetManagment.UseCases.UpdateVolunteerSocialNetwork;
using PetFamily.Application.PetManagment.UseCases.UploadFilesToPet;
using PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;


namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
       services.AddScoped<UpdateVolunteerInfoUseCase>() ;
        services.AddScoped<CreateVolunteerUseCase>() ;
        services.AddScoped<DeleteVolunteerUseCase>();
        services.AddScoped<UpdateVolunteerSocialNetworkUseCase>();
        services.AddScoped<AddPetUseCase>();
        services.AddScoped<GetPetUseCase>();
        services.AddScoped<DeletePetUseCase>();
        services.AddScoped<GetPetWhithPaginationUseCase>();
        services.AddScoped<UploadFilesPetUseCase>();
       
        return  services;
    }
}
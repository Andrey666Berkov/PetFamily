﻿using System.Drawing;

namespace PetFamily.Application.Modules.CreateVolunteer;

public record CreateVolunteerRequest(
    string FirstName, 
    string LastName, 
    string MiddleName,
    string Email, 
    string Description,
    int NumberPhone, 
    int Experience, 
    List<RequesitDto>? requisitesDto,
    List<SocialNetworkDto>? SocialNetworkDto);
    
    
    
    public record RequesitDto(string Title, string Description);
    public record SocialNetworkDto(string Name, string Link);
// AutoMapperProfile.cs
using AutoMapper;
using GourmeyGalleryApp.DTOs;
using GourmeyGalleryApp.Models.DTOs;
using GourmeyGalleryApp.Models.Entities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        CreateMap<Recipe, RecipeDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Rating, RatingDto>().ReverseMap();
    }
}

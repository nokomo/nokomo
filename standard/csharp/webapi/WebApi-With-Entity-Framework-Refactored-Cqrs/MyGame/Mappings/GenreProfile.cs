using System;
using AutoMapper;
using MyGame.Dtos;
using MyGame.Models;

namespace MyGame;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
    }

}

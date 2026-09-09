using AutoMapper;
using MyGame.Dtos;
using MyGame.Models;

namespace MyGame
{
    public class GameProfile : Profile 
    {
        public GameProfile()
        {
            CreateMap<Game, GameDetailsDto>();
            CreateMap<Game, GameSummaryDto>();
            CreateMap<CreateGameDto, Game>();
            CreateMap<UpdateGameDto, Game>();            
        }

    }
}

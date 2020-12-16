using AutoMapper;
using BE.Entities;
using System.Collections.Generic;
using ViewModels.ViewModels;

namespace Utilities
{
    public static class MapperConfiguration
    {
        public static void Initialize() => AutoMapper.Mapper.Initialize(GetMapperConfiguration);

        private static void GetMapperConfiguration(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserBE, UserViewModel>();
            cfg.CreateMap<BinnacleBE, BinnacleViewModel>();
            cfg.CreateMap<PermissionBE, PermissionViewModel>();
            cfg.CreateMap<PermissionsGroupBE, PermissionsGroupViewModel>();
            cfg.CreateMap<LanguageBE, LanguageViewModel>();
            cfg.CreateMap<ContractBE, ContractViewModel>();
            cfg.CreateMap<ServiceBE, ServiceViewModel>();
            cfg.CreateMap<AlbumBE, AlbumViewModel>();
            cfg.CreateMap<CategoryBE, CategoryViewModel>();
            cfg.CreateMap<VoteBE, VoteViewModel>();
            cfg.CreateMap<SongBE, SongViewModel>();
            cfg.CreateMap<ClaimBE, ClaimViewModel>();
            cfg.CreateMap<StateBE, StateViewModel>();







            cfg.CreateMap<UserViewModel,UserBE>();
            cfg.CreateMap<BinnacleViewModel, BinnacleBE>();
            cfg.CreateMap<PermissionViewModel, PermissionBE>();
            cfg.CreateMap<PermissionsGroupViewModel, PermissionsGroupBE>();
            cfg.CreateMap<LanguageViewModel, LanguageBE>();
            cfg.CreateMap<ContractViewModel, ContractBE>();
            cfg.CreateMap<ServiceViewModel, ServiceBE>();
            cfg.CreateMap<AlbumViewModel,AlbumBE>();
            cfg.CreateMap<CategoryViewModel, CategoryBE>();
            cfg.CreateMap<VoteViewModel, VoteBE>();
            cfg.CreateMap<SongViewModel,SongBE>();
            cfg.CreateMap<ClaimViewModel, ClaimBE>();
            cfg.CreateMap<StateViewModel, StateBE>();
        }



        
    }
}
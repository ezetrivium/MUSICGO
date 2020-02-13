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
            cfg.CreateMap<BinnacleViewModel, BinnacleViewModel>();
            cfg.CreateMap<PermissionBE, PermissionViewModel>();
            cfg.CreateMap<PermissionsGroupBE, PermissionsGroupViewModel>();
            cfg.CreateMap<LanguageBE, LanguageViewModel>();



            cfg.CreateMap<UserViewModel,UserBE>();
            cfg.CreateMap<BinnacleViewModel, BinnacleBE>();
            cfg.CreateMap<PermissionViewModel, PermissionBE>();
            cfg.CreateMap<PermissionsGroupViewModel, PermissionsGroupBE>();
            cfg.CreateMap<LanguageViewModel, LanguageBE>();
        }



        
    }
}
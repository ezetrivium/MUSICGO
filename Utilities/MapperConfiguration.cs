using AutoMapper;
using BE.Entities;
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
            cfg.CreateMap<LanguageBE, LanguageViewModel>();
            //cfg.CreateMap<Language, LanguageViewModel>();
            //cfg.CreateMap<Banner, BannerViewModel>();
            //cfg.CreateMap<Slide, SlideViewModel>();
            //cfg.CreateMap<Button, ButtonViewModel>();
            //cfg.CreateMap<CalendarDate, CalendarDateViewModel>();
            //cfg.CreateMap<LanguageBanner, LanguageBannerViewModel>();
            //cfg.CreateMap<LanguageNews, LanguageNewsViewModel>();
            //cfg.CreateMap<LanguageButton, LanguageButtonViewModel>();         
            //cfg.CreateMap<LanguageCalendarDate, LanguageCalendarDateViewModel>();
            //cfg.CreateMap<LanguageSlide, LanguageSlideViewModel>();
            //cfg.CreateMap<BannerButton, BannerButtonViewModel>();
            //cfg.CreateMap<SlideButton, SlideButtonViewModel>();


            cfg.CreateMap<UserViewModel,UserBE>();
            cfg.CreateMap<BinnacleViewModel, BinnacleViewModel>();
            cfg.CreateMap<PermissionViewModel, PermissionBE>();
            cfg.CreateMap<LanguageViewModel, LanguageBE>();
            //cfg.CreateMap<LanguageViewModel, Language>();
            //cfg.CreateMap<BannerViewModel, Banner>();
            //cfg.CreateMap<SlideViewModel, Slide>();
            //cfg.CreateMap<ButtonViewModel, Button>();
            //cfg.CreateMap<CalendarDateViewModel, CalendarDate>();
            //cfg.CreateMap<LanguageBannerViewModel, LanguageBanner>();
            //cfg.CreateMap<LanguageNewsViewModel, LanguageNews>();
            //cfg.CreateMap<LanguageButtonViewModel, LanguageButton>();
            //cfg.CreateMap<LanguageCalendarDateViewModel, LanguageCalendarDate>();
            //cfg.CreateMap<LanguageSlideViewModel, LanguageSlide>();
            //cfg.CreateMap<BannerButtonViewModel, BannerButton>();
            //cfg.CreateMap<SlideButtonViewModel, SlideButton>();

        }
    }
}
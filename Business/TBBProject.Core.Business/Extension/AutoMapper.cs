using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Business.Extension
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Users, UserVM>();
            CreateMap<UserVM, Users>();

            CreateMap<Resource, ResourcesVM>();
            CreateMap<ResourcesVM, Resource>();

            CreateMap<Language, LanguageVM>();
            CreateMap<LanguageVM, Language>();

            CreateMap<Role, RoleVM>();
            CreateMap<RoleVM, Role>();

            CreateMap<Authority, AuthorityVM>();
            CreateMap<AuthorityVM, Authority>().ForMember(dest => dest.ParentMenu,
                   opt => opt.MapFrom
                   (src => src.ParentMenu != 0 ? src.ParentMenu : null));

            CreateMap<Icons, IconVM>();
            CreateMap<IconVM, Icons>();

            CreateMap<Announcement, AnnouncementVM>().ForMember(dest => dest.AnnouncementTypeStr,
                   opt => opt.MapFrom
                   (src => src.AnnouncementType != null ? src.AnnouncementType.Name : ""))
                    .ForMember(dest => dest.IsReleaseStr,
                   opt => opt.MapFrom
                   (src => src.IsRelease == true ? LocalizationCaptions.IsRelease : src.IsRelease == false ? LocalizationCaptions.IsNotRelease : ""))
                   .ForMember(dest => dest.ApprovalStatusStr,
                   opt => opt.MapFrom
                   (src => src.ApprovalStatus == 0 ? LocalizationCaptions.Waiting : src.ApprovalStatus != 0 ? LocalizationCaptions.Waiting : ""));



            CreateMap<AnnouncementVM, Announcement>();

            CreateMap<AnnouncementLang, AnnouncementLangVM>();
            CreateMap<AnnouncementLangVM, AnnouncementLang>();

            CreateMap<AnnouncementType, AnnouncementTypeVM>();
            CreateMap<AnnouncementTypeVM, AnnouncementType>();

            CreateMap<News, NewsVM>()
                    .ForMember(dest => dest.IsReleaseStr,
                   opt => opt.MapFrom
                   (src => src.IsRelease == true ? LocalizationCaptions.IsRelease : src.IsRelease == false ? LocalizationCaptions.IsNotRelease : ""))
                   .ForMember(dest => dest.ApprovalStatusStr,
                   opt => opt.MapFrom
                   (src => src.ApprovalStatus == 0 ? LocalizationCaptions.Waiting : src.ApprovalStatus != 0 ? LocalizationCaptions.Waiting : ""));
            CreateMap<NewsVM, News>();

            CreateMap<NewsLang, NewsLangVM>();
            CreateMap<NewsLangVM, NewsLang>();

            CreateMap<NewsGallery, NewsGalleryVM>();
            CreateMap<NewsGalleryVM, NewsGallery>();


            CreateMap<VideoGallery, VideoGalleryVM>();
            CreateMap<VideoGalleryVM, VideoGallery>();

            CreateMap<VideoGalleryLang, VideoGalleryLangVM>();
            CreateMap<VideoGalleryLangVM, VideoGalleryLang>();

            CreateMap<AcademicSchedule, AcademicScheduleVM>();
            CreateMap<AcademicScheduleVM, AcademicSchedule>();

            CreateMap<AcademicScheduleLang, AcademicScheduleLangVM>();
            CreateMap<AcademicScheduleLangVM, AcademicScheduleLang>();
        }
    }
}

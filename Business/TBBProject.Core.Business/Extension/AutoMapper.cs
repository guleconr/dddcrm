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
            CreateMap<Users, UserVM>().ForMember(dest => dest.UserRoleId,
                   opt => opt.MapFrom
                   (src => src.UserRole.FirstOrDefault().RoleId));
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

            CreateMap<Announcement, AnnouncementVM>()
                .ForMember(dest => dest.TitleStr,
                   opt => opt.MapFrom
                   (src => src.AnnouncementLang != null ? src.AnnouncementLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault() != null ?
                   src.AnnouncementLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault().Title :
                   src.AnnouncementLang.FirstOrDefault().Title : ""))
                    .ForMember(dest => dest.IsReleaseStr,
                   opt => opt.MapFrom
                   (src => src.IsRelease== true ? "Yayında": src.IsRelease == false? "Yayında Değil":""))
                   .ForMember(dest => dest.ApprovalStatusStr,
                   opt => opt.MapFrom
                   (src => src.ApprovalStatus == ApprovalStatus.Waiting ? "Bekliyor" : src.ApprovalStatus == ApprovalStatus.Approval ? "Onaylandı" : ""));




            CreateMap<AnnouncementVM, Announcement>().ForMember(dest => dest.ReleaseDate,
                   opt => opt.MapFrom
                   (src => DateTime.ParseExact(src.ReleaseDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)));


            CreateMap<AnnouncementLang, AnnouncementLangVM>();
            CreateMap<AnnouncementLangVM, AnnouncementLang>();

            CreateMap<AnnouncementFile, AnnouncementFileVM>();
            CreateMap<AnnouncementFileVM, AnnouncementFile>();

            CreateMap<AnnouncementType, AnnouncementTypeVM>();
            CreateMap<AnnouncementTypeVM, AnnouncementType>();

            CreateMap<News, NewsVM>()
                .ForMember(dest => dest.TitleStr,
                   opt => opt.MapFrom
                   (src => src.NewsLang != null ? src.NewsLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault() != null ?
                   src.NewsLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault().Title :
                   src.NewsLang.FirstOrDefault().Title : ""))
                    .ForMember(dest => dest.IsReleaseStr,
                   opt => opt.MapFrom
                   (src => src.IsRelease == true ? "Yayında" : src.IsRelease == false ? "Yayında Değil" : ""))
                   .ForMember(dest => dest.ApprovalStatusStr,
                   opt => opt.MapFrom
                   (src => src.ApprovalStatus == ApprovalStatus.Waiting ? "Bekliyor" : src.ApprovalStatus == ApprovalStatus.Approval ? "Onaylandı" : ""));
            CreateMap<NewsVM, News>().ForMember(dest => dest.ReleaseDate,
                   opt => opt.MapFrom
                   (src => DateTime.ParseExact(src.ReleaseDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)));

            CreateMap<NewsLang, NewsLangVM>();
            CreateMap<NewsLangVM, NewsLang>();

            CreateMap<NewsGallery, NewsGalleryVM>();
            CreateMap<NewsGalleryVM, NewsGallery>();

            CreateMap<NewsFile, NewsFileVM>();
            CreateMap<NewsFileVM, NewsFile>();

            CreateMap<VideoGallery, VideoGalleryVM>()
                   .ForMember(dest => dest.TitleStr,
                   opt => opt.MapFrom
                   (src => src.VideoGalleryLang != null ? src.VideoGalleryLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault() != null ?
                   src.VideoGalleryLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault().Title :
                   src.VideoGalleryLang.FirstOrDefault().Title : ""));

            CreateMap<VideoGalleryVM, VideoGallery>().ForMember(dest => dest.ReleaseDate,
                   opt => opt.MapFrom
                   (src => DateTime.ParseExact(src.ReleaseDateStr, "dd/MM/yyyy",  System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)));

            CreateMap<VideoGalleryLang, VideoGalleryLangVM>();
            CreateMap<VideoGalleryLangVM, VideoGalleryLang>();

            CreateMap<AcademicSchedule, AcademicScheduleVM>()
                   .ForMember(dest => dest.TitleStr,
                   opt => opt.MapFrom
                   (src => src.AcademicScheduleLang != null ? src.AcademicScheduleLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault() != null ?
                   src.AcademicScheduleLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault().Title :
                   src.AcademicScheduleLang.FirstOrDefault().Title : ""));

            CreateMap<AcademicScheduleVM, AcademicSchedule>().ForMember(dest => dest.StartDate,
                   opt => opt.MapFrom
                   (src => DateTime.ParseExact(src.StartDateStr, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)))
                .ForMember(dest => dest.EndDate,
                   opt => opt.MapFrom
                   (src => DateTime.ParseExact(src.EndDateStr, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)));


            CreateMap<AcademicScheduleLang, AcademicScheduleLangVM>();
            CreateMap<AcademicScheduleLangVM, AcademicScheduleLang>();

            CreateMap<Content, ContentVM>()
                   .ForMember(dest => dest.IsReleaseStr,
                   opt => opt.MapFrom
                   (src => src.IsRelease == true ? "Yayında" : src.IsRelease == false ? "Yayında Değil" : ""))
                .ForMember(dest => dest.TitleStr,
                opt => opt.MapFrom
                (src => src.ContentLang != null ? src.ContentLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault() != null ?
                src.ContentLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault().Title :
                src.ContentLang.FirstOrDefault().Title : ""));
            CreateMap<ContentVM, Content>().ForMember(dest => dest.ReleaseDate,
                   opt => opt.MapFrom
                   (src => DateTime.ParseExact(src.ReleaseDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)));

            CreateMap<ContentLang, ContentLangVM>();
            CreateMap<ContentLangVM, ContentLang>();

            CreateMap<ContentFile, ContentFileVM>();
            CreateMap<ContentFileVM, ContentFile>();

            CreateMap<ContentGallery, ContentGalleryVM>();
            CreateMap<ContentGalleryVM, ContentGallery>();


            CreateMap<LegislationAnnouncement, LegislationAnnouncementVM>()
                  .ForMember(dest => dest.IsReleaseStr,
                   opt => opt.MapFrom
                   (src => src.IsRelease == true ? "Yayında" : src.IsRelease == false ? "Yayında Değil" : ""))
                .ForMember(dest => dest.TitleStr,
                opt => opt.MapFrom
                (src => src.LegislationAnnouncementLang != null ? src.LegislationAnnouncementLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault() != null ?
                src.LegislationAnnouncementLang.Where(i => i.Language.Name == "tr-TR").FirstOrDefault().Title :
                src.LegislationAnnouncementLang.FirstOrDefault().Title : ""));
            CreateMap<LegislationAnnouncementVM, LegislationAnnouncement>().ForMember(dest => dest.ReleaseDate,
                   opt => opt.MapFrom
                   (src => DateTime.ParseExact(src.ReleaseDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)))
                .ForMember(dest => dest.EndReleaseDate,
                   opt => opt.MapFrom
                   (src => DateTime.ParseExact(src.EndReleaseDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)));

            CreateMap<LegislationAnnouncementLang, LegislationAnnouncementLangVM>();
            CreateMap<LegislationAnnouncementLangVM, LegislationAnnouncementLang>();

            CreateMap<LegislationAnnouncementFile, LegislationAnnouncementFileVM>();
            CreateMap<LegislationAnnouncementFileVM, LegislationAnnouncementFile>();
            CreateMap<District, DistrictVM>();
            CreateMap<DistrictVM, District>();

            CreateMap<Municipality, MunicipalityVM>();
            CreateMap<MunicipalityVM, Municipality>();
        }
    }
}

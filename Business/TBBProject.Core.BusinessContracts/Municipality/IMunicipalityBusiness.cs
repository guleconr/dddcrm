using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts
{
    public interface IMunicipalityBusiness
    {
        DataSourceResult GetMunicipalityAll([DataSourceRequest] DataSourceRequest request, int? MunicipalityType);
        DataSourceResult GetMunicipalityDistrictAll([DataSourceRequest] DataSourceRequest request, long contentId);

        DistrictVM GetMunicipalityDistrict(long contentId);
        MunicipalityVM GetMunicipality(long contentId);



        DataSourceResult GetMunicipality(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateMunicipality(MunicipalityVM content);
        void UpdateMunicipality(MunicipalityVM content);
        void DeleteMunicipality(long Id);
        void AppMunicipality(long Id);


        void CreateMunicipalityDistrict(DistrictVM content);
        void UpdateMunicipalityDistrict(DistrictVM content);
        void DeleteMunicipalityDistrict(long Id);
    }
}

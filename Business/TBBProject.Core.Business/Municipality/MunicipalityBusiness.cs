using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Business
{

    public class MunicipalityBusiness : IMunicipalityBusiness
    {
        private readonly IMunicipalityDataLayer _municipalityDataLayer;
        private readonly IMapper _mapper;
        public MunicipalityBusiness(IMunicipalityDataLayer municipalityDataLayer, IMapper mapper)
        {
            _municipalityDataLayer = municipalityDataLayer;
            _mapper = mapper;
        }

        public DataSourceResult GetMunicipality(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var municipality = _municipalityDataLayer.GetMunicipality(Id).ToDataSourceResult(request);
            municipality.Data = _mapper.Map<List<MunicipalityVM>>(municipality.Data);
            return municipality;
        }

        public void CreateMunicipality(MunicipalityVM municipality)
        {

            var emunicipality = _mapper.Map<Municipality>(municipality);
            _municipalityDataLayer.CreateMunicipality(emunicipality);

        }

        public void UpdateMunicipality(MunicipalityVM municipality)
        {
            var emunicipality = _mapper.Map<Municipality>(municipality);
            _municipalityDataLayer.UpdateMunicipality(emunicipality);
        }

        public void DeleteMunicipality(long Id)
        {
            _municipalityDataLayer.DeleteMunicipality(Id);
        }

        public void AppMunicipality(long Id)
        {
            _municipalityDataLayer.AppMunicipality(Id);
        }

        public void CreateMunicipalityDistrict(DistrictVM municipality)
        {
            var emunicipality = _mapper.Map<District>(municipality);
            _municipalityDataLayer.CreateMunicipalityDistrict(emunicipality);

        }

        public void UpdateMunicipalityDistrict(DistrictVM municipality)
        {
            var emunicipality = _mapper.Map<District>(municipality);
            _municipalityDataLayer.UpdateMunicipalityDistrict(emunicipality);
        }

        public void DeleteMunicipalityDistrict(long Id)
        {
            _municipalityDataLayer.DeleteMunicipalityDistrict(Id);
        }

        public DataSourceResult GetMunicipalityDistrictAll([DataSourceRequest] DataSourceRequest request, long municipalityId)
        {
            var data = _municipalityDataLayer.GetMunicipalityDistrictAll(municipalityId).ToDataSourceResult(request);
            data.Data = _mapper.Map<List<DistrictVM>>(data.Data);
            return data;
        }

        public DistrictVM GetMunicipalityDistrict(long municipalityId)
        {
            var data = _municipalityDataLayer.GetMunicipalityDistrict(municipalityId);
            return _mapper.Map<DistrictVM>(data);
        }

        public MunicipalityVM GetMunicipality(long municipalityId)
        {
            var data = _municipalityDataLayer.GetMunicipalityWithId(municipalityId);
            return _mapper.Map<MunicipalityVM>(data);
        }

        public DataSourceResult GetMunicipalityAll([DataSourceRequest] DataSourceRequest request,  int? MunicipalityType)
        {
            var data = _municipalityDataLayer.GetMunicipalityAll();

            if (MunicipalityType!=null )
            {
                data = data.Where(i => i.MunicipalityType == (MunicipalityEnum)MunicipalityType);
            }
            if (request.Filters.Count > 0)
            {
                var filter = ( (Kendo.Mvc.FilterDescriptor)( (Kendo.Mvc.CompositeFilterDescriptor)request.Filters[0] ).FilterDescriptors[0] ).Value;
                data = data.Where(i => i.Name.Contains(filter.ToString()));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<MunicipalityVM>>(res.Data);
            return res;
        }
    }
}

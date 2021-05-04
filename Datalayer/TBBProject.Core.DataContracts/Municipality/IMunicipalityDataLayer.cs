using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface IMunicipalityDataLayer
    {
        IQueryable<Municipality> GetMunicipality(long Id);

        void CreateMunicipality(Municipality model);

        void UpdateMunicipality(Municipality model);

        void DeleteMunicipality(long Id);
        void AppMunicipality(long Id);

        IQueryable<Municipality> GetMunicipalityAll();
        IQueryable<District> GetMunicipalityDistrictAll(long municipalityId);

        District GetMunicipalityDistrict(long municipalityId);

        Municipality GetMunicipalityWithId(long municipalityId);




        void CreateMunicipalityDistrict(District model);

        void UpdateMunicipalityDistrict(District model);

        void DeleteMunicipalityDistrict(long Id);
    }
}

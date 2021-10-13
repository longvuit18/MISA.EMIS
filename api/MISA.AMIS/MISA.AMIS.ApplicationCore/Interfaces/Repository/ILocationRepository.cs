using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Interfaces.Repository
{
    /// <summary>
    /// inteface Location
    /// </summary>
    /// Created by: VLVU (25/9/2021)
    public interface ILocationRepository
    {
        /// <summary>
        /// Lấy toàn bộ các nước
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Country>> GetAllContries();
        /// <summary>
        /// Lấy các tỉnh theo id của nước
        /// </summary>
        /// <param name="countryId">id của nước</param>
        /// <returns></returns>
        Task<IEnumerable<Province>> GetProvinces(Guid countryId);
        /// <summary>
        /// lấy các huyện theo id nước và id tỉnh
        /// </summary>
        /// <param name="countryId">id của nước</param>
        /// <param name="provinceId"> id của tỉnh</param>
        /// <returns></returns>
        Task<IEnumerable<District>> GetDistricts(Guid countryId, int provinceId);
        /// <summary>
        /// lấy các xã theo id nước và id tỉnh, id của huyện
        /// </summary>
        /// <param name="countryId">id của nước</param>
        /// <param name="provinceId"> id của tỉnh</param>
        /// <param name="districtId"> id của huyện</param>
        /// <returns></returns>
        Task<IEnumerable<Ward>> GetWards(Guid    countryId, int provinceId, int districtId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;
using DTO;

namespace BLL
{
    public class ProvincesBLL
    {
        private readonly ClientProvinces _client;

        public ProvincesBLL()
        {
            _client = new ClientProvinces();
        }

        public async Task<List<string>> GetDataProvinces()
        {
            try
            {
                var provinces = await _client.GetAllProvincesAsync();
                return provinces?.OrderBy(p => p.name).Select(p => p.name).ToList();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Không lấy được dữ liệu: " + ex.Message);
                return null;
            }
        }

        public async Task<List<string>> GetDataDistrict_FromIdProvinces(int id)
        {
            try
            {
                var districts = await _client.GetAllDistrictAsync();
                var result = districts?.Where(d => d.idProvinces == id).OrderBy(d => d.name).Select(d => d.name).ToList();
                return result;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Không lấy được dữ liệu: " + ex.Message);
                return null;
            }
        }

        public async Task<List<string>> GetDataWard_FromDistrictAndProvinces(int idDistrict)
        {
            try
            {
                var wards = await _client.GetAllWardAsync();
                var result = wards?.Where(w => w.idDistricts == idDistrict).OrderBy(w => w.name).Select(w => w.name).ToList();
                return result;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Không lấy được dữ liệu: " + ex.Message);
                return null;
            }
        }

        public async Task<int> GetIdProvincesByName(string nameProvinces)
        {
            var allProvinces = await _client.GetAllProvincesAsync();
            return allProvinces?.FirstOrDefault(p => p.name.Equals(nameProvinces, StringComparison.OrdinalIgnoreCase))?.idProvinces ?? -1;
        }

        public async Task<int> GetIdDistrictByName(string nameDistrict)
        {
            var allDistrict = await _client.GetAllDistrictAsync();
            return allDistrict?.FirstOrDefault(p => p.name.Equals(nameDistrict, StringComparison.OrdinalIgnoreCase))?.idDistricts ?? -1;
        }
    }
}

using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormListPartnerBLL
    {
        ClientPartner clientPartner;
        public FormListPartnerBLL()
        {
            clientPartner = new ClientPartner();
        }
        private string BuildAddress(string address, string ward, string district, string province)
        {
            var components = new List<string>
            {
                address ?? "",
                !string.IsNullOrEmpty(ward) ? ward : "",
                !string.IsNullOrEmpty(district) ? district : "",
                !string.IsNullOrEmpty(province) ? province : ""
            };
            return string.Join(", ", components.Where(c => !string.IsNullOrEmpty(c)));
        }
        public async Task<List<PartnerDTO>> GetDataFullProperties()
        {
            try
            {
                var partners = await clientPartner.GetAllPartnerAsync();
                var query = partners.Select(pa => new PartnerDTO
                {
                    idPartner = pa.idPartner,
                    nameCompany = pa.nameCompany ?? "",
                    addressCompany = BuildAddress(pa.addressCompany, pa.nameWard, pa.nameDistrict, pa.nameProvince),
                    namePartner = pa.namePartner ?? "",
                    position = pa.position ?? "",
                    phoneNumber = pa.phoneNumber ?? "",
                    bankNumber = pa.bankNumber ?? "",
                    bankName = pa.bankName ?? "",
                    qhnsNumber = pa.qhnsNumber ?? "",
                    nameWard = pa.nameWard ?? "",
                    nameDistrict = pa.nameDistrict ?? "",
                    nameProvince = pa.nameProvince ?? ""
                }).ToList();

                return query;
            }
            catch (Exception)
            {
                return new List<PartnerDTO>();
            }
        }
        public async Task<List<DTO.Excel.PartnerExcelDTO>> GetDataExcel()
        {
            try
            {
                var partners = await clientPartner.GetAllPartnerAsync();
                var query = partners.Select(pa => new DTO.Excel.PartnerExcelDTO
                {
                    idPartner = pa.idPartner,
                    nameCompany = pa.nameCompany ?? "",
                    addressCompany = $"{pa.addressCompany ?? ""}, {pa.nameWard ?? ""}, {pa.nameDistrict ?? ""}, {pa.nameProvince ?? ""}",
                    namePartner = pa.namePartner ?? "",
                    position = pa.position ?? "",
                    phoneNumber = $"'{pa.phoneNumber ?? ""}",
                    bankNumber = $"'{pa.bankNumber ?? ""}",
                    bankName = pa.bankName ?? "",
                    qhnsNumber = pa.qhnsNumber ?? "",
                }).ToList();

                return query;
            }
            catch (Exception)
            {
                return new List<DTO.Excel.PartnerExcelDTO>();
            }
        }
        public async Task<string> Post(string json)
        {
            try
            {
                return await clientPartner.Post(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> Delete(int idContentWork)
        {
            try
            {
                return await clientPartner.Delete(idContentWork);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> Update(int idContentWork, string json)
        {
            try
            {
                return await clientPartner.Update(idContentWork, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<PartnerDTO>> SearchData(string search)
        {
            try
            {
                var partners = await clientPartner.GetAllPartnerAsync();
                var query = partners.Select(pa => new PartnerDTO
                {
                    idPartner = pa.idPartner,
                    nameCompany = pa.nameCompany ?? "",
                    addressCompany = BuildAddress(pa.addressCompany, pa.nameWard, pa.nameDistrict, pa.nameProvince),
                    namePartner = pa.namePartner ?? "",
                    position = pa.position ?? "",
                    phoneNumber = pa.phoneNumber ?? "",
                    bankNumber = pa.bankNumber ?? "",
                    bankName = pa.bankName ?? "",
                    qhnsNumber = pa.qhnsNumber ?? "",
                    nameWard = pa.nameWard ?? "",
                    nameDistrict = pa.nameDistrict ?? "",
                    nameProvince = pa.nameProvince ?? ""
                }).ToList();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.ToLower();
                    query = query.Where(pa =>
                        pa.nameCompany.ToLower().Contains(search) ||
                        pa.namePartner.ToLower().Contains(search)
                    ).ToList();
                }

                return query;
            }
            catch (Exception)
            {
                return new List<PartnerDTO>();
            }
        }
    }
}

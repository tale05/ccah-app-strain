using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System;

namespace API
{
    public static class ApiConfig
    {
        //public static readonly string BaseUri = "http://irthuit2024-001-site1.ftempurl.com/api";
        //public static readonly string BaseUri = "https://localhost:7168/api";

        public static readonly string BaseUri = "https://irt-api.ccah.edu.vn/api";

        public static HttpClient Client { get; }

        static ApiConfig()
        {
            Client = new HttpClient();
            //Client = new HttpClient
            //{
            //    BaseAddress = new Uri(BaseUri)
            //};
            //Client.DefaultRequestHeaders.Accept.Clear();
            //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            //Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public static string GetProvinceUri() => $"{BaseUri}/Provinces";
        public static string GetDistrictUri() => $"{BaseUri}/Districts";
        public static string GetWardUri() => $"{BaseUri}/Wards";
        public static string GetPhylumUri() => $"{BaseUri}/Phylum";
        public static string GetClassUri() => $"{BaseUri}/Class";
        public static string GetGenusUri() => $"{BaseUri}/Genus";
        public static string GetSpeciesUri() => $"{BaseUri}/Species";
        public static string GetConditionalStrainUri() => $"{BaseUri}/ConditionalStrain";
        public static string GetStrainUriNoPaging() => $"{BaseUri}/Strain/NoPaging";
        public static string GetStrainUri() => $"{BaseUri}/Strain";
        public static string GetStrainApprovalHistoryUri() => $"{BaseUri}/StrainApprovalHistory";
        public static string GetRoleForEmployeeUri() => $"{BaseUri}/RoleForEmployee";
        public static string GetEmployeeUri() => $"{BaseUri}/Employee";
        public static string GetAccountForEmployeeUri() => $"{BaseUri}/AccountForEmployee";
        public static string GetIsolatorStrainUri() => $"{BaseUri}/IsolatorStrain";
        public static string GetIdentifyStrainUri() => $"{BaseUri}/IdentifyStrain";
        public static string GetInventoryUri() => $"{BaseUri}/Inventory";
        public static string GetScienceNewspaperUri() => $"{BaseUri}/ScienceNewspaper";
        public static string GetAuthorNewspaperUri() => $"{BaseUri}/AuthorNewspaper";
        public static string GetPartnerUri() => $"{BaseUri}/Partner";
        public static string GetProjectUri() => $"{BaseUri}/Project";
        public static string GetProjectContentUri() => $"{BaseUri}/ProjectContent";
        public static string GetContentWorkUri() => $"{BaseUri}/ContentWork";
        public static string GetCustomerUri() => $"{BaseUri}/Customer";
        public static string GetAccountForCustomerUri() => $"{BaseUri}/AccountForCustomer";
        public static string GetBillUri() => $"{BaseUri}/Bill";
        public static string GetBillDetailUri() => $"{BaseUri}/BillDetail";
        public static string GetCartUri() => $"{BaseUri}/Cart";
        public static string GetCartDetailUri() => $"{BaseUri}/CartDetail";
        public static string GetOrderUri() => $"{BaseUri}/Order";
        public static string GetOrderDetailUri() => $"{BaseUri}/OrderDetail";
    }
}

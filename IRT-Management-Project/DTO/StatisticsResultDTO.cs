using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StatisticsResultDTO
    {
        public int TotalBills { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalStrainSold { get; set; }
        public string TopSoldStrainNumber { get; set; }
        public int TopSoldStrainQuantity { get; set; }
    }
    public class StatisticsStrainDTO
    {
        public string strainNumber { get; set; }
        public int strainQuantity { get; set; }
    }
}

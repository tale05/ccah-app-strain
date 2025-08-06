using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InventoryBLL
    {
        ClientInventory clientInventory;
        public InventoryBLL()
        {
            clientInventory = new ClientInventory();
        }
        public async Task<int> CheckStockStrain(int idStrain)
        {
            try
            {
                var quantity = (from iv in await clientInventory.GetAllInventoryAsync()
                                where iv.idStrain == idStrain
                                select iv.quantity).FirstOrDefault();
                if (quantity > 5)
                    return 1;
                else if (quantity == 0)
                    return 2;
                else
                    return 3;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

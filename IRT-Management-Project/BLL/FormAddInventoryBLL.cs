using API;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FormAddInventoryBLL
    {
        private ClientStrain clientStrain;
        private ClientInventory clientInventory;
        public FormAddInventoryBLL()
        {
            clientStrain = new ClientStrain();
            clientInventory = new ClientInventory();
        }
        public async Task<List<InventoryDTO>> GetData()
        {
            try
            {
                return (from inventory
                        in await clientInventory.GetAllInventoryAsync()
                        join st in await clientStrain.GetAllStrainsAsync() on inventory.idStrain equals st.idStrain
                        select new InventoryDTO
                        {
                            inventoryId = inventory.inventoryId,
                            idStrain = inventory.idStrain,
                            strainNumber = st.strainNumber,
                            quantity = inventory.quantity,
                            price = inventory.price.ToString("N2") + " VNĐ",
                            entryDate = inventory.entryDate != null ? DateTime.Parse(inventory.entryDate).ToString("dd/MM/yyyy") : "",
                            histories = inventory.histories ?? "",
                            priceValue = inventory.price,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<InventoryDTO>();
            }
        }
        public async Task<List<InventoryDTO>> Fill_1()
        {
            try
            {
                return (from inventory
                        in await clientInventory.GetAllInventoryAsync()
                        join st in await clientStrain.GetAllStrainsAsync() on inventory.idStrain equals st.idStrain
                        where inventory.quantity < 5
                        select new InventoryDTO
                        {
                            inventoryId = inventory.inventoryId,
                            idStrain = inventory.idStrain,
                            strainNumber = st.strainNumber,
                            quantity = inventory.quantity,
                            price = inventory.price.ToString("N2") + " VNĐ",
                            entryDate = inventory.entryDate != null ? DateTime.Parse(inventory.entryDate).ToString("dd/MM/yyyy") : "",
                            histories = inventory.histories ?? "",
                            priceValue = inventory.price,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<InventoryDTO>();
            }
        }
        public async Task<List<InventoryDTO>> Fill_2()
        {
            try
            {
                return (from inventory
                        in await clientInventory.GetAllInventoryAsync()
                        join st in await clientStrain.GetAllStrainsAsync() on inventory.idStrain equals st.idStrain
                        where inventory.quantity == 0
                        select new InventoryDTO
                        {
                            inventoryId = inventory.inventoryId,
                            idStrain = inventory.idStrain,
                            strainNumber = st.strainNumber,
                            quantity = inventory.quantity,
                            price = inventory.price.ToString("N2") + " VNĐ",
                            entryDate = inventory.entryDate != null ? DateTime.Parse(inventory.entryDate).ToString("dd/MM/yyyy") : "",
                            histories = inventory.histories ?? "",
                            priceValue = inventory.price,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<InventoryDTO>();
            }
        }
        public async Task<List<InventoryDTO>> Fill_3()
        {
            try
            {
                return (from inventory
                        in await clientInventory.GetAllInventoryAsync()
                        join st in await clientStrain.GetAllStrainsAsync() on inventory.idStrain equals st.idStrain
                        where inventory.quantity >= 5
                        select new InventoryDTO
                        {
                            inventoryId = inventory.inventoryId,
                            idStrain = inventory.idStrain,
                            strainNumber = st.strainNumber,
                            quantity = inventory.quantity,
                            price = inventory.price.ToString("N2") + " VNĐ",
                            entryDate = inventory.entryDate != null ? DateTime.Parse(inventory.entryDate).ToString("dd/MM/yyyy") : "",
                            histories = inventory.histories ?? "",
                            priceValue = inventory.price,
                        }).ToList();
            }
            catch (Exception)
            {
                return new List<InventoryDTO>();
            }
        }
        public async Task<string> Update(int id, string json)
        {
            try
            {
                return await clientInventory.Update(id, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}

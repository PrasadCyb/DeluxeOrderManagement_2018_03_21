using DeluxeOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeluxeOM.Services
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        List<Order> SearchOrders(OrderSearch orderSearch);
        Order SearchEditOrder(int id, int annId);
        bool EditOrder(Order order);
        OrderSearch GetSearchValues();
        TerritoryLanguage GetDropDownValue();
        OrderSaveStatus CreateOrder(Order saveOrder);
        void LogError(ErrorMgt error);
        Order UpdateOrder(Order order);
        void LockOrder(int orderId,int userId);
        void UnlockOrder(int? orderId, int userId);
        List<ImportValidationResult> Import(string filePath, int userId);
    }
}

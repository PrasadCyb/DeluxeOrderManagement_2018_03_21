using DeluxeOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetOrders(string whrSQLquery);
        List<Order> SearchOrders(string whrSQLquery, string channel);
        Order SearchEditOrder(string whrSQLquery);
        bool EditOrder(Order order);
        OrderSearch GetSearchValue();
        TerritoryLanguage GetDropDownValue();

        OrderSaveStatus CreateOrder(Order order, List<string> assets);
        void LogError(ErrorLog error);
        PipelineOrder GetPipeLineOrder(string requestNumber);
        void LockOrder(int orderId,int userId);
        void UnlockOrder(int? orderId,int userId);

        List<ImportValidationResult> Import(string filePath, int userId);

    }
}

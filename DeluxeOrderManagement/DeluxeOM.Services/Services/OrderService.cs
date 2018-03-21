//using DeluxeOM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;
using DeluxeOM.Utils.Config;

//using DeluxeOM.Repository.Interfaces;
//using DeluxeOM.Repository.Implementation;
using DeluxeOM.Repository;

namespace DeluxeOM.Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _repository = null;
        string notApplicable = "N/A";
        string blanks = "''";
        public OrderService()
        {
            _repository = new OrderRepository();
        }

        /// <summary>
        /// Populates all Order values
        /// </summary>
        /// <returns>List of Orders</returns>
        public List<Order> GetOrders()
        {
            string whrSQLquery = string.Empty;
            List<Order> orders = _repository.GetOrders(whrSQLquery);
            return orders;
        }

        /// <summary>
        /// Populates filter Order values
        /// </summary>
        /// <param name="orderSearch">orderSearch contains filter values to search orders from a database</param>
        /// <returns>List of Orders</returns>
        public List<Order> SearchOrders(OrderSearch orderSearch)
        {
            string whrSQLquery = string.Empty;
            string channel = string.Empty;
            string temp = string.Empty;
            if (!string.IsNullOrEmpty(orderSearch.StartDate) && !string.IsNullOrEmpty(orderSearch.EndDate))
            {
                //orderSearch.ESTStartDate = Convert.ToDateTime(orderSearch.StartDate);
                //orderSearch.ESTEndDate = Convert.ToDateTime(orderSearch.EndDate);
                orderSearch.ESTStartDate = DateTime.ParseExact(orderSearch.StartDate, "MM/dd/yyyy", null);
                orderSearch.ESTEndDate = DateTime.ParseExact(orderSearch.EndDate, "MM/dd/yyyy", null);
            }
            if (!string.IsNullOrEmpty(orderSearch.SelectedTitle))
            {
                temp = orderSearch.SelectedTitle;
                orderSearch.SelectedTitle = orderSearch.SelectedTitle.Replace("'", "\''");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " AG.VideoVersion ='" + orderSearch.SelectedTitle + "'  " : whrSQLquery + " AND AG.VideoVersion = '" + orderSearch.SelectedTitle + "' ";
            }

            if (!string.IsNullOrEmpty(orderSearch.EditType))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.Category ='" + orderSearch.EditType + "'  " : whrSQLquery + " AND O.Category = '" + orderSearch.EditType + "' ";
            }

            if (!string.IsNullOrEmpty(orderSearch.LocalEdit))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " AG.LocalEdit ='" + orderSearch.LocalEdit + "'  " : whrSQLquery + " AND AG.LocalEdit = '" + orderSearch.LocalEdit + "' ";
            }
            if (!string.IsNullOrEmpty(orderSearch.OrderStatus))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.OrderStatus ='" + orderSearch.OrderStatus + "'  " : whrSQLquery + " AND O.OrderStatus = '" + orderSearch.OrderStatus + "' ";
            }
            if (!string.IsNullOrEmpty(orderSearch.GreenLightSent))
            {
                if (orderSearch.GreenLightSent.Equals("Yes"))
                {
                    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.GreenlightSenttoPackaging IS NOT NULL " : whrSQLquery + " AND O.GreenlightSenttoPackaging IS NOT NULL ";
                    //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.GreenlightSenttoPackaging IS NOT NULL OR O.GreenlightSenttoPackaging <>'" + blanks + "'" : whrSQLquery + "AND O.GreenlightSenttoPackaging IS NOT NULL OR O.GreenlightSenttoPackaging <>'" + blanks + "'";
                }
                else if (orderSearch.GreenLightSent.Equals("No"))
                {
                    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.GreenlightSenttoPackaging IS NULL " : whrSQLquery + " AND O.GreenlightSenttoPackaging IS NULL ";
                    //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.GreenlightSenttoPackaging IS NULL OR O.GreenlightSenttoPackaging ='" + blanks + "'" : whrSQLquery + "AND O.GreenlightSenttoPackaging IS NULL OR O.GreenlightSenttoPackaging ='" + blanks + "'";
                }
            }
            if (!string.IsNullOrEmpty(orderSearch.GreenLightReceived))
            {
                if (orderSearch.GreenLightReceived.Equals("Yes"))
                {
                    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (O.GreenlightValidatedbyDMDM IS NOT NULL ) " : whrSQLquery + " AND (O.GreenlightValidatedbyDMDM IS NOT NULL ) ";
                }
                else if (orderSearch.GreenLightReceived.Equals("No"))
                {
                    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (O.GreenlightValidatedbyDMDM IS NULL ) " : whrSQLquery + " AND (O.GreenlightValidatedbyDMDM IS NULL ) ";
                }
            }
            if (!string.IsNullOrEmpty(orderSearch.Region))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " R.NAME ='" + orderSearch.Region + "'  " : whrSQLquery + " AND R.NAME = '" + orderSearch.Region + "' ";
            }
            //bool andPrefixed = false;
            if (orderSearch.Territory != null && orderSearch.Territory.Any())
            {
                string concatterritory = orderSearch.Territory.Aggregate((current, next) => current + "', '" + next);
                concatterritory = string.Format("{0}{1}{0}", "'", concatterritory);
                
                whrSQLquery += string.IsNullOrEmpty(whrSQLquery) ? string.Empty : " AND ";
                whrSQLquery += " T.WBTerritory IN (" + concatterritory + ") "; 
                //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " T.WBTerritory ='" + orderSearch.Territory + "'  " : whrSQLquery + " AND T.WBTerritory = '" + orderSearch.Territory + "' ";
            }

            if ((orderSearch.ESTStartDate != null && !(orderSearch.ESTStartDate.Equals(DateTime.MinValue))) && (orderSearch.ESTEndDate != null && !(orderSearch.ESTEndDate.Equals(DateTime.MinValue))))
            {
                string startDate = orderSearch.ESTStartDate.Date.ToString("yyyy-MM-dd");
                string endDate = orderSearch.ESTEndDate.AddDays(1).Date.ToString("yyyy-MM-dd");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (CF.MinClientStartDate >='" + startDate + "' AND CF.MaxClientEndDate <'" + endDate + "')  " : whrSQLquery + " AND (CF.MinClientStartDate >='" + startDate + "' AND CF.MaxClientEndDate <'" + endDate + "') ";
            }
            if (!string.IsNullOrEmpty(orderSearch.OrderType))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.FileType ='" + orderSearch.OrderType + "'  " : whrSQLquery + " AND O.FileType = '" + orderSearch.OrderType + "' ";
            }
            //if (orderSearch.Territory != null && orderSearch.Territory.Count() != 0)
            //{
            //    string territories = null;
            //    foreach (var x in orderSearch.Territory)
            //    {
            //        territories += "'" + x + "'" + ",";
            //    }
            //    territories = territories.Substring(0, territories.Length - 1);
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " T.WBTerritory IN(" + territories + ")  " : whrSQLquery + " AND T.WBTerritory IN(" + territories + ") ";
            //}
            if (!string.IsNullOrEmpty(orderSearch.MediaType))
            {
                channel = string.IsNullOrEmpty(channel) ? " C.Channel ='" + orderSearch.MediaType + "'  " : whrSQLquery + " AND C.Channel = '" + orderSearch.MediaType + "' ";
            }
            else
            {
                string alllChannel = "'EST'" + "," + "'POEST'" + "," + "'VOD'";
                channel = string.IsNullOrEmpty(channel) ? " C.Channel IN(" + alllChannel + ")  " : whrSQLquery + " AND C.Channel IN(" + alllChannel + ") ";
            }
            if (!string.IsNullOrEmpty(whrSQLquery))
            {
                whrSQLquery = "where" + whrSQLquery;
            }

            if (!string.IsNullOrEmpty(channel))
            {
                channel = "where" + channel;
            }

            if (!string.IsNullOrEmpty(orderSearch.SortBy))
            {
                whrSQLquery = whrSQLquery + " ORDER BY " + " " + orderSearch.SortBy + " " + orderSearch.SortOrder;
            }

            // this if block is to resore title name which is modified for search title with '
            if (!string.IsNullOrEmpty(temp))
            {
                orderSearch.SelectedTitle = temp;
            }


            List<Order> orders = _repository.SearchOrders(whrSQLquery, channel);
            return orders;
        }

        /// <summary>
        /// Search order for edit
        /// </summary>
        /// <param name="id">order id for edit</param>
        /// <param name="annId">announcement id for which order is created</param>
        /// <returns>Order</returns>
        public Order SearchEditOrder(int id, int annId)
        {
            string whrSQLquery = string.Empty;
            whrSQLquery = " O.Id='" + id + "' AND AG.Id='" + annId + "'";
            whrSQLquery = "where" + whrSQLquery;
            var order = _repository.SearchEditOrder(whrSQLquery);
            order.OrderStatuses = new List<string>() { "New", "Other Vendor" };
            return order;
        }

        /// <summary>
        /// Edit and save selected order
        /// </summary>
        /// <param name="saveOrder">saveOrder contains all details of the order which is selected for edit</param>
        /// <returns>status value(true/false)</returns>
        public bool EditOrder(Order saveOrder)
        {
            bool status;
            if (!string.IsNullOrEmpty(saveOrder.OrderStatus) && saveOrder.OrderStatus.Equals("Other Vendor"))
            {
                status = _repository.EditOrder(saveOrder);
            }
            else
            {
                var order = UpdateOrder(saveOrder);
                status = _repository.EditOrder(order);
                
            }
            return status;
        }

        /// <summary>
        /// Populate Filter values
        /// </summary>
        /// <returns>OrderSearch</returns>
        public OrderSearch GetSearchValues()
        {
            OrderSearch orderSearch = _repository.GetSearchValue();
            //var chkValues = new List<ChkValues>(){new ChkValues()
            //                                        {   Name="Yes",
            //                                            IsSelected=false
            //                                        },
            //                                      new ChkValues()
            //                                        {   Name="No",
            //                                            IsSelected=false
            //                                        }
            //                                      };

            var greenLights = new List<string>() { "Yes", "No" };

            orderSearch.SortByList = new List<KeyValue>()
            {
                new KeyValue {Text="Order Status", Value="O.OrderStatus" },
                new KeyValue {Text="Order Category", Value="O.Category" },
                new KeyValue {Text="Territory", Value="Territory" },
                new KeyValue {Text="Language", Value="Language" },
                new KeyValue {Text="Request No", Value="RequestNumber" },
                new KeyValue {Text="PO No", Value="MPO" },
                new KeyValue {Text="HAL ID", Value="HALID" },
                new KeyValue {Text="Vendor ID", Value="VendorId" },
                new KeyValue {Text="First Start Date", Value="FirstAnnouncedDate" },
                new KeyValue {Text="Due Date", Value="DeliveryDueDate" },
                new KeyValue {Text="Greenlight Sent", Value="GreenlightSenttoPackaging" },
                new KeyValue {Text="Greenlight Received", Value="GreenlightValidatedbyDMDM" },
                new KeyValue {Text="Asset Required", Value="LanguageType" },
                new KeyValue {Text="EST UPC", Value="ESTUPC" },
                new KeyValue {Text="VOD UPC", Value="IVODUPC" },
                new KeyValue {Text="Delivery Date", Value="ActualDeliveryDate" }
            };
            orderSearch.SortOrderList = new List<KeyValue>()
            {
                new KeyValue {Text="Ascending", Value="asc" },
                new KeyValue {Text="Descending", Value="desc" }
            };
            orderSearch.GreenLights = greenLights;
            orderSearch.GreenLights = greenLights;
            return orderSearch;
        }

        /// <summary>
        /// Populates DropDown value
        /// </summary>
        /// <returns>TerritoryLanguage</returns>
        public TerritoryLanguage GetDropDownValue()
        {
            var chkValues = new List<ChkValues>(){new ChkValues()
                                                    {   Name="Audio",
                                                        IsSelected=false
                                                    },
                                                  new ChkValues()
                                                    {   Name="Audio Description",
                                                        IsSelected=false
                                                    },
                                                  new ChkValues()
                                                    {   Name="Caption",
                                                        IsSelected=false
                                                    },
                                                  new ChkValues()
                                                    {   Name="Video",
                                                        IsSelected=false
                                                    },
                                                  new ChkValues()
                                                    {   Name="Forced Subtitle",
                                                        IsSelected=false
                                                    },
                                                  new ChkValues()
                                                    {   Name="Preview Films",
                                                        IsSelected=false
                                                    },
                                                  new ChkValues()
                                                    {   Name="Sub",
                                                        IsSelected=false
                                                    }
                                                  };
            var LocalEdits = new List<string>() { "Yes", "No" };
            TerritoryLanguage territoryLanguage = _repository.GetDropDownValue();
            territoryLanguage.Assets = chkValues;
            territoryLanguage.LocalEdits = LocalEdits;
            return territoryLanguage;
        }

        /// <summary>
        /// Create new orders
        /// </summary>
        /// <param name="saveOrder">saveOrder contains all details of the new order to be created in the system</param>
        /// <returns>OrderSaveStatus</returns>
        public OrderSaveStatus CreateOrder(Order saveOrder)
        {
            
            var order = UpdateOrder(saveOrder);
            List<string> assets = order.Assets.Where(x => x.IsSelected == true).Select(x => x.Name).ToList();
            var orderStatus = _repository.CreateOrder(order, assets);
            return orderStatus;
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="error"></param>
        public void LogError(ErrorMgt error)
        {
            ErrorLog errorlog = new ErrorLog { ErrorName = error.ErrorMessage };
            _repository.LogError(errorlog);
        }

        /// <summary>
        /// Update the Existing order
        /// </summary>
        /// <param name="order">order contains all the details of an order to be updated</param>
        /// <returns></returns>
        public Order UpdateOrder(Order order)
        {
            if (!string.IsNullOrEmpty(order.RequestNumber))
            {
                var pipeLineOrder = _repository.GetPipeLineOrder(order.RequestNumber);
                if (pipeLineOrder != null)
                {
                    order.MPO = pipeLineOrder.PurchaseOrder;
                    order.HALID = pipeLineOrder.HALOrderID;
                }
                
            }
            else
            {
                order.MPO = string.Empty;
                order.HALID = string.Empty;
            }
            if (string.IsNullOrEmpty(order.OrderStatus))
            {
                if (!string.IsNullOrEmpty(order.RequestNumber) && (!string.IsNullOrEmpty(order.MPO) || !string.IsNullOrEmpty(order.HALID)))
                {
                    order.OrderStatus = OrderStaus.Processing.ToString();
                }
                else if (!string.IsNullOrEmpty(order.RequestNumber))
                {
                    order.OrderStatus = "Request Received";
                }
                else
                {
                    order.OrderStatus = OrderStaus.New.ToString();
                }
            }
            if (!order.OrderStatus.Equals(OrderStaus.Cancelled.ToString()) && !order.OrderStatus.Equals(OrderStaus.Complete.ToString()))
            {
                if (!string.IsNullOrEmpty(order.RequestNumber) && (!string.IsNullOrEmpty(order.MPO) || !string.IsNullOrEmpty(order.HALID)))
                {
                    order.OrderStatus = OrderStaus.Processing.ToString();
                }
                else if (!string.IsNullOrEmpty(order.RequestNumber))
                {
                    order.OrderStatus = "Request Received";
                }
                else
                {
                    order.OrderStatus = OrderStaus.New.ToString();
                }
            }
            return order;
        }

        public void LockOrder(int orderId, int userId)
        {
            _repository.LockOrder(orderId,userId);
        }

        public void UnlockOrder(int? orderId, int userId)
        {
            _repository.UnlockOrder(orderId, userId);
        }

        public List<ImportValidationResult> Import(string filePath, int userId)
        {
            var result = _repository.Import(filePath, userId);
            //Use Adapter
            return result;
        }
    }
}

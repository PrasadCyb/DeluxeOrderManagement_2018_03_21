using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;
using System.Data.SqlClient;

namespace DeluxeOM.Repository
{
    /// <summary>
    /// Contains all methods for Order management
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="whrSQLquery"></param>
        /// <returns></returns>
        public List<Order> GetOrders(string whrSQLquery)
        {
            var _context = new DeluxeOrderManagementEntities();
            var orders = _context.usp_Get_Orders(whrSQLquery, string.Empty).Select(x =>
                                                          new Order
                                                          {
                                                              AnnouncemntId = x.AnnouncementId,
                                                              OrderId = x.OrderID,
                                                              //VId = x.VId,
                                                              Title = x.Title,
                                                              OrderStatus = x.OrderStatus,
                                                              OrderCategory = x.Category,
                                                              Region = x.Region,
                                                              Territory = x.Territory,
                                                              Language = x.Language,
                                                              RequestNumber = x.RequestNumber,
                                                              MPO = x.MPO,
                                                              HALID = x.HALID,
                                                              VendorId = x.VendorId,
                                                              FirstStartDate = x.FirstStartDate != null ? x.FirstStartDate.Value.ToString("MM/dd/yyyy") : null,
                                                              FirstAnnouncementDate = x.FirstAnnouncedDate != null ? x.FirstAnnouncedDate.Value.ToString("MM/dd/yyyy") : null,
                                                              DueDate = x.DeliveryDueDate != null ? x.DeliveryDueDate.Value.ToString("MM/dd/yyyy") : null,
                                                              GreenlightSent = x.GreenlightSenttoPackaging != null ? x.GreenlightSenttoPackaging.Value.ToString("MM/dd/yyyy") : null,
                                                              GreenlightReceived = x.GreenlightValidatedbyDMDM != null ? x.GreenlightValidatedbyDMDM.Value.ToString("MM/dd/yyyy") : null,
                                                              DeliveryDate= x.ActualDeliveryDate != null ? x.ActualDeliveryDate.Value.ToString("MM/dd/yyyy") : null,
                                                              AssetRequired = x.LanguageType,
                                                              ESTUPC = x.ESTUPC,
                                                              VODUPC = x.IVODUPC,
                                                              LockedBy=x.LockedBy,
                                                              LockedOn=x.LockedOn,
                                                              OrderType = x.OrderType,
                                                              IsLocked =x.IsLocked
                                                              //OrderType=x.OrderType

                                                          }).Distinct().OrderBy(x => x.OrderId).ToList();

            return orders;
        }


        /// <summary>
        /// Search orders based on filtered criteria
        /// </summary>
        /// <param name="whrSQLquery">Filters values concatenated into a string</param>
        /// <param name="channel">channel filter</param>
        /// <returns>List of orders</returns>
        public List<Order> SearchOrders(string whrSQLquery, string channel)
        {
            var _context = new DeluxeOrderManagementEntities();
            var orders = _context.usp_Get_Orders(whrSQLquery, channel).Select(x =>
                                                       new Order
                                                       {
                                                           AnnouncemntId = x.AnnouncementId,
                                                           OrderId = x.OrderID,
                                                           //VId = x.VId,
                                                           Title = x.Title,
                                                           OrderStatus = x.OrderStatus,
                                                           OrderCategory = x.Category,
                                                           Region = x.Region,
                                                           Territory = x.Territory,
                                                           Language = x.Language,
                                                           RequestNumber = x.RequestNumber,
                                                           MPO = x.MPO,
                                                           HALID = x.HALID,
                                                           VendorId = x.VendorId,
                                                           FirstStartDate = x.FirstStartDate != null ? x.FirstStartDate.Value.ToString("MM/dd/yyyy") : null,
                                                           FirstAnnouncementDate = x.FirstAnnouncedDate != null ? x.FirstAnnouncedDate.Value.ToString("MM/dd/yyyy") : null,
                                                           DueDate = x.DeliveryDueDate != null ? x.DeliveryDueDate.Value.ToString("MM/dd/yyyy") : null,
                                                           GreenlightSent = x.GreenlightSenttoPackaging != null ? x.GreenlightSenttoPackaging.Value.ToString("MM/dd/yyyy") : null,
                                                           GreenlightReceived = x.GreenlightValidatedbyDMDM != null ? x.GreenlightValidatedbyDMDM.Value.ToString("MM/dd/yyyy") : null,
                                                           DeliveryDate = x.ActualDeliveryDate != null ? x.ActualDeliveryDate.Value.ToString("MM/dd/yyyy") : null,
                                                           AssetRequired = x.LanguageType,
                                                           ESTUPC = x.ESTUPC,
                                                           VODUPC = x.IVODUPC,
                                                           LockedBy = x.LockedBy,
                                                           LockedOn = x.LockedOn,
                                                           OrderType = x.OrderType,
                                                           IsLocked = x.IsLocked
                                                           //OrderType=x.OrderType

                                                       }).Distinct().ToList();

            return orders;
        }

        /// <summary>
        /// Search order for edit
        /// </summary>
        /// <param name="whrSQLquery">Filters values concatenated into a string</param>
        /// <returns>Order to edit</returns>
        public Order SearchEditOrder(string whrSQLquery)
        {
            var _context = new DeluxeOrderManagementEntities();
            var order = _context.usp_Get_Orders(whrSQLquery, string.Empty).Select(x =>
                                                        new Order
                                                        {
                                                            AnnouncemntId = x.AnnouncementId,
                                                            OrderId = x.OrderID,
                                                            //VId = x.VId,
                                                            Title = x.Title,
                                                            OrderStatus = x.OrderStatus,
                                                            OrderCategory = x.Category,
                                                            Region = x.Region,
                                                            Territory = x.Territory,
                                                            Language = x.Language,
                                                            RequestNumber = x.RequestNumber,
                                                            MPO = x.MPO,
                                                            HALID = x.HALID,
                                                            VendorId = x.VendorId,
                                                            FirstStartDate = x.FirstStartDate != null ? x.FirstStartDate.Value.ToString("MM/dd/yyyy") : null,
                                                            FirstAnnouncementDate = x.FirstAnnouncedDate != null ? x.FirstAnnouncedDate.Value.ToString("MM/dd/yyyy") : null,
                                                            DueDate = x.DeliveryDueDate != null ? x.DeliveryDueDate.Value.ToString("MM/dd/yyyy") : null,
                                                            GreenlightSent = x.GreenlightSenttoPackaging != null ? x.GreenlightSenttoPackaging.Value.ToString("MM/dd/yyyy") : null,
                                                            GreenlightReceived = x.GreenlightValidatedbyDMDM != null ? x.GreenlightValidatedbyDMDM.Value.ToString("MM/dd/yyyy") : null,
                                                            DeliveryDate = x.ActualDeliveryDate != null ? x.ActualDeliveryDate.Value.ToString("MM/dd/yyyy") : null,
                                                            AssetRequired = x.LanguageType,
                                                            ESTUPC = x.ESTUPC,
                                                            VODUPC = x.IVODUPC,
                                                            LockedBy = x.LockedBy,
                                                            LockedOn = x.LockedOn,
                                                            OrderType = x.OrderType,
                                                            IsLocked = x.IsLocked
                                                            //OrderType=x.OrderType

                                                        }).FirstOrDefault();
            order.OrderTypes = _context.OrderGrids.Where(x=>x.FileType!=null).Select(x => x.FileType.ToString()).Distinct().ToList();

            return order;
        }

        /// <summary>
        /// Edit and save selected order
        /// </summary>
        /// <param name="order">order contains all details of the order which is selected for edit</param>
        /// <returns>True if order updated else false</returns>
        public bool EditOrder(Order order)
        {
            bool status = false;
            var _context = new DeluxeOrderManagementEntities();
            VID vid = new VID();

            var announcement = (from ann in _context.AnnouncementGrids
                                where ann.Id==order.AnnouncemntId
                                select ann).FirstOrDefault();
            var vidEty = (from vids in _context.VIDs
                          where (vids.VendorId == order.VendorId || string.IsNullOrEmpty(vids.VendorId)) && vids.VideoVersion == announcement.VideoVersion
                          select vids).FirstOrDefault();
            if (vidEty == null)
            {
                vid.VIDStatus = "PRIMARY";
                vid.VendorId = order.VendorId;
                vid.VideoVersion = announcement.VideoVersion;
                vid.TitleName = announcement.Title;
                vid.TitleCategory = order.OrderCategory;
                vid.EditName = announcement.LocalEdit;
                vid.CreatedOn = DateTime.UtcNow;
                vid.ModifiedOn = DateTime.UtcNow;
                vid.CreatedBy = "Deluxe";
                vid.ModifiedBy = "Deluxe";
                _context.VIDs.Add(vid);
                status = true;
            }
            else
            {
                vidEty.VendorId = order.VendorId;
                status = true;
            }
        
            var ety = _context.OrderGrids.Where(x => x.Id == order.OrderId).FirstOrDefault();
            if (ety != null)
            {
                ety.RequestNumber = order.RequestNumber;
                ety.HALID = order.HALID;
                ety.MPO = order.MPO;
                ety.DeliveryDueDate = ParseDate(order.DueDate);
                ety.GreenlightSenttoPackaging = ParseDate(order.GreenlightSent);
                ety.GreenlightValidatedbyDMDM = ParseDate(order.GreenlightReceived);
                ety.ActualDeliveryDate = ParseDate(order.DeliveryDate);
                ety.ESTUPC = order.ESTUPC;
                ety.IVODUPC = order.VODUPC;
                ety.Category = order.OrderCategory;
                ety.OrderStatus = order.OrderStatus;
                ety.VendorId = order.VendorId;
                ety.FileType = order.OrderType;
                //ety.VIDId = (orders.Count == 1 /*|| orderExceptCurrent.Count==1*/) ? vidEty.Id : vid.Id;
                status = true;

            }
            if (status)
            {
                _context.SaveChanges();
                if (string.IsNullOrEmpty(order.OrderStatus) || !order.OrderStatus.Equals("Other Vendor"))
                {
                    _context.usp_UpdateOG_Status(order.OrderId.ToString());
                }
            }
            return status;
        }

        /// <summary>
        /// Pupulate all dropdown value on order page
        /// </summary>
        /// <returns>OrderSearch</returns>
        public OrderSearch GetSearchValue()
        {
            OrderSearch orderSearch = new OrderSearch();
            var _context = new DeluxeOrderManagementEntities();
            var selectedTitles = (from vid in _context.VIDs
                                  select  new Title() {Name=vid.TitleName,VideoVersion=vid.VideoVersion }).GroupBy(x=>x.VideoVersion).Select(g=>g.FirstOrDefault()).OrderBy(x=>x.Name).ToList();
            var titleCategory = (from og in _context.OrderGrids
                                 select og.Category.ToString()).Distinct().OrderBy(x => x).ToList();
            var contentProviders = (from customer in _context.Customers
                                    where customer.Type == (int)Customers.ContentProvider
                                    select customer.Name.ToString()
                                  ).Distinct().ToList();
            var contentDistributors = (from customer in _context.Customers
                                       where customer.Type == (int)Customers.ContentDistributor
                                       select customer.Name.ToString()
                                  ).Distinct().ToList();






            //var localEdit = (from ann in _context.AnnouncementGrids
            //                 select new ChkValues
            //                 {
            //                     Name = ann.LocalEdit,
            //                     IsSelected = false
            //                 }).Distinct().ToList();
            //var orderStatus = (from order in _context.OrderGrids
            //                   where order.OrderStatus != "N/A" && order.OrderStatus != null
            //                   select new ChkValues
            //                   {
            //                       Name = order.OrderStatus,
            //                       IsSelected = false
            //                   }).Distinct().ToList();
            //var salesChannel= (from order in _context.SalesChannels
            //                   select new ChkValues
            //                   {
            //                       Name = order.Name,
            //                       IsSelected = false
            //                   }).Distinct().ToList();






            var orderStatus = (from order in _context.OrderGrids
                               where order.OrderStatus != "N/A" && order.OrderStatus != null
                               select order.OrderStatus.ToString()).Distinct().OrderBy(x => x).ToList();
            var localEdits = (from ann in _context.AnnouncementGrids
                              select ann.LocalEdit.ToString()).Distinct().ToList();
            var salesChannels = (from saleChannel in _context.SalesChannels
                                 select saleChannel.Name.ToString()).Distinct().ToList();
            var territories = (from trr in _context.Territories
                               select trr.WBTerritory.ToString()).Distinct().OrderBy(x => x).ToList();
            var orderTypes = (from order in _context.OrderGrids
                              where order.FileType != null
                              select order.FileType.ToString()).Distinct().OrderBy(x => x).ToList();

            orderSearch.SelectedTitles = selectedTitles;
            orderSearch.EditTypes = titleCategory;
            orderSearch.ContentProviders = contentProviders;
            orderSearch.ContentDistributors = contentDistributors;
            orderSearch.OrderStatuses = orderStatus;
            orderSearch.LocalEdits = localEdits;
            orderSearch.MediaTypes = salesChannels;
            orderSearch.Territories = territories;
            orderSearch.OrderTypes = orderTypes;
            return orderSearch;
        }

        /// <summary>
        /// Populate dropdown values on create order page
        /// </summary>
        /// <returns></returns>
        public TerritoryLanguage GetDropDownValue()
        {
            var _context = new DeluxeOrderManagementEntities();
            TerritoryLanguage territoryLanguage = new TerritoryLanguage();
            var orderCategory = (from og in _context.OrderGrids
                                 where !string.IsNullOrEmpty(og.Category)
                                 select og.Category.ToString()
                                 ).Distinct().ToList();
            var territory = _context.Territories.Select(x => new DeluxeOM.Models.Territory()
            {
                TerritoryID = x.Id,
                TerritoryName = x.WBTerritory
            }).Distinct().OrderBy(x => x.TerritoryName).ToList();
            var language = _context.Languages.Select(x => new DeluxeOM.Models.Language()
            {
                LanguageID = x.Id,
                LanguageName = x.Name
            }).Distinct().OrderBy(x => x.LanguageName).ToList();

            var title = _context.VIDs.Select(x => new DeluxeOM.Models.Title()
            {
                VideoVersion = x.VideoVersion,
                Name = x.TitleName
            }).GroupBy(x => x.VideoVersion).Select(g => g.FirstOrDefault()).OrderBy(x => x.Name).ToList();
            var orderTypes= _context.OrderGrids.Where(x => x.FileType != null).Select(x => x.FileType.ToString()).Distinct().ToList();
            territoryLanguage.Territory = territory;
            territoryLanguage.Language = language;
            territoryLanguage.Title = title;
            territoryLanguage.orderCategory = orderCategory;
            territoryLanguage.OrderTypes = orderTypes;
            return territoryLanguage;
        }

        /// <summary>
        /// Parse date to "MM/dd/yyyy" to avoid the environment issue
        /// </summary>
        /// <param name="date">cantains date to parse</param>
        /// <returns>date</returns>
        private DateTime? ParseDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return null;
            }
            else
            {
                return DateTime.ParseExact(date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// Create Order in the system
        /// </summary>
        /// <param name="order">Details of new order</param>
        /// <param name="assets">List of assets for new order</param>
        /// <returns>Order creation status</returns>
        public OrderSaveStatus CreateOrder(Order order, List<string> assets)
        {
            OrderSaveStatus orderStatus = new OrderSaveStatus();
            var _context = new DeluxeOrderManagementEntities();
            int languageId = Convert.ToInt32(order.Language);
            int territoryId = Convert.ToInt32(order.Territory);
            var announcement = (from ann in _context.AnnouncementGrids
                                where ann.VideoVersion == order.Title &&
                                      ann.LanguageId == languageId &&
                                      ann.TerritoryId == territoryId
                                select ann).FirstOrDefault();

            if (announcement != null)
            {
                var orders = (from og in _context.OrderGrids
                              where og.AnnouncementId == announcement.Id && assets.Contains(og.LanguageType)
                              select og).ToList();
                var vids = (from v in _context.VIDs
                            where (v.VendorId == order.VendorId || string.IsNullOrEmpty(v.VendorId)) && v.VideoVersion == announcement.VideoVersion
                            select v
                          ).FirstOrDefault();
                VID vid = null;
                //if (orders.Count == 0 || orders == null)
                //{
                    if (vids == null)
                    {
                        vid = new VID();
                        vid.VIDStatus = "PRIMARY";
                        vid.VendorId = order.VendorId;
                        vid.VideoVersion = announcement.VideoVersion;
                        vid.TitleName = announcement.Title;
                        vid.TitleCategory = order.OrderCategory;
                        vid.EditName = announcement.LocalEdit;
                        vid.CreatedOn = DateTime.UtcNow;
                        vid.ModifiedOn = DateTime.UtcNow;
                        vid.CreatedBy = "Deluxe";
                        vid.ModifiedBy = "Deluxe";
                        _context.VIDs.Add(vid);
                        //_context.SaveChanges();
                    }
                    else
                    {
                        vids.VendorId = order.VendorId;
                    }
                    foreach (var asset in assets)
                    {
                        OrderGrid orderGrid = new OrderGrid()
                        {
                            AnnouncementId = announcement.Id,
                            OrderStatus = order.OrderStatus,
                            MPO = order.MPO,
                            HALID = order.HALID,
                            Category = order.OrderCategory,
                            RequestNumber = order.RequestNumber,
                            DeliveryDueDate = ParseDate(order.DueDate),
                            GreenlightSenttoPackaging = ParseDate(order.GreenlightSent),
                            GreenlightValidatedbyDMDM = ParseDate(order.GreenlightReceived),
                            ActualDeliveryDate = ParseDate(order.DeliveryDate),
                            ESTUPC = order.ESTUPC,
                            IVODUPC = order.VODUPC,
                            LanguageType = asset,
                            FileType = order.OrderType,
                            CustomerId = 1,
                            VendorId = order.VendorId//(vid != null) ? vid.VendorId : vids.FirstOrDefault().VendorId

                        };
                        _context.OrderGrids.Add(orderGrid);
                        _context.SaveChanges();
                        _context.usp_UpdateOG_Status(orderGrid.Id.ToString());
                    }
                    orderStatus.ErrorMessage = Constant.orderSaved;
                    orderStatus.IsSaved = true;
                    orderStatus.IsAnnouncementExist = true;
                //}
                //else
                //{
                //    orderStatus.ErrorMessage = Constant.orderExist;
                //    orderStatus.IsSaved = false;
                //    orderStatus.IsAnnouncementExist = true;
                //}
            }
            else if (announcement == null && !string.IsNullOrEmpty(order.LocalEdit))
            {
                string title = _context.VIDs.Where(x => x.VideoVersion == order.Title).Select(x => x.TitleName).FirstOrDefault();
                AnnouncementGrid newAnnouncement = new AnnouncementGrid()
                {
                    VideoVersion = order.Title,
                    Title = title,
                    LanguageId = languageId,
                    TerritoryId = territoryId,
                    LocalEdit = order.LocalEdit,
                    CustomerId = 1,
                    JObId = 0

                };
                _context.AnnouncementGrids.Add(newAnnouncement);
                _context.SaveChanges();
                var orders = (from og in _context.OrderGrids
                              where og.AnnouncementId == newAnnouncement.Id && assets.Contains(og.LanguageType)
                              select og).ToList();
                var vids = (from v in _context.VIDs
                            where v.VendorId == order.VendorId && v.VideoVersion == newAnnouncement.VideoVersion
                            select v
                          ).FirstOrDefault();
                VID vid = null;
                //if (orders.Count == 0 || orders == null)
                //{
                    if (vids == null)
                    {
                        vid = new VID();
                        vid.VIDStatus = "PRIMARY";
                        vid.VendorId = order.VendorId;
                        vid.VideoVersion = newAnnouncement.VideoVersion;
                        vid.TitleName = newAnnouncement.Title;
                        vid.TitleCategory = order.OrderCategory;
                        vid.EditName = newAnnouncement.LocalEdit;
                        vid.CreatedOn = DateTime.UtcNow;
                        vid.ModifiedOn = DateTime.UtcNow;
                        vid.CreatedBy = "Deluxe";
                        vid.ModifiedBy = "Deluxe";
                        _context.VIDs.Add(vid);
                        _context.SaveChanges();
                    }
                    else
                    {
                        vids.VendorId = order.VendorId;
                    }
                    foreach (var asset in assets)
                    {
                        OrderGrid orderGrid = new OrderGrid()
                        {
                            AnnouncementId = newAnnouncement.Id,
                            OrderStatus = order.OrderStatus,
                            MPO = order.MPO,
                            HALID = order.HALID,
                            Category = order.OrderCategory,
                            RequestNumber = order.RequestNumber,
                            DeliveryDueDate = ParseDate(order.DueDate),
                            GreenlightSenttoPackaging = ParseDate(order.GreenlightSent),
                            GreenlightValidatedbyDMDM = ParseDate(order.GreenlightReceived),
                            ActualDeliveryDate = ParseDate(order.DeliveryDate),
                            ESTUPC = order.ESTUPC,
                            IVODUPC = order.VODUPC,
                            LanguageType = asset,
                            CustomerId = 1,
                            VendorId = order.VendorId//(vid != null) ? vid.VendorId : vids.FirstOrDefault().VendorId

                        };
                        _context.OrderGrids.Add(orderGrid);
                        _context.SaveChanges();
                        _context.usp_UpdateOG_Status(orderGrid.Id.ToString());
                    }
                    orderStatus.ErrorMessage = Constant.announcementNotExist;
                    orderStatus.IsSaved = true;
                    orderStatus.IsAnnouncementExist = false;
                //}
                //else
                //{
                //    orderStatus.ErrorMessage = Constant.orderExist;
                //    orderStatus.IsSaved = false;
                //    orderStatus.IsAnnouncementExist = false;
                //}
            }
            else
            {
                orderStatus.ErrorMessage = Constant.announcementNotExist;
                orderStatus.IsSaved = false;
                orderStatus.IsAnnouncementExist = false;
            }
            return orderStatus;
        }


        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="error"></param>
        public void LogError(ErrorLog error)
        {

            var _context = new DeluxeOrderManagementEntities();
            _context.ErrorLogs.Add(error);
            _context.SaveChanges();
        }


        /// <summary>
        /// Search pieline order
        /// </summary>
        /// <param name="requestNumber">CableLabsAssetID</param>
        /// <remark>Search pipeline order to auto populate HAL Id and PO</remark>
        /// <returns>PipelineOrder</returns>
        public PipelineOrder GetPipeLineOrder(string requestNumber)
        {
            var _context = new DeluxeOrderManagementEntities();
            var pipeLineOrder = (from po in _context.PipelineOrders
                                 where po.CableLabsAssetID == requestNumber
                                 select po
                               ).FirstOrDefault();
            return pipeLineOrder;
        }

        /// <summary>
        /// Lock order
        /// </summary>
        /// <param name="orderId">Order to lock</param>
        /// <param name="userId">Current userId</param>
        /// <remark>This method lock order for other user</remark>
        public void LockOrder(int orderId, int userId)
        {
            var _context = new DeluxeOrderManagementEntities();
            var order = _context.OrderGrids.Where(x => x.Id == orderId).FirstOrDefault();
            order.IsLocked = true;
            order.LockedBy = userId;
            order.LockedOn = DateTime.UtcNow;
            _context.SaveChanges();
        }


        /// <summary>
        /// Unlock order
        /// </summary>
        /// <param name="orderId">Order to unlock</param>
        /// <param name="userId">Current userId</param>
        /// <remark>This method unlock order for other user on click of save or cancel order</remark>
        public void UnlockOrder(int? orderId, int userId)
        {
            var _context = new DeluxeOrderManagementEntities();
            if (orderId != null)
            {
                var order = _context.OrderGrids.Where(x => x.Id == orderId && x.LockedBy == userId).FirstOrDefault();
                if (order != null)
                {
                    order.IsLocked = false;
                    order.LockedBy = null;
                    order.LockedOn = null;
                }
            }
            else
            {
                var order = _context.OrderGrids.Where(x => x.LockedBy == userId).ToList();
                foreach (var o in order)
                {
                    o.IsLocked = false;
                    o.LockedBy = null;
                    o.LockedOn = null;
                }
            }

            _context.SaveChanges();
        }


        /// <summary>
        /// Import Order list into system
        /// </summary>
        /// <param name="filePath">downloaded file path</param>
        /// <param name="userId">current userId</param>
        /// <returns>Validation result from BulkUpdate_Validations table</returns>
        public List<ImportValidationResult> Import(string filePath, int userId)
        {
            var _context = new DeluxeOrderManagementEntities();

            var fileParam = new SqlParameter
            {
                ParameterName = "filePath",
                Value = filePath
            };
            var userParam = new SqlParameter
            {
                ParameterName = "userId",
                Value = userId
            };
            return _context.Database.SqlQuery<BulkUpdate_Validations>("exec usp_BulkUpdateOrder @filePath, @userId", fileParam, userParam)
                .Select(x =>
                            new ImportValidationResult
                            {
                                Id = x.KeyId,
                                ColumnName = x.ColumnName,
                                ColumnValue = x.ColumnValue,
                                ValidationLevel = x.ValidationLevel,
                                ValidationMessage = x.ValidationMessage
                            }).ToList<ImportValidationResult>();

            //return _context.usp_BulkUpdateOrder(filePath, userId).Select(x =>
            //                                              new ImportValidationResult
            //                                              {
            //                                                Id = x.ID,
            //                                                ColumnName = x.ColumnName,
            //                                                ColumnValue = x.ColumnValue,
            //                                                ValidationLevel = x.ValidationLevel,
            //                                                ValidationMessage = x.ValidationMessage
            //                                              }).ToList(); 
            
        }
    }
}

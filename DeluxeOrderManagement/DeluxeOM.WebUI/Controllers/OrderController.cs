using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeluxeOM.Services;
using PagedList;
using DeluxeOM.Models;
using DeluxeOM.Utils.Config;
using System.Configuration;
using System.IO;
using DeluxeOM.Utils;
using System.Text;

namespace DeluxeOM.WebUI.Controllers
{
    /// <summary>
    /// Contains all methods for Order management
    /// </summary>
    [DeluxeOMAuthorize(Roles = "ReportAdmin")]
    public class OrderController : BaseController
    {
        public readonly int lockExpiryMin=App.Config.OrderUnlockPeriod;
        private IOrderService _service;
        ILogger _logger = null;
        /// <summary>
        /// Create instance for OrderService and EventLogger
        /// </summary>
        public OrderController()
        {
            _service = new OrderService();
            _logger = new EventLogger("OrderMgtController");
        }
        // GET: Oder
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Populate all filter values on page load
        /// </summary>
        /// <param name="orderSearch"></param>
        /// <param name="pageNumber"></param>
        /// <returns>Order View</returns>
        [HttpGet]
        public ActionResult Orders(OrderSearch orderSearch, int? pageNumber)
        {
            try
            {
                //_logger.Info("Orders HTTPGet started");
                if (pageNumber == 0)
                {
                    pageNumber = null;
                }
                OrderService _order = new OrderService();
                OrderSearch searchValues = _order.GetSearchValues();

                //to persist values between pages
                OrderSearch persistedValues = TempData.Peek("orderSearch") as OrderSearch;
                if (persistedValues != null)
                {
                    searchValues.SelectedTitle = persistedValues.SelectedTitle;
                    searchValues.ContentProvider = persistedValues.ContentProvider;
                    searchValues.ContentDistributor = persistedValues.ContentDistributor; ;
                    searchValues.EditType = persistedValues.EditType;
                    searchValues.LocalEdit = persistedValues.LocalEdit;
                    searchValues.OrderStatus = persistedValues.OrderStatus;
                    searchValues.GreenLightSent = persistedValues.GreenLightSent;
                    searchValues.GreenLightReceived = persistedValues.GreenLightReceived;
                    searchValues.Territory = persistedValues.Territory;
                    searchValues.MediaType = persistedValues.MediaType;
                    searchValues.StartDate = persistedValues.StartDate;
                    searchValues.EndDate = persistedValues.EndDate;
                    searchValues.SortBy = persistedValues.SortBy;
                    searchValues.SortOrder = persistedValues.SortOrder;
                    searchValues.OrderType = persistedValues.OrderType;
                }
                var orders = _order.SearchOrders(searchValues).ToPagedList(pageNumber ?? 1, 10);

                searchValues.SelectedTitleList = searchValues.SelectedTitles.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.VideoVersion
                }).Distinct().ToList();
                searchValues.TerritoryList = searchValues.Territories.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                OrderMgt orderMgt = new OrderMgt();
                orderMgt.orders = orders;
                orderMgt.OrderSearch = searchValues;
                if (TempData["SavedStatus"] != null)
                {
                    orderMgt.SavedStatus = Convert.ToBoolean(TempData["SavedStatus"]);
                }
                orderMgt.UserId = CurrentUser.UserId;
                orderMgt.OrderUnlockPeriod = lockExpiryMin;

                //_logger.Info("Orders HTTPGet completed");

                return View(orderMgt);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message + (ex.InnerException != null ? ex.InnerException.ToString() : string.Empty));
                return View("Error");
            }
        }

        /// <summary>
        /// Search order on basis of selected filter criteria
        /// </summary>
        /// <param name="orderSearch">orderSearch contains filter values to search orders from a database</param>
        /// <param name="pageNumber">pageNumber contains which page to be displayed on the UI</param>
        /// <returns>Order View</returns>
        [HttpGet]
        public ActionResult SearchOrder(OrderSearch orderSearch, int? pageNumber)
        {
            //_logger.Info("SearchOrder HTTPGet started");

            OrderService _order = new OrderService();
            try
            {
                if (pageNumber == 0)
                {
                    pageNumber = null;
                }
                orderSearch.Territory = string.IsNullOrEmpty(orderSearch.TerritoryConcat) ? orderSearch.Territory : orderSearch.TerritoryConcat.Split('|');
                var orders = _order.SearchOrders(orderSearch).ToPagedList(pageNumber ?? 1, 10);

                TempData["orderSearch"] = orderSearch;
                OrderSearch searchValues = _order.GetSearchValues();

                searchValues.SelectedTitleList = searchValues.SelectedTitles.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.VideoVersion
                }).Distinct().ToList();
                searchValues.TerritoryList = searchValues.Territories.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                OrderMgt orderMgt = new OrderMgt();
                orderMgt.orders = orders;
                orderMgt.OrderSearch = orderSearch;
                orderMgt.OrderSearch.ContentDistributors = searchValues.ContentDistributors;
                orderMgt.OrderSearch.ContentProviders = searchValues.ContentProviders;
                orderMgt.OrderSearch.SelectedTitleList = searchValues.SelectedTitleList;
                orderMgt.OrderSearch.EditTypes = searchValues.EditTypes;
                orderMgt.OrderSearch.LocalEdits = searchValues.LocalEdits;
                orderMgt.OrderSearch.OrderStatuses = searchValues.OrderStatuses;
                orderMgt.OrderSearch.MediaTypes = searchValues.MediaTypes;
                orderMgt.OrderSearch.GreenLights = searchValues.GreenLights;
                orderMgt.OrderSearch.GreenLights = searchValues.GreenLights;
                orderMgt.OrderSearch.TerritoryList = searchValues.TerritoryList;
                orderMgt.OrderSearch.SortByList = searchValues.SortByList;
                orderMgt.OrderSearch.SortOrderList = searchValues.SortOrderList;
                orderMgt.OrderSearch.OrderTypes = searchValues.OrderTypes;
                orderMgt.UserId = CurrentUser.UserId;
                orderMgt.OrderUnlockPeriod = lockExpiryMin;

                //_logger.Info("SearchOrder HTTPGet completed");

                return View("Orders", orderMgt);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message + (ex.InnerException != null ? ex.InnerException.ToString() : string.Empty));
                return View("Error");
            }
        }


        /// <summary>
        /// Search order for edit
        /// </summary>
        /// <param name="id">order id for edit</param>
        /// <param name="annId">announcement id for which order is created</param>
        /// <returns>EditOrder View</returns>
        [DeluxeOMAuthorize(Roles = "ManagerAdmin")]
        [HttpGet]
        public ActionResult EditOrder(int id, int annId)
        {
            try
            {
                OrderSearch persistedValues = TempData.Peek("orderSearch") as OrderSearch;

                OrderService _order = new OrderService();
                var order = _order.SearchEditOrder(id, annId);
                if (order.IsLocked == true && order.LockedBy != CurrentUser.UserId)
                {
                    if (order.LockedOn != null && order.LockedOn.Value.AddMinutes(lockExpiryMin) < DateTime.UtcNow)
                    {
                        order.IsAlloweToEdit = true;
                        _order.LockOrder(id, CurrentUser.UserId);
                    }
                    else
                    {
                        order.IsAlloweToEdit = false;
                    }
                }
                else
                {
                    order.IsAlloweToEdit = true;
                    _order.LockOrder(id, CurrentUser.UserId);
                }
                if (CurrentUser.IsInRole("SystemAdmin"))
                {
                    order.IsAllowedToDelete = true;
                }
                return View(order);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message + (ex.InnerException != null ? ex.InnerException.ToString() : string.Empty));
                return View("Error");
            }
        }


        /// <summary>
        /// Edit and save selected order
        /// </summary>
        /// <param name="order">order contains all details of the order which is selected for edit</param>
        /// <returns>Order View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DeluxeOMAuthorize(Roles = "ManagerAdmin")]
        public ActionResult EditOrder(Order order)
        {
            try
            {
                
                OrderService _order = new OrderService();
                bool status = _order.EditOrder(order);
                _order.UnlockOrder(order.OrderId,CurrentUser.UserId);
                TempData["SavedStatus"] = status;
                OrderSearch orderSearch = TempData.Peek("orderSearch") as OrderSearch;
                return RedirectToAction("SearchOrder", orderSearch);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message + (ex.InnerException != null ? ex.InnerException.ToString() : string.Empty));
                return View("Error");
            }
        }

        /// <summary>
        /// Cancel edit order
        /// </summary>
        /// <param name="id">order id which is canceled from editing</param>
        /// <returns>Order View</returns>
        [HttpGet]
        public ActionResult CancelEdit(int id)
        {
            try
            {
                OrderService _order = new OrderService();
                _order.UnlockOrder(id, CurrentUser.UserId);
                OrderSearch orderSearch = TempData.Peek("orderSearch") as OrderSearch;
                return RedirectToAction("SearchOrder", orderSearch);
            }
            catch(Exception ex)
            {
                //_logger.Error(ex.Message + (ex.InnerException != null ? ex.InnerException.ToString() : string.Empty));
                return View("Error");
            }
        }


        /// <summary>
        /// Create new orders
        /// </summary>
        /// <remark>This method populate all dropdown value and render create order view on UI</remark>
        /// <returns>CreateOrder View</returns>
        [DeluxeOMAuthorize(Roles = "ManagerAdmin")]
        [HttpGet]
        public ActionResult CreateOrder()
        {
            OrderSearch persistedValues = TempData.Peek("orderSearch") as OrderSearch;

            OrderService _order = new OrderService();
            Order order = new Order();
            var dropDownValues = _order.GetDropDownValue();
            order.Territories = dropDownValues.Territory.Select(x => new SelectListItem()
            {
                Text = x.TerritoryName,
                Value = x.TerritoryID.ToString()
            }).Distinct().ToList();

            order.Languages = dropDownValues.Language.Select(x => new SelectListItem()
            {
                Text = x.LanguageName,
                Value = x.LanguageID.ToString()
            }).Distinct().ToList();

            order.Titles = dropDownValues.Title.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.VideoVersion
            }).Distinct().ToList();
            order.Assets = dropDownValues.Assets;
            order.OrderCategories = dropDownValues.orderCategory;
            order.OrderTypes = dropDownValues.OrderTypes;
            order.LocalEdits = dropDownValues.LocalEdits;
            return View(order);
        }


        /// <summary>
        /// Create new orders
        /// </summary>
        /// <param name="saveOrder">saveOrder contains all details of the new order to be created in the system</param>
        /// <returns>Order View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(Order saveOrder)
        {
            OrderService _order = new OrderService();
            var orderStatus = _order.CreateOrder(saveOrder);
            OrderSearch orderSearch = TempData.Peek("orderSearch") as OrderSearch;
            if (orderStatus.IsSaved)
            {
                TempData["SavedStatus"] = orderStatus.IsSaved;
                
                return RedirectToAction("Orders", orderSearch);
            }
            else
            {
                Order order = new Order();
                var dropDownValues = _order.GetDropDownValue();
                order.Territories = dropDownValues.Territory.Select(x => new SelectListItem()
                {
                    Text = x.TerritoryName,
                    Value = x.TerritoryID.ToString()
                }).Distinct().ToList();

                order.Languages = dropDownValues.Language.Select(x => new SelectListItem()
                {
                    Text = x.LanguageName,
                    Value = x.LanguageID.ToString()
                }).Distinct().ToList();

                order.Titles = dropDownValues.Title.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.VideoVersion
                }).Distinct().ToList();
                order.Assets = dropDownValues.Assets;
                order.OrderCategories = dropDownValues.orderCategory;
                order.OrderTypes = dropDownValues.OrderTypes;
                order.LocalEdits = dropDownValues.LocalEdits;
                order.IsAnnouncementExist = orderStatus.IsAnnouncementExist;
                ModelState.AddModelError("", orderStatus.ErrorMessage);
                
                return View(order);
            }
        }


        /// <summary>
        /// Export orders to .xlsx file 
        /// </summary>
        /// <param name="orderSearch">orderSearch contains filter values to export orders to .xlsx file</param>
        /// <returns>FileResult which help to download file in browser</returns>
        [HttpGet]
        public FileResult ExportOrder(OrderSearch orderSearch)
        {
            ExportReportService _export = new ExportReportService();
            DownLoadFile downloadFile = _export.GenerateOrderReport(orderSearch);
            //string path = ConfigurationManager.AppSettings["directoryPath"];
            string path = App.Config.ReportDirectoryPath;
            //string downloadFile = string.Format(path, file);
            ////Stream fs = File.OpenRead(@"c:\testdocument.docx");
            //FileStream stream = new FileStream(downloadFile, FileMode.Open, FileAccess.Read);

            //byte[] buffer = new byte[2048];
            //byte[] bufferByte;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    int read;
            //    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        ms.Write(buffer, 0, read);
            //    }
            //    bufferByte = ms.ToArray();
            //};
            //stream.Close();
            //_export.DeleteFile(downloadFile);
            return File(downloadFile.bufferByte, "application/vnd.ms-excel", downloadFile.FileName);
        }

        /// <summary>
        /// Import Order list into system
        /// </summary>
        /// <param name="file">This parameter consists of a selected .xlsx file through the UI</param>
        /// <remark>This method calls through JQuery Ajax call and used to bulk update orders in the system</remark>
        /// <returns>Errors and Warning concatenated into the string</returns>
        [HttpPost]
        public string Import(HttpPostedFileBase file)
        {
            _logger.Info("Import() started. FileName :" + file != null ? file.FileName : string.Empty);
            string result = "Please select valid file !";
            try
            {
                var supportedTypes = new[] { "xls", "xlsx" };
                int filesize = 1024 * 5;
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    return "Only .xls|.xlsx files supported";
                }
                else if (file.ContentLength > (filesize * 1024))
                {
                    return "File size should be upto " + filesize + "KB";
                }

                if (file != null)
                {
                    string importDirectory = App.Config.ImportOrderDirectory;
                    string fileName = Path.GetFileName(file.FileName);
                    if (!Directory.Exists(importDirectory))
                    {
                        Directory.CreateDirectory(importDirectory);
                    }
                    string filePath = string.Format("{0}/{1}", importDirectory, fileName);
                    file.SaveAs(filePath);

                    _logger.Info(string.Format("File {0} saved", filePath));
                    var messages = _service.Import(filePath, this.CurrentUser.UserId);
                    result = this.buildValidations(messages);
                    return result;
                    //return "Operation completed successfully";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                result = "There is an error while importing a file.";
            }

            return result;
        }


        /// <summary>
        /// Format a list of validation into the string
        /// </summary>
        /// <param name="validations">validations contains list of validation(Errors and warning) from database</param>
        /// <returns>Errors and Warning concatenated into the string</returns>
        private string buildValidations(List<ImportValidationResult> validations)
        {
            string result = "File imported successfully.";
            //bool bErr= false;
            if (validations != null && validations.Any())
            {
                bool bErr = false;
                StringBuilder sb = new StringBuilder();
                if (validations.Any(x => x.ValidationLevel == "Error"))
                {
                    bErr = true;
                    sb.Append("Import operation has failed due to following errors. Please corerct these errors and reupload it.");
                    sb.Append("Errors :\n");
                    string errors = string.Join("\n", 
                        validations.Where(x => x.ValidationLevel == "Error")
                        .Select(y => string.Format("{0}", y.ValidationMessage)));
                    sb.Append(errors );
                }
                if (validations.Any(x => x.ValidationLevel == "Warning"))
                {
                    sb.Append(bErr ? "" : "Import operation succeeded with below warnings :\n");
                    sb.Append("\n Warnings :\n");
                    string warnings = string.Join("\n",
                        validations.Where(x => x.ValidationLevel == "Warning")
                        .Select(y => string.Format("{0}", y.ValidationMessage)));
                    sb.Append(warnings + "\n");
                }
                result = sb.ToString();
            }
            return result;
        }

    }
}
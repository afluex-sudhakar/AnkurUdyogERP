﻿using AnkurUdyogERP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AnkurUdyogERP.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            AdminDashboard model = new AdminDashboard();
            DataSet ds = model.GetDashBoardDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalUsers = ds.Tables[0].Rows[0]["TotalUsers"].ToString();
                ViewBag.BlockedUsers = ds.Tables[0].Rows[0]["BlockedUsers"].ToString();
                ViewBag.InactiveUsers = ds.Tables[0].Rows[0]["InactiveUsers"].ToString();
                ViewBag.ActiveUsers = ds.Tables[0].Rows[0]["ActiveUsers"].ToString();
            }

            List<AdminDashboard> lst = new List<AdminDashboard>();
            DataSet ds1 = model.DistributerListForAdminDashboard();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    AdminDashboard obj = new AdminDashboard();
                    obj.PK_DistributerId = dr["PK_DistributerId"].ToString();
                    obj.DistributerName = dr["DistributerName"].ToString();
                    obj.JoiningDate = dr["JoiningDate"].ToString();
                    obj.Limit = dr["Limit"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.TodayOrder = dr["TodayOrder"].ToString();
                    obj.DispatchOrder = dr["DispatchOrder"].ToString();
                    obj.PendingLimit = dr["PendingLimit"].ToString();
                    obj.TotalDispatch = dr["TotalDispatch"].ToString();
                    obj.TodayOrder = dr["TodayOrder"].ToString();

                    lst.Add(obj);
                }
                model.lstdistributerforadmin = lst;
            }

            List<AdminDashboard> lstt = new List<AdminDashboard>();
            DataSet ds2 = model.DistributerListForAdminDashboard();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds2.Tables[1].Rows)
                {
                    AdminDashboard obj = new AdminDashboard();
                    obj.PK_DistributerId = dr["PK_DistributerId"].ToString();
                    obj.DistributerName = dr["DistributerName"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.CurrentMonthOrder = dr["TodayOrder"].ToString();
                    obj.CurrentMonthDispatch = dr["DispatchOrder"].ToString();
                    obj.CurrentMonthPendency = dr["PendingLimit"].ToString();
                    lstt.Add(obj);
                }
                model.lstgeneralReport = lstt;
            }
            return View(model);
        }
        public ActionResult EmployeeList()
        {
            Employee model = new Employee();
            List<Employee> lst = new List<Employee>();
            DataSet ds = model.GetEmployeeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Employee obj = new Employee();
                    obj.PK_AdminId = dr["Pk_AdminId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Password = dr["Password"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.JoiningDate = dr["JoiningDate"].ToString();
                    obj.MobileNo = dr["Contact"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.FatherName = dr["FatherName"].ToString();
                    obj.Gender = dr["Gender"].ToString();
                    obj.Pincode = dr["PinCode"].ToString();
                    obj.State = dr["State"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.RoleName = dr["RoleName"].ToString();
                    lst.Add(obj);
                }
                model.lstEmployee = lst;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "Search")]
        [ActionName("EmployeeList")]
        public ActionResult EmployeeList(Employee model)
        {
            List<Employee> lst = new List<Employee>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetEmployeeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Employee obj = new Employee();
                    obj.PK_AdminId = dr["Pk_AdminId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Password = dr["Password"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.JoiningDate = dr["JoiningDate"].ToString();
                    obj.MobileNo = dr["Contact"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.FatherName = dr["FatherName"].ToString();
                    obj.Gender = dr["Gender"].ToString();
                    obj.Pincode = dr["PinCode"].ToString();
                    obj.State = dr["State"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.RoleName = dr["RoleName"].ToString();
                    lst.Add(obj);
                }
                model.lstEmployee = lst;
            }
            return View(model);
        }
        public ActionResult DeleteEmployee(Employee model, string Id)
        {
            try
            {
                model.AddedBy = Session["Pk_adminId"].ToString();
                model.PK_AdminId = Id;
                DataSet ds = model.DeleteEmployee();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmployeeRegistration"] = "Employee Deleted  Successfully";
                    }
                    else
                    {
                        TempData["EmployeeRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["EmployeeRegistration"] = ex.Message;
            }
            return RedirectToAction("EmployeeList", "Admin");
        }
        public ActionResult DistributerListForAdmin()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetDistributerList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DistributerId = dr["PK_DistributerId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Password = dr["Password"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.JoiningDate = dr["JoiningDate"].ToString();
                    obj.MobileNo = dr["Mobile"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Pincode = dr["PinCode"].ToString();
                    obj.State = dr["State"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.FirmName = dr["FirmName"].ToString();
                    obj.PanNo = dr["PancardNo"].ToString();
                    obj.GSTNO = dr["GSTNO"].ToString();
                    obj.Limit = dr["Limit"].ToString();
                    lst.Add(obj);
                }
                model.lstDistributer = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("DistributerListForAdmin")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DistributerListForAdmin(Master model)
        {
            List<Master> lst = new List<Master>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetDistributerList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DistributerId = dr["PK_DistributerId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Password = dr["Password"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.JoiningDate = dr["JoiningDate"].ToString();
                    obj.MobileNo = dr["Mobile"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Pincode = dr["PinCode"].ToString();
                    obj.State = dr["State"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.FirmName = dr["FirmName"].ToString();
                    obj.PanNo = dr["PancardNo"].ToString();
                    obj.GSTNO = dr["GSTNO"].ToString();
                    obj.Limit = dr["Limit"].ToString();
                    lst.Add(obj);
                }
                model.lstDistributer = lst;
            }
            return View(model);
        }
        public ActionResult DeleteDistributer(Employee model, string Id)
        {
            try
            {
                model.AddedBy = Session["Pk_adminId"].ToString();
                model.DistributerId = Id;
                DataSet ds = model.DeleteDistributer();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Distributer"] = "Distributer Deleted  Successfully";
                    }
                    else
                    {
                        TempData["Distributer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Distributer"] = ex.Message;
            }
            return RedirectToAction("DistributerListForAdmin", "Admin");
        }
        public ActionResult Profile(Employee model)
        {
             model.PK_AdminId = Session["Pk_adminId"].ToString();
            DataSet ds = model.GetProfileDetailsForAdmin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                ViewBag.MobileNo = ds.Tables[0].Rows[0]["Contact"].ToString();
                ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                ViewBag.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                ViewBag.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                ViewBag.Pincode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
            }
            return View(model);
        }
        public ActionResult OrderDetails()
        {
            #region ddldistributer
            int dcount = 0;
            Master master = new Master();
            List<SelectListItem> ddldistributer = new List<SelectListItem>();
            master.DistributerId = Session["Pk_adminId"].ToString();
            DataSet dsdistributer = master.GetDistributer();
            if (dsdistributer != null && dsdistributer.Tables.Count > 0 && dsdistributer.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsdistributer.Tables[0].Rows)
                {
                    if (dcount == 0)
                    {
                        ddldistributer.Add(new SelectListItem { Text = "Select Distributer", Value = "0" });
                    }
                    ddldistributer.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_DistributerId"].ToString() });
                    dcount = dcount + 1;
                }
            }
            ViewBag.ddldistributer = ddldistributer;
            #endregion
            Master model = new Master();
            List<Master> lst = new List<Master>();
            //model.DistributerId = Session["Pk_adminId"].ToString();
            DataSet ds = model.OrderRequestList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.OrderId = dr["PK_OrderId"].ToString();
                    obj.Distributer = dr["DistributerName"].ToString();
                    obj.PendingLimit = dr["PendingLimit"].ToString();
                    obj.Dealer = dr["DealerName"].ToString();
                    obj.Rate = dr["Rate"].ToString();
                    obj.OrderQuantity = dr["OrderQuantity"].ToString();
                    obj.TotalAmount = dr["TotalAmount"].ToString();
                    obj.Date = dr["Date"].ToString();
                    obj.Status = (dr["Status"].ToString());
                    obj.PK_DealerId = dr["FK_DealerId"].ToString();
                    obj.FK_DistributerId = (dr["FK_DistributerId"].ToString());
                    lst.Add(obj);
                }
                model.lstrequest = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("OrderDetails")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult OrderDetails(Master model)
        {
            List<Master> lst = new List<Master>();
            //model.Distributer = Session["Pk_adminId"].ToString();
            model.DistributerId = model.DistributerId == "0" ? null : model.DistributerId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.OrderRequestList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.OrderId = dr["PK_OrderId"].ToString();
                    obj.Distributer = dr["DistributerName"].ToString();
                    obj.PendingLimit = dr["PendingLimit"].ToString();
                    obj.Dealer = dr["DealerName"].ToString();
                    obj.Rate = dr["Rate"].ToString();
                    obj.OrderQuantity = dr["OrderQuantity"].ToString();
                    obj.TotalAmount = dr["TotalAmount"].ToString();
                    obj.Date = dr["Date"].ToString();
                    obj.Status = (dr["Status"].ToString());
                    lst.Add(obj);
                }
                model.lstrequest = lst;
            }

            #region ddldistributer
            int dcount = 0;
            Master master = new Master();
            List<SelectListItem> ddldistributer = new List<SelectListItem>();
            master.DistributerId = Session["Pk_adminId"].ToString();
            DataSet dsdistributer = master.GetDistributer();
            if (dsdistributer != null && dsdistributer.Tables.Count > 0 && dsdistributer.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsdistributer.Tables[0].Rows)
                {
                    if (dcount == 0)
                    {
                        ddldistributer.Add(new SelectListItem { Text = "Select Distributer", Value = "0" });
                    }
                    ddldistributer.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_DistributerId"].ToString() });
                    dcount = dcount + 1;
                }
            }
            ViewBag.ddldistributer = ddldistributer;
            #endregion
            return View(model);
        }
        
        public ActionResult GenerateReceipt(Master model, string OrderId)
        {
            if (OrderId != null)
            {
                model.OrderId = OrderId;
                DataSet ds = model.GetDeoDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.OrderId = ds.Tables[0].Rows[0]["PK_OrderId"].ToString();
                    ViewBag.Distributer = ds.Tables[0].Rows[0]["DistributerName"].ToString();
                    ViewBag.PendingLimit = ds.Tables[0].Rows[0]["PendingLimit"].ToString();
                    ViewBag.Dealer = ds.Tables[0].Rows[0]["DealerName"].ToString();
                    ViewBag.Rate = ds.Tables[0].Rows[0]["Rate"].ToString();
                    ViewBag.OrderQuantity = ds.Tables[0].Rows[0]["OrderQuantity"].ToString();
                    ViewBag.TotalAmount = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                    ViewBag.Date = ds.Tables[0].Rows[0]["Date"].ToString();
                    ViewBag.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                    ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                }
            }

            return View(model);
        }

        public ActionResult SaveIncreaseLimitDateWise(Master model, string AddOnLimit, string FK_DistributerId)
        {
            try
            {
                model.FK_DistributerId = FK_DistributerId;
                model.AddOnLimit = AddOnLimit;
                model.AddedBy = Session["Pk_adminId"].ToString();
                DataSet ds = model.SaveIncreaseLimitDateWise();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "yes";
                        //TempData["Increase"] = "Increase limit save Successfully !!";
                    }
                    else
                    {
                        model.Result = "no";
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DispatchReportForAdmin()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            model.DistributerId = Session["Pk_adminId"].ToString();
            DataSet ds = model.GetDispatchReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_BookingDispatchId = dr["Pk_BookingDispatchId"].ToString();
                    obj.Dealer = dr["DealerName"].ToString();
                    obj.DistributerName = dr["DistributerName"].ToString();
                    obj.BookingQuantity = dr["BookingQuantity"].ToString();
                    obj.DispatchQuantity = dr["DispatchQuantity"].ToString();
                    obj.DispatchDate = dr["DispatchDate"].ToString();
                    obj.TotalAmount = dr["Amount"].ToString();
                    obj.BookingDate = dr["BookingDate"].ToString();
                    lst.Add(obj);
                }
                model.lstDispatchOrder = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("DispatchReport")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DispatchReportForAdminSearch(Master model)
        {
            List<Master> lst = new List<Master>();
            model.DistributerId = Session["Pk_adminId"].ToString();
            DataSet ds = model.GetDispatchReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_BookingDispatchId = dr["Pk_BookingDispatchId"].ToString();
                    obj.Dealer = dr["DealerName"].ToString();
                    obj.DistributerName = dr["DistributerName"].ToString();
                    obj.BookingQuantity = dr["BookingQuantity"].ToString();
                    obj.DispatchQuantity = dr["DispatchQuantity"].ToString();
                    obj.DispatchDate = dr["DispatchDate"].ToString();
                    obj.TotalAmount = dr["Amount"].ToString();
                    obj.BookingDate = dr["BookingDate"].ToString();
                    lst.Add(obj);
                }
                model.lstDispatchOrder = lst;
            }
            return View(model);
        }



        public ActionResult DispatchForBookingRequest()
        {
            Admin model = new Admin();
            List<Admin> lst3 = new List<Admin>();
            DataSet ds = model.DealerDetailsByDistributerId();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_DealerId = dr["PK_DealerId"].ToString();
                    obj.Dealer = dr["Dealer"].ToString();

                    obj.TotalAmount = (dr["TotalAmount"].ToString());
                    obj.Dispatched = (dr["Dispatched"].ToString());
                    obj.DispatchPending = (dr["DispatchPending"].ToString());
                    obj.FK_DistributerId = dr["FK_DistributerId"].ToString();
                    obj.Distributor = dr["Distributor"].ToString();
                    obj.BookingDate = (dr["BookingDate"]).ToString();
                    obj.TotalBookingQuantity = (dr["TotalBookingQuantity"].ToString());
                    obj.Status = dr["Status"].ToString();

                    obj.TotalAmount = dr["TotalAmount"].ToString();
                    lst3.Add(obj);
                }
                model.lstdistributer = lst3;
            }
            return View(model);
        }

        public ActionResult BindDelearlist(string FK_DistributerId, string OrderDate)
        {
            Admin model = new Admin();
            model.DistributerId = FK_DistributerId;
            model.BookingDate = OrderDate;
            List<Admin> lst3 = new List<Admin>();

            DataSet ds = model.DealerDetailsByDistributerId();
            //model.DistributerId = Session["Pk_adminId"].ToString();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_DealerId = dr["PK_DealerId"].ToString();
                    obj.Dealer = dr["Dealer"].ToString();
                    obj.OrderQuantity = (dr["OrderQuantity"]).ToString();
                    obj.TotalAmount = (dr["TotalAmount"].ToString());
                    obj.Dispatched = (dr["Dispatched"].ToString());
                    obj.DispatchPending = (dr["DispatchPending"].ToString());

                    obj.FK_DistributerId = dr["FK_DistributerId"].ToString();
                    obj.Distributor = dr["Distributor"].ToString();
                    obj.BookingDate = (dr["BookingDate"]).ToString();
                    obj.TotalBookingQuantity = (dr["TotalBookingQuanity"].ToString());
                    obj.Status = dr["Status"].ToString();
                    obj.DispatchQuantity = dr["DispatchQuantity"].ToString();
                    obj.TotalAmount = dr["TotalAmount"].ToString();

                    lst3.Add(obj);
                }
                model.Delearlist = lst3;
            }

            return Json(model.Delearlist, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DispatchForBookingRequestAction(Admin order, string dataValue)
        {
            try
            {
                string DistributerID = "";
                string BookingDate = "";
                string PK_DealerId = "";
                string TotalAmount = "";
                string OrderQuantity = "";
                string DispatchValue = "";
                int rowsno = 0;
                var isValidModel = TryUpdateModel(order);
                var jss = new JavaScriptSerializer();
                var jdv = jss.Deserialize<dynamic>(dataValue);

                DataTable OrderDispatch = new DataTable();

                OrderDispatch.Columns.Add("DistributerID");
                OrderDispatch.Columns.Add("BookingDate");
                OrderDispatch.Columns.Add("PK_DealerId");
                OrderDispatch.Columns.Add("TotalAmount");
                OrderDispatch.Columns.Add("OrderQuantity");
                OrderDispatch.Columns.Add("DispatchValue");
                OrderDispatch.Columns.Add("rowsno");
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(jdv["OrderRequest"]);

                int numberOfRecords = dt.Rows.Count;
                foreach (DataRow row in dt.Rows)
                {
                    DistributerID = row["DistributerID"].ToString();
                    BookingDate = row["BookingDate"].ToString();
                    PK_DealerId = row["PK_DealerId"].ToString();
                    TotalAmount = row["TotalAmount"].ToString();
                    OrderQuantity = row["OrderQuantity"].ToString();
                    DispatchValue = row["DispatchValue"].ToString();
                    rowsno = rowsno + 1;
                    OrderDispatch.Rows.Add(DistributerID, BookingDate, PK_DealerId, TotalAmount, OrderQuantity, DispatchValue, rowsno);
                }
                order.dtOrderDispatch = OrderDispatch;
                order.AddedBy = Session["Pk_adminId"].ToString();
                DataSet ds = new DataSet();
                ds = order.SaveDispatchForBookingRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        order.Result = "Yes";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        order.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JsonResult { Data = new { status = order.Result } };
        }


        #region Change password for Admin

        public ActionResult ChangePassword()
        {
            List<SelectListItem> ddlPasswordType = Common.BindPasswordType();
            ViewBag.ddlPasswordType = ddlPasswordType;
            return View();

        }

        [HttpPost]
        [ActionName("ChangePassword")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePassword(Password obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_adminId"].ToString();
                obj.OldPassword = obj.OldPassword;
                obj.NewPassword = obj.NewPassword;
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangePassword"] = "Password updated successfully !!";
                        FormName = "ChangePassword";
                        Controller = "Admin";
                    }
                    else
                    {
                        TempData["ChangePassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePassword";
                        Controller = "Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangePassword"] = ex.Message;
                FormName = "Login";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }


        #endregion


        [HttpPost]
        [ActionName("OrderDetails")]
        [OnAction(ButtonName = "btnapprove")]
        public ActionResult ApproveOrderRequest(Master model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                string ctrRowCount = Request["hdRows"].ToString();
                string chk = "";
                string PK_OrderID = "";
                string FK_DistributerID = "";
                string FK_DealerID = "";
                int Id = 0;
                DataTable dtpayment = new DataTable();
                dtpayment.Columns.Add("Id");
                dtpayment.Columns.Add("PK_OrderID");
                dtpayment.Columns.Add("FK_DistributerID");
                dtpayment.Columns.Add("FK_DealerID");
                for (int i = 1; i < int.Parse(ctrRowCount); i++)
                {
                    chk = Request["chkpayment_" + i];
                    if (chk == "on")
                    {
                        Id = dtpayment.Rows.Count + 1;
                        PK_OrderID = Request["PK_OrderID_" + i].ToString();
                        FK_DistributerID = Request["FK_DistributerID_" + i].ToString();
                        FK_DealerID = Request["FK_DealerID_" + i].ToString();
                        dtpayment.Rows.Add(Id,PK_OrderID,FK_DistributerID, FK_DealerID);
                    }
                }
                model.dtTable = dtpayment;
                model.AddedBy = Session["Pk_adminId"].ToString();
                model.Status = "Approved";
                DataSet ds = model.ApproveOrderRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Order"] = "Booking Request Approved Successfully !!";
                        FormName = "OrderDetails";
                        Controller = "Admin";
                    }
                    else
                    {
                        TempData["Order"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "OrderDetails";
                        Controller = "Admin";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(FormName, Controller);
        }


        [HttpPost]
        [ActionName("OrderDetails")]
        [OnAction(ButtonName = "btnreject")]
        public ActionResult RejectOrderRequest(Master model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                string ctrRowCount = Request["hdRows"].ToString();
                string chk = "";
                string PK_OrderID = "";
                string FK_DistributerID = "";
                string FK_DealerID = "";
                int Id = 0;
                DataTable dtpayment = new DataTable();
                dtpayment.Columns.Add("Id");
                dtpayment.Columns.Add("PK_OrderID");
                dtpayment.Columns.Add("FK_DistributerID");
                dtpayment.Columns.Add("FK_DealerID");
                for (int i = 1; i < int.Parse(ctrRowCount); i++)
                {
                    chk = Request["chkpayment_" + i];
                    if (chk == "on")
                    {
                        Id = dtpayment.Rows.Count + 1;
                        PK_OrderID = Request["PK_OrderID_" + i].ToString();
                        FK_DistributerID = Request["FK_DistributerID_" + i].ToString();
                        FK_DealerID = Request["FK_DealerID_" + i].ToString();
                        dtpayment.Rows.Add(Id, PK_OrderID, FK_DistributerID, FK_DealerID);
                    }
                }
                model.dtTable = dtpayment;
                model.AddedBy = Session["Pk_adminId"].ToString();
                model.Status = "Rejected";
                DataSet ds = model.RejectOrderRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Order"] = "Booking Request Rejected Successfully !!";
                        FormName = "OrderDetails";
                        Controller = "Admin";
                    }
                    else
                    {
                        TempData["Order"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "OrderDetails";
                        Controller = "Admin";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ContactDetails()
        {
            Admin model = new Admin();
            List<Admin> Contactlst = new List<Admin>();
            DataSet ds = model.ContactDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_ContactId = r["PK_ContactId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PhoneNo = r["PhoneNo"].ToString();
                    obj.Message = r["Message"].ToString();
                    Contactlst.Add(obj);
                }
                model.lstContactDetails = Contactlst;
            }

            return View(model);
        }

    }
}


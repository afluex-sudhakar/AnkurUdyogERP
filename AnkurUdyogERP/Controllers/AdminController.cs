﻿using AnkurUdyogERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            DataSet ds = model.GetProfileDetails();
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
                    obj.Section = dr["Section"].ToString();
                    obj.Rate = dr["Rate"].ToString();
                    obj.OrderQuantity = dr["OrderQuantity"].ToString();
                    obj.TotalAmount = dr["TotalAmount"].ToString();
                    obj.Date = dr["Date"].ToString();
                    obj.Status = (dr["Status"].ToString());
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
                    obj.Section = dr["Section"].ToString();
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

        public ActionResult ApproveOrderRequest(string OrderId)
        {
            string FormName = " ";
            string Controller = "";
            try
            {
                if (OrderId != null)
                {
                    Master model = new Master();
                    model.OrderId = OrderId;
                    model.Status = "Approved";
                    model.AddedBy = Session["Pk_adminId"].ToString();
                    DataSet ds = model.ApproveOrderRequest();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Order"] = "Order Request Approved Successfully !!";
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
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(FormName, Controller);
        }
        

        public ActionResult RejectOrderRequest(string OrderId)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (OrderId != null)
                {
                    Master model = new Master();
                    model.OrderId = OrderId;
                    model.Status = "Rejected";
                    model.AddedBy = Session["Pk_adminId"].ToString();
                    DataSet ds = model.RejectOrderRequest();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Order"] = "Order Request Rejected Successfully !!";
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
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult GenerateReceipt()
        {
            return View();
        }
    }
}
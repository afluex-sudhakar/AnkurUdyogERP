﻿using AnkurUdyogERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnkurUdyogERP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Home model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                DataSet ds = model.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                    {
                        if (ds.Tables[0].Rows[0]["UserTypeName"].ToString() == "Admin")
                        {
                            Session["Pk_adminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                            FormName = "AdminDashboard";
                            Controller = "Admin";
                        }
                        else
                        {
                            TempData["msg"] = "Incorrect LoginId Or Password";
                            FormName = "Login";
                            Controller = "Home";
                        }
                    }
                    else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Employee")
                    {
                        Session["Pk_adminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                        Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                        Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        FormName = "EmployeeDashboard";
                        Controller = "Employee";
                    }
                    else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Distributer")
                    {
                        Session["Pk_adminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                        Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                        Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        FormName = "DistributerDashboard";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["msg"] = "Incorrect LoginId Or Password";
                        FormName = "Login";
                        Controller = "Home";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                FormName = "Login";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult LoginDistributer()
        {
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult LoginDistributerAction(Home model)
        {
            string FormName = "";
            string Controller = "";
            //ProjectStatusResponse datalist = null;
            try
            {
                DataSet ds = model.LoginDistributer();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["IsBlocked"].ToString() == "True")
                    {
                        TempData["Login"] = "Login Id Blocked !!";
                        FormName = "LoginDistributer";
                        Controller = "Home";
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "LoginDistributer";
                            Controller = "Home";
                        }
                        else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Distributer")
                        {
                            if (ds.Tables[0].Rows[0]["UserTypeName"].ToString() == "Distributer")
                            {
                                Session["PK_DistributerId"] = ds.Tables[0].Rows[0]["PK_DistributerId"].ToString();
                                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                                Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                                FormName = "DistributerDashboard";
                                Controller = "Distributer";
                            }
                            else
                            {
                                TempData["Login"] = "Incorrect Login ID Or Password";
                                FormName = "LoginDistributer";
                                Controller = "Home";
                            }
                        }
                    }
                }
                else
                {
                    TempData["Login"] = "Incorrect Login ID Or Password";
                    FormName = "LoginDistributer";
                    Controller = "Home";
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "LoginDistributer";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }
    }
}




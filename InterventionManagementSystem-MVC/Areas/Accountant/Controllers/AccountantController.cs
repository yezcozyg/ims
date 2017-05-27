﻿using IMSDBLayer.DataAccessObjects;
using IMSLogicLayer.ServiceInterfaces;
using IMSLogicLayer.Services;
using InterventionManagementSystem_MVC.Areas.Accountant.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMSLogicLayer.Enums;

namespace InterventionManagementSystem_MVC.Areas.Accountant.Controllers
{
    public class AccountantController : Controller
    {
        // GET: Accountant/Accountants
        public ActionResult Index()
        {
            
            IAccountantService accountant = GetAccountantService();
            var user = accountant.getDetail();
            var model = new AccountantViewModel()
            {
                Name = user.Name,
            };
            return View(model);
         
        }
        
        public ActionResult AccountListView()
        {
            IAccountantService accountant = GetAccountantService();
            var siteEngineerList = accountant.getAllSiteEngineer();
            var siteEnigeerVMList = new List<SiteEngineerViewModel>();
            foreach (var siteEngineer in siteEngineerList)
            {
                siteEnigeerVMList.Add(new SiteEngineerViewModel()
                {
                    Id = siteEngineer.Id.ToString(),
                    Name = siteEngineer.Name
                });

            }

            var managerList = accountant.getAllManger();
            var managerVMList = new List<ManagerViewModel>();
            foreach (var manager in managerList)
            {
                managerVMList.Add(new ManagerViewModel()
                {
                    Id = manager.Id.ToString(),
                    Name = manager.Name
                });

            }

            var model = new AccountListViewModel()
            {
                SiteEngineers = siteEnigeerVMList,
                Managers = managerVMList
            };


            return View(model);
        }


        //GET Default information of an User
        public ActionResult EditDistrict(string id)
        {
            var accountant = GetAccountantService();

            var user = accountant.getUserById(new Guid(id));
            user.District = accountant.getDistrictForUser(user.Id);

            var districts = accountant.getDistricts().Select(d=> new SelectListItem {Value = d.Id.ToString() ,Text=d.Name }).ToList();
         
            var model = new EditDistrictViewModel()
            {
                Name = user.Name,
                Id= id,
                CurrentDistrict = user.District.Name,
                DistrictList= districts
            };


            return View(model);
        }

   


        public ActionResult ChangeDistrict(FormCollection form)
        {
            return View();
        }

        public ActionResult ReportList()
        {
            var accountant = GetAccountantService();

            var reportList = Enum.GetValues(typeof(ReportType))
                                .Cast<ReportType>()
                                .Select(v => v.ToString())
                                .ToList();
            var reports = new List<ReportViewModel>();
            foreach (var report in reportList)
            {
                reports.Add(new ReportViewModel() { Name = report});
            }
            var model = new ReportsViewModel()
            {
                ReportList = reports
            };


            return View(model);
        }


        public ActionResult PrintReport(string name)
        {
            var accountant = GetAccountantService();
            ReportType reportType = (ReportType)Enum.Parse(typeof(ReportType), name);
            var report = new List<IMSLogicLayer.Models.ReportRow>();
            if (reportType == ReportType.AverageCostByEngineer)
            {
                report = accountant.printAverageCostByEngineer().ToList();
                foreach (var reportrow in report)
                {
                    reportrow.Hours = decimal.Round(reportrow.Hours.Value, 2, MidpointRounding.AwayFromZero);
                    reportrow.Costs = decimal.Round(reportrow.Costs.Value, 2, MidpointRounding.AwayFromZero);
                }
            }
            //if report is monthly cost by district redirect to monthly report page
            else if (reportType == ReportType.MonthlyCostByDistrict)
            {

                return PrintMonthlyReport();
            }
            else if (reportType == ReportType.TotalCostByDistrict)
            {
                report = accountant.printTotalCostByDistrict().ToList();
            }
            else if (reportType == ReportType.TotalCostByEngineer)
            {
                report = accountant.printTotalCostByEngineer().ToList();
            }



            return View();
        }



        public ActionResult PrintMonthlyReport()
        {
            return View();
        }
        private IAccountantService GetAccountantService()
        {
            var identityId = User.Identity.GetUserId();

            IAccountantService accountant = new AccountantService("f2c4f7b0-7e2b-4095-bc8a-594cbbbd77ea");
            
            return accountant;
        }

    }
}
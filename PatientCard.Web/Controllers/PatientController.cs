﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientCard.Core;
using PatientCard.Core.Services.Interfaces;

namespace PatientCard.Web.Controllers
{
    public class PatientController : Controller
    {
		private readonly ISystemService _systemService;

		public PatientController()
		{
			_systemService = Bootstrap.BuildFactory.GetInstance<ISystemService>();
		}


		public ActionResult Overview()
		{
			return View();
		}

		public ActionResult Editor()
		{
			return View();
		}

		public ActionResult FirstSurvey()
		{
			var model = _systemService.GetFirstSurveyOptions().OrderBy(n => n.Key).ToList();
			return View(model);
		}

		public ActionResult TreatmentPlan()
		{
			return View();
		}

		public ActionResult TreatmentPlanEditor()
		{
			return View();
		}

		public ActionResult Survey()
		{
			return View();
		}

		public ActionResult SurveyEditor()
		{
			return View();
		}

		public ActionResult Visit()
		{
			return View();
		}

		public ActionResult VisitEditor()
		{
			return View();
		}

		public ActionResult Status()
		{
			return View();
		}
    }
}

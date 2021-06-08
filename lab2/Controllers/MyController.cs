using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab2.Models;

namespace lab2.Controllers
{
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private MyModel myModel = new MyModel();
        public Tuple<string, string> Actions(int? num1, string act, int? num2)
        {
            string answ = "";
            int? res = 0;
            var map = new Tuple<string, string>("", "");
            switch (act)
            {
                case "add":
                    res = num1 + num2;
                    answ = res.ToString();
                    act = "+";
                    break;
                case "sub":
                    res = num1 - num2;
                    answ = res.ToString();
                    act = "-";
                    break;
                case "mult":
                    res = num1 * num2;
                    answ = res.ToString();
                    act = "*";
                    break;
                case "div":
                    if (num2 == 0)
                    {
                        answ = "Divide by zero!";
                        act = "/";
                        break;
                    }
                    else
                    {
                        res = num1 / num2;
                        answ = res.ToString();
                        act = "/";
                        break;
                    }
            }
            map = Tuple.Create(act, answ);
            return map;
        }
        public IActionResult ManualView(int? num1, string act, int? num2)
        {
            if (Request.Method == "GET")
                return View("ManualView");
            if (Request.Method == "POST")
            {
                Tuple<string, string> resmap = Actions(num1, act, num2);

                string data = $"{num1} {resmap.Item1} {num2} = {resmap.Item2}";
                ViewData["Result"] = data;

                return View("ResultView");
            }
            return View("Index");
        }
        [HttpGet]
        public IActionResult ManualWithSeparateHandlersView()
        {
            return View("ManualWithSeparateHandlersView");
        }
        [HttpPost]
        public IActionResult ManualWithSeparateHandlersView(int num1, string act, int num2)
        {
            Tuple<string, string> resmap = Actions(num1, act, num2);

            string data = $"{num1} {resmap.Item1} {num2} = {resmap.Item2}";
            ViewData["Result"] = data;

            return View("ResultView");
        }
        [HttpGet]
        public IActionResult ModelBindingParamView()
        {
            return View("ModelBindingParamView");
        }
        [HttpPost]
        public IActionResult ModelBindingParamView(int mnum1, string mact, int mnum2)
        {
            Tuple<string, string> resmap = this.Actions(mnum1, mact, mnum2);

            string data = $"{mnum1} {resmap.Item1} {mnum2} = {resmap.Item2}";
            ViewData["Result"] = data;

            return View("ResultView");
        }
        [HttpGet]
        public IActionResult ModelBindingSeparateView()
        {
            return View("ModelBindingSeparateView");
        }
        [HttpPost]
        public IActionResult ModelBindingSeparateView(MyModel model)
        {
            Tuple<string, string> resmap = this.Actions(model.num1, model.act, model.num2);

            string data = $"{model.num1} {resmap.Item1} {model.num2} = {resmap.Item2}";
            ViewData["Result"] = data;

            return View("ResultView");
        }
    }
}

using OnlineExamSystem.Models;
using OnlineExamSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using System.Web.Mvc.Html;


namespace OnlineExamSystem.Controllers
{
    public class HomeController : Controller
    {
        polldbEntities db = new polldbEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPollandOptions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPollandOptions(PollsViewModel p)
        {
            Models.Poll pq = new Models.Poll();

            try
            {
                pq.pollsubject = p.pollsubject;
                pq.PollCount = p.PollCount;
                //pq.pollsubject = p.Poll.pollsubject;
                //pq.PollCount = p.Poll.PollCount;
                db.Polls.Add(pq);
                db.SaveChanges();
            }
            catch (Exception)
            {
                // throw;
                ViewBag.msg = "لم يتم حفظ السؤال !!";
            }

        
            return RedirectToAction("InsertResponses");

        }

        [HttpGet]
        public ActionResult InsertResponses()

        {

            ViewBag.Polllist1 = (from pol in db.Polls
                                orderby pol.C_id descending
                                select new question { pollsubject = pol.pollsubject, PollCount=pol.PollCount ?? 0 ,pollId=pol.C_id}).Take(1).ToList();
            TempData["pollidtemp"]= ViewBag.Polllist1[0].pollId;
            return View();
        }

        [HttpPost]
        public ActionResult InsertResponses(PollsViewModel p)
        {
            Models.Poll pq = new Models.Poll();
            Models.PollOption po = new Models.PollOption();

            ViewBag.Polllist1 = (from pol in db.Polls
                                 orderby pol.C_id descending
                                 select new question { pollsubject = pol.pollsubject, PollCount = pol.PollCount ?? 0 }).Take(1).ToList();

            for(int i = 0; i < @ViewBag.Polllist1[0].PollCount; i++)
            {
                try
                {
                    po.optiontext = p.optiontext[i];
                    po.pollid =Convert.ToInt32( TempData["pollidtemp"]);
                    db.PollOptions.Add(po);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.msg = "لم يتم حفظ الجواب !!";
                }
            }
            return RedirectToAction("Responds");

        }
        [HttpGet]
        public ActionResult Responds()
        {
            ViewBag.Polllist = (from pol in db.Polls
                                join opt in db.PollOptions on pol.C_id equals opt.pollid
                                orderby pol.C_id descending
                                select new question { pollsubject = pol.pollsubject, optiontext = opt.optiontext, pollId = pol.C_id, pollOptionId = opt.pollid, Questionid=opt.C_id  }).ToList();

            ViewBag.count = ViewBag.Polllist.Count;

            return View();
        }

        [HttpPost]
        public ActionResult Responds(OptionViewModel model)
        {
            //using (polldbEntities db = new polldbEntities())
            //{
                if (ModelState.IsValid)
                {
                Models.pollresponse pr = new Models.pollresponse();

                    foreach (var q in model.polo.PollOptions)
                    {
                        var isExist = db.pollresponses.FirstOrDefault(qq => qq.pollid == model.polo.C_id && qq.optionid == q.polllid);
                        if (isExist != null)
                        {
                            pr.pollid = model.polo.C_id;
                            pr.optionid = q.polllCid;
                            //pr.ResponseCount += 1;

                            //var qId = q.C_id;
                            //var selectedAnswer = q.selectedoption;
                            db.pollresponses.Add(pr);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }
            //}
            return View(model);
            }

      
        [HttpGet]
        public ActionResult Details()
        {

            ViewBag.Detaillist = (from pol in db.Polls
                                  join opt in db.PollOptions on pol.C_id equals opt.pollid
                                  join res in db.pollresponses on pol.C_id equals res.C_id
                                  orderby pol.C_id descending
                                  select new question { pollsubject = pol.pollsubject, optiontext = opt.optiontext, pollId = pol.C_id, pollOptionId = opt.pollid, Questionid = opt.C_id , RpollId = res.pollid, RpollOptionId  = res.optionid}).ToList();

            ViewBag.count = ViewBag.Detaillist.Count;
            return View();
        }


        public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }


        }
    }

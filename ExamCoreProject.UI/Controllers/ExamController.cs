using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExamCoreProject.UI.Models;
using ExamProjectCore.Business.Abstract;
using ExamProjectCore.Entities;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;


namespace ExamCoreProject.UI.Controllers
{
    public class ExamController : Controller
    {
        private IExamService _examService;
        private IQuestionService _questionService;
        private IOptionService _optionService;
        private ICorrectAnswerService _correctAnswer;
        public ExamController(IExamService examService, IQuestionService questionService, IOptionService optionsService, ICorrectAnswerService correctAnswer)
        {
            _examService = examService;
            _questionService = questionService;
            _optionService = optionsService;
            _correctAnswer = correctAnswer;

        }

        public IActionResult CreateExam()
        {
            int counter = 0;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string link = "http://wired.com/most-recent/";
            Uri url = new Uri(link);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            var secilenhtml = @"//*[@id=""app-root""]/div/div[3]/div/div[2]/div/div[1]/div/div/ul"; // veriyi çekeceğimiz div alanı kategori listesi xpath adresi

            List<String> basliklar = new List<string>(); 
            List<String> linkler = new List<string>(); 
            List<String> metinler = new List<string>(); 

            var secilenHtmlList = document.DocumentNode.SelectNodes(secilenhtml); //selectnodes methoduyla verdiğimiz xpathin htmlini getiriyoruz.

            foreach (var items in secilenHtmlList)
            {
                foreach (var innerItem in items.SelectNodes("li"))
                {
                    try
                    {
                        metinler.Add(getData(innerItem.SelectNodes("a")[0].Attributes["href"].Value));
                        counter++;
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    basliklar.Add(innerItem.SelectNodes("div//a//h2")[0].InnerHtml);
                    if (counter >= 5)
                        break;
                }
            }

            ViewBag.Basliklar = basliklar;
            ViewBag.Metinler = metinler;
            return View();
        }


        public string getData(string eklink)
        {
            string link = "http://wired.com" + eklink;  
            Uri url = new Uri(link); 
            WebClient client = new WebClient(); 
            client.Encoding = Encoding.UTF8; //türkçe karakter sorunu yapmaması için encoding utf8 yapıyoruz.
            string html = client.DownloadString(url); // siteye bağlanıp tüm sayfanın html içeriğini çekiyoruz.
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument(); 
            document.LoadHtml(html);

            StringBuilder metin = new StringBuilder();
            HtmlNodeCollection secilenHtmlList = document.DocumentNode.SelectNodes("/html/body/div[1]/div/main/article/div[2]/div/div[1]/div[1]/div[1]"); //selectnodes methoduyla verdiğimiz xpathin htmlini getiriyoruz.
            foreach (var items in secilenHtmlList)
            {
                foreach (var innerItem in items.SelectNodes("p"))
                {

                    metin.Append(innerItem.InnerHtml); 

                }
            }
            return metin.ToString();
        }


       
        [HttpPost]
        public JsonResult CreateExam(string[] questiones, string[] choices, string[] correctAnswers, string header, string text)
        {

            if (questiones != null && choices != null && correctAnswers != null && header != null)
            {
                Exam ex = new Exam();
                ex.Title = header;
                ex.Content = text;
                _examService.Create(ex);
                var exID = ex.ExamId; 
                int a = 0;
                int b = 6;
                int k = 0;

                foreach (var item in questiones)
                {
                    Question q = new Question();
                    q.QuestionContent = item;
                    q.QuestionNumber = 1;
                    q.ExamId = exID; //soruya sınavId atılıyor
                    _questionService.Create(q);
                    var questionID = q.QuestionId; //soruId bir değişkende tutuluyor
                    for (int i = a; i <= b; i += 2)
                    {
                        Option op = new Option();
                        op.OptionContent = choices[i];
                        op.OptionName = choices[i + 1];
                        op.CorrectOption = true;
                        op.QuestionId = questionID; //cevaba soruId atılıyor
                        _optionService.Create(op);
                        var opId = op.OptionId;


                        if (op.OptionName == correctAnswers[k])
                        {
                            CorrectAnswer correct = new CorrectAnswer();
                            correct.Correct = correctAnswers[k];
                            correct.OptionId = opId;
                            _correctAnswer.Create(correct);
                        }
                    }
                    k++;
                    a += 8;
                    b += 8;

                }
                return Json(new { result = 1, message = "Succes." });

            }

            else
            {

                return Json(new { result = 0, message = "Failed." });
            }


        }

        public IActionResult ExamList() {

            var exList = _examService.GetAll().ToList();
            return View(exList);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public JsonResult DeleteExam(int id)
        {
            if (id != 0)
            {
                var findExam = _examService.GetById(id);
                _examService.Delete(findExam);
                var findQuestionList = _questionService.GetAll().Where(x => x.ExamId == id).ToList();
                foreach (var question in findQuestionList)
                {
                    int QuestionId = question.QuestionId;
                    _questionService.Delete(question);
                    var findOptionList = _optionService.GetAll().Where(x => x.QuestionId == QuestionId).ToList();
                    foreach (var op in findOptionList)
                    {
                        int opId = op.OptionId;
                        _optionService.Delete(op);
                        var findCorrectList = _correctAnswer.GetAll().Where(x => x.OptionId == opId).ToList();
                        foreach (var correct in findCorrectList)
                        {
                            _correctAnswer.Delete(correct);
                        }
                    }

                }
                return Json(true);

            }
            else
            {
                return Json(false);
            }
        }

       
        //Sınava girme get action'ı
        public ActionResult EnterExam(int id)
        {
                if (id != 0)
                {
                    EnterExamClass modal = new EnterExamClass();
                    var findExam = _examService.GetById(id);
                    modal.exam = findExam;
                    var findQuestionList = _questionService.GetAll().Where(x => x.ExamId == id).ToList();
                    modal.questions = findQuestionList;
                    List<Option> opList = new List<Option>();


                    foreach (var question in findQuestionList)
                    {
                        int QuestionId = question.QuestionId;
                        var findOptionList = _optionService.GetAll().Where(x => x.QuestionId == QuestionId).ToList();
                        foreach (var option in findOptionList)
                        {
                            opList.Add(option);
                        }
                    }
                    modal.options = opList;

                    return View(modal);
                }
                else
                {
                    var exList = _examService.GetAll();
                    return RedirectToAction("ExamList", exList);
                }

        }

        //Sınavı tamamlama post action'ı
        [HttpPost]
        public JsonResult EnterExam(int examId, string[] correctAnswers)
        {
            if (examId != 0 && correctAnswers != null)
            {
                var findExam = _examService.GetById(examId);
                var findQuestionList = _questionService.GetAll().Where(x => x.ExamId == examId).ToList();
                int c = 0;
                List<string> bgList = new List<string>();

                foreach (var question in findQuestionList)
                {
                    var findOptionList = _optionService.GetAll().Where(x => x.QuestionId == question.QuestionId).ToList();
                    foreach (var option in findOptionList)
                    {
                        var correct = _correctAnswer.GetAll().Where(x => x.OptionId == option.OptionId).FirstOrDefault();
                        if (correct != null)
                        {
                            if (correct.Correct == correctAnswers[c])
                            {
                                bgList.Add("green");
                            }
                            else
                            {
                                bgList.Add("red");

                            }
                        }

                    }
                    c++;
                }

                return Json(new { success = bgList });
            }
            else
            {
                return Json(new { });
            }
        }

    }
}
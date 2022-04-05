using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExamProjectCore.Business.Abstract;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace ExamProjectCore.WebUI.Controllers
{
    public class ExamController : Controller
    {
        private IExamService _examService;
        public ExamController(IExamService examService)
        {
            _examService = examService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateExam()
        {
            //if (Session["admin"] == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{
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

                List<String> basliklar = new List<string>(); //veriyi atacağımız stringbuilder
                List<String> linkler = new List<string>(); //veriyi atacağımız stringbuilder
                List<String> metinler = new List<string>(); //veriyi atacağımız stringbuilder

                var secilenHtmlList = document.DocumentNode.SelectNodes(secilenhtml); //selectnodes methoduyla verdiğimiz xpathin htmlini getiriyoruz.

                foreach (var items in secilenHtmlList)
                {
                    foreach (var innerItem in items.SelectNodes("li"))//her ul un içindeki li de dön
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

                ViewBag.Basliklar = basliklar;//textboxa tüm değerleri yaz
                ViewBag.Metinler = metinler;
                return View();
            //}
        }

        public string getData(string eklink)
        {
            string link = "http://wired.com" + eklink;  //link değişkenine çekeceğimiz web sayafasının linkini yazıyoruz.
            Uri url = new Uri(link); //Uri tipinde değişeken linkimizi veriyoruz.
            WebClient client = new WebClient(); // webclient nesnesini kullanıyoruz bağlanmak için.
            client.Encoding = Encoding.UTF8; //türkçe karakter sorunu yapmaması için encoding utf8 yapıyoruz.
            string html = client.DownloadString(url); // siteye bağlanıp tüm sayfanın html içeriğini çekiyoruz.
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument(); //kütüphanemizi kullanıp htmldocument oluşturuyoruz.
            document.LoadHtml(html);//documunt değişkeninin html ine çektiğimiz htmli veriyoruz

            // var secilenhtml = @"//*[@id=""app-root""]/div/div[3]/div/div[3]/div[1]/div[2]/main/article/div[1]"; // veriyi çekeceğimiz div alanı kategori listesi xpath adresi
            StringBuilder metin = new StringBuilder();
            HtmlNodeCollection secilenHtmlList = document.DocumentNode.SelectNodes("/html/body/div[1]/div/main/article/div[2]/div/div[1]/div[1]/div[1]"); //selectnodes methoduyla verdiğimiz xpathin htmlini getiriyoruz.
            foreach (var items in secilenHtmlList)
            {
                foreach (var innerItem in items.SelectNodes("p"))//her ul un içindeki li de dön
                {

                    metin.Append(innerItem.InnerHtml); // gelen değeri 

                }
            }
            return metin.ToString();
        }

    }
}
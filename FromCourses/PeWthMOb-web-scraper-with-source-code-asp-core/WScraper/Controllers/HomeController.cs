using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace WScraper.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        #region  1. Get Websites From Google Search

        [HttpGet]
        public IActionResult GetWebsiteFromGoogleSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetWebsiteFromGoogleSearch(string GoogleSearchQuery, string PageNumber)
        {
            // This section is for google page number
            /*=================================================
             Example :
             If we pass 0 than google page will show 1-10 result.
             If we pass 10 than google page will show 11-20 result.
             If we pass 20 than google page will show 21-30 result.
            =================================================*/

            // Below code is just to assign page numbers you can skip if you want.
            string Googlestart = "0";
            if (string.IsNullOrWhiteSpace(PageNumber.Trim()))
            {
                Googlestart = "0";
            }
            else
            {
                Googlestart = ((Convert.ToInt32(PageNumber.Trim()) - 1) * 10).ToString();
            }

            // HtmlAgilityPack is nuget Package for Holding Web page for Web scraping
            // LINK: https://html-agility-pack.net/
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

            // PageUrl is the url you passed from the inpu text
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.google.com/search?q=" + GoogleSearchQuery.Trim() + "&start=" + Googlestart);

            // Storing the page content in to the  variable for further processing
            string PageContent = doc.Text.ToString();


            /*==============================================================*/

            // Exrtracting Website List from the page :

            /*==============================================================*/


            //Using RegEx to check URL format in the contents of page.
            Regex RegForWebsite = new Regex(@"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", RegexOptions.IgnoreCase);

            //find websites that matches our RegEx i.e RegForWebsite
            MatchCollection WebsiteMatches = RegForWebsite.Matches(PageContent);


            List<string> WebsiteList = new List<string>();

            //try-catch for handing run time exception.
            try
            {
                foreach (Match WebsiteFullUrl in WebsiteMatches)
                {


                    Uri url = new Uri(WebsiteFullUrl.Value); // pass full urls to Uri for analysis.

                    // extract only website from the url
                    string ExtractedExactWebsite = url.Scheme + Uri.SchemeDelimiter + url.Authority + "/";
                    //don not add website which has google.com in domain
                    if (!ExtractedExactWebsite.ToLower().Contains("google.com"))
                    {
                        // add website to list
                        WebsiteList.Add(ExtractedExactWebsite);
                    }

                }
            }
            catch (Exception ex)
            {
                ;
            }
            // Remove Duplicate Websites & Assign it to ViewBag.DataWebsiteList
            ViewBag.DataWebsiteList = WebsiteList.Distinct().ToList();
            ViewBag.SearchText = GoogleSearchQuery;
            ViewBag.SearchPN = PageNumber;
            return View();
        }

        #endregion


        #region 2. Get URLs From ASK Search

        [HttpGet]
        public IActionResult GetUrlsFromAskSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUrlsFromAskSearch(string SearchQuery, string PageNumber)
        {
            // Example Search URL:https://www.ask.com/web?q=pizza&page=3

            // HtmlAgilityPack is nuget Package for Holding Web page for Web scraping
            // LINK: https://html-agility-pack.net/
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

            // PageUrl is the url you passed from the input text.
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.ask.com/web?q=" + SearchQuery.Trim() + "&page=" + PageNumber.Trim());

            // Storing the page content in to the  variable for further processing
            string PageContent = doc.Text.ToString();


            /*==============================================================*/

            // Exrtracting Urls from the page :

            /*==============================================================*/


            //Using RegEx to check URL format in the contents of page.
            Regex RegForUrls = new Regex(@"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", RegexOptions.IgnoreCase);

            //find urls that matches which our RegEx i.e RegForUrls
            MatchCollection URLsMatches = RegForUrls.Matches(PageContent);


            List<string> URLsList = new List<string>();

            //try-catch for handing run time exception.
            try
            {
                foreach (Match FullUrl in URLsMatches)
                {

                    // don not add urls which has ask.com in domain
                    if (!FullUrl.Value.ToLower().Contains("ask.com"))
                    {
                        // add URLs to list
                        URLsList.Add(FullUrl.Value);
                    }


                }
            }
            catch (Exception ex)
            {
                ;
            }
            // Remove Duplicate Urls & Assign it to ViewBag.DataUrlList
            ViewBag.DataUrlList = URLsList.Distinct().ToList();
            ViewBag.SearchText = SearchQuery;
            ViewBag.SearchPN = PageNumber;
            return View();
        }

        #endregion


        #region 3. Get Data From A url By Using ID selector of jquery

        [HttpGet]
        public IActionResult GetDataByID()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataByID(string PageUrl, string IdOfElement)
        {
            // HtmlAgilityPack is nuget Package for Holding Web page for Web scraping
            // LINK: https://html-agility-pack.net/
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = null;
            string PageContent = "";
            try
            {
                // PageUrl is the url you passed from the input text.
                doc = web.Load(PageUrl.Trim());
                // Storing the page content in to the  variable for further processing
                PageContent = doc.Text.ToString();
            }
            catch
            {
                //If HTTPS DON"T ALLOW TO DOWNLOAD PAGE source than this code shoul work
                var page = new HtmlWeb()
                {
                    PreRequest = request =>
                    {
                       
                        request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                        return true;
                    }
                };
                //trim the url
                var url = PageUrl.Trim();
                var data = page.Load(url);
                doc = data;
                // Storing the page content in to the  variable for further processing
                PageContent = data.Text.ToString();
            }


            /*==============================================================*/

            // Exrtracting Price of The Product :

            // For Testing
            // URL Example : https://www.amazon.com/G-Shock-Combi-Military-Watch-Black/dp/B003WPUU0U/ref=cts_wa_1_vtp
            // ID Example : price_inside_buybox

            /*==============================================================*/

            //try-catch for handing run time exception.
            try
            {
                string PriceOfProduct = "";

                // using XPATH selector id

                // For More selectors you can see below links:
                // https://johnresig.com/blog/xpath-css-selectors/
                // https://www.w3schools.com/xml/xpath_syntax.asp
                ////////////////////////////////////////////////////////

                var PriceData = doc.DocumentNode.SelectSingleNode("//*[@id='" + IdOfElement.Trim() + "']");

                if (PriceData != null)
                {
                    PriceOfProduct = PriceData.InnerText;
                }

                ViewBag.DataPriceOfProduct = PriceOfProduct;
                ViewBag.SearchText = PageUrl;
                ViewBag.SearchId = IdOfElement;
            }
            catch (Exception ex)
            {
                ;
            }

            return View();
        }

        #endregion


        #region 4. Get Data Using CSS Class attribute

        [HttpGet]
        public IActionResult GetDataByClass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataByClass(string PageUrl, string ClassOfElement)
        {
            // HtmlAgilityPack is nuget Package for Holding Web page for Web scraping
            // LINK: https://html-agility-pack.net/
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

            HtmlAgilityPack.HtmlDocument doc = null;

            string PageContent = "";
            try
            {
                // PageUrl is the url you passed from the input text.
                doc = web.Load(PageUrl.Trim());
                //Storing the page content in to the  variable for further processing
                PageContent = doc.Text.ToString();
            }
            catch
            {
                //If HTTPS DON"T ALLOW TO DOWNLOAD PAGE below code should work
                var page = new HtmlWeb()
                {
                    PreRequest = request =>
                    {
                        request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                        return true;
                    }
                };
                var url = PageUrl.Trim();
                var data = page.Load(url);
                doc = data;
                // Storing the page content in to the  variable for further processing
                PageContent = data.Text.ToString();
            }


            /*==============================================================*/

            // Exrtracting product categories  :

            // For Testing
            // URL Example : https://www.amazon.com/s/browse?_encoding=UTF8&node=16225005011&ref_=nav_shopall-export_nav_mw_sbd_intl_baby
            // Class Example : a-section a-text-center

            /*==============================================================*/

            //try-catch for handing run time exception.
            try
            {
                // List to hold the names of cats
                List<string> NameOfCats = new List<string>();

                // using XPATH selector for class // class can have multiple items.
                
                // For More selectors you can see below links:
                // https://johnresig.com/blog/xpath-css-selectors/
                // https://www.w3schools.com/xml/xpath_syntax.asp
                ////////////////////////////////////////////////////////

                var AllClassListData = doc.DocumentNode.SelectNodes("//*[contains(@class,'" + ClassOfElement.Trim() + "')]").ToList();

                if (AllClassListData != null && AllClassListData.Count() > 0)
                {
                    foreach (var SingleItem in AllClassListData)
                    {
                        NameOfCats.Add(SingleItem.InnerText);
                    }

                }

                ViewBag.DataRelatedProducts = NameOfCats;
                ViewBag.SearchText = PageUrl;
                ViewBag.SearchClass = ClassOfElement;
            }
            catch (Exception ex)
            {
                ;
            }

            return View();
        }

        #endregion


        #region 5. Get Emails From Google Search

        [HttpGet]
        public IActionResult GetEmailFromGoogleSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetEmailFromGoogleSearch(string GoogleSearchQuery, string PageNumber)
        {

            // Below code is just to assign page numbers you can skip if you want.
            string Googlestart = "0";
            if (string.IsNullOrWhiteSpace(PageNumber.Trim()))
            {
                Googlestart = "0";
            }
            else
            {
                Googlestart = ((Convert.ToInt32(PageNumber.Trim()) - 1) * 10).ToString();
            }

            // HtmlAgilityPack is nuget Package for Holding Web page for Web scraping
            // LINK: https://html-agility-pack.net/
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

            // PageUrl i sthe url you passed from the input text.
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.google.com/search?q=" + GoogleSearchQuery.Trim() + "&start=" + Googlestart.Trim());

            // Storing the page content in to the  variable for further processing
            string PageContent = doc.Text.ToString();


            /*==============================================================*/

            // Exrtracting Website List from the page :

            /*==============================================================*/


            //Using RegEx to check URL format in the contents of page.
            Regex RegForWebsite = new Regex(@"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", RegexOptions.IgnoreCase);

            //find websites that matches which our RegEx i.e RegForWebsite
            MatchCollection WebsiteMatches = RegForWebsite.Matches(PageContent);


            List<string> WebsiteList = new List<string>();

            //try-catch for handing run time exception.
            try
            {
                foreach (Match WebsiteFullUrl in WebsiteMatches)
                {


                    Uri url = new Uri(WebsiteFullUrl.Value); // pass full urls to Uri for analysis.

                    string ExtractedExactWebsite = url.Scheme + Uri.SchemeDelimiter + url.Authority + "/";

                    // add emails to list
                    WebsiteList.Add(ExtractedExactWebsite);

                }
            }
            catch (Exception ex)
            {
                ;
            }
            // Remove Duplicate Websites & Assign it to ViewBag.DataWebsiteList
            ViewBag.DataWebsiteList = WebsiteList.Distinct().ToList();

            /*==============================================================*/

            // Exrtracting Emails From list of websites home page :

            /*==============================================================*/

            //Now from every Website Get Emails from their domains

            var DistinctWebsites = WebsiteList.Distinct().ToList();

            List<string> EmailList = new List<string>();

            foreach (var website in DistinctWebsites)
            {
                try
                {
                    // try-catch if in case websites block parsing

                    HtmlAgilityPack.HtmlWeb webInternal = new HtmlAgilityPack.HtmlWeb();

                    // passing website url to get page content
                    HtmlAgilityPack.HtmlDocument docInternal = webInternal.Load(website);

                    string PageContentInternal = docInternal.Text.ToString();

                    //Using RegEx to check email format in the content of page.
                    Regex RegForEmail = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);

                    //find emails that matches which our RegEx i.e RegForEmail
                    MatchCollection emailMatches = RegForEmail.Matches(PageContentInternal);

                    //try-catch for handing run time exception.
                    try
                    {
                        foreach (Match EmailId in emailMatches)
                        {
                            EmailList.Add(EmailId.Value);
                            // add emails to list
                        }
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                }
                catch (Exception ex)
                {
                    ;
                }

            }
            ViewBag.DataEmailList = EmailList.Distinct().ToList();
            ViewBag.SearchText = GoogleSearchQuery;
            ViewBag.SearchPN = PageNumber;
            return View();
        }

        #endregion


        #region 6. Get Title And Emails From URL

        [HttpGet]
        public IActionResult GetPageTitleFromURL()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPageTitleFromURL(string PageUrl)
        {
            // HtmlAgilityPack is nuget Package for Holding Web page for Web scraping
            // LINK: https://html-agility-pack.net/
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

            // PageUrl is the url you passed from the input text.
            HtmlAgilityPack.HtmlDocument doc = web.Load(PageUrl.Trim());

            // Storing the page content in to the  variable for further processing
            string PageContent = doc.Text.ToString();


            /*==============================================================*/

            // Exrtracting Title of The page :

            /*==============================================================*/

            // We are scraping data of 'title' tag using XPATH Selectors 

            // For More XPATH selectors you can see below links:
            // https://johnresig.com/blog/xpath-css-selectors/
            // https://www.w3schools.com/xml/xpath_syntax.asp

            //try-catch for handing run time exception.
            try
            {
                string TitleOfPage = "";

                // using XPATH selector 
                var Title = doc.DocumentNode.SelectSingleNode("//title");

                if(Title!=null)
                {
                    TitleOfPage = Title.InnerText;
                }
               
                ViewBag.DataTitle = TitleOfPage;
            }
            catch(Exception ex)
            {
                ;
            }
            ViewBag.SearchedPageUrl = PageUrl;
            return View();
        }

        #endregion

        #region Notes

        // please note some websites block web scraping, so use this code wisely and extend with your logic.

        // Also you can extend these logic to any extend as per your logic and skill.

        #endregion












    }
}
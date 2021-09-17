using GalaSoft.MvvmLight.Command;
using HtmlAgilityPack;
using RssEspecial.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace RssEspecial.ViewModel
{
    public class DetailPageViewModel : BaseViewModel
    {

        private ItemModel _itemModel;
        public ItemModel ItemModel
        {
            get => _itemModel;
            set {
                _itemModel = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ContentModel> _contentItems;
        public ObservableCollection<ContentModel> ContentItems
        {
            get => _contentItems;
            set {
                _contentItems = value;
                RaisePropertyChanged();
            }

        }

        public RelayCommand GoBrowserCommand => new RelayCommand(GoBrowser);

        public void GoBrowser()
        {
            Device.OpenUri(new Uri(ItemModel.Url));
        }


        private async Task<List<ContentModel>> GetBloggerHtmlAsync(string url)
        {
            var contentList = new List<ContentModel>();
            //  var url = "http://www.juegosmental.es/2019/11/reto-del-triangulo-de-las-bermudas.html#.XfCbzvxS-Um";

            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var postHtml = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("id", "")
                 .Equals("main")).ToList();


            var title = postHtml[0].Descendants().Where(node => node.GetAttributeValue("class", "").Equals("post-title")).ToList()[0].InnerText.Trim('\r', '\n', '\t');
            Console.WriteLine(title);

            var innerHTML = postHtml[0].Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("post-body entry-content")).ToList()[0].Descendants().ToList();//[0].Descendants().ToList();

            foreach (var item in innerHTML)
            {
                var index = innerHTML.IndexOf(item);
                if (index == 83)
                {

                }
                if (item.Name == "#text")
                {
                    if (!String.IsNullOrEmpty(item.InnerHtml) && item.InnerHtml != "\n" && !item.InnerHtml.Contains("adsbygoogle"))
                    {
                        if (item.ParentNode.Name == "b")
                        {
                            Console.WriteLine($"{innerHTML.IndexOf(item)} | {item.Name} | {item.ParentNode.InnerHtml}");
                            contentList.Add(new ContentModel() { IsImage = false, Text = item.InnerHtml, FontAttributes = FontAttributes.Bold });
                        }
                        else
                        {
                            Console.WriteLine($"{innerHTML.IndexOf(item)} | {item.Name} | {item.InnerHtml}");
                            contentList.Add(new ContentModel() { IsImage = false, Text = item.InnerHtml });
                        }


                    }




                }
                else if (item.Name == "img")
                {
                    var urll = item.Attributes["src"].Value;

                    Console.WriteLine($"{innerHTML.IndexOf(item)} | {item.Name} | {urll}");
                    contentList.Add(new ContentModel() { IsImage = true, Text = urll });

                    var list = item.Descendants().ToList();
                }
            }

            return contentList;

        }
        private async Task<List<ContentModel>> GetWordePressHtmlAsync(string url)
        {
            var contentList = new List<ContentModel>();
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var postHtml = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("id", "")
                 .Equals("main")).ToList();


            //var title = postHtml[0].Descendants().Where(node => node.GetAttributeValue("class", "").Equals("entry-title")).ToList()[0].InnerText.Trim('\r', '\n', '\t');//  [0].InnerText.Trim('\r', '\n', '\t');
            //Console.WriteLine(title);

            var innerHTML = postHtml[0].Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("entry-content")).ToList()[0].Descendants().ToList();//[0].Descendants().ToList();

            foreach (var item in innerHTML)
            {
                //var urll = item.Attributes["src"].Value;

                //Console.WriteLine($"{innerHTML.IndexOf(item)} | {item.Name} | {urll}");
                //var list = item.Descendants().ToList();
                if (item.GetAttributeValue("class", "").Equals("entry-title"))
                {
                    var title = item.InnerText.Trim('\r', '\n', '\t');//  [0].InnerText.Trim('\r', '\n', '\t');
                    Console.WriteLine(title);

                }
                else
                if (item.Name == "#text")
                {

                    if (!String.IsNullOrEmpty(item.InnerHtml) && item.InnerHtml != "\n" && !item.InnerHtml.Contains("adsbygoogle"))
                    {
                        if (item.ParentNode.Name == "b")
                        {
                            Console.WriteLine($"{innerHTML.IndexOf(item)} | {item.Name} | {item.ParentNode.InnerHtml}");
                            contentList.Add(new ContentModel() { IsImage = false, Text = item.InnerHtml, FontAttributes = FontAttributes.Bold });
                        }
                        else
                        {
                            Console.WriteLine($"{innerHTML.IndexOf(item)} | {item.Name} | {item.InnerHtml}");
                            contentList.Add(new ContentModel() { IsImage = false, Text = item.InnerHtml });
                        }


                    }

                }
                else if (item.Name == "img")
                {

                    var urll = item.Attributes["src"].Value;
                    //if (urll.Contains("?"))
                    //{
                    //   urll= urll.Split('?')[0];
                    //}

                    Console.WriteLine($"{innerHTML.IndexOf(item)} | {item.Name} | {urll}");
                    contentList.Add(new ContentModel() { IsImage = true, Text = urll });

                }
            }



            return contentList;

        }



        internal async void Initilize(ItemModel model)
        {
            ItemModel = model;

            var loader = await MaterialDialog.Instance.LoadingDialogAsync("Loading");

            ContentItems = new ObservableCollection<ContentModel>();
            if (model.IsBlogger)
            {
                var list = await GetBloggerHtmlAsync(model.Url);
                ContentItems = new ObservableCollection<ContentModel>(list);
            }
            else
            {
                var list = await GetWordePressHtmlAsync(model.Url);
                ContentItems = new ObservableCollection<ContentModel>(list);
            }

            await loader.DismissAsync();
        }
    }
}

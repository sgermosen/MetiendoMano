using RssEspecial.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RssEspecial
{
    public class ContentDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextDataTemplate { get; set; }
        public DataTemplate ImageDataTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ContentModel contentModel)
            {
                return contentModel.IsImage ? ImageDataTemplate : TextDataTemplate;
            }
            return TextDataTemplate;
        }
    }
}

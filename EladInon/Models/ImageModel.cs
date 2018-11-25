using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using EladInon.Models;
using System.Web;

namespace ImageViewer.Models
{
    public class ImageModel : List<Image>
    {
        public ImageModel()
        {
            string directoryOfImage =  HttpContext.Current.Server.MapPath("~/Images/");
            XDocument imageData = XDocument.Load(directoryOfImage + @"/ImageMetaData.xml");
            var images = from image in imageData.Descendants("image") select new Image(image.Element("filename").Value, image.Element("description").Value);
            this.AddRange(images.ToList<Image>());
        }
    }
}


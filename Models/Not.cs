using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotUygulamasıUnited.Models
{
    public class Not 
    {
        public int NotId { get; set; } 
        public string Baslik { get; set; } 
        public string Icerik { get; set; } 
        public int? UstNotId { get; set; } 
        public virtual Not UstNot { get; set; } // Notun üstündeki not
        public virtual ICollection<Not> AltNotlar { get; set; } // Notun altındaki notlar
    }

}
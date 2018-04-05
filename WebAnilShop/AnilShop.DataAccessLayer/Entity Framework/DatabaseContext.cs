using AnilShop.DataAccessLayer;
using AnilShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnilShop.DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Adresler> Adresler { get; set; }
        public DbSet<Kampanya> Kampanyalar { get; set; }
        public DbSet<Sepet> Sepetler { get; set; }
        public DbSet<Resim> Resimler { get; set; }
        public DbSet<SepetUrun> SepetUrun { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }




        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}

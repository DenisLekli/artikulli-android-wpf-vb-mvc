using ArtikullServices.Data;
using ArtikullServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikullServices.Repository
{
    public class ProduktRepository
    {
        private readonly ArtikullServicesDbContext _context;

        private readonly DbSet<Produkt> _Produkt;
        public ProduktRepository(ArtikullServicesDbContext context)
        {
            _context = context;
            this._Produkt = _context.Set<Produkt>();
        }



        public IEnumerable<Produkt> GetProduktSearch(string name)
        {
            var productsLists = _Produkt.AsNoTracking().Where(n=>n.Emertimi.StartsWith(name)).ToList();
            
            return productsLists;
        }

        public bool addProdukt(Produkt produkt)
        {
            Produkt ProduktChecks = _context.Set<Produkt>().Find(produkt.id);
            if (ProduktChecks != null)
                return false;
            _Produkt.Add(produkt);
            _context.SaveChanges();
            return true;
        }


        public IEnumerable<Produkt> getProdukt()
        {
            return _Produkt.ToList();
        }
        public bool deleteProdukt(int id)
        {
            if(id==null)
                return false;
            var produkt = _Produkt.Find(id);
            _Produkt.Remove(produkt);
            _context.SaveChanges();
            return true;
        }




        public Produkt? getProduktId(int id)
        {
            var produkt = _Produkt.Find(id);
            if (produkt == null)
                return null;
            return produkt;
        }
        public bool editProdukt(Produkt produkt,int id)
        {
            var procuctDb = _context.Set<Produkt>().AsNoTracking().Where(n=>n.id==id).FirstOrDefault();
            if (procuctDb != null)
            {
                produkt.id = id;
                _Produkt.Update(produkt);
                _context.SaveChanges();
            }
            else
            {
                return false;
            }
            
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CosItem
    {
        public Carte produsCos { get; set; }
        public int cantitate { get; set; }
    }

    public class Cos
    {
        public int Id { get; set; }
        List<CosItem> items = new List<CosItem>();
        public IEnumerable<CosItem> Items { get { return items; } }


        public void Add(Carte carte, int cant = 1)
        {
            var item = items.FirstOrDefault(s => s.produsCos.Id == carte.Id);
            if(item == null)
            {
                items.Add(new CosItem
                {
                    produsCos = carte,
                    cantitate = cant
                }) ;
            }
            else
            {
                item.cantitate += cant;
            }
        }

        public void UpdateCantitate(int id, int cantitate)
        {
            var item = items.Find(s => s.produsCos.Id == id);
            if(item != null)
            {
                item.cantitate = cantitate;
            }
        }

        public double TotalMoney()
        {
            var total = items.Sum(s => s.produsCos.Pret * s.cantitate);
            return (double)total;
        }

        public void RemoveElement(int id)
        {
            items.RemoveAll(s => s.produsCos.Id == id);
        }

    }


}
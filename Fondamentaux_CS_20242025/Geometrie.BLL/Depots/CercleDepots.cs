using Geometrie.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrie.BLL.Depots
{
    public class CercleDepot : IDepot<Cercle>
    {
        private readonly GeometrieContext _context;

        public CercleDepot(GeometrieContext context)
        {
            _context = context;
        }

        public IEnumerable<Cercle> GetAll() => _context.cercle.Select(c => new Cercle(new Point((int)c.CentreX, (int)c.CentreY), c.Rayon)).ToList();

        public Cercle? GetById(int id)
        {
            var cercleDal = _context.cercle.Find(id);
            if (cercleDal == null) return null;
            return new Cercle(new Point((int)cercleDal.CentreX, (int)cercleDal.CentreY), cercleDal.Rayon);
        }

        public void Add(Cercle cercle)
        {
            var cercleDal = new Cercle_DAL
            {
                CentreX = cercle.Centre.X,
                CentreY = cercle.Centre.Y,
                Rayon = cercle.Rayon
            };
            _context.cercle.Add(cercleDal);
            _context.SaveChanges();
        }

        public void Update(Cercle cercle)
        {
            var cercleDal = new Cercle_DAL
            {
                Id = cercle.Centre.Id ?? 0,
                CentreX = cercle.Centre.X,
                CentreY = cercle.Centre.Y,
                Rayon = cercle.Rayon
            };
            _context.cercle.Update(cercleDal);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var cercleDal = _context.cercle.Find(id);
            if (cercleDal != null)
            {
                _context.cercle.Remove(cercleDal);
                _context.SaveChanges();
            }
        }

        Cercle IDepot<Cercle>.Add(Cercle element)
        {
            var cercleDal = new Cercle_DAL
            {
                CentreX = element.Centre.X,
                CentreY = element.Centre.Y,
                Rayon = element.Rayon
            };
            _context.cercle.Add(cercleDal);
            _context.SaveChanges();
            var cercle = new Cercle(new Point(element.Centre.X, element.Centre.Y), element.Rayon);
            var idProperty = typeof(Cercle).GetProperty("Id");
            if (idProperty != null)
            {
                idProperty.SetValue(cercle, cercleDal.Id);
            }
            return cercle;
        }

        Cercle IDepot<Cercle>.Update(Cercle element)
        {
            var cercleDal = _context.cercle.Find(element.Id);
            if (cercleDal == null) throw new ArgumentException("Cercle not found");

            cercleDal.CentreX = element.Centre.X;
            cercleDal.CentreY = element.Centre.Y;
            cercleDal.Rayon = element.Rayon;

            _context.cercle.Update(cercleDal);
            _context.SaveChanges();

            return element;
        }

        public IDepot<Cercle> Delete(Cercle element)
        {
            var cercleDal = _context.cercle.Find(element.Id);
            if (cercleDal != null)
            {
                _context.cercle.Remove(cercleDal);
                _context.SaveChanges();
            }
            return this;
        }

        IDepot<Cercle> IDepot<Cercle>.Delete(int id)
        {
            var cercleDal = _context.cercle.Find(id);
            if (cercleDal != null)
            {
                _context.cercle.Remove(cercleDal);
                _context.SaveChanges();
            }
            return this;
        }
    }

}

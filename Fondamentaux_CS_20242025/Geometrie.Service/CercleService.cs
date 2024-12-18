using Geometrie.BLL.Depots;
using Geometrie.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrie.Service
{
    public class CercleService
    {
        private readonly IDepot<Cercle> _depot;

        public CercleService(IDepot<Cercle> depot)
        {
            _depot = depot;
        }

        public IEnumerable<Cercle> GetAllCercles() => _depot.GetAll();

        public Cercle? GetCercleById(int id)
        {
            return _depot.GetById(id);
        }

        public void AddCercle(Cercle cercle) => _depot.Add(cercle);

        public void UpdateCercle(Cercle cercle) => _depot.Update(cercle);

        public void DeleteCercle(int id) => _depot.Delete(id);

        public double GetTotalArea(IEnumerable<int> ids)
        {
            return _depot.GetAll().Where(c => ids.Contains(c.Id)).Sum(c => c.CalculerAire());
        }
    }

}

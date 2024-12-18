using Geometrie.BLL;
using Geometrie.BLL.Depots;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrie.Service.Tests
{
    public class CercleService_Tests
    {
        // Arrange
        private readonly CercleService _cercleService;
        private readonly Mock<IDepot<Cercle>> _mockDepot;
        private readonly List<Cercle> _cercles;

        // Arc
        public CercleService_Tests()
        {
            _cercles = new List<Cercle>
                {
                    new Cercle(new Point(0, 0), 1),
                    new Cercle(new Point(0, 0), 2),
                    new Cercle(new Point(0, 0), 3),
                    new Cercle(new Point(0, 0), 4),
                    new Cercle(new Point(0, 0), 5),
                };

            _mockDepot = new Mock<IDepot<Cercle>>();
            _mockDepot.Setup(d => d.GetAll()).Returns(_cercles);
            _mockDepot.Setup(d => d.GetById(It.IsAny<int>())).Returns((int id) => _cercles.Find(c => c.Id == id));
            _mockDepot.Setup(d => d.Add(It.IsAny<Cercle>())).Callback((Cercle c) => _cercles.Add(c));
            _mockDepot.Setup(d => d.Update(It.IsAny<Cercle>())).Callback((Cercle c) =>
            {
                var index = _cercles.FindIndex(existingCercle => existingCercle.Id == c.Id);
                if (index != -1)
                {
                    _cercles[index] = c;
                }
            });
            _mockDepot.Setup(d => d.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var cercle = _cercles.Find(c => c.Id == id);
                if (cercle != null)
                {
                    _cercles.Remove(cercle);
                }
            });

            _cercleService = new CercleService(_mockDepot.Object);
        }

        //Assert

        [Fact]
        public void GetAllCercles_ShouldReturnAllCercles()
        {
            // Act
            var cercles = _cercleService.GetAllCercles();

            // Assert
            Assert.Equal(_cercles, cercles);
        }
        [Fact]
        public void GetCercleById_ShouldReturnCercle()
        {
            // Arrange
            var cercle = _cercles.First();

            // Act
            var result = _cercleService.GetCercleById(cercle.Id);

            // Assert
            Assert.Equal(cercle, result);
        }
        [Fact]
        public void AddCercle_ShouldAddCercle()
        {
            // Arrange
            var cercle = new Cercle(new Point(0, 0), 6);

            // Act
            _cercleService.AddCercle(cercle);

            // Assert
            Assert.Contains(cercle, _cercles);
        }
        [Fact]
        public void UpdateCercle_ShouldUpdateCercle()
        {
            // Arrange
            var cercle = _cercles.First();
            var newCercle = new Cercle(new Point(0, 0), 6);
            var idProperty = typeof(Cercle).GetProperty("Id");
            if (idProperty != null)
            {
                idProperty.SetValue(newCercle, cercle.Id);
            }

            // Act
            _cercleService.UpdateCercle(newCercle);

            // Assert
            Assert.Contains(newCercle, _cercles);
        }
        [Fact]
        public void DeleteCercle_ShouldDeleteCercle()
        {
            // Arrange
            var cercle = _cercles.First();

            // Act
            _cercleService.DeleteCercle(cercle.Id);

            // Assert
            Assert.DoesNotContain(cercle, _cercles);
        }
        [Fact]

        public void GetTotalArea_ShouldReturnTotalArea()
        {
            // Arrange
            var ids = new List<int> { 1, 3, 5 };
            var expectedArea = _cercles.Where(c => ids.Contains(c.Id)).Sum(c => c.CalculerAire());

            // Act
            var totalArea = _cercleService.GetTotalArea(ids);

            // Assert
            Assert.Equal(expectedArea, totalArea);
        }
    }
}

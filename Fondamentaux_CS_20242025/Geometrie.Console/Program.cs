﻿using Geometrie.BLL;
using Geometrie.BLL.Exceptions;
using Geometrie.Service;
using Geometrie.BLL.Depots;
using Geometrie.DAL;



var context = new GeometrieContext();
IDepot<Cercle> cercleDepot = new CercleDepot(context);
CercleService cercleService = new CercleService(cercleDepot);

var p1 = new Point(4, 8);
var p2 = new Point(3, 5);
Console.WriteLine(p1.CalculerDistance(p2));
Console.WriteLine(p1.X);

var tri = new Triangle(p1, p2, new Point(1, 2));

for (int i = 0; i < tri.Count; i++)
{
    Console.WriteLine(tri[0]);
}

foreach (var point in tri)
{
    Console.WriteLine(point);
}

cercleService.AddCercle(new Cercle(p1, 3.0));
cercleService.AddCercle(new Cercle(p2, 4.0));
cercleService.AddCercle(new Cercle(new Point(0, 0), 5.0));

List<int> ids = new List<int> { 1, 3 };
double totalArea = cercleService.GetTotalArea(ids);

Console.WriteLine($"L'aire totale des cercles avec les IDs fournis est : {totalArea}");

var cercle = new Cercle(p1, 5);
Console.WriteLine(tri.CalculerPerimetre());
Console.WriteLine(cercle.CalculerPerimetre());

var listeDeFormes = new List<IForme>();
listeDeFormes.Add(tri);
listeDeFormes.Add(cercle);

foreach (var forme in listeDeFormes)
{
    Console.WriteLine($"Périmètre : {forme.CalculerPerimetre()}");
    Console.WriteLine($"Aire : {forme.CalculerAire()}");
}

try
{
    var a = new Point(1, 1);
    var b = new Point(2, 2);
    var c = new Point(3, 3);

    var triangleQuiPlante = new Triangle(a, b, c);
}
catch (PolygoneException ex)
{
    Console.WriteLine(ex.Message);
    foreach (var point in ex.Points)
    {
        Console.WriteLine(point);
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


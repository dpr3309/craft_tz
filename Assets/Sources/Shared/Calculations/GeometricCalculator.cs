using System;
using UnityEngine;

namespace Craft_TZ.Shared.Calculations 
{
    public class GeometricCalculator
    {
        public static double CalculateAreaOfTriangle(Vector2 point1, Vector2 point2, Vector2 point3)
        {
            double area = Math.Abs(0.5f * (point1.x * point2.y - point2.x * point1.y + point2.x * point3.y - point3.x * point2.y + point3.x * point1.y - point1.x * point3.y));
            return area;
        }

        public static double CalculateAreaOfSquare(float sideLength)
        {
            if (Math.Abs(sideLength) < 1e-5)
                Debug.LogWarning($"[GeometricCalculator.CalculateAreaOfSquare] sideLength = 0!");

            return Math.Pow(sideLength, 2);
        }

        public static bool SquareContainsPoint(Vector2 coordinatesCenterOfSquare, Vector2 coordinatesPoint, float halfSideLength, double squareArea)
        {
            if (squareArea == 0)
                throw new Exception($"[GeometricCalculator.SquareContainsPoint] square area = 0!");

            // точка лежит в области квадратa
            // если сумма площадей всех треугольников, образованных точкой и двумя точками всех сторон равна площади всего квадрата

            //получим вершины квадрата
            Vector2 a = new Vector2(coordinatesCenterOfSquare.x - halfSideLength, coordinatesCenterOfSquare.y + halfSideLength);
            Vector2 b = new Vector2(coordinatesCenterOfSquare.x + halfSideLength, coordinatesCenterOfSquare.y + halfSideLength);
            Vector2 c = new Vector2(coordinatesCenterOfSquare.x + halfSideLength, coordinatesCenterOfSquare.y - halfSideLength);
            Vector2 d = new Vector2(coordinatesCenterOfSquare.x - halfSideLength, coordinatesCenterOfSquare.y - halfSideLength);

            var areaOfTriangle1 = CalculateAreaOfTriangle(a, b, coordinatesPoint);
            var areaOfTriangle2 = CalculateAreaOfTriangle(b, c, coordinatesPoint);
            var areaOfTriangle3 = CalculateAreaOfTriangle(c, d, coordinatesPoint);
            var areaOfTriangle4 = CalculateAreaOfTriangle(a, d, coordinatesPoint);

            return Math.Abs(areaOfTriangle1 + areaOfTriangle2 + areaOfTriangle3 + areaOfTriangle4 - squareArea) < 1e-5;
        }

        public static bool CircleContainsPoint(Vector2 coordinatesCenterOfCircle, Vector2 coordinatesPoint, float radius)
        {
            return (Vector2.Distance(coordinatesCenterOfCircle, coordinatesPoint) - radius) < 1e-5;
        }
    }

}
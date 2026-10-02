using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Triangles.Models;

namespace Triangles.Controllers
{
    public class TriangleController : Controller
    {
        public string Info(Triangle tr)
        {
            double[] sides = { tr.Side1, tr.Side2, tr.Side3 };
            Array.Sort(sides);

            double perimeter = sides[0] + sides[1] + sides[2];
            double p = perimeter / 2;
            double area = Math.Sqrt(p * (p - sides[0]) * (p - sides[1]) * (p - sides[2]));

            return string.Format(
                "Triangle:{0}({1}, {2}, {3}){0}Reduced:{0}({4:F2}, {5:F2}, {6:F2}){0}{0}Area = {7:F2}{0}Perimeter = {8}",
                Environment.NewLine,
                sides[0], sides[1], sides[2],
                sides[0] / perimeter, sides[1] / perimeter, sides[2] / perimeter,
                area, perimeter
            );
        }

        public string Area(Triangle tr)
        {
            double p = (tr.Side1 + tr.Side2 + tr.Side3) / 2;
            double area = Math.Sqrt(p * (p - tr.Side1) * (p - tr.Side2) * (p - tr.Side3));
            return string.Format("{0:F4}", area);
        }

        public double Perimeter(Triangle tr)
        {
            return tr.Side1 + tr.Side2 + tr.Side3;
        }

        public bool IsRightAngled(Triangle tr)
        {
            double[] s = { tr.Side1, tr.Side2, tr.Side3 };
            Array.Sort(s);
            return Triangle.AreEqual(s[0] * s[0] + s[1] * s[1], s[2] * s[2]);
        }

        public bool IsEquilateral(Triangle tr)
        {

            return Triangle.AreEqual(tr.Side1, tr.Side2) &&
                   Triangle.AreEqual(tr.Side2, tr.Side3) &&
                   Triangle.AreEqual(tr.Side1, tr.Side3);
        }

        public bool IsIsosceles(Triangle tr)
        {
            return Triangle.AreEqual(tr.Side1, tr.Side2) || Triangle.AreEqual(tr.Side2, tr.Side3) || Triangle.AreEqual(tr.Side1, tr.Side3);
        }

        public bool AreCongruent(Triangle tr1, Triangle tr2)
        {
            double[] s1 = { tr1.Side1, tr1.Side2, tr1.Side3 };
            double[] s2 = { tr2.Side1, tr2.Side2, tr2.Side3 };
            Array.Sort(s1);
            Array.Sort(s2);

            return Triangle.AreEqual(s1[0], s2[0]) && Triangle.AreEqual(s1[1], s2[1]) && Triangle.AreEqual(s1[2], s2[2]);
        }

        public bool AreSimilar(Triangle tr1, Triangle tr2)
        {
            double[] s1 = { tr1.Side1, tr1.Side2, tr1.Side3 };
            double[] s2 = { tr2.Side1, tr2.Side2, tr2.Side3 };
            Array.Sort(s1);
            Array.Sort(s2);

            double ratio1 = s1[0] / s2[0];
            double ratio2 = s1[1] / s2[1];
            double ratio3 = s1[2] / s2[2];

            return Triangle.AreEqual(ratio1, ratio2) && Triangle.AreEqual(ratio2, ratio3);
        }

        public string InfoGreatestPerimeter(Triangle[] tr)
        {
            if (tr == null || tr.Length == 0) return string.Empty;

            var maxTr = tr.OrderByDescending(t => t.Side1 + t.Side2 + t.Side3).First();
            return Info(maxTr);
        }

        public string InfoGreatestArea(Triangle[] tr)
        {
            if (tr == null || tr.Length == 0) return string.Empty;

            var maxTr = tr.OrderByDescending(t =>
            {
                double p = (t.Side1 + t.Side2 + t.Side3) / 2;
                return Math.Sqrt(p * (p - t.Side1) * (p - t.Side2) * (p - t.Side3));
            }).First();
            return Info(maxTr);
        }

        public string NumbersPairwiseNotSimilar(Triangle[] tr)
        {
            string result = "";
            for (int i = 0; i < tr.Length; i++)
            {
                for (int j = i + 1; j < tr.Length; j++)
                {
                    if (!AreSimilar(tr[i], tr[j]))
                    {
                        if (result != "") result += Environment.NewLine;
                        result += $"({i + 1}, {j + 1})";
                    }
                }
            }
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using Pudelko.Utils;
using Pudelko.Enums;

namespace Pudelko {

    class Program {
        static void Main(string[] args) {
            var p1 = new Pudelko(1, 2, 3);
            var p2 = new Pudelko(3, 5.05, 7.77);
            
            Console.WriteLine(p1.ToString("m"));
            Console.WriteLine(p2.ToString("m"));

            Console.WriteLine("Objętość " + p1.Objetosc);
            Console.WriteLine("Pole " + p2.Pole);

            Console.WriteLine("Czy pudełka są takie same? " + p1.Equals(p2));
        }
    }
    
}
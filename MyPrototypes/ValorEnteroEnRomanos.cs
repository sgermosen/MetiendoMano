using System;
using System.Collections.Generic;

namespace RomanosANumerosx
{
    public class CadenaValor
    {
        public int ValorEntero { get; set; }
        public string ValorRomano { get; set; }

    }

    class Program
    {

        static void Main(string[] args)
        {
            var listaElementos = new List<CadenaValor>();
            bool cadenaValida;

            listaElementos.Add(new CadenaValor { ValorEntero = 900, ValorRomano = "CM" });
            listaElementos.Add(new CadenaValor { ValorEntero = 400, ValorRomano = "CD" });
            listaElementos.Add(new CadenaValor { ValorEntero = 90, ValorRomano = "XC" });
            listaElementos.Add(new CadenaValor { ValorEntero = 40, ValorRomano = "XL" });
            listaElementos.Add(new CadenaValor { ValorEntero = 9, ValorRomano = "IX" });
            listaElementos.Add(new CadenaValor { ValorEntero = 4, ValorRomano = "IV" });
            listaElementos.Add(new CadenaValor { ValorEntero = 1, ValorRomano = "I" });
            listaElementos.Add(new CadenaValor { ValorEntero = 5, ValorRomano = "V" });
            listaElementos.Add(new CadenaValor { ValorEntero = 10, ValorRomano = "X" });
            listaElementos.Add(new CadenaValor { ValorEntero = 50, ValorRomano = "L" });
            listaElementos.Add(new CadenaValor { ValorEntero = 100, ValorRomano = "C" });
            listaElementos.Add(new CadenaValor { ValorEntero = 500, ValorRomano = "D" });
            listaElementos.Add(new CadenaValor { ValorEntero = 1000, ValorRomano = "M" });

            string cadenaUsuario;
            int valorNumerico = 0;
            int repetir;

            do
            {
                Console.WriteLine("Digite una cadena romana Valida");
                cadenaUsuario = Console.ReadLine();

                foreach (var item in listaElementos)
                {
                    var nuevaCadena = cadenaUsuario.Replace(item.ValorRomano, "");
                    var cantidad = (cadenaUsuario.Length - nuevaCadena.Length) / item.ValorRomano.Length;
                    cadenaUsuario = nuevaCadena;
                    valorNumerico += cantidad * item.ValorEntero;
                }

                Console.WriteLine($"El valor numerico es: {valorNumerico}");
                Console.WriteLine("Desea repetir?   1:No, Cualquier otra opcion:Si");
                try
                {
                    repetir = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    repetir = 0;
                    valorNumerico = 0;
                }

            } while (cadenaValida = false || repetir == 0);

            Console.WriteLine("Uy.... que tarde es..... Bueno me Voy!!!!!!");
        }


       
    }
}

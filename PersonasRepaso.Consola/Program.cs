using System;
using System.Collections.Generic;
using PersonasRepaso.Datos;
using PersonasRepaso.Entidades;

namespace PersonasRepaso.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            Persona persona = new Persona();
            persona.Nombres = "Juanito";
            persona.Apellido = "Perez";
            persona.Sexo = Sexo.Masculino;
            persona.DNI = "24.243.568";
            persona.FechaDeNacimiento = new DateTime(2000, 10, 5);
            persona.Altura = 1.6;
            persona.Peso = 64;

            Console.Write($"IMC: {persona.CalcularIMC()}");

        }
    }
}

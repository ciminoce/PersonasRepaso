using System;

namespace PersonasRepaso.Entidades
{
    public class Persona
    {
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        public Sexo Sexo { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string DNI { get; set; }

        public Persona()
        {
            
        }

        public int InformarEdad()
        {
            return (int) Math.Truncate(DateTime.Today.Subtract(FechaDeNacimiento).TotalDays / 365.25);
        }
        public string DecirNombreApellido()
        {
            return $"{Nombres} {Apellido}";
        }
        
        public override string ToString()
        {

            return $"{Apellido.ToUpper()}, {Nombres} - {DNI} - Edad={InformarEdad()} "; 
        }

    }
}

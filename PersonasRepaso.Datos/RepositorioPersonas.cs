using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PersonasRepaso.Entidades;

namespace PersonasRepaso.Datos
{
    public class RepositorioPersonas
    {
        private readonly string _archivo = Environment.CurrentDirectory + "\\Personas.txt";
        private readonly string _archivoBak = Environment.CurrentDirectory + "\\Personas.bak";
        public List<Persona> listaPersonas { get; set; } = new List<Persona>();

        public RepositorioPersonas()
        {
            LeerDatosDelArchivo();
        }

        private void LeerDatosDelArchivo()
        {
            if (File.Exists(_archivo))
            {
                StreamReader lector = new StreamReader(_archivo);
                while (!lector.EndOfStream)
                {
                    var linea = lector.ReadLine();
                    Persona persona = ConstruirPersona(linea);
                    listaPersonas.Add(persona);
                }
                lector.Close();

            }
        }

        private Persona ConstruirPersona(string linea)
        {
            var campos = linea.Split(';');
            Persona persona = new Persona
            {
                Apellido = campos[0],
                Nombres = campos[1],
                DNI = campos[2],
                Sexo = (Sexo) int.Parse(campos[3]),
                FechaDeNacimiento = DateTime.Parse(campos[4])
            };
            return persona;

        }

        public void Agregar(Persona persona)
        {
            AgregarRegistroAlArchivo(persona);

            listaPersonas.Add(persona);
        }

        private void AgregarRegistroAlArchivo(Persona persona)
        {
            StreamWriter escritor = new StreamWriter(_archivo, true);
            var linea = ConstruirLinea(persona);
            escritor.WriteLine(linea);
            escritor.Close();
        }

        private string ConstruirLinea(Persona persona)
        {
            var linea =
                $"{persona.Apellido};{persona.Nombres};{persona.DNI};{persona.Sexo.GetHashCode()};{persona.FechaDeNacimiento.ToShortDateString()}";
            return linea;
        }

        public void Borrar(Persona persona)
        {
            BorrarRegistroDelArchivo(persona);


            listaPersonas.Remove(persona);
        }

        private void BorrarRegistroDelArchivo(Persona persona)
        {
            /*Abro el archivo de lectura*/
            StreamReader lector = new StreamReader(_archivo);
            /*Abro el archivo de escritura*/
            StreamWriter escritor = new StreamWriter(_archivoBak);
            /*Recorro todo el archivo que tengo que leer*/
            while (!lector.EndOfStream)
            {
                var linea = lector.ReadLine(); //Leo una linea
                Persona personaEnArchivo = ConstruirPersona(linea); //La convierto a Persona
                if (personaEnArchivo.DNI != persona.DNI) //Comparo los DNI
                {
                    /*Si son distintos guardo los datos de la persona
                     del archivo de lectura en el archivo de escritura
                    porque no es la persona que quiero borrar
                    En caso que los DNI sea iguales, se saltea la persona y
                    se continua con la lectura y grabación*/
                    escritor.WriteLine(linea);
                }
            }

            lector.Close();
            escritor.Close();
            File.Delete(_archivo);
            File.Move(_archivoBak, _archivo);
        }

        public void Editar(Persona personaModificada)
        {
            StreamReader lector = new StreamReader(_archivo);
            StreamWriter escritor = new StreamWriter(_archivoBak);
            while (!lector.EndOfStream)
            {
                var linea = lector.ReadLine();
                var personaEnArchivo = ConstruirPersona(linea);
                if (personaEnArchivo.DNI != personaModificada.DNI)
                {
                    escritor.WriteLine(linea);
                }
                else
                {
                    linea = ConstruirLinea(personaModificada);
                    escritor.WriteLine(linea);
                }
            }
            lector.Close();
            escritor.Close();
            File.Delete(_archivo);
            File.Move(_archivoBak, _archivo);

        }

        public List<Persona> GetLista()
        {
            return listaPersonas;
        }

        public Persona GetPersona(string dni)
        {
            return listaPersonas.FirstOrDefault(p => p.DNI == dni);

        }

        public List<Persona> GetLista(Sexo sexo)
        {
            return listaPersonas.Where(p => p.Sexo == sexo).ToList();
        }
        public int GetCantidad()
        {
            return listaPersonas.Count;
        }

        public List<Persona> GetListaOrdenada()
        {
            return listaPersonas.OrderByDescending(p => p.InformarEdad()).ToList();
        }

        public List<Persona> GetListaFiltrada(Sexo femenino)
        {
            return listaPersonas.Where(p => p.Sexo == femenino).ToList();
        }
    }
}

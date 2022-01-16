using System;

namespace ProgramaMultiplicacion
{
  public  struct Preguntas
    {
        public string pregunta;
        public string mensajeRespuesta;
        public int respuestaCorrecta;
        public int intentosMaximos;
        public bool siguientePregunta;
        public int intentoActual;
        public int maximoPreguntas;
    }

    public  class Functions
    {
      
        int numeralPreguntas;

       public Preguntas obtenerPreguntas()
        {
            // srand(time(0));
            Random rand = new Random();
            int multiplicando = (rand.Next() % 10) + 1;
            int multiplicador = (rand.Next() % 10) + 1;
            numeralPreguntas = (numeralPreguntas + 1);
            Preguntas preguntaActual = new Preguntas();
            preguntaActual.intentosMaximos = 4;
            preguntaActual.pregunta = numeralPreguntas.ToString() + " - Cuanto es " +
           multiplicando.ToString() + " por " + multiplicador.ToString() + "?";
            preguntaActual.respuestaCorrecta = (multiplicando * multiplicador);
            preguntaActual.maximoPreguntas = 15;
            return preguntaActual;
        }
      public  Preguntas verificarRespuesta(int respuestaUsuario, Preguntas preg)
        {
            string[] respuestaCorrecta = new string[4] { "Bravo, eres un campeon!", "Excelente!", "Buen trabajo!", "Muy bien!" };
            string[] respuestaInCorrecta = new string[4] { "Uf! No es correcta, por favor intenta de nuevo.","Incorrecto." +
            "Intenta una vez mas.","No te rindas! Trata de nuevo.","Tu puedes, sigue intentando." };
            int intentoActual = preg.intentoActual;
            if (preg.respuestaCorrecta == respuestaUsuario)
            {
                preg.mensajeRespuesta = respuestaCorrecta[intentoActual];
                preg.siguientePregunta = true;
            }
            else
            {
                preg.mensajeRespuesta = respuestaInCorrecta[intentoActual];
                preg.siguientePregunta = false;
            }
            return preg;

        }

    }

    class Program
    {
         
        static void Main(string[] args)
        {
            // Preguntas obtenerPreguntas();
            //  Preguntas verificarRespuesta(int respuestaUsuario, Preguntas preg);

           var myfunc = new Functions();
            Console.WriteLine(" \t **Bienvenido al sistema de calculadora v1.0** ");
        int intentos = 0;
        int maxPreg = 0;
        int cantRespuestasCorrectas = 0;
        int cantRespuestasIncorrectas = 0;
            Preguntas preg = myfunc.obtenerPreguntas();
        preg.intentoActual = 0;
        do
        {
                Console.WriteLine(preg.pregunta);
            int respuestaUsuario;
                respuestaUsuario = Convert.ToInt32( Console.ReadLine());
            preg = myfunc.verificarRespuesta(respuestaUsuario, preg);
           
            Console.WriteLine( preg.mensajeRespuesta );
            if (preg.siguientePregunta)
            {
                intentos = 0;
                maxPreg = maxPreg + 1;
                cantRespuestasCorrectas = cantRespuestasCorrectas + 1;
                preg = myfunc.obtenerPreguntas();
                preg.intentoActual = 0;
            }
            else
            {
                intentos = intentos + 1;
            }
            preg.intentoActual = intentos;
            //Si agoto los intentos maximos
            //muestro la respuesta y paso a la siguiente pregunta
            if (intentos >= preg.intentosMaximos)
            {
                cantRespuestasIncorrectas = cantRespuestasIncorrectas + 1;

                    Console.WriteLine("Maximo de intentos alcanzados. La respuesta correcta es: " +
               preg.respuestaCorrecta.ToString());
                intentos = 0;
                maxPreg = maxPreg + 1;
                preg = myfunc.obtenerPreguntas();
                preg.intentoActual = 0;
            }
        } while (maxPreg < preg.maximoPreguntas);
        int _cantRespuestasCorrectas = cantRespuestasCorrectas;
        int _cantRespuestasIncorrectas = cantRespuestasIncorrectas;
            Console.WriteLine("Cantidad de respuestas correctas: " + _cantRespuestasCorrectas.ToString());

            Console.WriteLine("Cantidad de respuestas incorrectas: " + _cantRespuestasIncorrectas.ToString());
        float rendimientoCorrecto = (float)(cantRespuestasCorrectas / 15.00) * 100;
        float rendimientoInCorrecto = (float)(cantRespuestasIncorrectas / 15.00) * 100;
        int _rendimientoCorrecto = Convert.ToInt32(rendimientoCorrecto);
        if (_rendimientoCorrecto > 70)
        {
                Console.WriteLine(" \t * Tu rendimiento en porcentaje es: " +
            _rendimientoCorrecto.ToString() + "% * ");
                Console.WriteLine(" \t * Felicitaciones, puedes avanzar al siguiente nivel! * ");
        }
        else
        {
            int _rendimientoInCorrecto = Convert.ToInt32( rendimientoInCorrecto);
                Console.WriteLine(" \t * Tu rendimiento en porcentaje es: " +
            _rendimientoInCorrecto.ToString() + "% * " );
                Console.WriteLine(" \t * Te recomendamos que repitas el ejercicio * ");
        }
    }
   
    }
    }
 

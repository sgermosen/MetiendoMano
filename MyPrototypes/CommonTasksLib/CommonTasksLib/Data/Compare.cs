using System;
using System.Collections.Generic;

namespace CommonTasksLib.Data
{
    /// <summary>
    /// Clase customizada para realizar comparación entre instancias
    /// de una misma clase.
    /// </summary>
    /// <typeparam name="T">Tipo de clase sobre la cual se realiza la comparación.</typeparam>
    public class Compare<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Función de comparación para establecer la comparación.
        /// </summary>
        readonly Func<T, T, bool> compareFunction;

        /// <summary>
        /// Función de hash para comparación de los objetos en base
        /// a una clave hash.
        /// </summary>
        readonly Func<T, int> hashFunction;

        /// <summary>
        /// Constructor para la clase de comparación que establece la
        /// función de comparación en base a la función de comparación
        /// enviada como argumento.
        /// </summary>
        /// <param name="compareFunction">
        /// Función de comparación a ser usada para realizar la comparación.
        /// </param>
        public Compare(Func<T, T, bool> compareFunction)
        {
            this.compareFunction = compareFunction;
        }

        /// <summary>
        /// Constructor para la clase de comparación que establece la
        /// función de comparación y la función de hash en base a los
        /// argumentos de comparación y hash recibidos.
        /// </summary>
        /// <param name="compareFunction">
        /// Función de comparación a ser usada para realizar la comparación.
        /// </param>
        /// <param name="hashFunction">
        /// Función de hash a ser usada para la comparación hash.
        /// </param>
        public Compare(Func<T, T, bool> compareFunction, Func<T, int> hashFunction)
        {
            this.compareFunction = compareFunction;
            this.hashFunction = hashFunction;
        }

        /// <summary>
        /// Método utilizado para realizar la comparación de igualdad entre 2 objetos
        /// enviados por argumento, utilizando la función de comparación establecida
        /// para realizar la comparación.
        /// </summary>
        /// <param name="x">Objeto x del tipo T a ser comparado</param>
        /// <param name="y">Objeto y del tipo T a ser comparado</param>
        /// <returns>
        /// valor Booleano indicando si los objetos son iguales en base al criterio
        /// de comparación utilizado.
        /// </returns>
        public bool Equals(T x, T y)
        {
            return compareFunction(x, y);
        }

        /// <summary>
        /// Función que calcula la clave hash de un objeto recibido por argumento
        /// en base a la función de hash definida.
        /// </summary>
        /// <param name="obj">Objeto al que se le calculará la clave hash.</param>
        /// <returns>
        /// un valor del tipo entero con la clave hash de ese objeto en base a la
        /// función hash utilizada para el cálculo de la clave.
        /// </returns>
        public int GetHashCode(T obj)
        {
            return hashFunction(obj);
        }
    }
}

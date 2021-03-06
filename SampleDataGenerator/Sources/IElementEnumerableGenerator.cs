﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDataGenerator.Sources
{
    /// <summary>
    /// Exposes a data generator for a given data type
    /// </summary>
    /// <typeparam name="TProp">Source data type</typeparam>
    public interface IElementEnumerableGenerator<out TProp>
    {
        /// <summary>
        /// Generates a specific amount of elements
        /// </summary>
        /// <param name="count">Number of elements to generate</param>
        /// <returns>IEnumerable of data type</returns>
        IEnumerable<TProp> Generate(int count);
    }
}

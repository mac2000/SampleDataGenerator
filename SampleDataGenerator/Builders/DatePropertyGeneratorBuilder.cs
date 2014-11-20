﻿namespace SampleDataGenerator.Builders
{
    using System;
    using System.Linq.Expressions;
    using SampleDataGenerator.Generators;

    public class DatePropertyGeneratorBuilder<TObj> : PropertyGeneratorBuilder<TObj, DateTime>
    {
        private readonly Random rnd = new Random();

        public DatePropertyGeneratorBuilder(ObjectGeneratorBuilder<TObj> from, Expression<Func<TObj, DateTime>> expr)
            : base(from, expr)
        {
        }

        public ObjectGeneratorBuilder<TObj> Range(DateTime start, DateTime end)
        {
            var pgen = new DateGenerator(start, end);

            return this.Add(pgen);
        }        
    }
}
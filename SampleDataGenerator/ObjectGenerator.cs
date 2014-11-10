﻿namespace SampleDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    
    /// <summary>
    /// Object generator
    /// </summary>
    /// <typeparam name="TObj">Object type</typeparam>
    public class ObjectGenerator<TObj>
    {
        private class Assigner<TProp> : IPropertyAssigner<TObj>
        {
            private readonly Action<TObj, TProp> action;
            private readonly IPropertyGenerator<TProp> generator;

            public Assigner(Expression<Func<TObj, TProp>> expr, IPropertyGenerator<TProp> generator)
            {
                var member = expr.Body;
                var param = Expression.Parameter(typeof(TProp), "value");
                var set = Expression.Lambda<Action<TObj, TProp>>(Expression.Assign(member, param), expr.Parameters[0], param);

                this.action = set.Compile();
                this.generator = generator;
            }

            public void SetValue(TObj target)
            {
                action(target, generator.Get());
            }
        }

        private List<IPropertyAssigner<TObj>> assigners = new List<IPropertyAssigner<TObj>>();

        internal void Add<TProp>(IPropertyGenerator<TProp> build, Expression<Func<TObj, TProp>> expr)
        {
            this.assigners.Add(new Assigner<TProp>(expr, build));
        }

        public IEnumerable<TObj> Generate(int count)
        {
            return Enumerable.Range(0, count)
                .Select(x => this.Create());
        }

        private TObj Create()
        {
            var o = Activator.CreateInstance<TObj>();

            this.assigners.ForEach(x => x.SetValue(o));

            return o;
        }
    }
}

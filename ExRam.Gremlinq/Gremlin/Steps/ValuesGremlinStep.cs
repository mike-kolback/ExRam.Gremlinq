using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;

namespace ExRam.Gremlinq
{
    public sealed class ValuesGremlinStep<TSource, TTarget> : NonTerminalGremlinStep
    {
        private readonly Expression<Func<TSource, TTarget>>[] _projections;

        public ValuesGremlinStep(Expression<Func<TSource, TTarget>>[] projections)
        {
            this._projections = projections;
        }

        public override IEnumerable<TerminalGremlinStep> Resolve(IGraphModel model)
        {
            var keys = this._projections
                .Select(projection =>
                {
                    if (projection.Body is MemberExpression memberExpression)
                        return model.GetIdentifier(memberExpression.Member.Name);

                    throw new NotSupportedException();
                })
                .ToArray();

            var numberOfIdSteps = keys
                .OfType<T>()
                .Count(x => x == T.Id);

            var propertyKeys = keys
                .OfType<string>()
                .Cast<object>()
                .ToArray();

            if (numberOfIdSteps > 1 || (numberOfIdSteps > 0 && propertyKeys.Length > 0))
                throw new NotSupportedException();

            if (numberOfIdSteps > 0)
                yield return new TerminalGremlinStep("id");
            else
            {
                yield return new TerminalGremlinStep(
                    "values",
                    propertyKeys
                        .ToImmutableList());
            }
        }
    }
}
﻿using System.Collections.Immutable;

namespace ExRam.Gremlinq.Core
{
    public sealed class InEStep : DerivedLabelNamesStep
    {
        public static readonly InEStep Empty = new InEStep(ImmutableArray<string>.Empty);

        public InEStep(ImmutableArray<string> labels) : base(labels)
        {
        }
    }
}

﻿namespace ExRam.Gremlinq.Core.Serialization
{
    public interface IGremlinQueryElementVisitorCollection
    {
        IGremlinQueryElementVisitorCollection TryAdd<TSerializedQuery, TVisitor>() where TVisitor : IGremlinQueryElementVisitor<TSerializedQuery>, new();
        IGremlinQueryElementVisitorCollection Set<TSerializedQuery, TVisitor>() where TVisitor : IGremlinQueryElementVisitor<TSerializedQuery>, new();

        IGremlinQueryElementVisitor<TSerializedQuery> Get<TSerializedQuery>();
    }
}
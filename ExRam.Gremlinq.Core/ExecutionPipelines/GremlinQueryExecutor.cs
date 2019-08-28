﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ExRam.Gremlinq.Core
{
    public static class GremlinQueryExecutor
    {
        private sealed class EchoGremlinQueryExecutor<TSerializedQuery> : IGremlinQueryExecutor<TSerializedQuery, TSerializedQuery>
        {
            public IAsyncEnumerable<TSerializedQuery> Execute(TSerializedQuery serializedQuery)
            {
                return AsyncEnumerableEx.Return(serializedQuery);
            }
        }

        public static IGremlinQueryExecutor<TSerializedQuery, TSerializedQuery> Echo<TSerializedQuery>()
        {
            return new EchoGremlinQueryExecutor<TSerializedQuery>();
        }
    }

    internal sealed class GremlinQueryExecutor<TSerializedQuery, TExecutionResult>
    {
        private sealed class InvalidQueryExecutor : IGremlinQueryExecutor<TSerializedQuery, TExecutionResult>
        {
            public IAsyncEnumerable<TExecutionResult> Execute(TSerializedQuery serializedQuery)
            {
                return AsyncEnumerableEx.Throw<TExecutionResult>(new InvalidOperationException($"'{nameof(Execute)}' must not be called on {nameof(GremlinQueryExecutor)}.Invalid. If you are getting this exception while executing a query, set a proper {nameof(GremlinQueryExecutor)} on the {nameof(GremlinQuerySource)} (e.g. with 'g.WithRemote(...)' for WebSockets which can be found in the 'ExRam.Gremlinq.Providers.WebSocket' package)."));
            }
        }

        public static readonly IGremlinQueryExecutor<TSerializedQuery, TExecutionResult> Invalid = new InvalidQueryExecutor();
    }
}

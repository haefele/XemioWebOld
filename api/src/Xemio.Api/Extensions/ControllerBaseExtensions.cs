using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Xemio.Api.Extensions
{

    public static class ControllerBaseExtensions
    {
        public static StatusCodeResult Conflict(this ControllerBase self)
        {
            return self.StatusCode(StatusCodes.Status409Conflict);
        }
    }

    public static class LinqExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IList<T> self, Func<T, IEnumerable<T>> childSelector)
        {
            var stack = new Stack<T>();
            
            foreach (var item in self)
                stack.Push(item);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;

                foreach (var child in childSelector(current))
                    stack.Push(child);
            }
        }
    }
}

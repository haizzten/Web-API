using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace f7.Services
{

    public class MyViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context) { }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var newViewsLocation = viewLocations.ToList<string>();
            newViewsLocation.AddRange(new string[]
            {
                "/Areas/{2}/Views/{0}.cshtml"
            });

            // foreach (var loc in newViewsLocation)
            // {
            //     Console.WriteLine("-- {0}", loc);
            // }
            return newViewsLocation;
        }

    }
}
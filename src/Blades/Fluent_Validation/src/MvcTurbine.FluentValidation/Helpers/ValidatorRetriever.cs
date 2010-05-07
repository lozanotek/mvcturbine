using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcTurbine.FluentValidation.Helpers
{
    public class ValidatorRetriever
    {
        public IEnumerable<Type> GetAllValidatorTypes()
        {
            var list = new List<Type>();

            foreach (var assembly in GetAllAssemblies())
                list.AddRange(GetAllValidatorsInThisAssembly(assembly));

            return list;
        }

        private static IEnumerable<Type> GetAllValidatorsInThisAssembly(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(x => ThisTypeImplementsAnInterface(x) && ThisTypeIsAValidator(x));
        }

        private static bool ThisTypeImplementsAnInterface(Type x)
        {
            return x.GetInterfaces() != null;
        }

        private static bool ThisTypeIsAValidator(Type x)
        {
            return x.GetInterfaces().Any(i => (i.FullName ?? string.Empty).StartsWith("FluentValidation.IValidator`1"));
        }

        private static IEnumerable<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("FluentValidation,") == false)
                .ToList();
        }
    }
}
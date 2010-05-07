using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.FluentValidation.Helpers
{
    public class ServiceLocatorValidatorFactory : IValidatorFactory
    {
        private readonly IList<ValidatorMapping> validatorMappings;
        private readonly IServiceLocator serviceLocator;

        public ServiceLocatorValidatorFactory(IServiceLocator serviceLocator)
        {
            validatorMappings = new List<ValidatorMapping>();
            this.serviceLocator = serviceLocator;
        }

        public void AddValidatorToBeResolved(Type validatorType)
        {
            var genericValidatorType = GetTheGenericValidatorType(validatorType);

            ThrowAnExceptionIfThisIsNotAFluentValidator(genericValidatorType);

            validatorMappings.Add(CreateTheValidatorMappingForTHisType(validatorType, genericValidatorType));
        }

        public IValidator<T> GetValidator<T>()
        {
            return GetValidator(typeof (T)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
            var validatorType = GetTheValidatorForThisType(type);

            ThrowInvalidExceptionIfNoValidatorHasBeenRegistered(type, validatorType);

            return serviceLocator.Resolve(validatorType) as IValidator;
        }

        private static ValidatorMapping CreateTheValidatorMappingForTHisType(Type validatorType, Type genericType)
        {
            return new ValidatorMapping{
                                           TypeToValidate = GetTheTypeThatTheValidatorValidates(genericType),
                                           ValidatorType = validatorType
                                       };
        }

        private static void ThrowAnExceptionIfThisIsNotAFluentValidator(Type genericType)
        {
            if (ThisIsNotAGenericFluentValidator(genericType))
                throw new ArgumentException("May only pass IValidator<T> to AddValidatorToBeResolved.");
        }

        private static Type GetTheTypeThatTheValidatorValidates(Type genericType)
        {
            return genericType.GetGenericArguments()[0];
        }

        private static bool ThisIsNotAGenericFluentValidator(Type genericType)
        {
            return genericType == null;
        }

        private static Type GetTheGenericValidatorType(Type validatorType)
        {
            return validatorType.GetInterfaces()
                .Where(TheInterfaceIsAGenericValidator())
                .FirstOrDefault();
        }

        private static Func<Type, bool> TheInterfaceIsAGenericValidator()
        {
            return x => x.IsGenericType && x.FullName.StartsWith("FluentValidation.IValidator`1");
        }

        private static void ThrowInvalidExceptionIfNoValidatorHasBeenRegistered(Type type, Type validatorType)
        {
            if (TheValidatorHasNotBeenAdded(validatorType))
                throw new ArgumentException(string.Format("The {0} type was not registered with the validator factory.", type.Name));
        }

        private static bool TheValidatorHasNotBeenAdded(Type validatorType)
        {
            return validatorType == null;
        }

        private Type GetTheValidatorForThisType(Type type)
        {
            return validatorMappings
                .Where(x => x.TypeToValidate == type)
                .Select(x => x.ValidatorType)
                .FirstOrDefault();
        }

        private class ValidatorMapping
        {
            public Type TypeToValidate { get; set; }
            public Type ValidatorType { get; set; }
        }
    }
}
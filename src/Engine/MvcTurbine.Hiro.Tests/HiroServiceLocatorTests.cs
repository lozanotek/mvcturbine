using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.Hiro.Tests
{
    [Subject(typeof (HiroServiceLocator))]
    public class when_instantiating : hiro_service_locator_test
    {
        private It should_exist =
            () => locator.ShouldNotBeNull();
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_using_the_generic_interface_to_generic_type_registration : hiro_service_locator_test
    {
        private Because of =
            () => locator.Register<IThing, Thing>();

        private It should_be_able_to_resolve_with_generic =
            () => locator.Resolve<IThing>()
                      .ShouldBeOfType(typeof (Thing));

        private It should_be_able_to_resolve_with_type =
            () => locator.Resolve<IThing>(typeof (IThing))
                      .ShouldBeOfType(typeof (Thing));

        private It should_be_able_to_resolve_with_type_only =
            () => locator.Resolve(typeof (IThing))
                      .ShouldBeOfType<Thing>();

        private It should_be_able_to_be_resolved_with_services =
            () =>
                {
                    var services = locator.ResolveServices(typeof (IThing));
                    services.Count().ShouldEqual(1);
                    services.Single().ShouldBeOfType(typeof (IThing));
                };

        public interface IThing
        {
        }

        public class Thing : IThing
        {
        }
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_using_the_generic_interface_to_type_argument_registration : hiro_service_locator_test
    {
        private Because of =
            () => locator.Register<IThing>(typeof (Thing));

        private It should_be_able_to_resolve_with_generic =
            () => locator.Resolve<IThing>()
                      .ShouldBeOfType(typeof (Thing));

        private It should_be_able_to_resolve_with_type =
            () => locator.Resolve<IThing>(typeof (IThing))
                      .ShouldBeOfType(typeof (Thing));

        private It should_be_able_to_resolve_with_type_only =
            () => locator.Resolve(typeof (IThing))
                      .ShouldBeOfType<Thing>();

        private It should_be_able_to_be_resolved_with_services =
            () =>
                {
                    var services = locator.ResolveServices(typeof (IThing));
                    services.Count().ShouldEqual(1);
                    services.Single().ShouldBeOfType(typeof (IThing));
                };

        public interface IThing
        {
        }

        public class Thing : IThing
        {
        }
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_registering_by_generic_interface_and_generic_type_and_key : hiro_service_locator_test
    {
        private Because of =
            () => locator.Register<IThing, Thing>("a key");

        private It should_be_able_to_resolve_with_generic_and_key =
            () => locator.Resolve<IThing>("a key")
                      .ShouldBeOfType(typeof (Thing));

        private It should_be_able_to_be_resolved_with_services =
            () =>
                {
                    var services = locator.ResolveServices(typeof (IThing));
                    services.Count().ShouldEqual(1);
                    services.Single().ShouldBeOfType(typeof (IThing));
                };

        public interface IThing
        {
        }

        public class Thing : IThing
        {
        }
    }

    [Subject(typeof(HiroServiceLocator))]
    public class when_registering_two_things_with_generic_interface_and_generic_type_without_key : hiro_service_locator_test
    {
        private Because of =
            () =>
                {
                    locator.Register<IThing, WhiteThing>();
                    locator.Register<IThing, PurpleThing>();
                };

        private It should_be_able_to_be_resolved_with_two_services =
            () =>
                {
                    var services = locator.ResolveServices(typeof (IThing));
                    services.Count().ShouldEqual(2);
                    services.Count(x => x.GetType() == typeof (WhiteThing)).ShouldEqual(1);
                    services.Count(x => x.GetType() == typeof (PurpleThing)).ShouldEqual(1);
                };

        public interface IThing
        {
        }

        public class WhiteThing : IThing
        {
        }

        public class PurpleThing : IThing
        {
        }
    }

    [Subject(typeof(HiroServiceLocator))]
    public class when_registering_two_things_with_generic_interface_and_argument_type_without_key : hiro_service_locator_test
    {
        private Because of =
            () =>
            {
                locator.Register<IThing>(typeof(WhiteThing));
                locator.Register<IThing>(typeof(PurpleThing));
            };

        private It should_be_able_to_be_resolved_with_two_services =
            () =>
            {
                var services = locator.ResolveServices(typeof(IThing));
                services.Count().ShouldEqual(2);
                services.Count(x => x.GetType() == typeof(WhiteThing)).ShouldEqual(1);
                services.Count(x => x.GetType() == typeof(PurpleThing)).ShouldEqual(1);
            };

        public interface IThing
        {
        }

        public class WhiteThing : IThing
        {
        }

        public class PurpleThing : IThing
        {
        }
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_registering_two_types_by_key : hiro_service_locator_test
    {
        private Because of =
            () =>
                {
                    locator.Register<IThing, WhiteThing>("white");
                    locator.Register<IThing, PurpleThing>("purple");
                };

        private It should_resolve_two_services =
            () => locator.ResolveServices<IThing>()
                      .Count().ShouldEqual(2);

        private It should_resolve_each_based_on_key =
            () =>
                {
                    locator.Resolve<IThing>("white").ShouldBeOfType(typeof(WhiteThing));
                    locator.Resolve<IThing>("purple").ShouldBeOfType(typeof(PurpleThing));
                };


        public interface IThing
        {
        }

        public class PurpleThing : IThing
        {
        }

        public class WhiteThing : IThing
        {
        }
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_registering_a_single_instance : hiro_service_locator_test
    {
        private Because of =
            () => locator.Register<IThing>(registeredItem);

        private It should_return_the_same_instance =
            () => locator.Resolve<IThing>()
                      .ShouldBeTheSameAs(registeredItem);

        private static Thing registeredItem = new Thing();

        public interface IThing {}

        public class Thing : IThing {}
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_registering_a_factory_method : hiro_service_locator_test
    {
        private Because of =
            () => locator.Register<IThing>(() =>
                                               {
                                                   count++;
                                                   return new Thing(count);
                                               });

        private It should_call_the_factory_method_when_resolving =
            () =>
                {
                    count = 0;
                    locator.Resolve<IThing>().Count.ShouldEqual(1);
                    locator.Resolve<IThing>().Count.ShouldEqual(2);
                    locator.Resolve<IThing>().Count.ShouldEqual(3);
                };

        private static int count;

        public interface IThing
        {
            int Count { get; }
        }

        public class Thing : IThing {
            private readonly int count;

            public Thing(int count)
            {
                this.count = count;
            }

            public int Count
            {
                get { return count; }
            }
        }
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_registering_something_after_a_resolve : hiro_service_locator_test
    {
        private It should_resolve_the_first_thing_registered =
            () =>
                {
                    locator.Register<IApple, Apple>();
                    locator.Resolve<IApple>();
                    locator.Register<IOrange, Orange>();
                };

        private It should_be_able_to_resolve_the_first_type =
            () => locator.Resolve<IApple>().ShouldBeOfType(typeof (Apple));

        private It should_be_able_to_resolve_the_second_type =
            () =>
                {
                    locator.Resolve<IOrange>().ShouldBeOfType(typeof(Orange));
                };

        public interface IApple { }
        public class Apple : IApple { }

        public interface IOrange {}
        public class Orange : IOrange {}
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_resolving_a_concrete_class_that_has_never_been_registered
    {
        private It should_be_able_to_resolve_through_generic_resolve =
            () => new HiroServiceLocator().Resolve<ConcreteClass>()
                      .ShouldNotBeNull();

        private It should_be_able_to_resolve_through_type_resolve =
            () => new HiroServiceLocator().Resolve(typeof (ConcreteClass))
                      .ShouldBeOfType(typeof(ConcreteClass));

        private It should_be_able_to_resolve_with_type_and_Generic =
            () => new HiroServiceLocator().Resolve<ConcreteClass>(typeof(ConcreteClass))
                      .ShouldBeOfType(typeof(ConcreteClass));

        private static ConcreteClass concreteClass;

        public class ConcreteClass
        {
        }
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_resolving_services_that_have_never_been_registered : hiro_service_locator_test
    {
        private Because of =
            () =>
                {
                    results = locator.ResolveServices<IThing>();
                };

        private It should_return_no_items =
            () => results.Count().ShouldEqual(0);

        private static IEnumerable<IThing> results;

        public interface IThing{}
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_an_generic_resolve_has_failed_with_interface : hiro_service_locator_test
    {
        private Because of =
            () =>
                {
                    try
                    {
                        // this will fail
                        locator.Resolve<IThing>();
                    } catch {}
                };

        private It should_be_able_to_resolve_services =
            () => locator.ResolveServices<IThing>().Count().ShouldEqual(0);

        public interface IThing { }
    }

    [Subject(typeof(HiroServiceLocator))]
    public class when_an_type_resolve_has_failed_with_interface : hiro_service_locator_test
    {
        private Because of =
            () =>
            {
                try
                {
                    // this will fail
                    locator.Resolve(typeof(IThing));
                }
                catch { }
            };

        private It should_be_able_to_resolve_services =
            () => locator.ResolveServices<IThing>().Count().ShouldEqual(0);

        public interface IThing { }
    }

    [Subject(typeof(HiroServiceLocator))]
    public class when_an_generic_and_type_resolve_has_failed_with_interface : hiro_service_locator_test
    {
        private Because of =
            () =>
            {
                try
                {
                    // this will fail
                    locator.Resolve<IThing>(typeof(IThing));
                }
                catch { }
            };

        private It should_be_able_to_resolve_services =
            () => locator.ResolveServices<IThing>().Count().ShouldEqual(0);

        public interface IThing { }
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_registering_a_class_whose_dependencies_have_not_been_registered : hiro_service_locator_test
    {
        private Because of =
            () =>
                {
                    locator.Register<ISundae, Sundae>();
                };

        private It should_be_able_to_resolve_things_without_throwing_an_error =
            () => locator.ResolveServices<IChocolate>()
                      .Count().ShouldEqual(0);

        private It should_be_able_to_resolve_the_type_if_the_dependencies_are_registered_later =
            () =>
                {
                    locator.Register<IChocolate, Chocolate>();
                    locator.Resolve<ISundae>().ShouldBeOfType(typeof(Sundae));
                };

        public interface ISundae
        {
        }

        public class Sundae : ISundae
        {
            public Sundae(IChocolate chocolate){}
        }

        public interface IChocolate
        {
        }
        public class Chocolate : IChocolate{}
    }

    [Subject(typeof (HiroServiceLocator))]
    public class when_registering_an_interface_to_an_interface_with_generic_registration : hiro_service_locator_test
    {
        private Because of =
            () => locator.Register<IThing, IThing>();

        private It should_be_able_to_function_without_erroring =
            () => locator.ResolveServices<IThing>()
                      .Count().ShouldEqual(0);

        private It should_be_able_to_register_and_resolve_future_changes =
            () =>
                {
                    locator.Register<IAnotherThing, AnotherThing>();
                    locator.Resolve<IAnotherThing>().ShouldBeOfType(typeof (AnotherThing));
                };

        public interface IThing{}

        public interface IAnotherThing{}

        public class AnotherThing : IAnotherThing{}
    }

    [Subject(typeof(HiroServiceLocator))]
    public class when_registering_an_interface_to_an_interface_with_generic_keyed_registration : hiro_service_locator_test
    {
        private Because of =
            () => locator.Register<IThing, IThing>("test");

        private It should_be_able_to_function_without_erroring =
            () => locator.ResolveServices<IThing>()
                      .Count().ShouldEqual(0);

        private It should_be_able_to_register_and_resolve_future_changes =
            () =>
            {
                locator.Register<IAnotherThing, AnotherThing>();
                locator.Resolve<IAnotherThing>().ShouldBeOfType(typeof(AnotherThing));
            };

        public interface IThing { }

        public interface IAnotherThing { }

        public class AnotherThing : IAnotherThing { }
    }

    [Subject(typeof(HiroServiceLocator))]
    public class when_registering_an_interface_to_an_interface_with_type_registration : hiro_service_locator_test
    {
        private Because of =
            () => locator.Register<IThing>(typeof (IThing));

        private It should_be_able_to_function_without_erroring =
            () => locator.ResolveServices<IThing>()
                      .Count().ShouldEqual(0);

        private It should_be_able_to_register_and_resolve_future_changes =
            () =>
            {
                locator.Register<IAnotherThing, AnotherThing>();
                locator.Resolve<IAnotherThing>().ShouldBeOfType(typeof(AnotherThing));
            };

        public interface IThing { }

        public interface IAnotherThing { }

        public class AnotherThing : IAnotherThing { }
    }

    public class hiro_service_locator_test
    {
        public hiro_service_locator_test()
        {
            locator = new HiroServiceLocator();
        }

        public static HiroServiceLocator locator;
    }
}
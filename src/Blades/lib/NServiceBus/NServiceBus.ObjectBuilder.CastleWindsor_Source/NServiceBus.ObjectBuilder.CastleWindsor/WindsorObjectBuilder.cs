namespace NServiceBus.ObjectBuilder.CastleWindsor
{
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Releasers;
    using Castle.Windsor;
    using NServiceBus.ObjectBuilder;
    using NServiceBus.ObjectBuilder.Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class WindsorObjectBuilder : IContainer
    {
        public WindsorObjectBuilder()
        {
            this.Container = new WindsorContainer();
            this.Container.get_Kernel().set_ReleasePolicy(new NoTrackingReleasePolicy());
        }

        public WindsorObjectBuilder(IWindsorContainer container)
        {
            this.Container = container;
        }

        private static IEnumerable<Type> GetAllServiceTypesFor(Type t)
        {
            if (t == null)
            {
                return new List<Type>();
            }
            List<Type> list2 = new List<Type>(t.GetInterfaces()) {
                t
            };
            List<Type> list = list2;
            foreach (Type type in t.GetInterfaces())
            {
                list.AddRange(GetAllServiceTypesFor(type));
            }
            return list;
        }

        private IHandler GetHandlerForType(Type concreteComponent)
        {
            return (from h in this.Container.get_Kernel().GetAssignableHandlers(typeof(object))
                where h.get_ComponentModel().get_Implementation() == concreteComponent
                select h).FirstOrDefault<IHandler>();
        }

        private static LifestyleType GetLifestyleTypeFrom(ComponentCallModelEnum callModel)
        {
            switch (callModel)
            {
                case ComponentCallModelEnum.Singleton:
                    return 1;

                case ComponentCallModelEnum.Singlecall:
                    return 3;
            }
            return 0;
        }

        object IContainer.Build(Type typeToBuild)
        {
            try
            {
                return this.Container.Resolve(typeToBuild);
            }
            catch (ComponentNotFoundException)
            {
                return null;
            }
        }

        IEnumerable<object> IContainer.BuildAll(Type typeToBuild)
        {
            return new BuildAll>d__0(-2) { <>4__this = this, <>3__typeToBuild = typeToBuild };
        }

        void IContainer.Configure(Type concreteComponent, ComponentCallModelEnum callModel)
        {
            if (this.GetHandlerForType(concreteComponent) == null)
            {
                LifestyleType lifestyleTypeFrom = GetLifestyleTypeFrom(callModel);
                ComponentRegistration<object> registration = Component.For(GetAllServiceTypesFor(concreteComponent)).ImplementedBy(concreteComponent);
                registration.get_LifeStyle().Is(lifestyleTypeFrom);
                this.Container.get_Kernel().Register(new IRegistration[] { registration });
            }
        }

        void IContainer.ConfigureProperty(Type component, string property, object value)
        {
            IHandler handlerForType = this.GetHandlerForType(component);
            if (handlerForType == null)
            {
                throw new InvalidOperationException("Cannot configure property for a type which hadn't been configured yet. Please call 'Configure' first.");
            }
            handlerForType.AddCustomDependencyValue(property, value);
        }

        void IContainer.RegisterSingleton(Type lookupType, object instance)
        {
            this.Container.get_Kernel().AddComponentInstance(Guid.NewGuid().ToString(), lookupType, instance);
        }

        public IWindsorContainer Container { get; set; }

        [CompilerGenerated]
        private sealed class BuildAll>d__0 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
        {
            private int <>1__state;
            private object <>2__current;
            public Type <>3__typeToBuild;
            public WindsorObjectBuilder <>4__this;
            public IEnumerator <>7__wrap2;
            public IDisposable <>7__wrap3;
            private int <>l__initialThreadId;
            public object <component>5__1;
            public Type typeToBuild;

            [DebuggerHidden]
            public BuildAll>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
            }

            private void <>m__Finally4()
            {
                this.<>1__state = -1;
                this.<>7__wrap3 = this.<>7__wrap2 as IDisposable;
                if (this.<>7__wrap3 != null)
                {
                    this.<>7__wrap3.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<>7__wrap2 = this.<>4__this.Container.ResolveAll(this.typeToBuild).GetEnumerator();
                            this.<>1__state = 1;
                            goto Label_007B;

                        case 2:
                            this.<>1__state = 1;
                            goto Label_007B;

                        default:
                            goto Label_008E;
                    }
                Label_004C:
                    this.<component>5__1 = this.<>7__wrap2.Current;
                    this.<>2__current = this.<component>5__1;
                    this.<>1__state = 2;
                    return true;
                Label_007B:
                    if (this.<>7__wrap2.MoveNext())
                    {
                        goto Label_004C;
                    }
                    this.<>m__Finally4();
                Label_008E:
                    flag = false;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                WindsorObjectBuilder.BuildAll>d__0 d__;
                if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new WindsorObjectBuilder.BuildAll>d__0(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.typeToBuild = this.<>3__typeToBuild;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
                switch (this.<>1__state)
                {
                    case 1:
                    case 2:
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally4();
                        }
                        return;
                }
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }
        }
    }
}


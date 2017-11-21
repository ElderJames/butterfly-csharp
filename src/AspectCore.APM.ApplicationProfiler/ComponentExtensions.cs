﻿using System;
using AspectCore.APM.Core;
using AspectCore.APM.Profiler;
using AspectCore.Injector;

namespace AspectCore.APM.ApplicationProfiler
{
    public static class ComponentExtensions
    {
        public static ComponentOptions AddApplicationProfiler(this ComponentOptions apmComponent)
        {
            return AddApplicationProfiler(apmComponent, null);
        }


        public static ComponentOptions AddApplicationProfiler(this ComponentOptions apmComponent, Action<ApplicationProfilingOptions> configure)
        {
            if (apmComponent == null)
            {
                throw new ArgumentNullException(nameof(apmComponent));
            }
            var options = new ApplicationProfilingOptions();
            configure?.Invoke(options);
            apmComponent.Services.AddType<IOptionAccessor<ApplicationProfilingOptions>, ApplicationProfilingOptions>(Lifetime.Singleton);
            apmComponent.Services.AddType<IProfilerSetup, ApplicationProfilerSetup>(Lifetime.Singleton);
            apmComponent.Services.AddType<IProfiler<ApplicationGCProfilingContext>, ApplicationGCProfiler>(Lifetime.Singleton);
            apmComponent.Services.AddType<IProfiler<ApplicationThreadingProfilingContext>, ApplicationThreadingProfiler>(Lifetime.Singleton);
            return apmComponent;
        }
    }
}
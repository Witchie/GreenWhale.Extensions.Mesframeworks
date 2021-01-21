using GreenWhale.Mesframeworks;
#if !NET451
using Microsoft.Extensions.DependencyInjection;
#endif
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenWhale.Extensions.Mesframeworks
{
#if !NET451
    public static class MesFrameworkExtension
    {
        /// <summary>
        /// 添加MES服务框架，见服务<see cref="IMesFramework"/>
        /// </summary>
        /// <param name="serviceDescriptors"></param>
        /// <returns></returns>
        public static IServiceCollection AddMesFramework(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddTransient<IMesFramework, MesFramework>();
            return serviceDescriptors;
        }
    }
#endif
}

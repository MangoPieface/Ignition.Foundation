/*************************************

DO NOT CHANGE THIS FILE - UPDATE GlassMapperScCustom.cs

**************************************/

using Glass.Mapper;
using Glass.Mapper.Maps;
using Glass.Mapper.Sc.Configuration.Fluent;
using Glass.Mapper.Sc.IoC;
using Sitecore.Pipelines;

// WebActivator has been removed. If you wish to continue using WebActivator uncomment the line below
// and delete the Glass.Mapper.Sc.CastleWindsor.config file from the Sitecore Config Include folder.
//[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(GlassMapperSc), "Start")]
namespace Ignition.Foundation.CompositionRoot
{
	public class GlassMapperSc
	{
        public void Process(PipelineArgs args)
        {
            Start();
        }

        public static void Start()
        {
            var resolver = Context.Default?.DependencyResolver ?? GlassMapperScCustom.CreateResolver();
            var context = Context.Default ?? Context.Create(resolver);
            LoadConfigurationMaps(resolver, context);
            context?.Load(GlassMapperScCustom.GlassLoaders());
            GlassMapperScCustom.PostLoad();
        }

        public static void LoadConfigurationMaps(Glass.Mapper.IoC.IDependencyResolver resolver, Context context)
        {
            var dependencyResolver = resolver as DependencyResolver;
            if (dependencyResolver == null) return;

            if (dependencyResolver.ConfigurationMapFactory is ConfigurationMapConfigFactory)
                GlassMapperScCustom.AddMaps(dependencyResolver.ConfigurationMapFactory);

            IConfigurationMap configurationMap = new ConfigurationMap(dependencyResolver);
            var configurationLoader = configurationMap.GetConfigurationLoader<SitecoreFluentConfigurationLoader>();
            context.Load(configurationLoader);
        }
    }
}

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Http
Imports System.Web.Mvc
Imports System.Web.Optimization
Imports System.Web.Routing

Namespace dxSampleT590924

	Public Class MvcApplication
		Inherits HttpApplication

		Protected Sub Application_Start()
			AreaRegistration.RegisterAllAreas()
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
			GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
			RouteConfig.RegisterRoutes(RouteTable.Routes)
			BundleConfig.RegisterBundles(BundleTable.Bundles)
			DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles)

			' Uncomment to use pre-17.2 routing for .Mvc() and .WebApi() data sources
			' DevExtreme.AspNet.Mvc.Compatibility.DataSource.UseLegacyRouting = true;
			' Uncomment to use pre-17.2 behavior for the "required" validation check
			' DevExtreme.AspNet.Mvc.Compatibility.Validation.IgnoreRequiredForBoolean = false;
		End Sub
	End Class
End Namespace

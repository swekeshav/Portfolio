using Moq;

namespace Portfolio.Web.Tests.ExceptionHandler;

public static class ServiceProviderMockExtensions
{
	public static IServiceProvider SetupService(this Mock<IServiceProvider> sp, Type serviceType, object instance)
	{
		sp.Setup(x => x.GetService(serviceType)).Returns(instance);
		return sp.Object;
	}
}

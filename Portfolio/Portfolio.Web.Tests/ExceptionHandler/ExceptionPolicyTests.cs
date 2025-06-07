using System;
using NUnit.Framework;
using Portfolio.Web;

namespace Portfolio.Web.Tests.ExceptionHandler;

[TestFixture]
public class ExceptionPolicyTests
{
	[Test]
	public void ShouldLogout_ReturnsFalse_ForNormalException()
	{
		var policy = new ExceptionPolicy();
		var ex = new Exception("test");
		Assert.That(policy.ShouldLogout(ex), Is.False);
	}

	[Test]
	public void ShouldLogout_ReturnsTrue_ForUnauthorizedAccessException()
	{
		var policy = new ExceptionPolicy();
		var ex = new UnauthorizedAccessException();
		Assert.That(policy.ShouldLogout(ex), Is.True);
	}
}

// Example custom exception for demonstration

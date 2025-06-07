namespace Portfolio.Web;

public interface IExceptionPolicy
{
	bool ShouldLogout(Exception exception);
}

public class ExceptionPolicy
{
	public bool ShouldLogout(Exception exception)
	{
		return exception is UnauthorizedAccessException;
	}
}

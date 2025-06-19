namespace Portfolio.Web;

public interface IExceptionPolicy
{
	bool ShouldLogout(Exception exception);
}

public class ExceptionPolicy: IExceptionPolicy
{
	public bool ShouldLogout(Exception exception)
	{
		return exception is UnauthorizedAccessException;
	}
}

namespace Portfolio.Web;

public class SocialIcon
{
	public int ID { get; set; }
	public string IconName { get; set; } = null!;
	public string IconBgColor { get; set; } = null!;
	public string IconTargetUrl { get; set; } = null!;
	public string IconClass { get; set; } = null!;

	public static List<SocialIcon> AppSocialIcons()
	{
		List<SocialIcon> icons = new()
		{
			new SocialIcon { ID = 1, IconName = "Google", IconBgColor = "#dd4b39", IconTargetUrl = "www.google.com", IconClass = "fa fa-google" },
			new SocialIcon { ID = 2, IconName = "Facebook", IconBgColor = "#3B5998", IconTargetUrl = "www.facebook.com", IconClass = "fa fa-facebook" },
			new SocialIcon { ID = 3, IconName = "Linked In", IconBgColor = "#007bb5", IconTargetUrl = "www.linkedin.com", IconClass = "fa fa-fa-linkedin" },
			new SocialIcon { ID = 4, IconName = "YouTube", IconBgColor = "#007bb5", IconTargetUrl = "www.youtube.com", IconClass = "fa fa-youtube" },
			new SocialIcon { ID = 5, IconName = "Twitter", IconBgColor = "#55acee", IconTargetUrl = "www.twitter.com", IconClass = "fa fa-twitter" }
		};

		return icons;
	}
}
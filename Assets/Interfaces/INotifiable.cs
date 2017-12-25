public enum NotificationType
{
	Player_Health = 0,
	Enemy_Health,
	Player_Offense,
	Player_Defense,
	Player_Flank,
	Player_Inflitrate,
	Player_Suppress,
	Enemy_Offense,
	Enemy_Defense,
	Enemy_Flank,
	Enemy_Inflitrate,
	Enemy_Suppress,

	PointsRemaining,
	Level,

	Total
}

public interface INotifiable
{
	void Notify(NotificationType nType, string value);
}
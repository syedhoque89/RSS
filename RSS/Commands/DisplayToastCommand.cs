namespace RSS.Commands
{
	public interface IDisplayToastCommand
	{
		void Execute(string message, bool longDuration = false);
	}
}
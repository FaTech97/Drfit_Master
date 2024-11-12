namespace _GAME.scripts.Player
{
	public class PlayerHealth
	{
		public int MaxHealth = 5;
		public int health = 5;

		public void takeDamage()
		{
			health -= 1;
		}
	}
}

namespace Elfshock.Services
{
    using Elfshock.Contracts;

    public class GameService : IGameService
    {
        public int CharacterSelect()
        {

            Console.WriteLine("Choose character type:");
            Console.WriteLine("Options:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Archer");
            Console.WriteLine("3) Mage");
            Console.Write("Your pick: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                Console.Write("Your pick: ");
            }

            return choice;
        }

    }
}

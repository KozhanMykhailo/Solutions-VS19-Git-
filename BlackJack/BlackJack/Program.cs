using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    struct Deck
    {
        public Card Name;
        public Suits Suit;
        public int Value;

        public Deck[] InitializationDeck()
        {
            Deck[] cards = new Deck[36];
            int index = 0;
            foreach (Suits s in Enum.GetValues(typeof(Suits)))
            {
                foreach (Card c in Enum.GetValues(typeof(Card)))
                {
                    cards[index].Suit = s;
                    cards[index].Name = c;
                    cards[index].Value = (int)c;
                    index++;
                }
            }
            return cards;
        }

        public void DeckMixed(Deck[] de)
        {
            Random rand = new Random();
            for (int i = de.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                Deck tmp = de[j];
                de[j] = de[i];
                de[i] = tmp;
            }
        }
        public void PrintInfoCard(Deck[] cards, int i)
        {
            Console.WriteLine($"{cards[i].Name} , {cards[i].Suit} = {cards[i].Value} \n ");
        }
        public int AddCard(Deck[] c, int indexOld, out int lastIndexCard)
        {
            PrintInfoCard(c, indexOld);
            int cardValue = c[indexOld].Value;
            lastIndexCard = ++indexOld;
            return cardValue;
        }
        public int GiveTwoCard(Deck[] c, int indexOld, out int lastIndexCard)
        {
            int valuesCards = 0;
            int tempIndex = indexOld;
            for (int i = indexOld; i < indexOld + 2; i++)
            {
                PrintInfoCard(c, i);
                int cardValue = c[i].Value;
                valuesCards += cardValue;
                tempIndex++;
            }
            lastIndexCard = tempIndex;
            return valuesCards;
        }
    }
    struct Player
    {
        public Players Name { get; set; }
        private uint NumberOfVictories { get; set; }
        public int NewValueCard { get; set; }
        
        public bool Answer { get; set; } 
        public Player(Players name, uint point, int value, bool ans)
        {
            Name = name;
            NumberOfVictories = point;
            NewValueCard = value;
            Answer = ans;
        }
        public void UpdateNumOfVic()
        {
            NumberOfVictories++;
        }
        public string GetPoint()
        {
            return NumberOfVictories.ToString();
        }
        public void PrintNameWinner()
        {
            Console.WriteLine($"{Name} WIN!!!");
        }
        public void PrintCardRecipient()
        {
            Console.WriteLine($"{Name} gets:");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Deck c = new Deck();
            GameMethods gameMethods = new GameMethods();
            Deck[] cards = c.InitializationDeck();
            Player human = new Player(Players.Player, 0, 0,true);
            Player ai = new Player(Players.Bot_Vanya, 0, 0,true);

            bool restart;
            do
            {
                c.DeckMixed(cards);
                Console.Clear();
                int playerCardValue = 0;
                int aiCardValue = 0;
                human.Answer = true;
                ai.Answer = true;

                Console.WriteLine("The deck is ready!\n");

                var firstPlayer = gameMethods.WhoGoesFirst() % 2 == 0;
                if (firstPlayer)
                {
                    Console.WriteLine("The first player to receive a card (YOU)"); //Первым получает карты Игрок(ТЫ)
                    Console.WriteLine("The second receives AI cards(COMPUTER)\n");
                }
                else
                {
                    Console.WriteLine("The first to receive AI cards (COMPUTER)"); //Первым получает карты ИИ(КОМПЬЮТЕР)
                    Console.WriteLine("The second player receives the card(YOU)\n");
                }

                // Раздаем по 2 карты в соответствии с очередью кто первый должен был их получить

                int lastIndexCard = 0;
                if (firstPlayer)
                {
                    human.PrintCardRecipient();
                    human.NewValueCard = c.GiveTwoCard(cards, lastIndexCard, out lastIndexCard);
                    playerCardValue += human.NewValueCard;

                    ai.PrintCardRecipient();
                    ai.NewValueCard = c.GiveTwoCard(cards, lastIndexCard, out lastIndexCard);
                    aiCardValue += ai.NewValueCard;
                }
                else
                {
                    ai.PrintCardRecipient();
                    ai.NewValueCard = c.GiveTwoCard(cards, lastIndexCard, out lastIndexCard);
                    aiCardValue += ai.NewValueCard;

                    human.PrintCardRecipient();
                    human.NewValueCard = c.GiveTwoCard(cards, lastIndexCard, out lastIndexCard);
                    playerCardValue += human.NewValueCard;
                }

                Console.WriteLine($"Summa {human.Name} : {playerCardValue}");
                Console.WriteLine($"Summa {ai.Name} : {aiCardValue}\n");

                if (playerCardValue == 22 || playerCardValue == 21)
                {
                    Console.WriteLine($"{human.Name} WIN , him score = {playerCardValue}\n");
                    Console.WriteLine($"END GAME\n");
                    human.UpdateNumOfVic();
                    restart = gameMethods.RestartGame();
                    continue;
                }
                else if (aiCardValue == 22 || aiCardValue == 21)
                {
                    Console.WriteLine($"{ai.Name} WIN , him score = {aiCardValue}\n");
                    Console.WriteLine($"END GAME\n");
                    ai.UpdateNumOfVic();
                    restart = gameMethods.RestartGame();
                    continue;
                }
                else
                {
                    Console.WriteLine($"Nobody scored 21, continue the game\n");
                }

                //Начинаем спрашивать не хотят ли игроки взять дополнительные карты 
                
                do
                {
                    //Взависимости кто первый получал карты , тот и начинает первым брать дополнительные карты
                    if (firstPlayer)
                    {
                        if (human.Answer && gameMethods.Question())
                        {
                            human.PrintCardRecipient();
                            human.NewValueCard = c.AddCard(cards, lastIndexCard, out lastIndexCard);
                            playerCardValue += human.NewValueCard;
                        }
                        else
                        {
                            human.Answer = false;
                        }

                        if (ai.Answer && gameMethods.Question(aiCardValue))
                        {
                            ai.PrintCardRecipient();
                            ai.NewValueCard = c.AddCard(cards, lastIndexCard, out lastIndexCard);
                            aiCardValue += ai.NewValueCard;
                        }
                        else
                        {
                            ai.Answer = false;
                        }
                    }
                    else
                    {
                        if (ai.Answer && gameMethods.Question(aiCardValue))
                        {
                            ai.PrintCardRecipient();
                            ai.NewValueCard = c.AddCard(cards, lastIndexCard, out lastIndexCard);
                            aiCardValue += ai.NewValueCard;
                        }
                        else
                        {
                            ai.Answer = false;
                        }

                        if (human.Answer && gameMethods.Question())
                        {
                            human.PrintCardRecipient();
                            human.NewValueCard = c.AddCard(cards, lastIndexCard, out lastIndexCard);
                            playerCardValue += human.NewValueCard;
                        }
                        else
                        {
                            human.Answer = false;
                        }
                    }
                    Console.WriteLine($"Summa {human.Name} : {playerCardValue}");
                    Console.WriteLine($"Summa {ai.Name} : {aiCardValue}\n");
                }
                while ((human.Answer || ai.Answer) && (playerCardValue < 21 && aiCardValue < 21));

                Console.WriteLine($"Value calculation...\n");

                if (aiCardValue == playerCardValue)
                {
                    Console.WriteLine($"Draw!!!");
                }
                else if (aiCardValue > 21 && playerCardValue < 21)
                {
                    human.PrintNameWinner();
                    human.UpdateNumOfVic();
                }
                else if (playerCardValue > 21 && aiCardValue < 21)
                {
                    ai.PrintNameWinner();
                    ai.UpdateNumOfVic();
                }
                else if (playerCardValue > 21 && aiCardValue > 21)
                {
                    if (playerCardValue < aiCardValue)
                    {
                        human.PrintNameWinner();
                        human.UpdateNumOfVic();
                    }
                    else
                    {
                        ai.PrintNameWinner();
                        ai.UpdateNumOfVic();
                    }
                }
                else
                {
                    if (playerCardValue > aiCardValue)
                    {
                        human.PrintNameWinner();
                        human.UpdateNumOfVic();
                    }
                    else
                    {
                        ai.PrintNameWinner();
                        ai.UpdateNumOfVic();
                    }
                }
                Console.WriteLine($"END GAME\n");
                restart = gameMethods.RestartGame();
            }
            while (restart);
            Console.WriteLine($"{human.Name} wins : {human.GetPoint()}\n{ai.Name} wins : {ai.GetPoint()}");
            Console.ReadLine();
        }

    }
    enum Card
    {
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 2,
        Lady = 3,
        King = 4,
        Ace = 11
    }
    enum Suits
    {
        Diamonds = 0,
        Clubs = 1,
        Hearts = 2,
        Spedes = 3
    }
    enum Players
    {
        Player,
        Bot_Vanya
    }
    struct GameMethods
    {
        public int WhoGoesFirst()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
        public bool Question()
        {
            bool temp = true;
            do
            {
                Console.WriteLine($"Want to get another card?( yes / no)");
                var answer = Console.ReadLine().ToLower();
                if (answer == "yes")
                {
                    temp = false;
                    return true;
                }
                else if (answer == "no")
                {
                    temp = false;
                    return false;
                }
            }
            while (temp);
            return false;
            
        }
        public bool Question(int aiCardValue)
        {
            if (aiCardValue < 19)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RestartGame()
        {
            bool temp = true;
            do
            {
                Console.WriteLine($"Do you want to start a new game?(Yes / No)");
                var answer = Console.ReadLine().ToLower();
                if (answer == "yes")
                {
                    temp = false;
                    return true;
                }
                else if (answer == "no")
                {
                    temp = false;
                    return false;
                }
            }
            while (temp);
            return false;
            
        }
    }
}

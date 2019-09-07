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

        public void PrintInfoCard(Card name, Suits suit, int value)
        {
            Console.WriteLine($"{name} , {suit} = {value} \n ");
        }

        public int AddCard(Deck[] c, int indexCard)
        {
            PrintInfoCard(c[indexCard].Name, c[indexCard].Suit, c[indexCard].Value);
            int cardValue = c[indexCard].Value;
            return cardValue;
        }

        public int GiveTwoCard(Deck[] c, int index)
        {
            int valuesCards = 0;
            int tempIndex = index;
            for (int i = index; i < index + 2; i++)
            {
                PrintInfoCard(c[tempIndex].Name, c[tempIndex].Suit, c[tempIndex].Value);
                int cardValue = c[i].Value;
                valuesCards += cardValue;
                tempIndex++;
            }
            return valuesCards;
        }
    }
    struct Player
    {
        public PlayersName Name { get; set; }
        private uint NumberOfVictories { get; set; }
        public int NewValueCard { get; set; }
        
        public bool Answer { get; set; } 
        public Player(PlayersName name, uint point, int value, bool ans)
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

        public string PrintNumberOfVictories()
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
            Player human = new Player(PlayersName.Player, 0, 0,true);
            Player ai = new Player(PlayersName.Bot_Vanya, 0, 0,true);

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
                    //human.NewValueCard = c.GiveTwoCard(cards, lastIndexCard, out lastIndexCard);
                    human.NewValueCard = c.GiveTwoCard(cards, lastIndexCard);
                    playerCardValue += human.NewValueCard;
                    lastIndexCard += 2;

                    ai.PrintCardRecipient();
                    ai.NewValueCard = c.GiveTwoCard(cards, lastIndexCard);
                    aiCardValue += ai.NewValueCard;
                    lastIndexCard += 2;
                }
                else
                {
                    ai.PrintCardRecipient();
                    ai.NewValueCard = c.GiveTwoCard(cards, lastIndexCard);
                    aiCardValue += ai.NewValueCard;
                    lastIndexCard += 2;

                    human.PrintCardRecipient();
                    human.NewValueCard = c.GiveTwoCard(cards, lastIndexCard);
                    playerCardValue += human.NewValueCard;
                    lastIndexCard += 2;
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
                            human.NewValueCard = c.AddCard(cards, lastIndexCard);
                            playerCardValue += human.NewValueCard;
                            lastIndexCard++;
                        }
                        else
                        {
                            human.Answer = false;
                        }

                        if (ai.Answer && gameMethods.Question(aiCardValue))
                        {
                            ai.PrintCardRecipient();
                            ai.NewValueCard = c.AddCard(cards, lastIndexCard);
                            aiCardValue += ai.NewValueCard;
                            lastIndexCard++;
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
                            ai.NewValueCard = c.AddCard(cards, lastIndexCard);
                            aiCardValue += ai.NewValueCard;
                            lastIndexCard++;
                        }
                        else
                        {
                            ai.Answer = false;
                        }

                        if (human.Answer && gameMethods.Question())
                        {
                            human.PrintCardRecipient();
                            human.NewValueCard = c.AddCard(cards, lastIndexCard);
                            playerCardValue += human.NewValueCard;
                            lastIndexCard++;
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
            Console.WriteLine($"{human.Name} wins : {human.PrintNumberOfVictories()}\n{ai.Name} wins : {ai.PrintNumberOfVictories()}");
            Console.ReadLine();
        }

    }
    enum Card
    {
        Jack = 2,
        Lady ,
        King ,
        Six = 6,
        Seven ,
        Eight ,
        Nine ,
        Ten ,        
        Ace 
    }

    enum Suits
    {
        Diamonds = 0,
        Clubs = 1,
        Hearts = 2,
        Spedes = 3
    }

    enum PlayersName
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
            Console.WriteLine($"Want to get another card?( y / n)");
            bool temp = true;
            do
            {
                var answer = Console.ReadLine().ToLower();
                if (answer == "y")
                {
                    temp = false;
                    return true;
                }
                else if (answer == "n")
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
            Console.WriteLine($"Do you want to start a new game?( Y / N )");
            bool temp = true;
            do
            {
                var answer = Console.ReadLine().ToLower();
                if (answer == "y")
                {
                    temp = false;
                    return true;
                }
                else if (answer != string.Empty)
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

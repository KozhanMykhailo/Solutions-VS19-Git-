using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    struct Cards
    {
        public string Name;
        public string Suit;
        public int Value;
    }
    struct Players
    {
        private string Name { get; set; }
        private uint Point { get; set; }

        public Players(string name, uint point)
        {
            Name = name;
            Point = point;
        }
        public void UpdatePoint()
        {
            Point++;
        }
        public string GetName()
        {
            return Name;
        }
        public string GetPoint()
        {
            return Point.ToString();
        }
        public void GetNameWinner()
        {
            Console.WriteLine($"{Name} WIN!!!");
        }
        public void GetCardRecipient()
        {
            Console.WriteLine($"{Name} gets:");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Cards[] cards = p.Deck();
            Players human = new Players("Player", 0);
            Players ai = new Players("Bot Vanya", 0);

            bool restart;
            do
            {
                p.DeckMixed(cards);
                Console.Clear();
                int playerCardValue = 0;
                int aiCardValue = 0; // ----- ai == AI(Иску́сственный интелле́кт)

                Console.WriteLine("The deck is ready!\n");

                var firstPlayer = p.WhoGoesFirst() % 2 == 0;
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

                int lastIndex = 0;
                if (firstPlayer)
                {
                    for (int indexCard = 0; indexCard < 2; indexCard++)
                    {
                        human.GetCardRecipient();
                        var newValueP = p.AddCard(cards, indexCard, out lastIndex);
                        playerCardValue += newValueP;
                    }
                    for (int indexCard = 2; indexCard < 4; indexCard++)
                    {
                        ai.GetCardRecipient();
                        var newValueAI = p.AddCard(cards, indexCard, out lastIndex);
                        aiCardValue += newValueAI;
                    }
                }
                else
                {
                    for (int indexCard = 0; indexCard < 2; indexCard++)
                    {
                        ai.GetCardRecipient();
                        var newValueAI = p.AddCard(cards, indexCard, out lastIndex);
                        aiCardValue += newValueAI;
                    }
                    for (int indexCard = 2; indexCard < 4; indexCard++)
                    {
                        human.GetCardRecipient();
                        var newValueP = p.AddCard(cards, indexCard, out lastIndex);
                        playerCardValue += newValueP;
                    }
                }

                Console.WriteLine($"Summa {human.GetName()} : {playerCardValue}");
                Console.WriteLine($"Summa {ai.GetName()} : {aiCardValue}\n");

                if (playerCardValue == 22 || playerCardValue == 21)
                {
                    Console.WriteLine($"{human.GetName()} WIN , him score = {playerCardValue}\n");
                    Console.WriteLine($"END GAME\n");
                    human.UpdatePoint();
                    restart = p.RestartGame();
                    continue;
                }
                else if (aiCardValue == 22 || aiCardValue == 21)
                {
                    Console.WriteLine($"{ai.GetName()} WIN , him score = {aiCardValue}\n");
                    Console.WriteLine($"END GAME\n");
                    ai.UpdatePoint();
                    restart = p.RestartGame();
                    continue;
                }
                else
                {
                    Console.WriteLine($"Nobody scored 21, continue the game\n");
                }

                //Начинаем спрашивать не хотят ли игроки взять дополнительные карты 
                bool answerPlayer = true;
                bool answerAI = true;

                do
                {
                    //Взависимости кто первый получал карты , тот и начинает первым брать дополнительные карты
                    if (firstPlayer)
                    {
                        if (answerPlayer && Question())
                        {
                            human.GetCardRecipient();
                            var newValueAI = p.AddCard(cards, lastIndex, out lastIndex);
                            playerCardValue += newValueAI;
                        }
                        else
                        {
                            answerPlayer = false;
                        }

                        if (answerAI && p.Question(aiCardValue))
                        {
                            ai.GetCardRecipient();
                            var newValueAI = p.AddCard(cards, lastIndex, out lastIndex);
                            aiCardValue += newValueAI;
                        }
                        else
                        {
                            answerAI = false;
                        }
                    }
                    else
                    {
                        if (answerAI && p.Question(aiCardValue))
                        {
                            ai.GetCardRecipient();
                            var newValueAI = p.AddCard(cards, lastIndex, out lastIndex);
                            aiCardValue += newValueAI;
                        }
                        else
                        {
                            answerAI = false;
                        }

                        if (answerPlayer && Question())
                        {
                            human.GetCardRecipient();
                            var newValueAI = p.AddCard(cards, lastIndex, out lastIndex);
                            playerCardValue += newValueAI;
                        }
                        else
                        {
                            answerPlayer = false;
                        }
                    }
                    Console.WriteLine($"Summa {human.GetName()} : {playerCardValue}");
                    Console.WriteLine($"Summa {ai.GetName()} : {aiCardValue}\n");
                }
                while ((answerPlayer || answerAI) && (playerCardValue < 21 && aiCardValue < 21));

                Console.WriteLine($"Value calculation...\n");

                if (aiCardValue == playerCardValue)
                {
                    Console.WriteLine($"Draw!!!");
                }
                else if (aiCardValue > 21 && playerCardValue < 21)
                {
                    human.GetNameWinner();
                    human.UpdatePoint();
                }
                else if (playerCardValue > 21 && aiCardValue < 21)
                {
                    ai.GetNameWinner();
                    ai.UpdatePoint();
                }
                else if (playerCardValue > 21 && aiCardValue > 21)
                {
                    if (playerCardValue < aiCardValue)
                    {
                        human.GetNameWinner();
                        human.UpdatePoint();
                    }
                    else
                    {
                        ai.GetNameWinner();
                        ai.UpdatePoint();
                    }
                }
                else
                {
                    if (playerCardValue > aiCardValue)
                    {
                        human.GetNameWinner();
                        human.UpdatePoint();
                    }
                    else
                    {
                        ai.GetNameWinner();
                        ai.UpdatePoint();
                    }
                }
                Console.WriteLine($"END GAME\n");
                restart = p.RestartGame();
            }
            while (restart);
            Console.WriteLine($"{human.GetName()} wins : {human.GetPoint()}\n{ai.GetName()} wins : {ai.GetPoint()}");
            Console.ReadLine();
        }
        private int WhoGoesFirst()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
        static bool Question()
        {
            Console.WriteLine($"Want to get another card?( yes / no)");
            var temp = Console.ReadLine().ToLower();
            if (temp == "yes")
            {
                return true;
            }
            else if (temp == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine($"Enter( yes / no)");
                return Question();
            }
        }
        private bool Question(int aiCardValue)
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
        private bool RestartGame()
        {
            Console.WriteLine($"Do you want to start a new game?(Yes / No)");
            var temp = Console.ReadLine().ToLower();
            if (temp == "yes")
            {
                return true;
            }
            else if (temp == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine($"Enter( yes / no)");
                return RestartGame();
            }

        }

        private Cards[] Deck()
        {
            Cards[] cards = new Cards[36];
            int index = 0;
            for (Suits s = Suits.Diamonds; s <= Suits.Spedes; s++)
            {
                for (Card c = Card.Jack; c <= Card.Ace; c++)
                {
                    if ((int)c != 5)// в колоде нет значения 5, то есть в перечислении значений Card его тоже нет , по этому при инициализации колоды на 36 карт его небходимо пропустить , иначе индекс будет больше чем длинна массива!!!
                    {
                        cards[index].Suit = s.ToString();
                        cards[index].Name = c.ToString();
                        cards[index].Value = Convert.ToInt32(c);
                        index++;
                    }
                }
            }
            return cards;
        }
        private Cards[] DeckMixed(Cards[] de)
        {
            Random rand = new Random();

            for (int i = de.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                Cards tmp = de[j];
                de[j] = de[i];
                de[i] = tmp;
            }

            return de;
        }

        private void PrintInfoCard(Cards[] cards, int i)
        {
            Console.WriteLine($"{cards[i].Name} , {cards[i].Suit} = {cards[i].Value} \n ");
        }

        private int AddCard(Cards[] c, int indexOld, out int lastIndex)
        {
            Program p = new Program();
            p.PrintInfoCard(c, indexOld);
            int cardValue = c[indexOld].Value;
            lastIndex = ++indexOld;
            return cardValue;
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
}

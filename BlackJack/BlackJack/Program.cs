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

    class Program
    {
        static void Main(string[] args)
        {
            Cards[] cardsNew = Deck();

            int[] resultgame = new int[2];// [0] - победы игрока(человека) , [1] - победы ИИ
            

            bool restart;
            do
            {
                var cards = DeckMixed(cardsNew);
                Console.Clear();
                int playerCardValue = 0;
                int aiCardValue = 0; // ----- ai == AI(Иску́сственный интелле́кт)

                Console.WriteLine("The deck is ready!\n");

                var firstPlayer = WhoGoesFirst() % 2 == 0;
                if (firstPlayer == true)
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
                        Console.WriteLine($"Players gets:");
                        InfoCard(cards, indexCard);
                        lastIndex = indexCard;
                        playerCardValue += cards[indexCard].Value;
                    }
                    for (int indexCard = 2; indexCard < 4; indexCard++)
                    {
                        Console.WriteLine($"AI gets:");
                        InfoCard(cards, indexCard);
                        lastIndex = indexCard;
                        aiCardValue += cards[indexCard].Value;
                    }

                }
                else
                {
                    for (int indexCard = 0; indexCard < 2; indexCard++)
                    {
                        Console.WriteLine($"AI gets:");
                        InfoCard(cards, indexCard);
                        lastIndex = indexCard;
                        aiCardValue += cards[indexCard].Value;
                    }
                    for (int indexCard = 2; indexCard < 4; indexCard++)
                    {
                        Console.WriteLine($"Players gets:");
                        InfoCard(cards, indexCard);
                        lastIndex = indexCard;
                        playerCardValue += cards[indexCard].Value;
                    }
                }

                Console.WriteLine($"Summa Player : {playerCardValue}");
                Console.WriteLine($"Summa AI : {aiCardValue}\n");

                if (playerCardValue == 22 || playerCardValue == 21)
                {
                    Console.WriteLine($"Players WIN , him score = {playerCardValue}\n");
                    Console.WriteLine($"END GAME\n");
                    resultgame[0]++;

                    bool rg1 = RestartGame();
                    if (rg1)
                    {
                        restart = true;
                    }
                    else
                    {
                        restart = false;
                    }
                    continue;   // break; 
                }
                else if (aiCardValue == 22 || aiCardValue == 21)
                {
                    Console.WriteLine($"AI WIN , him score = {aiCardValue}\n");
                    Console.WriteLine($"END GAME\n");
                    resultgame[1]++;

                    bool rg2 = RestartGame();
                    if (rg2 == true)
                    {
                        restart = true;
                    }
                    else
                    {
                        restart = false;
                    }
                    continue; //break;
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

                    switch (firstPlayer)
                    {
                        case true:
                            var resultQuestionPlayer = Question();
                            if (resultQuestionPlayer)
                            {
                                var result = AddCard(lastIndex);
                                playerCardValue += cards[result].Value;
                                answerPlayer = true;
                                lastIndex++;
                                Console.WriteLine($"Players gets:\n");
                                InfoCard(cards, lastIndex);
                            }
                            else
                            {
                                answerPlayer = false;
                            }

                            var resultQuestionAI = Question(aiCardValue);
                            if (resultQuestionAI)
                            {
                                var move = AddCard(lastIndex);
                                Console.WriteLine("AI took a card\n");
                                aiCardValue += cards[move].Value;
                                answerAI = true;
                                lastIndex++;
                                InfoCard(cards, lastIndex);
                            }
                            else
                            {
                                answerAI = false;
                            }
                            break;
                        case false:
                            var resultQuestionAI1 = Question(aiCardValue);
                            if (resultQuestionAI1)
                            {
                                var move = AddCard(lastIndex);
                                Console.WriteLine("AI took a card\n");
                                aiCardValue += cards[move].Value;
                                answerAI = true;
                                lastIndex++;
                                InfoCard(cards, lastIndex);
                            }
                            else
                            {
                                answerAI = false;
                            }

                            var resultQuestionPlayer1 = Question();
                            if (resultQuestionPlayer1)
                            {
                                var result = AddCard(lastIndex);
                                playerCardValue += cards[result].Value;
                                answerPlayer = true;
                                lastIndex++;
                                Console.WriteLine($"Players gets:\n");
                                InfoCard(cards, lastIndex);
                            }
                            else
                            {
                                answerPlayer = false;
                            }
                            break;
                    }

                    Console.WriteLine($"Summa Player : {playerCardValue}");
                    Console.WriteLine($"Summa AI : {aiCardValue}\n");
                }
                while ((answerPlayer || answerAI) && (playerCardValue < 21 && aiCardValue < 21));

                Console.WriteLine($"Value calculation...\n");

                if (aiCardValue == playerCardValue)
                {
                    Console.WriteLine($"Draw!!!");
                }
                else if (aiCardValue > 21 && playerCardValue < 21)
                {
                    Console.WriteLine($"Players WIN!!!");
                    resultgame[0]++;
                }
                else if (playerCardValue > 21 && aiCardValue < 21)
                {
                    Console.WriteLine($"AI WIN!!!");
                    resultgame[1]++;
                }
                else if (playerCardValue > 21 && aiCardValue > 21)
                {
                    if (playerCardValue < aiCardValue)
                    {
                        Console.WriteLine($"Players WIN!!!");
                        resultgame[0]++;
                    }
                    else
                    {
                        Console.WriteLine($"AI WIN!!!");
                        resultgame[1]++;
                    }
                }
                else
                {
                    if (playerCardValue > aiCardValue)
                    {
                        Console.WriteLine($"Players WIN!!!");
                        resultgame[0]++;
                    }
                    else
                    {
                        Console.WriteLine($"AI WIN!!!");
                        resultgame[1]++;
                    }
                }
                Console.WriteLine($"END GAME\n");
                bool rg = RestartGame();
                if (rg)
                {
                    restart = true;
                }
                else
                {
                    restart = false;
                }
            }
            while (restart);
            Console.WriteLine($"{Players.Player} wins : {resultgame[0]}\n{Players.AI} wins : {resultgame[1]}");
            Console.ReadLine();
        }

        static int WhoGoesFirst()
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
        static bool Question(int aiCardValue)
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
        static int AddCard(int indexLast)
        {
            var nextcard = ++indexLast;
            return nextcard;
        }


        static bool RestartGame()
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

        static Cards[] Deck()
        {
            Random r = new Random();
            Cards[] cards = new Cards[36];
            for (int i = 0; i < cards.Length; i += 4)
            {
                cards[i].Suit = Suits.Diamonds.ToString();
            }
            for (int i = 1; i < cards.Length; i += 4)
            {
                cards[i].Suit = Suits.Clubs.ToString();
            }
            for (int i = 2; i < cards.Length; i += 4)
            {
                cards[i].Suit = Suits.Hearts.ToString();
            }
            for (int i = 3; i < cards.Length; i += 4)
            {
                cards[i].Suit = Suits.Spedes.ToString();
            }
            for (int i = 0; i < 4; i++)
            {
                cards[i].Name = Card.Six.ToString();
                cards[i].Value = Convert.ToInt32(Card.Six);
            }
            for (int i = 4; i < 8; i++)
            {
                cards[i].Name = Card.Seven.ToString();
                cards[i].Value = Convert.ToInt32(Card.Seven);
            }
            for (int i = 8; i < 12; i++)
            {
                cards[i].Name = Card.Eight.ToString();
                cards[i].Value = Convert.ToInt32(Card.Eight);
            }
            for (int i = 12; i < 16; i++)
            {
                cards[i].Name = Card.Nine.ToString();
                cards[i].Value = Convert.ToInt32(Card.Nine);
            }
            for (int i = 16; i < 20; i++)
            {
                cards[i].Name = Card.Ten.ToString();
                cards[i].Value = Convert.ToInt32(Card.Ten);
            }
            for (int i = 20; i < 24; i++)
            {
                cards[i].Name = Card.Jack.ToString();
                cards[i].Value = Convert.ToInt32(Card.Jack);
            }
            for (int i = 24; i < 28; i++)
            {
                cards[i].Name = Card.Lady.ToString();
                cards[i].Value = Convert.ToInt32(Card.Lady);
            }
            for (int i = 28; i < 32; i++)
            {
                cards[i].Name = Card.King.ToString();
                cards[i].Value = Convert.ToInt32(Card.King);
            }
            for (int i = 32; i < 36; i++)
            {
                cards[i].Name = Card.Ace.ToString();
                cards[i].Value = Convert.ToInt32(Card.Ace);
            }

            return cards;
        }
        static Cards[] DeckMixed(Cards[] de)
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

        static void InfoCard(Cards[] cards, int i)
        {
            Console.WriteLine($"{cards[i].Name} , {cards[i].Suit} = {cards[i].Value} \n ");
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
        AI
    }

}

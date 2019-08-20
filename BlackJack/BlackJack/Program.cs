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
            int[] resultgame = new int[2];// [0] - победы игрока(человека) , [1] - победы ИИ

            bool restart ;
            do
            {
                Console.Clear();
                int playerCardValue = 0;
                int aiCardValue = 0; // ----- ai == AI(Иску́сственный интелле́кт)

                #region Initialization cards
                Cards[] cards = new Cards[36];
                cards[0].Name = "Six"; cards[0].Suit = "Clubs"; cards[0].Value = 6;// Креста
                cards[1].Name = "Six"; cards[1].Suit = "Spedes"; cards[1].Value = 6;// Пика
                cards[2].Name = "Six"; cards[2].Suit = "Hearts"; cards[2].Value = 6;// Черви
                cards[3].Name = "Six"; cards[3].Suit = "Diamonds"; cards[3].Value = 6;// Бубна

                cards[4].Name = "Seven"; cards[4].Suit = "Clubs"; cards[4].Value = 7;// Креста
                cards[5].Name = "Seven"; cards[5].Suit = "Spedes"; cards[5].Value = 7;// Пика
                cards[6].Name = "Seven"; cards[6].Suit = "Hearts"; cards[6].Value = 7;// Черви
                cards[7].Name = "Seven"; cards[7].Suit = "Diamonds"; cards[7].Value = 7;// Бубна

                cards[8].Name = "Eight"; cards[8].Suit = "Clubs"; cards[8].Value = 8;// Креста
                cards[9].Name = "Eight"; cards[9].Suit = "Spedes"; cards[9].Value = 8;// Пика
                cards[10].Name = "Eight"; cards[10].Suit = "Hearts"; cards[10].Value = 8;// Черви
                cards[11].Name = "Eight"; cards[11].Suit = "Diamonds"; cards[11].Value = 8;// Бубна

                cards[12].Name = "Nine"; cards[12].Suit = "Clubs"; cards[12].Value = 9;// Креста
                cards[13].Name = "Nine"; cards[13].Suit = "Spedes"; cards[13].Value = 9;// Пика
                cards[14].Name = "Nine"; cards[14].Suit = "Hearts"; cards[14].Value = 9;// Черви
                cards[15].Name = "Nine"; cards[15].Suit = "Diamonds"; cards[15].Value = 9;// Бубна

                cards[16].Name = "Ten"; cards[16].Suit = "Clubs"; cards[16].Value = 10;// Креста
                cards[17].Name = "Ten"; cards[17].Suit = "Spedes"; cards[17].Value = 10;// Пика
                cards[18].Name = "Ten"; cards[18].Suit = "Hearts"; cards[18].Value = 10;// Черви
                cards[19].Name = "Ten"; cards[19].Suit = "Diamonds"; cards[19].Value = 10;// Бубна

                cards[20].Name = "Jack"; cards[20].Suit = "Clubs"; cards[20].Value = 2;// Креста
                cards[21].Name = "Jack"; cards[21].Suit = "Spedes"; cards[21].Value = 2;// Пика
                cards[22].Name = "Jack"; cards[22].Suit = "Hearts"; cards[22].Value = 2;// Черви
                cards[23].Name = "Jack"; cards[23].Suit = "Diamonds"; cards[23].Value = 2;// Бубна

                cards[24].Name = "Lady"; cards[24].Suit = "Clubs"; cards[24].Value = 3;// Креста
                cards[25].Name = "Lady"; cards[25].Suit = "Spedes"; cards[25].Value = 3;// Пика
                cards[26].Name = "Lady"; cards[26].Suit = "Hearts"; cards[26].Value = 3;// Черви
                cards[27].Name = "Lady"; cards[27].Suit = "Diamonds"; cards[27].Value = 3;// Бубна

                cards[28].Name = "King"; cards[28].Suit = "Clubs"; cards[28].Value = 4;// Креста
                cards[29].Name = "King"; cards[29].Suit = "Spedes"; cards[29].Value = 4;// Пика
                cards[30].Name = "King"; cards[30].Suit = "Hearts"; cards[30].Value = 4;// Черви
                cards[31].Name = "King"; cards[31].Suit = "Diamonds"; cards[31].Value = 4;// Бубна

                cards[32].Name = "Ace"; cards[32].Suit = "Clubs"; cards[32].Value = 11;// Креста
                cards[33].Name = "Ace"; cards[33].Suit = "Spedes"; cards[33].Value = 11;// Пика
                cards[34].Name = "Ace"; cards[34].Suit = "Hearts"; cards[34].Value = 11;// Черви
                cards[35].Name = "Ace"; cards[35].Suit = "Diamonds"; cards[35].Value = 11;// Бубна                    

                #endregion

                Console.WriteLine("The deck is ready!\n");

                // int firstPlayer = WhoGoesFirst();
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
                //Random rnd = new Random();
                //var card1Player = rnd.Next(0, 35);
                //var card2Player = rnd.Next(0, 35);
                //var card1AI = rnd.Next(0, 35);
                //var card2AI = rnd.Next(0, 35);

                //Console.WriteLine($"Cards Player\n 1:{cards[card1Player].Name}  {cards[card1Player].Suit}\n  2:{cards[card2Player].Name} {cards[card2Player].Suit} \n" +
                //    $"Cards AI\n 1:{cards[card1AI].Name}  {cards[card1AI].Suit}\n  2:{cards[card2AI].Name}  {cards[card2AI].Suit} ");

                //playerCardValue = cards[card1Player].Value + cards[card2Player].Value;
                //aiCardValue = cards[card1AI].Value + cards[card2AI].Value;

                // Раздаем по 2 карты в соответствии с очередью кто первый должен был их получить

                Random rnd = new Random();
                if (firstPlayer == true)
                {
                    var card1Player = rnd.Next(0, 35);
                    var card2Player = rnd.Next(0, 35);
                    var card1AI = rnd.Next(0, 35);
                    var card2AI = rnd.Next(0, 35);
                    Console.WriteLine($"Cards Player\n 1:{cards[card1Player].Name}  {cards[card1Player].Suit}\n  2:{cards[card2Player].Name} {cards[card2Player].Suit} \n" +
                        $"Cards AI\n 1:{cards[card1AI].Name}  {cards[card1AI].Suit}\n  2:{cards[card2AI].Name}  {cards[card2AI].Suit}\n");

                    playerCardValue = cards[card1Player].Value + cards[card2Player].Value;
                    aiCardValue = cards[card1AI].Value + cards[card2AI].Value;
                }
                else
                {
                    var card1AI = rnd.Next(0, 35);
                    var card2AI = rnd.Next(0, 35);
                    var card1Player = rnd.Next(0, 35);
                    var card2Player = rnd.Next(0, 35);
                    Console.WriteLine($"Cards AI\n 1:{ cards[card1AI].Name}  { cards[card1AI].Suit}\n  2:{ cards[card2AI].Name}  { cards[card2AI].Suit}\n" +
                                      $"Cards Player\n 1:{cards[card1Player].Name}  {cards[card1Player].Suit}\n  2:{cards[card2Player].Name} {cards[card2Player].Suit}\n");

                    aiCardValue = cards[card1AI].Value + cards[card2AI].Value;
                    playerCardValue = cards[card1Player].Value + cards[card2Player].Value;
                }

                Console.WriteLine($"Summa Player : {playerCardValue}");
                Console.WriteLine($"Summa AI : {aiCardValue}\n");

                if (playerCardValue == 22 || playerCardValue == 21)
                {
                    Console.WriteLine($"Players WIN , him score = {playerCardValue}\n");
                    resultgame[0]++;
                    return;
                }
                else if (aiCardValue == 22 || aiCardValue == 21)
                {
                    Console.WriteLine($"AI WIN , him score = {playerCardValue}\n");
                    resultgame[1]++;
                    return;
                }
                else
                {
                    Console.WriteLine($"Nobody scored 21, continue the game");
                }

                //Начинаем спрашивать не хотят ли игроки взять дополнительные карты 

                switch (firstPlayer)
                {
                    case true:
                        var result = AddCardPlayer();
                        if (result == 1000)
                        {
                            playerCardValue += 0;
                        }
                        else
                        {
                            playerCardValue += cards[result].Value;
                        }

                        var move = AddCardAI(aiCardValue);
                        if (move == 1000)
                        {
                            aiCardValue += 0;
                        }
                        else
                        {
                            aiCardValue += cards[move].Value;
                        }
                        break;
                    case false:
                        var move1 = AddCardAI(aiCardValue);
                        if (move1 == 1000)
                        {
                            aiCardValue += 0;
                        }
                        else
                        {
                            aiCardValue += cards[move1].Value;
                        }

                        var result1 = AddCardPlayer();
                        if (result1 == 1000)
                        {
                            playerCardValue += 0;
                        }
                        else
                        {
                            playerCardValue += cards[result1].Value;
                        }
                        break;
                }




                Console.WriteLine($"Summa Player : {playerCardValue}");
                Console.WriteLine($"Summa AI : {aiCardValue}\n");
                Console.WriteLine($"Value calculation...\n");


                if (playerCardValue == 22 || playerCardValue == 21)
                {
                    Console.WriteLine($"Players WIN , him score = {playerCardValue}\n");
                    resultgame[0]++;
                    return;
                }
                else if (aiCardValue == 22 || aiCardValue == 21)
                {
                    Console.WriteLine($"AI WIN , him score = {playerCardValue}\n");
                    resultgame[1]++;
                    return;
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
                    Console.WriteLine($"END GAME\n");
                }
                bool rg = RestartGame();
                if(rg == true)
                {
                    restart = true;
                }
                else
                {
                    restart = false;
                }
               // Console.ReadLine();
            }
            while (restart == true);
            Console.WriteLine($"Player wins : {resultgame[0]}\nAI wins : {resultgame[1]}");
            Console.ReadLine();
        }

        static int WhoGoesFirst()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        static int AddCardPlayer()
        {

            Console.WriteLine($"Want to get another card?( yes / no)");
            var temp = Console.ReadLine();
            Random r = new Random();
            if (temp == "yes" || temp == "Yes")
            {

                var nextcard = r.Next(0, 35);
                return nextcard;
            }
            else if (temp == "No" || temp == "no")
            {
                return 1000;// 1000 == No
            }
            else
            {
                Console.WriteLine($"Enter( yes / no)");
                return AddCardPlayer();
            }
        }

       

        static int AddCardAI(int value)
        {
            Random r = new Random();
            if (value<19)
            {
                var nextcard = r.Next(0, 35);
                return nextcard;
            }
            else
            {
                return 1000; // 1000 == STOP(NO)
            }
        }

        static bool RestartGame()
        {
            Console.WriteLine($"Do you want to start a new game?(Yes / No)");
            var temp = Console.ReadLine();
            if (temp == "yes" || temp == "Yes")
            {                
                return true;
            }
            else if (temp == "No" || temp == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine($"Enter( yes / no)");
                return RestartGame();
            }

        }

    }
}

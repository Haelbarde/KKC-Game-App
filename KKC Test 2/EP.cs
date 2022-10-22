using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKC_Test_2
{
    internal class EP
    {

        int[] EPTotals = new int[9];
        List<List<Player>> playersInFields = new List<List<Player>>();
        List<Player> playersElevated = new List<Player>();


        /// <summary>
        /// Manages all Elevation Point calculations.
        /// </summary>
        /// <param name="input"></param>

        public EP(ValueRange input)
        {
            var values = input.Values;

            ImportPlayers(values);

            for (int i = 0; i < 9; i++)
            {
                playersInFields.Add(new List<Player>());
            }
        }

        public void _Upload()
        {
           /* // Save Points to GM sheet
            var valueRange = new ValueRange();
            valueRange.Values = _ToLists(points);
            if (KKC.service is not null)
            {
                var updateRequest = KKC.service.Spreadsheets.Values.Update(valueRange, KKC.SpreadsheetIDGM, $"{KKC.gmSheet}!C2:K" + _playerList.Count + 1);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                var updateResponse = updateRequest.Execute();

            }*/
        }


        public IList<IList<object>> _ToLists(int[,] array)
        {
            IList<IList<object>> list = new List<IList<object>>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                IList<object> col = new List<object>();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    col.Add((object)array[i, j]);
                }
                list.Add(col);
            }
            return list;
        }



        /// <summary>
        /// Import existing EP totals from GM Spreadsheet into each player's EP field.
        /// </summary>
        /// <param name="playerInput"></param>
        public void ImportPlayers(IList<IList<object>> playerInput)
        {

            if (playerInput is not null && playerInput.Count > 0)
            {
                foreach (Player player in KKC.playerList)
                {
                    for (int field = 0; field < 9; field++)
                    {
                        int.TryParse(playerInput[player.ID][field].ToString(), out player.EP[field]);
                    }
                }
            }

            // Log imported values
            WriteToConsole();
            Console.WriteLine("Imported Array");
            
        }


        public static void ImportRound()
        {
            // Import player data from each sheet

            foreach (Player player in KKC.playerList)
            {
                ValueRange response = KKC.RequestPlayerRange(player.ID, KKC.playerSheet, "B11:B15");
                var input = response.Values;
                foreach (var EP in input)
                {
                    var EPinput = EP[0].ToString();
                    if (EPinput is not null)
                    {
                        string val = EPinput;
                        if (val == "Rhetoric & Logic")
                        {
                            val = "RhetoricAndLogic";
                        }

                        if (Enum.TryParse(val, out KKC.Fields field) is false)
                        {
                            throw new Exception("");
                        }
                        player.EPFiled.Add(field);
                        // Adds the EP to the existing EP total
                        player.EP[(int)field] += 1;
                    }

                }

            }
        }





        public void WriteToConsole()
        {
            Console.WriteLine("         Lin Ari R&L Arc Sym Phy Alc Art Nam");

            foreach (Player player in KKC.playerList)
            {
                string output = player.Name.Substring(0, 4);
                output += "  [   ";
                for (int field = 0; field < 9; field++)
                {
                    output += player.EP[field] + "   ";
                }
                output += "]";
                Console.WriteLine(output);
            }
        }



        /// <summary>
        /// Find the sum of all EP invested in the specified field.
        /// </summary>>
        public int EPPool(KKC.Fields field)
        {
            int sum = 0;
            int index = (int)field;
            foreach (Player player in KKC.playerList)
            {

                if (player.EP[index] > 0 && playersElevated.Contains(player) is false)
                {
                    sum += player.EP[index];
                    playersInFields[index].Add(player);
                }
            }
            return sum;
        }


        /// <summary>
        /// Randomly elevates a player in the specified field.
        /// </summary>
        public void ElevateField(KKC.Fields field)
        {

            Random random = new Random();

            

           
            // Check if Master or Elthe
            if (false)
            {

            }
            else
            {
                int total = EPPool(field);
                if (total > 0)
                {
                    int selection = random.Next(0, total + 1);
                    int sum = 0;
                    foreach (Player player in playersInFields[(int)field])
                    {
                        sum += player.EP[(int)field];
                        // Current player is the selected player
                        if (sum >= selection)
                        {
                            player.Elevate((KKC.Fields)field);
                            playersElevated.Add(player);
                                
                            Console.WriteLine($"{(KKC.Fields)field}: {player.Name} ({selection})");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{field}: No player elevated");
                }
            }

        }















    }
}

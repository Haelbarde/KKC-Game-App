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
        public static int[,] points;
        static int col;
        static int row;
        private List<Player> _playerList;
        List<int> elevatedPlayers;

        /// <summary>
        /// Manages all Elevation Point calculations.
        /// </summary>
        /// <param name="input"></param>
        public EP(ValueRange input, List<Player> players)
        {
            var values = input.Values;
            col = Enum.GetNames(typeof(KKC.Fields)).Length;
            row = values.Count;
            var array = new int[row, col];
            points = array;
            _playerList = players;
            elevatedPlayers = new List<int>();
            _ImportPlayers(values);
        }

        public void _Upload()
        {
            // Save Points to GM sheet
            var valueRange = new ValueRange();
            valueRange.Values = _ToLists(points);
            if (KKC.service is not null)
            {
                var updateRequest = KKC.service.Spreadsheets.Values.Update(valueRange, KKC.SpreadsheetIDGM, $"{KKC.gmSheet}!C2:K" + _playerList.Count + 1);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                var updateResponse = updateRequest.Execute();

            }
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
        /// Import existing EP from GM Spreadsheet.
        /// </summary>
        /// <param name="playerInput"></param>
        public void _ImportPlayers(IList<IList<object>> playerInput)
        {
         
            if (playerInput != null && playerInput.Count > 0)
            {
                int playerID = 0;
                foreach (var row in playerInput)
                {
                    for (int col = 0; col < 9; col++) 
                    {
                        int.TryParse(row[col].ToString(), out points[playerID, col]);
                    }
                    playerID++;
                }
            }

            // Log imported values
            Console.WriteLine("Imported Array");
            _WriteToConsole();
        }




        public static void _ImportRound()
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

                        points[player.ID, (int)field] += 1;
                    }
                   
                }

            }
        }
        public void _WriteToConsole()
        {
            Console.WriteLine("         Lin Ari R&L Arc Sym Phy Alc Art Nam");

            for (int i = 0; i < row; i++)
            {
                string output = _playerList[i].Name.Substring(0, 4);
                output += "  [   ";
                for (int j = 0; j < col; j++)
                {
                    output += points[i, j] + "   ";
                }
                output += "]";
                Console.WriteLine(output);
            }
        }



        /// <summary>
        /// Find the sum of all EP invested in the specified field.
        /// </summary>
        /// <param name="field"></param>
        /// <returns>Total EP invested in given field.</returns>
        public Tuple<List<int>,int> _SumColumn(KKC.Fields field)
        {
            List<int> playerIDs = new List<int>();
            int sum = 0;
            for (int playerID = 0; playerID < row; playerID++)
            {
                int i = points[playerID, (int)field];
                if ( (i > 0) && (elevatedPlayers.Contains(playerID) == false) )
                {
                    sum += i;
                    playerIDs.Add(playerID);
                }
            }
            return new Tuple<List<int>, int> (playerIDs, sum);
        }

        /// <summary>
        /// Randomly elevates a player in the specified field.
        /// </summary>
        /// <param name="field"></param>
        /// <returns>Player ID eleveted in the given field as an int.</returns>
        public int _ElevateField(KKC.Fields field)
        {
            
            Random random = new Random();
           
            var optionsToElevate = _SumColumn(field);
            int totalEP = optionsToElevate.Item2;

            if (totalEP > 0)
            {
                List<int> players = optionsToElevate.Item1;

                int selection = random.Next(0, totalEP) + 1;



                int playerID = 0;
                int sum = points[players[playerID], (int)field];

                while (sum < selection)
                {
                    playerID++;
                    sum += points[players[playerID], (int)field];
                }


                elevatedPlayers.Add(players[playerID]);

                // Deduct EP from elevated Player
                int ep = points[players[playerID], (int)field];
                ep = Math.Max(ep - 5, 0);
                points[players[playerID], (int)field] = ep;

                Console.WriteLine($"{field}: {_playerList[players[playerID]].Name} ({selection})");

                return players[playerID];
            }
            else
            {
                Console.WriteLine($"{field}: No player elevated");
                return -1;
            }
            
        }
    }
}

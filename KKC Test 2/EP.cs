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
        List<List<Player>> playersInFields = new List<List<Player>>();
        List<Player> playersElevated = new List<Player>();

        /// <summary>
        /// Manages all Elevation Point calculations.
        /// </summary>
        public EP()
        {
            var input = KKC.RequestGMRange(KKC.gmSheet, "C2:K" + (KKC.playerList.Count + 1));
            var values = input.Values;

            ImportPlayers(values);

            for (int i = 0; i < 9; i++)
            {
                playersInFields.Add(new List<Player>());
            }
        }


        /// <summary>
        /// Import existing EP totals from GM Spreadsheet into each player's EP field.
        /// </summary>
        /// <param name="playerInput">Array of data imported from GM Spreadsheet.</param>
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
            Debug.Log("Imported Array", ConsoleColor.Magenta, false);
            WriteToConsole();
        }

        /// <summary>
        /// Imports data from each Player's Sheet.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void ImportRound()
        {
            // Import player data from each sheet

            foreach (Player player in KKC.playerList)
            {
                ValueRange response = KKC.RequestPlayerRange(player, KKC.playerSheet, "B11:B15");
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


        /// <summary>
        /// Randomly elevates a player in the specified field.
        /// </summary>
        public void ElevateField(KKC.Fields field)
        {
            Random random = new();

            // Check if Master or Elthe.
            // Also need to deal with the issue if multiple fields would want to elevate.
            if (false)
            {/*
                // Check if more than one field would elevate to Master.
                List<KKC.Fields> other = new List<KKC.Fields>();
                foreach (KKC.Fields e in Enum.GetValues(typeof(KKC.Fields)))
                {
                    if (EP[(int)e] >= 15)
                    {
                        other.Add(e);
                    }
                }

                // If additional feeds, handle it
                // - If has more EP than any other, elevate that field
                // - Otherwise, randomly choose between fields tied for most EP
                if (other.Count > 1) // Additional fields, so handle situation
                {
                    int max = 0;
                    List<KKC.Fields> mostEP = new List<KKC.Fields>();
                    foreach (KKC.Fields e in other)
                    {
                        if (EP[(int)e] == max)
                        {
                            mostEP.Add(e);
                        }
                        else if (EP[(int)e] > max)
                        {
                            max = EP[(int)e];
                            mostEP.Clear();
                            mostEP.Add(e);
                        }
                    }

                    // Overide field with new field to be elevated.
                    if (mostEP.Count > 1)
                    {
                        Random rand = new();
                        int selection = rand.Next(0, mostEP.Count);
                        field = mostEP[selection];
                    }
                    else
                    {
                        field = mostEP[0];
                    }
                }*/
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
                                
                            Debug.Log($"{(KKC.Fields)field}: {player.Name} ({selection})", ConsoleColor.Blue, false);
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log($"{field}: No player elevated", ConsoleColor.Blue, false);
                }
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
        /// Uploads updated EP to GM Sheet
        /// </summary>
        public void Upload()
        {
            // Save Points to GM sheet
            var valueRange = new ValueRange();
            valueRange.Values = ToLists();
            if (KKC.service is not null)
            {
                var updateRequest = KKC.service.Spreadsheets.Values.Update(valueRange, KKC.SpreadsheetIDGM, $"{KKC.gmSheet}!C2:K" + KKC.playerList.Count + 1);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                var updateResponse = updateRequest.Execute();
            }
        }

        /// <summary>
        /// Packages EP into the ValueRange datatype
        /// </summary>
        /// <returns>Returns converted EP structure.</returns>
        public IList<IList<object>> ToLists()
        {
            Debug.Log($"Packaging Player EP arrays for upload...");
            IList<IList<object>> list = new List<IList<object>>();
            foreach (Player player in KKC.playerList)
            {
                IList<object> col = new List<object>();
                foreach (KKC.Fields field in Enum.GetValues(typeof(KKC.Fields)))
                {
                    col.Add((object)player.EP[(int)field]);
                }
                list.Add(col);
                Debug.Log($"Added {player.Name}.");
            }
            Debug.Log($"Complete.");
            return list;
        }

        /// <summary>
        /// Writes to console the current EP array.
        /// </summary>
        public static void WriteToConsole()
        {
            Debug.Log("         Lin Ari R&L Arc Sym Phy Alc Art Nam", ConsoleColor.Yellow, false);

            foreach (Player player in KKC.playerList)
            {
                string output = player.Name.Substring(0, 4);
                output += "  [   ";
                for (int field = 0; field < 9; field++)
                {
                    output += player.EP[field] + "   ";
                }
                output += "]";
                Debug.Log(output, ConsoleColor.Yellow, false);
            }
        }
    }
}

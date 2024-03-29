﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKC_Test_2
{
    internal class KKC
    {
        /// <summary>
        /// List of all player objects. All player references should be retrieved from this list.
        /// </summary>
        public static List<Player> playerList { get; set; } = new List<Player>();

        /// <summary>
        /// The different fields of study in the university. Use (int)KKC.Fields to cast to integer to use as an index to access lists and arrays.
        /// </summary>
        public enum Fields { Linguistics, Arithmetics, RhetoricAndLogic, Archives, Sympathy, Physicking, Alchemy, Artificery, Naming };

        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "KKC GM App";
        public static readonly string SpreadsheetIDGM = "1zJGpn9_3My5ExYwMr_4Qu--B927N0eTBEjIoiQ3y-J0";
        public static readonly string gmSheet = "GM Data";
        public static readonly string playerSheet = "Player Input";
        public static SheetsService service;



        /// <summary>
        /// Manages connection to Google Sheets, and importing initial player list into local memory.
        /// </summary>
        public KKC()
        {
            // Set up credentials for using the Google Sheets API
            GoogleCredential credential;
            using (var stream = new FileStream("sheets_auth_key.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }
            Debug.Log("Credential Created.");


            // Open Sheets requests
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            Debug.Log("Service Created.");


            // Import player list from GM Sheet
            int numberOfPlayers = 0;
            var response1 = RequestGMRange(gmSheet, "B1:B1").Values;;
            int.TryParse((response1[0][0]).ToString(), out numberOfPlayers);

            var response = RequestGMRange(gmSheet, "A2:B" + (numberOfPlayers + 1));
            var values = response.Values;


            // Display retrieved values in the player combobox
            Debug.Log("Adding players to ComboBox and static PlayerList...");
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // row[0] has the name, row[1] has the sheet reference string
                    if (row is not null)
                    {
                        var name = row[0].ToString();
                        var sheet = row[1].ToString();
                        if (name is not null && sheet is not null)
                        {
                            Player player = new Player(name, sheet);
                            playerList.Add(player);
                            Debug.Log($"Added {player.Name}.");
                        }
                    } 
                }
            }
        }

        public static void ImportAll()
        {
            EP.Import();
            Horns.Import();
        }

        public static void ProcessAll()
        {
            EP.Process();
            Horns.Process();
        }

        /// <summary>
        /// Request Data from the GM Sheet Google Sheet.
        /// </summary>
        /// <param name="sheet">The name of the relevant tab on the Google Sheet.</param>
        /// <param name="inputRange">The Cell Range to import.</param>
        /// <returns>Returns list of rows, each of which list the values in the columns of range.</returns>
        public static ValueRange RequestGMRange(string sheet, string inputRange)
        {
            Debug.Log("Request range from GM Sheet...");
            var sheetRange = $"{sheet}!" + inputRange;
            var request = service.Spreadsheets.Values.Get(SpreadsheetIDGM, sheetRange);
            var range = request.Execute(); ;
            Debug.Log("Request returned.");
            return range;
        }


        /// <summary>
        /// Request Data from the specified Player Google Sheet.
        /// </summary>
        /// <param name="playerID">The Player object from KKC.PlayerList</param>
        /// <param name="sheet">The name of the relevant tab on the Google Sheet.</param>
        /// <param name="inputRange">The Cell Range to import.</param>
        /// <returns>Returns list of rows, each of which list the values in the columns of range.</returns>
        public static ValueRange RequestPlayerRange(Player player, string sheet, string inputRange)
        {
            Debug.Log("Request range from Player Sheet...");
            var sheetRange = $"{sheet}!" + inputRange;
            var request = service.Spreadsheets.Values.Get(player.Sheet, sheetRange);
            var range = request.Execute();
            Debug.Log("Request returned.");
            return range;
        }
    }
}

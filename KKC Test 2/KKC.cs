using Google.Apis.Auth.OAuth2;
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
        /// Player Name, Sheet ID
        /// </summary>
        public static List<Player> playerList { get; set; } = new List<Player>();
        public enum Fields { Linguistics, Arithmetics, RhetoricAndLogic, Archives, Sympathy, Physicking, Alchemy, Artificery, Naming };

        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "KKC GM App";
        public static readonly string SpreadsheetIDGM = "1zJGpn9_3My5ExYwMr_4Qu--B927N0eTBEjIoiQ3y-J0";
        static readonly List<string> SpreadsheetIDPlayers = new List<string> {
                    "1NqHM6p0JzW2P6RmWrANnxI_xPeExslcsRqNiP7onYZw",
                    "11fQfzEk4GD-p030RBGMHhPzUsI0A1Si0S-Y84A0p8zk" };

        public static readonly string gmSheet = "GM Data";
        public static readonly string playerSheet = "Player Input";
        public static SheetsService service;

        public KKC()
        {
            // Set up credentials for using the Google Sheets API
            GoogleCredential credential;
            using (var stream = new FileStream("sheets_auth_key.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }

            // Open Sheets requests
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });




            // Import player list from GM Sheet
            int numberOfPlayers = 0;
            var response1 = RequestGMRange(gmSheet, "B1:B1").Values;;
            int.TryParse((response1[0][0]).ToString(), out numberOfPlayers);

            var response = RequestGMRange(gmSheet, "A2:B" + (numberOfPlayers + 1));
            var values = response.Values;

            // Display retrieved values in the player combobox
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
                        }
                        
                    }
                    
                }
            }
        }

        void SetupSheetsConnection()
        {
            


            
        }


        public ValueRange RequestGMRange(string sheet, string inputRange)
        {
            var range = $"{sheet}!" + inputRange;
            var request = service.Spreadsheets.Values.Get(SpreadsheetIDGM, range);
            return request.Execute();          
        }

        public static ValueRange RequestPlayerRange(int playerID, string sheet, string inputRange)
        {
            var range = $"{sheet}!" + inputRange;
            var request = service.Spreadsheets.Values.Get(KKC.playerList[playerID].Sheet, range);
            return request.Execute();
        }
    }
}

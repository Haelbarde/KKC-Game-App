using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.IO;


/* Useful Links
 * KKC Rules - https://docs.google.com/document/d/10TC5lTdIK4vhIh_4sJo73Q7y41L6n64Tqt9iua-L2eg/
 * LG 65 - https://www.17thshard.com/forum/topic/89830-long-game-65-dancing-through-life/
 * LG 65 Master Sheet - https://docs.google.com/spreadsheets/d/1ds7JOCMGmJnTRFj_G_9YXtqjqwBwZp6PUXT-ZK5dVDA/edit
 * Test Sheet - https://docs.google.com/spreadsheets/d/1NqHM6p0JzW2P6RmWrANnxI_xPeExslcsRqNiP7onYZw/edit
 * Google Sheets & .NET Core Read/Write example - https://www.youtube.com/watch?v=afTiNU6EoA8&ab_channel=Twilio
 * Google Sheets API - https://developers.google.com/api-client-library/dotnet/apis/sheets/v4
 */

namespace KKC_Test_2
{
    public partial class Form1 : Form
    {
      

        KKC kkc;
        EP? ep;

        public Form1()
        {
            Debug.Log("Form Opened.");

            Debug.Log("Start Initialising components...");
            InitializeComponent();
            Debug.Log("Completed.");

            Debug.Log("Create KKC object...");
            kkc = new KKC();
            Debug.Log("Completed.");


            // Setup player list in Form combobox
            Debug.Log("Setup players in ComboBox...");
            playerListBox.Items.Clear();
            foreach (var player in KKC.playerList)
            {
                playerListBox.Items.Add(player.Name);
            }
            Debug.Log("Completed.");

            // Import EP from GM sheet
            Debug.Log("Start importing EP totals from GM Sheet...");
            if (KKC.playerList.Count > 0)
            {
                ep = new EP();
            }
            Debug.Log("Completed.");

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (ep is not null)
            {
                // Imports current EP from player sheets
                EP.ImportRound();

                EP.WriteToConsole();


                // Elevate each player. Currently returns the player ID of the elevated player, or -1 if no elevation.
                foreach (KKC.Fields field in Enum.GetValues(typeof(KKC.Fields)))
                {
                    ep.ElevateField(field);
                }

                EP.WriteToConsole();

                // TO DO
                // - Update Rank
                // - Update Player Record
                // - Update GM Data

                // pointArray._Upload();







                /*
                var range = $"{playerSheet}!C9:C13";
                var valueRange = new ValueRange();

                var objectList = new List<object>() { EP1.Text, EP2.Text, EP3.Text, EP4.Text, EP5.Text };
                valueRange.MajorDimension = "COLUMNS";
                valueRange.Values = new List<IList<object>> { objectList };


                var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreadsheetID, range);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                var updateResponse = updateRequest.Execute(); 

                /*var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetID, range);
                appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                var appendResponse = appendRequest.Execute();*/
            }

        }


        void OutputEPArray()
        {
            /*var range = $"{gmSheet}!C2:K" + (KKC.playerList.Count + 1);
            var valueRange = new ValueRange();

            var objectList = new List<object>() { EP1.Text, EP2.Text, EP3.Text, EP4.Text, EP5.Text };
            valueRange.MajorDimension = "COLUMNS";
            valueRange.Values = new List<IList<object>> { objectList };


            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreadsheetID, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateResponse = updateRequest.Execute();

            EP = new int[KKC.playerList.Count, 9];
            //var range = $"{gmSheet}!C2:K" + (playerList.Count + 1);
            var request = service.Spreadsheets.Values.Get(SpreadsheetIDGM, range);
            var response = request.Execute();

            // Retrieve values from response
            var values = response.Values;

            // Display retrieved values in the player combobox
            if (values != null && values.Count > 0)
            {
                int playerInt = 0;
                foreach (var row in values)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        int.TryParse(row[col].ToString(), out EP[playerInt, col]);
                    }
                    playerInt++;
                }

            }*/

        }

    }
}
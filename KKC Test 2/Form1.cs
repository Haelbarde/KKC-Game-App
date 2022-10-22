using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.IO;


/* Useful Links
 * == SE Links ==
 * KKC Rules - https://docs.google.com/document/d/10TC5lTdIK4vhIh_4sJo73Q7y41L6n64Tqt9iua-L2eg/
 * LG 65 - https://www.17thshard.com/forum/topic/89830-long-game-65-dancing-through-life/
 * LG 65 Master Sheet - https://docs.google.com/spreadsheets/d/1ds7JOCMGmJnTRFj_G_9YXtqjqwBwZp6PUXT-ZK5dVDA
 * 
 * == Scripting References ==
 * Google Sheets & .NET Core Read/Write example - https://www.youtube.com/watch?v=afTiNU6EoA8&ab_channel=Twilio
 * Google Sheets API - https://developers.google.com/api-client-library/dotnet/apis/sheets/v4
 * 
 * == Test Sheets == 
 * GM Sheet - https://docs.google.com/spreadsheets/d/1zJGpn9_3My5ExYwMr_4Qu--B927N0eTBEjIoiQ3y-J0
 * Player 1 - https://docs.google.com/spreadsheets/d/1NqHM6p0JzW2P6RmWrANnxI_xPeExslcsRqNiP7onYZw
 * Player 2 - https://docs.google.com/spreadsheets/d/11fQfzEk4GD-p030RBGMHhPzUsI0A1Si0S-Y84A0p8zk
 * Player 3 - https://docs.google.com/spreadsheets/d/1EkScJbcAS9LOECyWL-Zu7lz39L8ezvY5FUIr8P3Vt8s
 * Player 4 - https://docs.google.com/spreadsheets/d/1D1vwFwI-_DNGD0Pl-Q-bxUlENqxZkqQnAovgdQ0Fqb0
 * Player 5 - https://docs.google.com/spreadsheets/d/1Lw-LweoUz48TTAwhB0poKV2Xs5sD_Y8EhXQtjFQHmec
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

                Debug.Log($"Uploading to Google Sheets...");
                ep.Upload();
                Debug.Log($"Upload Completed.");

            }
        }
    }
}
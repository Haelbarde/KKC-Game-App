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
        /* Probably should strcture this as a sequence of calls to different objects based on the Turn Order, which would look something like:
         * 
         * 1. Import all data
         *   - The state during the turn just ended
         *   - All orders regarding actions taken, etc.
         *   -> Somewhere in here needs to factor all the information that will need to be manually input, such as post/PM counts, RPing, etc.
         * 2. Complaints being filed.
         * 3. Non-Offensive Roles + Item Actions, including Imre
         *   - Will need to classify the different actions in this area so as to see if there is any required order. Specifically if things affect the options in the Imre.
         *   - The Imre itself will have some order of precedence. For example, purchases and/or gambling may rely on a Devi loan. Although paying back Devi may rely on selling items or gambling. 
         *      - Some of these will need to be split into two parts to manage this, and form a fairly clear sequence of required Order of Actions. 
         *      - Anything else can happen separately, potentially intentionally kept separate for easier of access to the rigid list of ordered actions.
         * 4. Offensive actions. 
         *   - I imagine this will be fairly straight forward as a matter of dealing with any chances of immediate failure, generating a list of targets, and then doing any necessary roles and/or checks against defences to determine the outcome.
         *   - If it doesn't already exist, will need to work out and publish the offical order that protections get used. This will definitely have been Clarified somewhere. 
         * 5. Elevations - this system is mostly implemented already.
         * 6. Insanity rolls - should be easy. Any permanent bonuses should be already stored and/or easily checked, roll, and process. Main thing will be setting appropriate flags and behviour for the Crockery.
         * 7. The Horns - partialy implemented. Primarily need to track and deal with the consequences, but I think by the time we get here, dealing with lost actions and the suchlike will be easier to envision.
         * 
         * Then for the start of each Cycle.
         * 8. Stipend payout - just a matter of looking up Player.Origin and setting the correct amount.
         * 9. Admissions & Tuition
         *   - One of the areas highly reliant on GM entered data to track half of these. 
         *   - Affected by: Is Master, Inflations/Reductions, Vints, Arithmetics
         * 10. Change of Lodging. And presumably paying for it at this point too. 
         * 11. Uploading to GM sheet. This step should allow for the final review before the final results are pushed to players. 
         *   - Should be possible to recalculate the results if GM's tweak anything.
         *   - GM's should ideally be able to review each step if necessary to ensure everything is working and/or tweak outcomes when necessary.
         *      - Should provide opportunity for the GMs to enter physical dice rolls for any dice roll in game that might allow it.
         * 12. Uploading to Player sheets.
         *   - Update the main tab with current status.
         *   - Copy current input into a separate tab that contains a history of all inputs/states
         *      - Potentially have a third tab for displaying just one turn/cycle at a time, based on a combo box/valideted cell.
         * 
         * Action systems are going to somewhat rely on the spreadsheet to appropriately sanitize inputs for processing. 
         *   - Parsing strings should be avoided by using validated fields.
         *   - Appropriate number of actions should be based on conditional formatting and cell validation, though can be enforced by code, with a text warning that the sheet was incorrectly filled.
         */
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
                // EP.ImportNewEP();

                EP.WriteToConsole();


                // Elevate each player. Currently returns the player ID of the elevated player, or -1 if no elevation.
                // Need to deal with elevating a PC to Master first.
                foreach (KKC.Fields field in Enum.GetValues(typeof(KKC.Fields)))
                {
                    EP.ElevateField(field);
                }

                EP.WriteToConsole();

                // TO DO
                // - Update Rank
                // - Update Player Record
                // - Update GM Data

                Debug.Log($"Uploading to Google Sheets...");
                EP.Upload();
                Debug.Log($"Upload Completed.");

            }
        }

        private void importBTN_Click(object sender, EventArgs e)
        {
            KKC.ImportAll();

        }

        private void turnBTN_Click(object sender, EventArgs e)
        {
            KKC.ProcessAll();
        }

        private void uploadBTN_Click(object sender, EventArgs e)
        {
            
        }
    }
}
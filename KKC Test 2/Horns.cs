using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKC_Test_2
{
    internal class Horns
    {
        public enum Outcome { Dropped, Mischief, RecklessUse, ConductUnbecoming, Expulsion }
        
        public static List<Player> playersOnHorns = new List<Player>();
        List<KKC.Fields> mastersNPCs;
        List<Player> masterPCs;

        // Initialise, then run DistributeDP, followed by OnTheHorns().
        public Horns()
        {
            // Setup NPC Masters
            mastersNPCs = new List<KKC.Fields>();
                mastersNPCs.Add(KKC.Fields.Linguistics);
                mastersNPCs.Add(KKC.Fields.Arithmetics);
                mastersNPCs.Add(KKC.Fields.RhetoricAndLogic);
                mastersNPCs.Add(KKC.Fields.Archives);
                mastersNPCs.Add(KKC.Fields.Sympathy);
                mastersNPCs.Add(KKC.Fields.Physicking);
                mastersNPCs.Add(KKC.Fields.Alchemy);
                mastersNPCs.Add(KKC.Fields.Artificery);
                mastersNPCs.Add(KKC.Fields.Naming);

            masterPCs = new List<Player>();
            foreach (Player p in KKC.playerList)
            {
                // Setup Complaints
                p.LodgeComplaints();

                // Setup PC masters
                if (p.Elevation == Player.Rank.Master)
                {
                    masterPCs.Add(p);
                    mastersNPCs.Remove(p.MasterOf);
                }
            }

            // Cull duplicate entries in playersOnHorns
            playersOnHorns = playersOnHorns.Distinct().ToList();

            // Remove PC masters from list of DP
            foreach (Player p in masterPCs)
            {
                playersOnHorns.Remove(p);
                // Don't know if masters should still get the tuition inflation from the complaint though. Check where tuition is actually calculated.
            }


        }

        // _players should contain one instance of every player that received any complaints.
        // Player objects will store the total amount of DP given (in Player.DP),
        // and a List<Player> of all complaints against them.
        public void DistributeDP()
        {
            // DP from NPC masters
            Random rand = new Random();
            foreach (var master in mastersNPCs)
            {
                AddDP(master, playersOnHorns[rand.Next(0, playersOnHorns.Count)]);
                AddDP(master, playersOnHorns[rand.Next(0, playersOnHorns.Count)]);
                AddDP(master, playersOnHorns[rand.Next(0, playersOnHorns.Count)]);
                AddDP(master, playersOnHorns[rand.Next(0, playersOnHorns.Count)]);
                AddDP(master, playersOnHorns[rand.Next(0, playersOnHorns.Count)]);
            }
            // DP from PC masters
            foreach (Player master in masterPCs)
            {
                foreach (Player player in master.Discipline)
                {
                    AddDP(player.MasterOf, player);
                }
            }
            // DP from complaints
            foreach (Player player in playersOnHorns)
            {
                AddDP(player);
            }
        }

        public void AddDP(KKC.Fields master, Player player)
        {
            // If EP is available for the particular field, EP offsets the DP, otherwise increase amount of DP.
            if(player.EP[(int)master] > 0)
            {
                player.EP[(int)master] -= 1;
            }
            else
            {
                player.DP += 1;
            }
        }

        public void AddDP(Player player)
        {
            // Add 1 DP for every 2 complaints (rounded down).
            player.DP += player.ComplaintsReceived.Count / 2;
        }

        // Determines the outcome of all players with DP.
        public void OnTheHorns()
        {
            foreach (Player player in playersOnHorns)
            {
                if (player.DP >= 5)
                {
                    player.TuitionAdjustment.OnHorns = true;
                }
                Random rand = new Random();
                switch (player.DP)
                {
                    case < 5:
                        break;
                    case < 7:
                        switch (rand.Next(1, 101))
                        {
                            case <= 60:
                                // Charges Dropped
                                player.DPOutcome = Outcome.Dropped;
                                break;
                            case <= 90:
                                // Undignified Mischief (Apology)
                                player.DPOutcome = Outcome.Mischief;
                                break;
                            case <= 100:
                                // Reckless Use of Sympathy (Lashings)
                                player.DPOutcome = Outcome.RecklessUse;
                                break;
                            default:
                                // Error
                                break;
                        }
                        break;
                    case <= 10:
                        switch (rand.Next(1,101))
                        {
                            case <= 20:
                                // Charges Dropped
                                player.DPOutcome = Outcome.Dropped;
                                break;
                            case <= 50:
                                // Undignified Mischief (Apology)
                                player.DPOutcome = Outcome.Mischief;
                                break;
                            case <= 80:
                                // Reckless Use of Sympathy (Lashings)
                                player.DPOutcome = Outcome.RecklessUse;
                                break;
                            case <= 100:
                                // Conduct Unbecoming a Member of the Arcanum (Lashings)
                                player.DPOutcome = Outcome.ConductUnbecoming;
                                break;
                            default:
                                // Error
                                break;
                        }
                        break;
                    case <= 12:
                        switch (rand.Next(1, 101))
                        {
                            case <= 20:
                                // Undignified Mischief (Apology)
                                player.DPOutcome = Outcome.Mischief;
                                break;
                            case <= 40:
                                // Reckless Use of Sympathy (Lashings)
                                player.DPOutcome = Outcome.RecklessUse;
                                break;
                            case <= 90:
                                // Conduct Unbecoming a Member of the Arcanum (Lashings)
                                player.DPOutcome = Outcome.ConductUnbecoming;
                                break;
                            case <= 100:
                                // Conduct Unbecoming a Member of the Arcanum (Expulsion)
                                player.DPOutcome = Outcome.Expulsion;
                                break;
                            default:
                                // Error
                                break;
                        }
                        break;
                    case <= 14:
                        switch (rand.Next(1, 101))
                        {
                            case <= 10:
                                // Undignified Mischief (Apology)
                                player.DPOutcome = Outcome.Mischief;
                                break;
                            case <= 30:
                                // Reckless Use of Sympathy (Lashings)
                                player.DPOutcome = Outcome.RecklessUse;
                                break;
                            case <= 70:
                                // Conduct Unbecoming a Member of the Arcanum (Lashings)
                                player.DPOutcome = Outcome.ConductUnbecoming;
                                break;
                            case <= 100:
                                // Conduct Unbecoming a Member of the Arcanum (Expulsion)
                                player.DPOutcome = Outcome.Expulsion;
                                break;
                            default:
                                // Error
                                break;
                        }
                        break;
                    case <= 16:
                        switch (rand.Next(1, 101))
                        {
                            case <= 5:
                                // Undignified Mischief (Apology)
                                player.DPOutcome = Outcome.Mischief;
                                break;
                            case <= 10:
                                // Reckless Use of Sympathy (Lashings)
                                player.DPOutcome = Outcome.RecklessUse;
                                break;
                            case <= 35:
                                // Conduct Unbecoming a Member of the Arcanum (Lashings)
                                player.DPOutcome = Outcome.ConductUnbecoming;
                                break;
                            case <= 100:
                                // Conduct Unbecoming a Member of the Arcanum (Expulsion)
                                player.DPOutcome = Outcome.Expulsion;
                                break;
                            default:
                                // Error
                                break;
                        }
                        break;
                    case <= 18:
                        switch (rand.Next(1, 101))
                        {
                            case <= 20:
                                // Conduct Unbecoming a Member of the Arcanum (Lashings)
                                player.DPOutcome = Outcome.ConductUnbecoming;
                                break;
                            case <= 100:
                                // Conduct Unbecoming a Member of the Arcanum (Expulsion)
                                player.DPOutcome = Outcome.Expulsion;
                                break;
                            default:
                                // Error
                                break;
                        }
                        break;
                    case 19:
                        switch (rand.Next(1, 101))
                        {
                            case <= 10:
                                // Conduct Unbecoming a Member of the Arcanum (Lashings)
                                player.DPOutcome = Outcome.ConductUnbecoming;
                                break;
                            case <= 100:
                                // Conduct Unbecoming a Member of the Arcanum (Expulsion)
                                player.DPOutcome = Outcome.Expulsion;
                                break;
                            default:
                                // Error
                                break;
                        }
                        break;
                    case > 20:
                        // Conduct Unbecoming a Member of the Arcanum (Expulsion)
                        player.DPOutcome = Outcome.Expulsion;
                        break;
                    default:
                        break;
                }    
            }
        }
    }
}

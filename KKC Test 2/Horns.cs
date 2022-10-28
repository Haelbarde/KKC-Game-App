using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKC_Test_2
{
    internal class Horns
    {
        /// <summary>
        /// Enum for possible outcomes of the Horns.
        /// Dropped - Charges are dropped, no further effect beyond normal tuition reductions.
        /// Mischief - Undignified Mischief. Punishment is a Formal Apology: Player must apologize in thread. Failure to do so results in 2 DP the next turn and a further 2 talent tuition increase.
        /// RecklessUse - Reckless Use of Sympathy. Punichment is a Public Lashing: Cannot take actions for 1 turn, unless they have nahlrout. 
        /// ConductUnbecoming - Conduct Unbecoming a Member of Arcanum. Punishment is a Public Lashing: Cannot take actions for 3 turns, unless they have nahlrout.
        /// Expulsion - Conduct Unbecoming a Member of Arcanum. Punishment is Expulsion.
        /// </summary>
        public enum Outcome { Dropped, Mischief, RecklessUse, ConductUnbecoming, Expulsion }
        
        public static List<Player> playersOnHorns = new();
        static List<KKC.Fields> mastersNPCs = new();
        static List<Player> masterPCs = new();

        /// <summary>
        /// Initialise, then run DistributeDP, followed by OnTheHorns().
        /// </summary>
        public Horns()
        {
            

        }

        public static void Import()
        {
            // Setup NPC Masters
            foreach (KKC.Fields field in Enum.GetValues(typeof(KKC.Fields)))
            {
                mastersNPCs.Add(field);
            }
            
            // Setup PC Masters
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

            // Cull duplicate entries from playersOnHorns
            playersOnHorns = playersOnHorns.Distinct().ToList();

            // Remove PC masters from list of DP
            foreach (Player p in masterPCs)
            {
                playersOnHorns.Remove(p);
                // Don't know if masters should still get the tuition inflation from the complaint though. Check where tuition is actually calculated.
            }
        }

        public static void Process()
        {
            DistributeDP();
            OnTheHorns();
        }

        /// <summary>
        /// Distributes DP to players, taking into account randomised DP from NPC masters, designated DP from PC masters, and DP from complaints.
        /// </summary>
        static void DistributeDP()
        {
            // DP from NPC masters
            foreach (var master in mastersNPCs)
            {
                AddDP(master, playersOnHorns[Dice.Roll(playersOnHorns.Count)-1]);
                AddDP(master, playersOnHorns[Dice.Roll(playersOnHorns.Count)-1]);
                AddDP(master, playersOnHorns[Dice.Roll(playersOnHorns.Count)-1]);
                AddDP(master, playersOnHorns[Dice.Roll(playersOnHorns.Count)-1]);
                AddDP(master, playersOnHorns[Dice.Roll(playersOnHorns.Count)-1]);
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

        /// <summary>
        /// Assigns DP from a master, taking into account any corrospending EP to offset the gained DP.
        /// </summary>
        /// <param name="master">The field of the master assigning DP.</param>
        /// <param name="player">The player DP is being assigned to.</param>
        static void AddDP(KKC.Fields master, Player player)
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

        /// <summary>
        /// Converts complaints on a target player into the required number of DP (1 DP for every two complaints).
        /// </summary>
        /// <param name="player">Player to add a DP to.</param>
        static void AddDP(Player player)
        {
            // Add 1 DP for every 2 complaints (rounded down).
            player.DP += player.ComplaintsReceived.Count / 2;
        }

        /// <summary>
        /// Determines the outcome of all players with DP.
        /// </summary>        
        static void OnTheHorns()
        {
            foreach (Player player in playersOnHorns)
            {
                if (player.DP >= 5)
                {
                    player.TuitionAdjustment.OnHorns = true;
                }
                switch (player.DP)
                {
                    case < 5:
                        break;
                    case < 7:
                        switch (Dice.Roll(100))
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
                        switch (Dice.Roll(100))
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
                        switch (Dice.Roll(100))
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
                        switch (Dice.Roll(100))
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
                        switch (Dice.Roll(100))
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
                        switch (Dice.Roll(100))
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
                        switch (Dice.Roll(100))
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

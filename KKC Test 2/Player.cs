using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKC_Test_2
{
    internal class Player
    {
        public enum Rank { Student, Elir, Relar, Elthe, Master }
        public enum Lodge { Streets, Underthing, Crockery, Mews, Ankers, WindyTower, GreyMan, GoldenPony, HorseAndFour }
        public enum Background { Vint, Aturan, Yll, Ceald, EdemaRuh}

        static int Count = 0;
        public bool Sane;

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A string containing the ID of the personal Google Sheet for the player.
        /// </summary>
        public string Sheet { get; }

        /// <summary>
        /// The ID number corresponding to the order on the player list.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// The players rank in the university: Student -> E'lir -> Re'lar -> El'the -> Master
        /// </summary>
        public Rank Elevation { get; set; }

        /// <summary>
        /// When a player reaches the rank of Master, stores the field they are currently master of.
        /// </summary>
        public KKC.Fields MasterOf { get; set; }

        /// <summary>
        /// Records the current total of EP (Elevation Points) held by the player as an array, where the index corresponds to the position in the Fields Enum in KKC.
        /// </summary>
        public int[] EP { get; set; }

        /// <summary>
        /// The current IP (Insanity Point) modifier of the player, as determined by the Mews or number of names known.
        /// NOTE: May be changed out for a different system at a later date.
        /// </summary>
        public int IP { get; set; }

        /// <summary>
        /// The current lodging of the player.
        /// </summary>
        public Lodge Lodging;

        /// <summary>
        /// The social class of the player, to determine player stipends and/or other effects that reference social class.
        /// </summary>
        public Background origin;

        /// <summary>
        /// The total coin held by a player, in talents. A drab is 0.01 talents, and a jot is 0.1 talents.
        /// </summary>
        public double Coin;

        /// <summary>
        /// The EP submitted by a player for the turn. This varies based on Rank: up to 5 for a student, and then one less for each subsequent rank. Masters do not submit EP.
        /// </summary>
        public List<KKC.Fields> EPFiled { get; set; } = new List<KKC.Fields>();

        /// <summary>
        /// The complaints filed by the player. A max of two by default but can be up to 4 if a student has Rhetoric & Logic: Proficient in Hyperbole.
        /// </summary>
        public List<Player> ComplaintsToFile;

        /// <summary>
        /// The complaints filed against the current player. This is populated by the Horns() constructor calling Player.LodgeComplaint().
        /// </summary>
        public List<Player> ComplaintsReceived;

        /// <summary>
        /// Stores the result of being taken on the Horns.
        /// </summary>
        public Horns.Outcome DPOutcome;

        /// <summary>
        /// An object that stores all the flags related to tuition adjuments, both inflations and reductions.
        /// </summary>
        public TuitionAdjustments TuitionAdjustment;

        /// <summary>
        /// Stores the DP assigned to the player by Masters and complaints. Used by Horns.
        /// </summary>
        public int DP { get; set; }

        /// <summary>
        /// If the player is a Master, stores the up to 5 DP they can assign.
        /// </summary>
        public List<Player> Discipline { get; set; }

        public Player(string name, string sheet)
        {
            ID = Count;
            Count++;
            Name = name;
            Sheet = sheet;
            EP = new int[9];
            DP = 0;
            Discipline = new List<Player>();
            ComplaintsReceived = new List<Player>();
            ComplaintsToFile = new List<Player>();
            TuitionAdjustment = new TuitionAdjustments();
            

            // Import these values
            origin = Background.Vint;
            Lodging = Lodge.Mews;
            Sane = true;
            IP = 0;
            Coin = 0;
            Elevation = Rank.Student;
            if (Elevation == Rank.Master)
            {
                // Discipline.Add() x5
            }
            // ComplaintsToFile.Add() x2


        }

        /// <summary>
        /// Elevates a player.
        /// Note: When elevating El'the to Master, check if any other fields would elevate to Mas
        /// </summary>
        /// <param name="field">The field to elevate from.</param>
        public void Elevate(KKC.Fields field)
        {
            if (Elevation != Rank.Elthe || Elevation != Rank.Master)
            {
                EP[(int)field] = Math.Max(EP[(int)field] - 5, 0);
            } 
            else if (Elevation == Rank.Elthe)
            {
                EP[(int)field] -= 15;
            }
            switch (Elevation)
            {
                case Rank.Student:
                    Elevation = Rank.Elir;
                    break;
                case Rank.Elir:
                    Elevation = Rank.Relar;
                    break;
                case Rank.Relar:
                    Elevation = Rank.Elthe;
                    break;
                case Rank.Elthe:
                    Elevation = Rank.Master;
                    MasterOf = field;
                    break;
                default:
                    Console.WriteLine("No Elevation");
                    break;
            }
        }


        /// <summary>
        /// Processes all player complaints
        /// </summary>
        public void LodgeComplaints()
        {
            foreach (Player player in ComplaintsToFile)
            {
                // Files complaint against target. Refer to Horns().
                player.ComplaintsReceived.Add(this);

                // Flags tuition inflation against target for being complained against
                player.TuitionAdjustment.ComplainedAgainst += 1;

                // Flags tuition reduction for filing a complaint
                TuitionAdjustment.FiledComplaint = true;

                // Adds to list of players to be considered for DP
                Horns.playersOnHorns.Add(player);
            }

        }

        public class TuitionAdjustments
        {

            // Purchased an item from Apocathery
            // bool placedContract 
            public bool Posted, FiledEP, FiledComplaint, SentPM, SoldItem, AttemptPipes, OnHorns;
            public int QualityPost, QualityRP, FullfilledContracts, ComplainedAgainst;
            
            public TuitionAdjustments()
            {
                // Reductions
                Posted = false;
                QualityPost = 0;
                QualityRP = 0;
                FiledEP = false;
                FiledComplaint = false;
                SentPM = false;
                SoldItem = false;
                // Purchased an item from Apocathery
                // bool placedContract 
                AttemptPipes = false;
                FullfilledContracts = 0;

                // Inflations
                OnHorns = false;
                ComplainedAgainst = 0;

            }
        }


    }
}

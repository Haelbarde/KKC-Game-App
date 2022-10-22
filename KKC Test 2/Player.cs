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

        public string Name { get; }
        public string Sheet { get; }
        public int ID { get; }
        public Rank Elevation { get; set; }
        public KKC.Fields MasterOf { get; set; }
        public int[] EP { get; set; }
        public int IP { get; set; }
        public Lodge Lodging;
        public Background origin;
        public double Coin;

        public List<KKC.Fields> EPFiled { get; set; } = new List<KKC.Fields>();

        public List<Player> ComplaintsToFile;
        public List<Player> ComplaintsReceived;
        public Horns.Outcome DPOutcome;
        public TuitionAdjustments TuitionAdjustment;
        public int DP { get; set; }

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

        // Should never be called on a Master, and only called on Elthe if they have 15 EP, and a check has been done for any other fields also trying to elevate to Master.
        public void Elevate(KKC.Fields field)
        {
            if (Elevation != Rank.Elthe || Elevation != Rank.Master)
            {
                EP[(int)field] = Math.Max(EP[(int)field] - 5, 0);
            } else if (Elevation == Rank.Elthe)
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

using System.IO;
using System;
using System.Threading.Tasks;
using System.Text;

namespace Palkanlaskenta_projekti
{
    class MainClass
    {
              

        public static void Main (string[] args)
        {
            int syote = 10;
            while (syote != 0)
            {
                Console.WriteLine("Valitse toiminta:");
                Console.WriteLine("[0] Pois");
                Console.WriteLine("[1] Lisätä uusi henkilö");
                syote = Int32.Parse(Console.ReadLine());

                if (syote == 1)
                {
                    Console.WriteLine("Sukunimi:");
                    string Sukunimi = Console.ReadLine();
                    Console.WriteLine("Etunimi:");
                    string Etunimi = Console.ReadLine();
                    Console.WriteLine("Henkilötunnus:");
                    string Henkilotunnus = Console.ReadLine();
                    Console.WriteLine("Bruttopalkka:");
                    double Bruttopalkka = Double.Parse(Console.ReadLine());

                    string rivi = Sukunimi + ";" + Etunimi + ";" + Henkilotunnus + ";" + Bruttopalkka;

                    // Tie tiedostoon henkilokunta.csv, joka sijaitsee projektin kansiossa
                    string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));
                    
                    // Lisää rivin tiedostoon
                    using (StreamWriter sw = File.AppendText(tie))
                    {
                        sw.WriteLine(rivi);
                    }
                    Console.WriteLine("Tiedot lisätty tiedostoon: " + tie);
                }
            }

            
            



        }

        






    }
    














}




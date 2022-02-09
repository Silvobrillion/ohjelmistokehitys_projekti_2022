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
                Console.WriteLine("[2] Poista henkilö");
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

                    // Varmistaa halutaanko tiedot lisätä tiedostoon
                    Console.WriteLine("Haluatko lisätä seuraavat tiedot tiedostoon:" + rivi);
                    Console.WriteLine("[K] Kyllä");
                    Console.WriteLine("[E] Ei");
                    var teksti = Console.ReadLine();

                    if (teksti == "K")
                    {
                        // Lisää rivin tiedostoon
                        using (StreamWriter sw = File.AppendText(tie))
                        {
                            sw.WriteLine(rivi);
                        }
                        Console.WriteLine("Tiedot lisätty tiedostoon: " + tie);
                    }
                    else if (teksti == "E")
                    {
                        Console.WriteLine("Tietoja ei tallennettu.");
                    }
                }
                if (syote == 2)
                {

                    int vastaus = 10;
                    while (vastaus != 0)

                        Console.WriteLine("Anna poistettavan henkilön henkilötunnus:");
                    string Henkilotunnus = Console.ReadLine();
                    Console.WriteLine("Haluatko varmasti poistaa henkilön: " + Henkilotunnus);
                    Console.WriteLine("[1] Kyllä");
                    Console.WriteLine("[2] Ei");
                    vastaus = Int32.Parse(Console.ReadLine());

                    if (vastaus == 1)
                    {
                        string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));
                        using (StreamWriter sw = File.AppendText(tie))
                        {

                        }
                    }
                    else
                    {
                        Console.WriteLine("Poisto peruutettu");
                    }
                }

            
            



        }

        






    }
    














}




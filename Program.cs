using System.IO;
using System;
using System.Threading.Tasks;
using System.Text;

namespace Palkanlaskenta_projekti
{
    class MainClass
    {


        public static void Main(string[] args)
        {
            int syote = 10;
            while (syote != 0)
            {
                Console.WriteLine("Valitse toiminta:");
                Console.WriteLine("[0] Pois");
                Console.WriteLine("[1] Lisätä uusi henkilö");
                Console.WriteLine("[2] Poista henkilö");
                Console.WriteLine("[3] Tulosta työntekijöiden tiedot");
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
                        Console.WriteLine("");
                    }
                    else if (teksti == "E")
                    {
                        Console.WriteLine("Tietoja ei tallennettu.");
                        Console.WriteLine("");
                    }
                }
                if (syote == 2)
                {

                    Console.WriteLine("Anna poistettavan henkilön henkilötunnus:");
                    string Henkilotunnus = Console.ReadLine();
                    Console.WriteLine("Haluatko varmasti poistaa henkilön: " + Henkilotunnus);
                    Console.WriteLine("[K] Kyllä");
                    Console.WriteLine("[E] Ei");
                    var teksti = Console.ReadLine();

                    if (teksti == "K")
                    {
                        string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));
                        string[] Lines = File.ReadAllLines(tie);
                        File.Delete(tie);// Deleting the file
                        using (StreamWriter sw = File.AppendText(tie))

                        {
                            foreach (string line in Lines)
                            {
                                if (line.IndexOf(Henkilotunnus) >= 0)
                                {
                                    //Skip the line
                                    continue;
                                }
                                else
                                {
                                    sw.WriteLine(line);
                                }
                            }
                            Console.WriteLine("Henkilön tiedot poistettu");
                            Console.WriteLine("");
                        }


                    }
                    else if (teksti == "E")
                    {
                        Console.WriteLine("Poisto peruutettu");
                        Console.WriteLine("");
                    }
                }
                if (syote == 3)
                {
                    string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));
                    string[] Lines = File.ReadAllLines(tie);
                    Console.WriteLine(String.Join("\n", Lines));
                    Console.WriteLine("");
                }

            }

        }

    }

}




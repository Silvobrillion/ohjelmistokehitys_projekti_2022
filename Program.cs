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
            string syote = "99";
            while (syote != "0")
            {
                Console.WriteLine("Valitse toiminta:");
                Console.WriteLine("[0] Pois");
                Console.WriteLine("[1] Lisää uusi henkilö");
                Console.WriteLine("[2] Poista henkilö");
                Console.WriteLine("[3] Tulosta työntekijöiden tiedot");
                syote = Console.ReadLine();
                Console.WriteLine("");

                if (syote == "0")
                {
                    break;
                }
                if (syote == "1")
                {
                    Console.WriteLine("Sukunimi:");
                    string Sukunimi = Console.ReadLine();
                    Console.WriteLine("Etunimi:");
                    string Etunimi = Console.ReadLine();
                    Console.WriteLine("Henkilötunnus:");
                    string Henkilotunnus = Console.ReadLine();

                    double Bruttopalkka;
                    string input;

                    do
                    {
                        Console.WriteLine("Bruttopalkka:");
                        input = Console.ReadLine();
                        if (!Double.TryParse(input, out Bruttopalkka))
                        {
                            Console.WriteLine("Virheellinen syöte. Kokeile uudelleen.");
                        }
                    }

                    while (Bruttopalkka <= 0);
                    Console.WriteLine("");

                    string rivi = Sukunimi + ";" + Etunimi + ";" + Henkilotunnus + ";" + Bruttopalkka;

                    // Tie tiedostoon henkilokunta.csv, joka sijaitsee projektin kansiossa
                    string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));

                    var teksti = "123";

                    // Varmistaa halutaanko tiedot lisätä tiedostoon

                    while (teksti != "K" || teksti != "E")
                    {
                        Console.WriteLine("Haluatko lisätä seuraavat tiedot tiedostoon:" + rivi);
                        Console.WriteLine("[K] Kyllä");
                        Console.WriteLine("[E] Ei");
                        teksti = Console.ReadLine().ToUpper();

                        if (teksti == "K")
                        {
                            // Lisää rivin tiedostoon
                            using (StreamWriter sw = File.AppendText(tie))
                            {
                                sw.WriteLine(rivi);
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Tiedot lisätty tiedostoon: " + tie);
                            Console.WriteLine("");
                            break;
                        }
                        else if (teksti == "E")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Tietoja ei tallennettu.");
                            Console.WriteLine("");
                            break;
                        }
                    }

                }
                if (syote == "2")
                {
                    Console.WriteLine("Anna poistettavan henkilön henkilötunnus:");
                    string Henkilotunnus = Console.ReadLine();

                    string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));
                    string content = File.ReadAllText(tie);

                    if (content.IndexOf(Henkilotunnus) > -1) // Tarkastaa löytyykö henkilötunnus tiedostosta
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Haluatko varmasti poistaa henkilön: " + Henkilotunnus, "\n");
                        Console.WriteLine("[K] Kyllä");
                        Console.WriteLine("[E] Ei");
                        var teksti = Console.ReadLine();

                        if (teksti == "K")
                        {

                            string[] Lines = File.ReadAllLines(tie);
                            File.Delete(tie);// Poistaa tiedoston
                            using (StreamWriter sw = File.AppendText(tie))

                            {
                                foreach (string line in Lines)
                                {
                                    if (line.IndexOf(Henkilotunnus) >= 0)
                                    {
                                        //Skippaa rivin 
                                        continue;
                                    }
                                    else
                                    {
                                        sw.WriteLine(line);
                                    }
                                }
                                Console.WriteLine("");
                                Console.WriteLine("Henkilön tiedot poistettu");
                                Console.WriteLine("");
                            }

                        }
                        else if (teksti == "E")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Poisto peruutettu");
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Henkilöä ei löydy");
                        Console.WriteLine("");
                    }
                }


                if (syote == "3")
                {
                    string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));
                    string[] Lines = File.ReadAllLines(tie);
                    Console.WriteLine(String.Join("\n", Lines));
                    Console.WriteLine("");
                }
                else
                {
                    syote = "99";
                }

            }

        }

    }

}




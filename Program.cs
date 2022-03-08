﻿using System.IO;
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
                Console.WriteLine("[3] Muokkaa henkilön tietoja");
                Console.WriteLine("[4] Tulosta työntekijöiden tiedot");
                Console.WriteLine("[5] Palkan koostumus");
                
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


                    Console.WriteLine("Anna muokattavan henkilön henkilötunnus:");
                    string Henkilotunnus = Console.ReadLine();
                    Console.WriteLine("");

                    StringBuilder uusihenkilokunta = new StringBuilder();

                    string[] file = File.ReadAllLines(tie);

                    foreach (string line in file)

                    {

                        if (line.Contains(Henkilotunnus) && Henkilotunnus.Length == 11)

                        {
                            Console.WriteLine("Muokattava henkilö:");
                            Console.WriteLine(line);
                            Console.WriteLine("");

                            Console.WriteLine("Anna muokattava tieto:");
                            string muokattava = Console.ReadLine();
                            Console.WriteLine("");

                            if (line.Contains(muokattava))
                            {
                                Console.WriteLine("Anna korvaava tieto:");
                                string korvaava = Console.ReadLine();
                                Console.WriteLine("");

                                Henkilotunnus = line.Replace(muokattava, korvaava);

                                uusihenkilokunta.Append(Henkilotunnus + "\r\n");

                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Muokattavaa tietoa ei löytynyt.");
                                Console.WriteLine("");
                            }

                        }

                        uusihenkilokunta.Append(line + "\r\n");

                    }

                    File.WriteAllText(tie, uusihenkilokunta.ToString());

                }

                if (syote == "4")
                {
                    string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));
                    string[] Lines = File.ReadAllLines(tie);
                    List<Henkilokunta> henkilolista = new List<Henkilokunta>();

                    foreach (string line in Lines)
                    {
                        string[] pilkottuRivi = line.Split(';');
                        Henkilokunta henkilokunta = new Henkilokunta();
                        henkilokunta.Sukunimi = pilkottuRivi[0];
                        henkilokunta.Etunimi = pilkottuRivi[1];
                        henkilokunta.Henkilotunnus = pilkottuRivi[2];
                        henkilokunta.Bruttopalkka = pilkottuRivi[3];

                        henkilolista.Add(henkilokunta);
                    }

                    Console.WriteLine("Sukunimi\tEtunimi\t\tHenkilötunnus\t\tBruttopalkka");
                    for (int i = 0; i < henkilolista.Count(); i++)
                    {
                        Henkilokunta listahlokunnasta = henkilolista[i];

                        string tuloste = $"{listahlokunnasta.Sukunimi}\t\t{listahlokunnasta.Etunimi}\t\t{listahlokunnasta.Henkilotunnus}\t\t{listahlokunnasta.Bruttopalkka}";

                        Console.WriteLine(tuloste);
                    }
                    Console.WriteLine("");
                }

                if (syote == "5") // Netto/brutto-laskuri https://www.palkka.fi/palkkalaskuri/nettolaskuri.htm
                {
                    double bruttopalkka;
                    string input;
                    do
                    {
                        Console.WriteLine("Anna bruttopalkka:");
                        input = Console.ReadLine();
                        if (!Double.TryParse(input, out bruttopalkka))
                        {
                            Console.WriteLine("Virheellinen syöte. Kokeile uudelleen.");
                        }
                    }
                    while (bruttopalkka <= 0);

                    int ika;
                    string input2;
                    do
                    {
                        Console.WriteLine("Anna ikä:");
                        input2 = Console.ReadLine();
                        if (!Int32.TryParse(input2, out ika))
                        {
                            Console.WriteLine("Virheellinen syöte. Kokeile uudelleen.");
                        }
                    }
                    while (ika <= 0);
                                        
                    double veroprosentti;
                    string input3;
                    do
                    {
                        Console.WriteLine("Anna ennakonpidätysprosentti (%):");
                        input3 = Console.ReadLine();
                        if (!Double.TryParse(input3, out veroprosentti))
                        {
                            Console.WriteLine("Virheellinen syöte. Kokeile uudelleen.");
                        }
                    }
                    while (veroprosentti < 0);
                    
                    double ennakkopidatys = veroprosentti / 100;
                    
                    double liitonmaksu;
                    string input4;
                    do
                    {
                        Console.WriteLine("Anna mahdollinen liiton jäsenmaksuprosentti (%) (jos maksua ei peritä palkasta, paina 0)");
                        input4 = Console.ReadLine();
                        if (!Double.TryParse(input4, out liitonmaksu))
                        {
                            Console.WriteLine("Virheellinen syöte. Kokeile uudelleen.");
                        }
                    }
                    while (liitonmaksu < 0);
                    
                    double liittoprosentti = liitonmaksu / 100;
                    double liitto = (bruttopalkka * liittoprosentti);
                    double elakevakuutusmaksu = 0;
                    double tyoelakevakuutusmaksu = 0;
                    double tyottomyysvakuutusmaksu = bruttopalkka * 0.015;
                    double sairasvakuutuspaivaraha = bruttopalkka * 0.0118;

                    if (ika < 53 && ika > 62)
                    {
                        elakevakuutusmaksu = bruttopalkka * 0.0865;
                        tyoelakevakuutusmaksu = bruttopalkka * 0.162;
                    }
                    else
                    {
                        elakevakuutusmaksu = bruttopalkka * 0.0715;
                        tyoelakevakuutusmaksu = bruttopalkka * 0.177;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Ovatko syöttämäsi tiedot oikein?");
                    Console.WriteLine("Bruttopalkka:" + bruttopalkka + " ikä:" + ika + " veroprosentti:" + veroprosentti);
                    Console.WriteLine("");
                    Console.WriteLine("[K] Kyllä");
                    Console.WriteLine("[E] Ei");
                    var vastaus = Console.ReadLine().ToUpper();
                    Console.WriteLine("");

                    if (vastaus == "K")
                    {
                        Console.WriteLine("Ennakkopidätyksen osuus palkasta on " + ennakkopidatys * bruttopalkka + " euroa.");
                        Console.WriteLine("Eläkevakuutusmaksun osuus palkasta on " + elakevakuutusmaksu + " euroa.");
                        Console.WriteLine("Työttömyysvakuutusmaksun osuus palkasta on " + tyottomyysvakuutusmaksu + " euroa.");
                        Console.WriteLine("Sairasvakuutuksen päivärahamaksun osuus palkasta on " + sairasvakuutuspaivaraha + " euroa." );
                        Console.WriteLine("Liiton jäsenmaksun osuus palkasta on " + liitto + " euroa");
                        Console.WriteLine("Nettopalkka on " + (bruttopalkka - (ennakkopidatys * bruttopalkka) - elakevakuutusmaksu - tyottomyysvakuutusmaksu - sairasvakuutuspaivaraha - liitto) + " euroa");
                        Console.WriteLine("");
                        Console.WriteLine("Työnantajan osuus maksuista:");
                        Console.WriteLine("Työeläkevakuutusmaksu " + tyoelakevakuutusmaksu + " euroa.");
                        Console.WriteLine("Sairausvakuutusmaksu " + bruttopalkka * 0.0134 + " euroa.");
                        Console.WriteLine("Työttömyysvakuutusmaksu " + bruttopalkka * 0.005 + " euroa.");
                        Console.WriteLine("");

                    }

                    else if (vastaus == "E")
                    {
                        Console.WriteLine("Ennakonpidätyksen ja muiden kulujen lasku peruutettu.");
                        Console.WriteLine("");
                    }

                    else
                    {
                        Console.WriteLine("Virheellinen syöte");
                        Console.WriteLine("");
                        
                    }

                }                

                else
                {
                    syote = "99";
                }

            }

        }

    }

    internal class Henkilokunta
    {
        public string Sukunimi { get; internal set; }
        public string Etunimi { get; internal set; }
        public string Henkilotunnus { get; internal set; }
        public string Bruttopalkka { get; internal set; }
    }
}




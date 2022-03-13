using System.IO;
using System;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;

namespace Palkanlaskenta_projekti
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            string syote = "99";

            // Tie(polku) tiedostoon henkilokunta.csv, joka sijaitsee projektin kansiossa
            string tie = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, Path.GetFileName("henkilokunta.csv"));
            if (!File.Exists(@tie))
            {
                File.Create(@tie);
                Console.WriteLine("-----");
                Console.WriteLine("HUOM! Aikaisempaa tiedostoa henkilokunta.csv ei löydetty. Uusi tiedosto luotu. Ole hyvä ja käynnistä ohjelma uudestaan!");
                Console.WriteLine("-----");
                syote = "0";
            }          
            
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
                    Regex regex = new Regex(@"^(0[1-9]|[12]\d|3[01])(0[1-9]|1[0-2])([5-9]\d\+|\d\d-|[01]\dA)\d{3}[\dABCDEFHJKLMNPRSTUVWXY]$");
                    string Henkilotunnus = Console.ReadLine();
                    while (!regex.IsMatch(Henkilotunnus))
                    {
                        Console.WriteLine("Virheellinen henkilötunnus, kokeile uudelleen.");
                        Henkilotunnus = Console.ReadLine();
                    }

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
                    Console.WriteLine("Anna poistettavan henkilön henkilötunnus kokonaan:");
                    string Henkilotunnus = Console.ReadLine();

                    string content = File.ReadAllText(tie);

                    if (content.IndexOf(Henkilotunnus) > -1 && Henkilotunnus.Length == 11) // Tarkastaa löytyykö henkilötunnus tiedostosta
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Haluatko varmasti poistaa henkilön: " + Henkilotunnus, "\n");
                        Console.WriteLine("[K] Kyllä");
                        Console.WriteLine("[E] Ei");
                        var teksti = Console.ReadLine().ToUpper();

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
                        else
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
                    Console.WriteLine("Anna muokattavan henkilön henkilötunnus kokonaan:");
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

                    Console.WriteLine(new string('-', 71));
                    Console.WriteLine(String.Format("|{0,-20}|{1,-20}|{2,-14}|{3,-12}|", "Sukunimi", "Etunimi", "Henkilötunnus", "Bruttopalkka"));
                    Console.WriteLine(new string('-', 71));

                    for (int i = 0; i < henkilolista.Count(); i++)
                    {           
                        Henkilokunta listahlokunnasta = henkilolista[i];
                        Console.WriteLine(String.Format("|{0,-20}|{1,-20}|{2,-14}|{3,-12}|", listahlokunnasta.Sukunimi, listahlokunnasta.Etunimi, listahlokunnasta.Henkilotunnus, listahlokunnasta.Bruttopalkka));                        
                    }
                    Console.WriteLine(new string('-', 71));
                    Console.WriteLine("");
                }

                if (syote == "5") 
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

                    Console.WriteLine("Anna ennakonpidätysprosentti (%):");
                    Regex reg = new Regex(@"^([0-9]|([1-9][0-9])|100)$");
                    string veroprosentti = Console.ReadLine();
                    while (!reg.IsMatch(veroprosentti))
                    {
                        Console.WriteLine("Virheellinen syöte, kokeile uudelleen.");
                        Console.WriteLine("Anna ennakonpidätysprosentti (%)");
                        veroprosentti = Console.ReadLine();
                    }
                    double veroprossa = double.Parse(veroprosentti);
                    double ennakkopidatys = Math.Round(veroprossa / 100, 2);

                    Console.WriteLine("Anna mahdollinen liiton jäsenmaksuprosentti (%) (jos maksua ei peritä palkasta, paina 0):");
                    Regex re = new Regex(@"^([0-9]|([1-9][0-9])|100)$");
                    string liittoprosentti = Console.ReadLine();
                    while (!re.IsMatch(liittoprosentti))
                    {
                        Console.WriteLine("Virheellinen syöte, kokeile uudelleen.");
                        Console.WriteLine("Anna mahdollinen liiton jäsenmaksuprosentti (%) (jos maksua ei peritä palkasta, paina 0)");
                        liittoprosentti = Console.ReadLine();
                    }
                    double liittoprossa = double.Parse(liittoprosentti);
                    double liittomaksu = liittoprossa / 100;

                    double liitto = Math.Round(bruttopalkka * liittomaksu, 2);
                    double elakevakuutusmaksu = 0;
                    double tyoelakevakuutusmaksu = 0;
                    double tyottomyysvakuutusmaksu = Math.Round(bruttopalkka * 0.015, 2);
                    double sairasvakuutuspaivaraha = Math.Round(bruttopalkka * 0.0118, 2);

                    if (ika < 53 && ika > 62)
                    {
                        elakevakuutusmaksu = Math.Round(bruttopalkka * 0.0865, 2);
                        tyoelakevakuutusmaksu = Math.Round(bruttopalkka * 0.162, 2);
                    }
                    else
                    {
                        elakevakuutusmaksu = Math.Round(bruttopalkka * 0.0715, 2);
                        tyoelakevakuutusmaksu = Math.Round(bruttopalkka * 0.177, 2);
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Ovatko syöttämäsi tiedot oikein?");
                    Console.WriteLine("Bruttopalkka:" + bruttopalkka + ", Ikä:" + ika + ", Veroprosentti:" + veroprosentti + ", Liiton jäsenmaksun prosentti:" + liittoprossa);
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
                        Console.WriteLine("Liiton jäsenmaksun osuus palkasta on " + liitto + " euroa.");
                        Console.WriteLine("Nettopalkka on " + Math.Round((bruttopalkka - (ennakkopidatys * bruttopalkka) - elakevakuutusmaksu - tyottomyysvakuutusmaksu - sairasvakuutuspaivaraha - liitto), 2) + " euroa.");
                        Console.WriteLine("");
                        Console.WriteLine("Työnantajan osuus maksuista:");
                        Console.WriteLine("Työeläkevakuutusmaksu " + tyoelakevakuutusmaksu + " euroa.");
                        Console.WriteLine("Sairausvakuutusmaksu " + Math.Round(bruttopalkka * 0.0134, 2) + " euroa.");
                        Console.WriteLine("Työttömyysvakuutusmaksu " + Math.Round(bruttopalkka * 0.005, 2) + " euroa.");
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

    
}




using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {            
            string nazwaPlikuWejsciowego = "data.csv";
            string nazwaPlikuWyjsciowego = "result.xml";
            string typPlikuWyjsciowego = "xml";

            if (args != null && args.Length == 3)
            {
                nazwaPlikuWejsciowego = args[0];
                nazwaPlikuWyjsciowego = args[1];
                typPlikuWyjsciowego = args[2];
            }

            try
            {
                File.Delete("log.txt");

                var sciezkaPlikuWejsciowego = Path.GetDirectoryName(nazwaPlikuWejsciowego);
                var sciezkaPlikuWyjsciowego = Path.GetDirectoryName(nazwaPlikuWyjsciowego);

                if (!string.IsNullOrEmpty(sciezkaPlikuWejsciowego) && !Directory.Exists(sciezkaPlikuWejsciowego))
                    throw new ArgumentException("Podana ścieżka pliku wejsciowego jest niepoprawna");

                if (!File.Exists(Path.GetFullPath(nazwaPlikuWejsciowego)))
                    throw new FileNotFoundException("Plik wejsciowy o podanej nazwie nie istnieje");

                if (!string.IsNullOrEmpty(sciezkaPlikuWyjsciowego) && !Directory.Exists(sciezkaPlikuWyjsciowego))
                    throw new ArgumentException("Podana ścieżka pliku wyjsciowego jest niepoprawna");

                if (typPlikuWyjsciowego.ToLower() != "xml" && typPlikuWyjsciowego.ToLower() != "json")
                    throw new ArgumentException("Niepoprawny typ danych pliku wyjsciowego");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                File.AppendAllText("log.txt", ex.Message + "\n");
                return;
            }            

            string[] kolumny = new string[9];
            Regex nazwiskoRegex = new Regex(@"[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]*");
            Regex mailRegex = new Regex(@"@\b[\w,\.]*");

            var list = new List<Student>();
            var liczbaStudentowStudiow = new Dictionary<string, int>();

            var fi = new FileInfo(nazwaPlikuWejsciowego);
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    try
                    {
                        kolumny = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                        if (kolumny.Length != 9)
                            throw new BrakKolumnException("Brak wymaganych 9 kolumn: " + line);

                        var nazwisko = nazwiskoRegex.Match(kolumny[1]).ToString();

                        if (list.Any(e => e.Imie.ToLower() == kolumny[0].ToLower() && e.Nazwisko.ToLower() == nazwisko.ToLower()))
                            throw new DuplikatException("Znaleziono duplikat: " + line);

                        var st = new Student
                        {
                            Imie = kolumny[0],
                            Nazwisko = nazwisko,
                            Index = "s" + kolumny[4],
                            Date = DateTime.Parse(kolumny[5]).ToString("dd.MM.yyyy"),
                            Email = nazwisko + mailRegex.Match(kolumny[6]).ToString(),
                            ImieMatki = kolumny[7],
                            ImieOjca = kolumny[8],

                            studia = new Studia
                            {
                                Kierunek = kolumny[2],
                                Tryb = kolumny[3]
                            }
                        };
                        if (!liczbaStudentowStudiow.ContainsKey(kolumny[2]))
                            liczbaStudentowStudiow.Add(kolumny[2], 1);
                        else
                            liczbaStudentowStudiow[kolumny[2]]++;

                        list.Add(st);
                    }
                    catch (BrakKolumnException ex)
                    {
                        File.AppendAllText("log.txt", ex.Message + "\n");                        
                    }
                    catch (DuplikatException ex)
                    {
                        File.AppendAllText("log.txt", ex.Message + "\n");
                    }
                }
            }

            var wynik = new Wynik()
            {
                CreatedAt = DateTime.Now.ToString("dd.MM.yyyy"),
                Author = "Paweł Gut",
                activieStudies = new List<ActivieStudies>(),
                studenci = list
            };

            foreach (var item in liczbaStudentowStudiow)
            {
                wynik.activieStudies.Add(new ActivieStudies() { Nazwa = item.Key, LiczbaStudentow = item.Value });
            }

            if (typPlikuWyjsciowego.ToLower() == "xml")
            {
                FileStream writer = new FileStream(nazwaPlikuWyjsciowego, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(Wynik),
                                           new XmlRootAttribute("uczelnia"));
                serializer.Serialize(writer, wynik);
            }
            else
            {
                string json = JsonSerializer.Serialize(wynik, typeof(Wynik));
                File.WriteAllText(nazwaPlikuWyjsciowego, json);
            }

            Console.WriteLine("Program zakonczyl dzialanie");
            Console.ReadLine();
        }
    }
}

#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/21 at 18:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: Program.cs
// * Edited on 2024/12/23 at 15:12:16
// --------------------------------

#endregion

#region

using CEX_Olimpiadi.Classes;
using CEX_Olimpiadi.DAO_Classes;

#endregion

Console.WriteLine("Hello, World!");
// Menu principale
Console.WriteLine("Benvenuto nel programma di gestione delle Olimpiadi!");
Console.WriteLine("Cosa vuoi fare?");
// Opzioni:
// Fate in modo che un utente possa scegliere un Atleta e vedere in ordine tutte le medaglie che ha vinto
// Fate si che, scelto dall'utente un evento, si possano vedere tutte le gare svolte
// Scelto un Atleta dall'utente, voglio vedere il numero di ori, argenti e bronzi vinti in totale nella sua carriera
// Scelta una nazione dall'utente, voglio vedere tutti gli atleti che hanno vinto medaglie
// Visualizzare il nominativo e la nazione dell'atleta più vecchio a vincere una medaglia d'oro (voglio vedere il più vecchio a vincere rispetto alla data di vittoria: se uno ha vinto l'oro a 20 anni e ora è il più vecchio non va bene!! Voglio l'80enne vincitore ad esempio!)
// Quante medaglie sono state vinte in sport di squadra e quanti atleti ne facevano parte
// Il nome della categoria più vinta nel medagliere
var menu = @"
1. Visualizza le medaglie di un atleta
2. Visualizza le gare di un evento
3. Visualizza il numero di medaglie vinte da un atleta
4. Visualizza gli atleti di una nazione che hanno vinto medaglie
5. Visualizza l'atleta più vecchio a vincere una medaglia d'oro
6. Visualizza il numero di medaglie vinte in sport di squadra e quanti atleti ne facevano parte
7. Visualizza la categoria più vinta nel medagliere
8. Esci
";

var isRunning = true;

while (isRunning)
{
    Console.WriteLine(menu);
    Console.Write("Scelta: ");
    var choice = int.Parse(Console.ReadLine() ?? "0");
    switch (choice)
    {
        case 1:
            // Visualizza le medaglie di un atleta scelto e a quale evento appartengono
            Choice1();
            break;
        case 2:
            //? Visualizza le gare di un evento
            Choice2();
            break;
        case 3:
            //Visualizza il numero di medaglie vinte da
            Choice3();
            break;
        case 4:
            //Visualizza gli atleti di una nazione che hanno vinto medaglie
            Choice4();
            break;
        case 5:
            //Visualizza l'atleta più vecchio a vincere una medaglia d'oro
            Choice5();
            break;
        case 6:
            //Visualizza il numero di medaglie vinte in sport di squadra e quanti atleti ne facevano parte
            Choice6();
            break;
        case 7:
            //Visualizza la categoria più vinta nel medagliere
            Choice7();
            break;
        case 8:
            isRunning = false;
            break;
    }
}

Console.WriteLine("Arrivederci!");

// Medodi per la gestione delle scelte dell'utente
static void Choice1()
{
    Console.WriteLine("Inserisci il nome dell'atleta: ");
    var name = Console.ReadLine()?.ToLower() ?? string.Empty;
    Console.WriteLine("Inserisci il cognome dell'atleta: ");
    var surname = Console.ReadLine()?.ToLower() ?? string.Empty;
    // Recupero l'atleta
    var atleta = (Athlete)DaoAthletes.GetInstance().GetAthleteByFullName(name, surname);
    if (atleta == null)
    {
        Console.WriteLine("Atleta non trovato\n Vuoi riprovare? (s/n)");
        if (Console.ReadLine()?.ToLower() == "s")
            Choice1();
    }

    // Visualizzo le medaglie
    var athleteId = DaoAthletes.GetInstance().GetAthleteIdByFullName(name, surname);
    var medaglie = DaoMedals.GetInstance().GetAthleteMedals(athleteId);
    Console.WriteLine($"Medaglie vinte da {atleta.Name} {atleta.Surname}:");
    foreach (var medaglia in medaglie)
    {
        var medal = medaglia;
        Console.WriteLine($"- {medal.MedalTier} {medal.Competition?.Type}, {medal.Event?.Name} {medal.Event?.Year}");
    }
}

static void Choice2()
{
    Console.WriteLine("Inserisci il nome dell'evento: ");
    var evento = Console.ReadLine()?.ToLower() ?? string.Empty;
    Console.WriteLine("Inserisci l'anno dell'evento: ");
    var anno = int.Parse(Console.ReadLine() ?? "0");
    // Recupero l'evento
    var eventoId = DaoEvents.GetInstance().GetEventIdByNameAndYear(evento, anno);
    var gare = DaoCompetitions.GetInstance().GetCompetitionsByEventId(eventoId);
    Console.WriteLine($"Gare svolte nell'evento {evento}");
    foreach (var gara in gare)
        Console.WriteLine(gara.ToString());
}

static void Choice3()
{
    Console.WriteLine("Inserisci il nome dell'atleta: ");
    var name = Console.ReadLine()?.ToLower() ?? string.Empty;
    Console.WriteLine("Inserisci il cognome dell'atleta: ");
    var surname = Console.ReadLine()?.ToLower() ?? string.Empty;
    // Recupero l'atleta
    var atleta = (Athlete)DaoAthletes.GetInstance().GetAthleteByFullName(name, surname);
    if (atleta == null)
    {
        Console.WriteLine("Atleta non trovato\n Vuoi riprovare? (s/n)");
        if (Console.ReadLine()?.ToLower() == "s")
            Choice1();
    }

    var athleteId = DaoAthletes.GetInstance().GetAthleteIdByFullName(name, surname);
    var medaglie = DaoMedals.GetInstance().GetAthleteMedals(athleteId);
    Console.WriteLine($"Medaglie vinte da {atleta.Name} {atleta.Surname}: ");
    var goldCount = 0;
    var silverCount = 0;
    var bronzeCount = 0;
    foreach (var medaglia in medaglie)
        switch (medaglia.MedalTier)
        {
            case "Oro":
                goldCount++;
                break;
            case "Argento":
                silverCount++;
                break;
            case "Bronzo":
                bronzeCount++;
                break;
        }

    if (goldCount == 0 && silverCount == 0 && bronzeCount == 0)
        Console.WriteLine("L'atleta non ha vinto medaglie");
    else
        Console.Write($"Ori: {goldCount}, Argenti: {silverCount}, Bronzi: {bronzeCount}");
}

static void Choice4()
{
    Console.WriteLine("Inserisci il nome della nazione: ");
    var country = Console.ReadLine()?.ToLower() ?? string.Empty;
    var atleti = DaoAthletes.GetInstance().GetAthletesByCountry(country);
    if (atleti.Count == 0)
    {
        Console.WriteLine("Nessun atleta trovato\n Vuoi riprovare? (s/n)");
        if (Console.ReadLine()?.ToLower() == "s")
            Choice4();
    }

    // Controllo se gli atleti hanno vinto medaglie
    atleti = atleti.FindAll(atleta => DaoMedals.GetInstance().GetAthleteMedals(atleta.Id).Count > 0);
    Console.WriteLine($"Atleti della nazione {country} che hanno vinto medaglie: ");
    foreach (var atleta in atleti)
    {
        Console.WriteLine(atleta.ToString());
        // Recupero le medaglie
        var medaglie = DaoMedals.GetInstance().GetAthleteMedals(atleta.Id);
        foreach (var medaglia in medaglie)
        {
            var medal = (Medal)medaglia;
            Console.WriteLine(
                $"- {medal.MedalTier} {medal.Competition?.Type}, {medal.Event?.Name} {medal.Event?.Year}");
        }
    }
}

static void Choice5()
{
    // Recupero l'atleta più vecchio a vincere una medaglia d'oro
    var atleta = DaoAthletes.GetInstance().GetOldestGoldWinner();
    Console.WriteLine($"L'atleta più vecchio a vincere una medaglia d'oro è {atleta.Name} {atleta.Surname}");
    // recupero la gara in cui ha vinto
    var medaglia = DaoMedals.GetInstance().GetAthleteMedals(atleta.Id).Find(medal => medal.MedalTier == "Oro");
    Console.WriteLine(
        $"Medaglia vinta in {medaglia?.Competition?.Type}, {medaglia?.Event?.Name} {medaglia?.Event?.Year}");
    // Recupero l'età
    var eta = DateTime.Now.Year - atleta.Dob.Year;
    Console.WriteLine($"All'età di {eta} anni");
}

static void Choice6()
{
    //Visualizza il numero di medaglie vinte in sport di squadra e quanti atleti ne facevano parte
    // esempio
    // Sport: Pallanuoto -> Giocatori: 10, Medaglie: 2;
    // Sport: Scherma a Squadre -> Giocatori: 2, Medaglie: 1)
    var medals = DaoMedals.GetInstance().GetMedals();
    var teamMedals = medals.FindAll(medal => medal.Competition?.IsTeamComp == true);
    var teamMedalsGrouped = teamMedals.GroupBy(medal => medal.Competition?.Type);
    foreach (var group in teamMedalsGrouped)
    {
        var sport = group.Key;
        var count = group.Count();
        var athletes = group.Select(medal => medal.Athlete).Distinct().Count();
        Console.WriteLine($"Sport: {sport} -> Giocatori: {athletes}, Medaglie: {count}");
    }
}

static void Choice7()
{
    //Visualizza lo sport più vinto nel medagliere
    var medals = DaoMedals.GetInstance().GetMedals();
    var sports = medals.GroupBy(medal => medal.Competition?.Type);
    var maxSport = sports.OrderByDescending(sport => sport.Count()).First();
    Console.WriteLine($"Lo sport più vinto è {maxSport.Key}");
    // Visualizzo le medaglie
    foreach (var medaglia in maxSport)
    {
        var medal = (Medal)medaglia;
        Console.WriteLine(
            $"- {medal.MedalTier} {medal.Competition?.Type}, {medal.Event?.Name} {medal.Event?.Year} ({medal.Event?.Location})");
        Console.WriteLine($"  Atleta: {medal.Athlete?.Name} {medal.Athlete?.Surname} ({medal.Athlete?.Country}), Classe: {medal.Athlete?.Dob:yyyy}");
    }
}

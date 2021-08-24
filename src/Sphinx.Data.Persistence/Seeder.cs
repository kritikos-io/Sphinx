namespace Kritikos.Sphinx.Data.Persistence
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli;
  using Kritikos.Sphinx.Web.Shared.Enums;

  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Logging;

  public static class Seeder
  {
    private static readonly List<(string Greek, string English, string Italian)> PreTest =
      new()
      {
        ("Αγρός", "Field", "Campo"),
        ("Αθανασία", "Immortality", "Eternita "),
        ("Άλογο", "Horse", "Cavallo"),
        ("Απορία", "Question", "Domanda"),
        ("Αστέρι", "Star", "Stella"),
        ("Αφρός", "Foam", "Spuma"),
        ("Βάρος", "Weight", "Peso"),
        ("Βεβαιότητα", "Certainty ", "Sicurezza"),
        ("Βιβλίο", "Book", "Libro"),
        ("Βλέμμα", "Look", "Sguardo"),
        ("Βόας", "Boa", "Boa "),
        ("Βράδυ", "Evening", "Sera"),
        ("Γαλαξίας", "Galaxy", "Galassio"),
        ("Γεράνι", "Geranium", "Geranio"),
        ("Γη", "Earth", "Terra"),
        ("Γιατρός", "Doctor", "Dottore "),
        ("Γνώμη", "Opinion", "Opinione"),
        ("Γρανάζι", "Gear", "Ingranaggio"),
        ("Δασοφύλακας", "Ranger", "Guardia forestale"),
        ("Δεντρολίβανο", "Rosemary", "Rosmarino"),
        ("Δηλητήριο", "Poison", "Veleno"),
        ("Διόπτρα", "Diopter", "Diottria"),
        ("Δουλειά", "Employment", "Lavoro"),
        ("Δρόμος", "Street", "Strada"),
        ("Έαρ", "Spring", "Primavera"),
        ("Εγγύτητα", "Proximity", "Vicinanza"),
        ("Εθελοντής", "Volunteer", "Volontario"),
        ("Εκκόλαψη ", "Incubation", "Cova"),
        ("Έξω", "Outside", "Fuori"),
        ("Ευωδιά ", "Fragrance", "Profumo"),
        ("Ζατρίκιο ", "Chess", "Scacchi"),
        ("Ζέφυρος", "Zephyr", "Zefiro"),
        ("Ζήτηση", "Demand", "Richiesta"),
        ("Ζιζάνιο", "Weed", "Malerba"),
        ("Ζόφος", "Gloom", "Oscurita"),
        ("Ζύθος", "Beer", "Birra"),
        ("Ήβη", "Puberty", "Puberta"),
        ("Ηδονή", "Pleasure", "Soddisfazione"),
        ("Ηθική", "Morality", "Moralita"),
        ("Ηλεκτρισμός", "Electricity", "Elettricita"),
        ("Ήττα", "Defeat ", "Sconfitta"),
        ("Ηφαίστειο", "Volcano", "Volcano "),
        ("Θάνατος", "Death", "Morte"),
        ("Θειάφι", "Sulphur", "Solfuro"),
        ("Θέσπιση", "Enactment", "Varo"),
        ("Θηλασμός", "Breast-feeding", "Allattamento al seno"),
        ("Θίασος", "Troupe", "Compagnia"),
        ("Θυμός", "Anger ", "Rabbia"),
        ("Ιαχή", "Battle cry", "Grido di battaglia"),
        ("Ιδίωμα", "Idiom", "Frase idiomatica"),
        ("Ίζημα ", "Sediment", "Sediment "),
        ("Ικρίωμα ", "Gallows ", "Forca"),
        ("Ισθμός", "Isthmus ", "Istmo"),
        ("Ίχνος", "Trace", "Traccia"),
        ("Κακοφωνία ", "Cacophony", "Cacofonia"),
        ("Κέρμα", "Coin", "Moneta"),
        ("Κηδεμονία", "Custody", "Custodia"),
        ("Κιβωτός", "Ark", "Arca"),
        ("Κοσμογονία", "Cosmogony ", "Cosmogonia"),
        ("Κωμικός", "Comedian", "Comico"),
        ("Λαχτάρα", "Avidity", "Bramosia"),
        ("Λαίλαπα", "Conflagration", "Conflagrazione"),
        ("Λεμόνι", "Lemon", "Limone"),
        ("Λίμνη", "Lake", "Lago"),
        ("Λουτήρας", "Wash-basin", "Lavandino"),
        ("Λύπη", "Sorrow", "Tristezza"),
        ("Μαινάδα", "Maenad", "Menade"),
        ("Μάρτυρας", "Martyr", "Martire"),
        ("Μειδίαμα", "Sneer", "Sogghigno"),
        ("Μέλι", "Honey", "Miele"),
        ("Μηλίτης", "Cider", "Sidro"),
        ("Μονόκερως ", "Unicorn", "Unicorno"),
        ("Ναρκαλιευτής", "Minesweeper", "Dragamine"),
        ("Νέμεσις ", "Nemesis", "Nemesi"),
        ("Νήμα ", "Clew", "Gomitolo"),
        ("Νίκη", "Victory", "Vittoria"),
        ("Νομοτέλεια", "Causality", "Causalita"),
        ("Νωθρότητα", "Dullness ", "Inerzia"),
        ("Ξάφνιασμα", "Startle ", "Cogliere di sorpresa"),
        ("Ξεσηκωμός", "Rebellion ", "Ribellione"),
        ("Ξιφομάχος", "Fencer", "Schermidore"),
        ("Ξόρκι", "Spell", "Incantesimo"),
        ("Ξυράφι", "Razor", "Rasoio"),
        ("Ξωτικό", "Elf", "Elfo"),
        ("Οβολός", "Alms ", "Elemosina"),
        ("Οδαλίσκη", "Odalisque", "Odalisca"),
        ("Ολότητα", "Entirety ", "Completezza"),
        ("Οξύμωρο", "Oxymoron ", "Ossimoro"),
        ("Οστό", "Bone ", "Osso"),
        ("Όχλος", "Mob", "Folla"),
        ("Παραπομπή", "Citation ", "Citazione"),
        ("Πειρατής", "Pirate ", "Pirata"),
        ("Πηγάδι", "Well ", "Sorgente"),
        ("Πιρόγα", "Dugout ", "Piroga"),
        ("Πλεούμενο", "Vessel ", "Nave"),
        ("Πρόσχημα", "Cop-out", "Pretesto"),
        ("Ράβδος", "Bar ", "Barra"),
        ("Ρέμα", "Stream", "Ruscello"),
        ("Ρητορική", "Rhetoric", "Retorica"),
        ("Ρόδα", "Wheel", "Ruota"),
        ("Ροή", "Flow", "Flusso"),
        ("Ρώμη ", "Muscularity", "Potenza"),
        ("Σάβανο ", "Cerement", "Drappo Funebre"),
        ("Σελήνη", "Moon", "Luna"),
        ("Σήψη", "Sepsis ", "Sepsi"),
        ("Σιμιγδάλι", "Semolina ", "Semola"),
        ("Σκότος", "Dark ", "Buio"),
        ("Σμήνος", "Flock", "Stormo"),
        ("Τάφος", "Grave", "Tomba"),
        ("Τεκμήριο", "Evidence", "Prova"),
        ("Τμήμα", "Branch ", "Branca"),
        ("Τοκογλύφος", "Loan shark ", "Usuraio"),
        ("Τραπέζι", "Table", "Tavolo"),
        ("Τυφώνας ", "Hurricane", "Uragano"),
        ("Υάκινθος", "Hyacinth", "Giacinto"),
        ("Ύβρις ", "Hubris ", "Alterigia"),
        ("Υλοποίηση", "Actualization", "Attuazione"),
        ("Υπεράνθρωπος", "Super-human", "Sovrumano"),
        ("Υφή", "Consistency", "Consistenza"),
        ("Ύψος", "Height", "Altezza"),
        ("Φάντασμα", "Apparition", "Fantasma"),
        ("Φέγγος", "Lambency", "Lucernario "),
        ("Φίλος", "Friend", "Amico"),
        ("Φόβος", "Fright ", "Paura"),
        ("Φρένο", "Brake", "Freno"),
        ("Φυτολόγιο", "Herbarium", "Erbario"),
        ("Χάος", "Chaos", "Caos"),
        ("Χειμώνας ", "Winter", "Inverno"),
        ("Χημεία", "Chemistry", "Chimica"),
        ("Χίμαιρα ", "Chimera", "Chimera"),
        ("Χοάνη", "Funnel", "Tramoggia"),
        ("Χώμα", "Soil", "Terreno"),
        ("Ψαλίδι", "Scissors", "Forbici"),
        ("Ψεγάδι ", "Blemish", "Imperfezione"),
        ("Ψέμα", "Fib", "Frottola"),
        ("Ψησταριά ", "Grill", "Griglia"),
        ("Ψίθυρος", "Whisper", "Orrechio"),
        ("Ψωμί", "Bread", "Pane"),
        ("Ωάριο", "Ovum ", "Ovulo"),
        ("Ωδείο ", "Conservatory ", "Conservatorio"),
        ("Ώθηση", "Boost", "Spinta"),
        ("Ωκεανός", "Ocean", "Oceano"),
        ("Ωραιοπάθεια", "Narcissism ", "Vanagloria"),
        ("Ωό", "Egg", "Uovo"),
      };

    public static async Task Seed(IServiceScopeFactory scopeFactory, ILogger<SphinxDbContext> logger)
    {
      using var scope = scopeFactory.CreateScope();
      var ctx = scope.ServiceProvider.GetRequiredService<SphinxDbContext>();
      var migrations = (await ctx.Database.GetPendingMigrationsAsync()).ToList();

      if (migrations.Any())
      {
        logger.LogWarning("Could not seed, database has pending migrations. {Migrations}", migrations);
        return;
      }

      if (!await ctx.DataSets.AnyAsync(x => x.Name == "Pre-test"))
      {
        logger.LogInformation("Seeding pre-test dataset...");
        AddPreTest(ctx);
      }

      await ctx.SaveChangesAsync();
    }

    private static void AddPreTest(SphinxDbContext ctx)
    {
      var dataset = new DataSet { Name = "Pre-test", };
      foreach (var (greek, english, italian) in PreTest)
      {
        var primary = new PrimarySignificantStimulus
        {
          DataSet = dataset, Content = greek, MediaType = StimulusMediaType.Text, Title = "Greek Words",
        };

        var secondaryEnglish = new SecondarySignificantStimulus
        {
          DataSet = dataset, Content = english, MediaType = StimulusMediaType.Text, Title = "English Words",
        };

        var secondaryItalian = new SecondarySignificantStimulus
        {
          DataSet = dataset, Content = italian, MediaType = StimulusMediaType.Text, Title = "Italian Words",
        };

        ctx.SignificantMatches.Add(new SignificantMatch { Primary = primary, Secondary = secondaryEnglish });
        ctx.SignificantMatches.Add(new SignificantMatch { Primary = primary, Secondary = secondaryItalian });
      }
    }
  }
}

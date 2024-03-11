using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public interface IPrinter
{
    string Print(Invoice invoice, Dictionary<string, Play> plays);

}
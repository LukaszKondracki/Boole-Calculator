using System.Text.Json;

var values = """
    true  | false | true  | false
    false | false | true  | true 
    true  | true  | false | false
    false | true  | false | true 
    true  | false | false | false
    true  | true  | true  | true 
    false | false | false | false
    """
    .Split('\n')
    .Select(x => x.Trim())
    .Select(x => x
        .Split('|')
        .Select(s => s.Trim())
        .Select(bool.Parse)
        .ToArray()
    );

var funcs = new Func<bool, bool, bool, bool, bool>[] {
    (a, b, c, d) => !a && (b || !d ) && c,
    (a, b, c, d) => !(a || b || !(c && d)),
    (a, b, c, d) => d && b || !(d || !!c)
};

var c = 'A';
foreach (var func in funcs)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($" {c++}");
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine("    # |   a   |   b   |   c   |   d   | solution ");
    Console.ResetColor();
    var v = 1;
    foreach (var val in values)
    {
        var solution = func(val[0], val[1], val[2], val[3]);

        Console.WriteLine($"    {v++} | {string.Join(" | ", val.Select(x => $"{x, -5}"))} | {solution,-5}".ToLower());
    }
}
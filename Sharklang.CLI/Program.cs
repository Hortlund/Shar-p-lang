using Sharklang.Core;

namespace Sharklang.CLI;

internal static class Program
{
    private const string fish = @"
                         /`·.¸
                        /¸...¸`:·
                     ¸.·´  ¸   `·.¸.·´)
                    : © ):´;      ¸  {
                     `·.¸ `·  ¸.·´\`·¸)
                         `\\´´\¸.·´

    ";
    private const string welcome = @"


   _____ _                 __      _ _  __    _                   _ 
  / ____| |               / /     | | | \ \  | |                 | |
 | (___ | |__   __ _ _ __| | _ __ | | | _| | | | __ _ _ __   __ _| |
  \___ \| '_ \ / _` | '__| || '_ \| | |/ / | | |/ _` | '_ \ / _` | |
  ____) | | | | (_| | |  | || |_) | |   <| | | | (_| | | | | (_| |_|
 |_____/|_| |_|\__,_|_|  | || .__/| |_|\_\ | |_|\__,_|_| |_|\__, (_)
                          \_\ |   | |   /_/                  __/ |  
                            |_|   |_|                       |___/   


    ";
    private static void Main(string[] args)
    {
        const string prompt = ">> ";
        Console.Write(welcome);
        Console.WriteLine(fish);

        while (true)
        {
            string? line;
            Console.Write(prompt);

            line = Console.ReadLine();

            if(line == null)
            {
                return;
            }

            var lexer = new Lexer(line);
            Token tok = lexer.NextToken();
            while (tok.tokenType != TokenType.EOF)
            {
                Console.WriteLine(tok);
                tok = lexer.NextToken();
            }
        }
    }
}

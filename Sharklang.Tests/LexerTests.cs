using Sharklang.Core;
using NUnit.Framework;


namespace Sharklang.Tests
{
    public class Tests
    {
        private string _input;
        private Lexer _lexer;
        private IList<Token> _tokens;

        [SetUp]
        public void Setup()
        {

            _input = @"

            l�t five vara 5;
            l�t ten vara 10;

            l�t add vara fn(x,y) {
                x + y;
            };

            l�t result vara add(five, ten);

            ";

            _lexer = new Lexer(_input);
            _tokens = new List<Token>
            {
                new(TokenType.LET, "l�t"),
                new(TokenType.IDENT, "five"),
                new(TokenType.ASSIGN, "vara"),
                new(TokenType.INT, "5"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.LET, "l�t"),
                new(TokenType.IDENT, "ten"),
                new(TokenType.ASSIGN, "vara"),
                new(TokenType.INT, "10"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.LET, "l�t"),
                new(TokenType.IDENT, "add"),
                new(TokenType.ASSIGN, "vara"),
                new(TokenType.FUNCTION, "fn"),
                new(TokenType.LPAREN, "("),
                new(TokenType.IDENT, "x"),
                new(TokenType.COMMA, ","),
                new(TokenType.IDENT, "y"),
                new(TokenType.RPAREN, ")"),
                new(TokenType.LBRACE, "{"),
                new(TokenType.IDENT, "x"),
                new(TokenType.PLUS, "+"),
                new(TokenType.IDENT, "y"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.RBRACE, "}"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.LET, "l�t"),
                new(TokenType.IDENT, "result"),
                new(TokenType.ASSIGN, "vara"),
                new(TokenType.IDENT, "add"),
                new(TokenType.LPAREN, "("),
                new(TokenType.IDENT, "five"),
                new(TokenType.COMMA, ","),
                new(TokenType.IDENT, "ten"),
                new(TokenType.RPAREN, ")"),
                new(TokenType.SEMICOLON, ";")
            };
        }

        [Test]
        public void NextTokenTests()
        {
            foreach (var expected in _tokens)
            {
                var token = _lexer.NextToken();
                Console.WriteLine($"Expected: {expected}, Actual: {token}");
                Assert.That(expected, Is.EqualTo(token));
            }
        }
    }
}
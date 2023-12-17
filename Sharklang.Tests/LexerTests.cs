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
            låt five vara 5;
            låt ten vara 10;

            låt add vara fn(x,y) {
                x + y;
            };

            låt result vara add(five, ten);
            !-/*5;
            5 < 10 > 5;

            om(5 < 10) {
                ge sant;
            } annars {
                ge falskt;
            }

            10 lika 10;
            10 olika 9;
            10 += 9;
            ";

            _lexer = new Lexer(_input);
            _tokens = new List<Token>
            {
                new(TokenType.LET, "låt"),
                new(TokenType.IDENT, "five"),
                new(TokenType.ASSIGN, "vara"),
                new(TokenType.INT, "5"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.LET, "låt"),
                new(TokenType.IDENT, "ten"),
                new(TokenType.ASSIGN, "vara"),
                new(TokenType.INT, "10"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.LET, "låt"),
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
                new(TokenType.LET, "låt"),
                new(TokenType.IDENT, "result"),
                new(TokenType.ASSIGN, "vara"),
                new(TokenType.IDENT, "add"),
                new(TokenType.LPAREN, "("),
                new(TokenType.IDENT, "five"),
                new(TokenType.COMMA, ","),
                new(TokenType.IDENT, "ten"),
                new(TokenType.RPAREN, ")"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.BANG, "!"),
                new(TokenType.MINUS, "-"),
                new(TokenType.SLASH, "/"),
                new(TokenType.ASTERISK, "*"),
                new(TokenType.INT, "5"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.INT, "5"),
                new(TokenType.LT, "<"),
                new(TokenType.INT, "10"),
                new(TokenType.GT, ">"),
                new(TokenType.INT, "5"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.IF, "om"),
                new(TokenType.LPAREN, "("),
                new(TokenType.INT, "5"),
                new(TokenType.LT, "<"),
                new(TokenType.INT, "10"),
                new(TokenType.RPAREN, ")"),
                new(TokenType.LBRACE, "{"),
                new(TokenType.RETURN, "ge"),
                new(TokenType.TRUE, "sant"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.RBRACE, "}"),
                new(TokenType.ELSE, "annars"),
                new(TokenType.LBRACE, "{"),
                new(TokenType.RETURN, "ge"),
                new(TokenType.FALSE, "falskt"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.RBRACE, "}"),
                new(TokenType.INT, "10"),
                new(TokenType.EQ, "lika"),
                new(TokenType.INT, "10"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.INT, "10"),
                new(TokenType.NOT_EQ, "olika"),
                new(TokenType.INT, "9"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.INT, "10"),
                new(TokenType.PLUS_EQ, "+="),
                new(TokenType.INT, "9"),
                new(TokenType.SEMICOLON, ";")
            };
        }

        [Test]
        public void NextTokenTests()
        {
            foreach (var expected in _tokens)
            {
                var token = _lexer.NextToken();
                Console.WriteLine($" is {expected} + {token}");
                Assert.That(expected, Is.EqualTo(token));
            }
        }
    }
}
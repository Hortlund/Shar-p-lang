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

            let five = 5;
            let ten = 10;
            
            let add = fn(x,y) {
                x + y;
            };

            let result = add(five, ten);

            ";

            _lexer = new Lexer(_input);
            _tokens = new List<Token>
            {
                new(TokenType.LET, "let"),
                new(TokenType.IDENT, "five"),
                new(TokenType.ASSIGN, "="),
                new(TokenType.INT, "5"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.LET, "let"),
                new(TokenType.IDENT, "ten"),
                new(TokenType.ASSIGN, "="),
                new(TokenType.INT, "10"),
                new(TokenType.SEMICOLON, ";"),
                new(TokenType.LET, "let"),
                new(TokenType.IDENT, "add"),
                new(TokenType.ASSIGN, "="),
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
                new(TokenType.LET, "let"),
                new(TokenType.IDENT, "result"),
                new(TokenType.ASSIGN, "="),
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
                Assert.That(expected, Is.EqualTo(token));
            }
        }
    }
}
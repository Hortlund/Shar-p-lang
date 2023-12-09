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
            _input = "=+(){},;";
            _lexer = new Lexer(_input);
            _tokens = new List<Token>
            {
                new(TokenType.ASSIGN, "="),
                new(TokenType.PLUS, "+"),
                new(TokenType.LPAREN, "("),
                new(TokenType.RPAREN, ")"),
                new(TokenType.LBRACE, "{"),
                new(TokenType.RBRACE, "}"),
                new(TokenType.COMMA, ","),
                new(TokenType.SEMICOLON, ";"),

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
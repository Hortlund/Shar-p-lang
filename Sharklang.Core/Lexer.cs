using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharklang.Core
{
    public enum TokenType
    {
        Illegal,
        EOF,

        IDENT,
        INT,

        ASSIGN,
        PLUS,

        COMMA,
        SEMICOLON,

        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,

        FUNCTION,
        LET
    }

    public record Token(TokenType tokenType, string literal)
    {
        public Token(TokenType type, Rune literal) : this(type, literal.ToString())
        {

        }
    }

    public class Lexer
    {
        private readonly string _input;

        private int _position;

        private int _readPosition;

        private Rune _ch;

        private readonly Dictionary<string, TokenType> _keywords = new()
        {
            { "fn", TokenType.FUNCTION },
            { "låt", TokenType.LET },
            { "vara", TokenType.ASSIGN }
        };

        public Lexer(string input)
        {
            _input = input;
            ReadChar();
        }

        private void ReadChar()
        {
            if (_readPosition >= _input.Length)
            {
                _ch = Rune.ReplacementChar; // End of file or invalid character
            }
            else
            {
                _ch = Rune.GetRuneAt(_input, _readPosition);
            }

            _position = _readPosition; // Current reading position
            _readPosition += (_ch == Rune.ReplacementChar) ? 0 : _ch.Utf16SequenceLength;
        }

        public Token NextToken()
        {
            Token tok;

            SkipWhiteSpace();

            switch(_ch)
            {
                case var ch when ch == new Rune(';'):
                    tok = new Token(TokenType.SEMICOLON, _ch.ToString());
                    break;
                case var ch when ch == new Rune('('):
                    tok = new Token(TokenType.LPAREN, _ch.ToString());
                    break;
                case var ch when ch == new Rune(')'):
                    tok = new Token(TokenType.RPAREN, _ch.ToString());
                    break;
                case var ch when ch == new Rune(','):
                    tok = new Token(TokenType.COMMA, _ch.ToString());
                    break;
                case var ch when ch == new Rune('+'):
                    tok = new Token(TokenType.PLUS, _ch.ToString());
                    break;
                case var ch when ch == new Rune('{'):
                    tok = new Token(TokenType.LBRACE, _ch.ToString());
                    break;
                case var ch when ch == new Rune('}'):
                    tok = new Token(TokenType.RBRACE, _ch.ToString());
                    break;
                case var ch when ch == Rune.ReplacementChar:
                    tok = new Token(TokenType.EOF, "");
                    break;
                default:
                    if (IsLetter(_ch))
                    {
                        var ident = ReadIdentifier();
                        var type = LookupIdent(ident);
                        return new Token(type, ident);
                    }
                    else if (IsDigit(_ch))
                    {
                        var literal = ReadNumber();
                        return new Token(TokenType.INT, literal);
                    }
                    else
                    {
                        tok = new Token(TokenType.Illegal, _ch.ToString());
                    }
                    break;
            }

            ReadChar();
            return tok;    
        }

        private TokenType LookupIdent(string ident)
        {
            if (_keywords.ContainsKey(ident))
            {
                return _keywords[ident];
            }
            else
            {
                return TokenType.IDENT;
            }
        }

        private string ReadIdentifier()
        {
            var position = _position;
            while (IsLetter(_ch))
            {
                ReadChar();
            }
            return _input.Substring(position, _position - position);
        }

        private string ReadNumber()
        {
            var position = _position;
            while (IsDigit(_ch))
            {
                ReadChar();
            }
            return _input.Substring(position, _position - position);
        }

        private static bool IsLetter(Rune ch)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(ch.Value);
            return category == UnicodeCategory.LowercaseLetter ||
                   category == UnicodeCategory.UppercaseLetter ||
                   ch == new Rune('_');
        }
        private static bool IsDigit(Rune ch)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(ch.Value);
            return category == UnicodeCategory.DecimalDigitNumber;
        }

        private void SkipWhiteSpace()
{
    while (_ch != Rune.ReplacementChar && Char.IsWhiteSpace((char)_ch.Value))
    {
        ReadChar();
    }
}
    }
}

using System;
using System.Collections.Generic;
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
        public Token(TokenType type, char literal) : this(type, literal.ToString())
        {

        }
    }

    public class Lexer
    {
        private readonly string _input;

        private int _position;

        private int _readPosition;

        private char _ch;

        private readonly Dictionary<string, TokenType> _keywords = new()
        {
            { "fn", TokenType.FUNCTION },
            { "let", TokenType.LET }
        };

        public Lexer(string input)
        {
            _input = input;
            ReadChar();
        }

        private void ReadChar()
        {
            if(_readPosition >= _input.Length)
            {
                _ch = '\0';
            }
            else
            {
                _ch = _input[_readPosition];
            }

            _position = _readPosition;
            _readPosition++;
        }

        public Token NextToken()
        {
            Token tok;

            SkipWhiteSpace();

            switch(_ch)
            {
                case '=':
                    tok = new Token(TokenType.ASSIGN, _ch);
                    break;
                case ';':
                    tok = new Token(TokenType.SEMICOLON, _ch);
                    break;
                case '(':
                    tok = new Token(TokenType.LPAREN, _ch);
                    break;
                case ')':
                    tok = new Token(TokenType.RPAREN, _ch);
                    break;
                case ',':
                    tok = new Token(TokenType.COMMA, _ch);
                    break;
                case '+':
                    tok = new Token(TokenType.PLUS, _ch);
                    break;
                case '{':
                    tok = new Token(TokenType.LBRACE, _ch);
                    break;
                case '}':
                    tok = new Token(TokenType.RBRACE, _ch);
                    break;
                case '\0':
                    tok = new Token(TokenType.EOF, "");
                    break;
                default:
                    if (IsLetter(_ch))
                    {
                        var ident = ReadIdentifier();
                        var type = LookupIdent(ident);
                        tok = new Token(type, ident);
                        return tok;
                    }
                    if (IsDigit(_ch))
                    {
                        var literal = ReadNumber();
                        tok = new Token(TokenType.INT, literal);
                        return tok;

                    }

                    tok = new Token(TokenType.Illegal, _ch);
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

        private static bool IsLetter(char ch) => ch is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or '_';
        private static bool IsDigit(char ch) => ch is >= '0' and <= '9';

        private void SkipWhiteSpace()
        {
            while (_ch is ' ' or '\t' or '\n' or '\r')
            {
                ReadChar();
            }
        }
    }
}

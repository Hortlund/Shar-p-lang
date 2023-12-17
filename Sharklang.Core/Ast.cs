using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharklang.Core
{
    public record Node(Token Token);

    public record Statement(Token Token) : Node(Token)
    {
        public string TokenLiteral => Token.literal;
        public virtual string String => Token.literal;
    }

    public record Expression(Token Token) : Node(Token)
    {
        public string TokenLiteral => Token.literal;
        public virtual string String => Token.literal;
    }

    public record Program(List<Statement> Statements) : Node(new Token(TokenType.Illegal, ""))
    {
        public string TokenLiteral => Statements.Count > 0 ? Statements[0].TokenLiteral : "";
        public string String => string.Join("", Statements.Select(x => x.String));
    }
}

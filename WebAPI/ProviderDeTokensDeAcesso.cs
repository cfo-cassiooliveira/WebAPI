using ConectionDB;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;

namespace WebAPI
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        private readonly ConectionDbContext? _dbContext;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (_dbContext == null)
            {
                context.SetError("Error", "Conexão com o banco não encontrada.");
                throw new ArgumentNullException(nameof(_dbContext));
            }
            else
            {
                var usuario = _dbContext.Usuario.FirstOrDefault(x => x.Nome == context.UserName && x.Senha == context.Password);

                if (usuario == null)
                {
                    context.SetError("invalid_grant", "Usuário não encontrado um senha incorreta.");
                    return;
                }

                var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
                context.Validated(identidadeUsuario);
            }
        }
    }
}

using System.Security.Cryptography;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Configuration;
using Users.Entities;
using Users.Interfaces;

namespace Users.Services;

public class JwtService : IJwtService
{
    private readonly string secret;
    private readonly string issuer;
    private readonly string audience;
    private readonly string jid;

    public JwtService(IConfiguration configuration)
    {
        this.secret = configuration.GetValue<string>("JWT:secret");
        this.issuer = configuration.GetValue<string>("JWT:issuer");
        this.audience = configuration.GetValue<string>("JWT:audience");
        this.jid = configuration.GetValue<string>("JWT:jid");
    }

    public string generate(Dto dto)
    {
        return JwtBuilder.Create()
            .WithAlgorithm(new RS2048Algorithm(RSA.Create()))
            .WithSecret(this.secret)
            .Issuer(this.issuer)
            .Audience(this.audience)
            .Id(this.jid)
            .ExpirationTime(DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
            .AddClaim("google_id", dto.google_id)
            .AddClaim("description", dto.name)
            .Encode();
    }

    public string? check(string jwt)
    {
        try
        {
            if (JwtBuilder.Create()
                .WithAlgorithm(new RS2048Algorithm(RSA.Create()))
                .WithSecret(this.secret)
                .MustVerifySignature()
                .Decode<IDictionary<string, string>>(jwt)
                .TryGetValue("google_id", out var google_id))
                return google_id;
        }
        catch (Exception)
        {
            return null;
        }
        return null;
    }
}

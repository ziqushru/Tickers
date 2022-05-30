using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Users.Roles.Entities;
using Users.Roles.Interfaces;

namespace Users.Roles.Endpoints;

public class Create : Endpoint<Dto, Dto>
{
    public override void Configure()
    {
        this.Version(0);
        this.Post("/users/roles");
        this.Description(b => b
                .Accepts<Dto>("application/json+custom")
                .Produces<Dto>(200, "application/json+custom")
                .Produces<ErrorResponse>(400, "application/json+problem")
                .ProducesProblem(403),
            clearDefaults: true);
        this.Summary(s => {
            s.Summary = "Create";
            s.Description = "Create new";
            s[200] = "Create Succeded";
            s[403] = "Forbidden Access";
        });
        this.Options(b =>
            b.RequireCors(x =>
                    x.AllowAnyOrigin())
                .RequireHost("api.tickers.gr")
                .ProducesProblem(404));
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(Dto dto, CancellationToken cancellation_token)
    {
        var response = new Dto()
        {
            description = dto.description
        };

        await this.SendAsync(response, cancellation: cancellation_token);
    }
}

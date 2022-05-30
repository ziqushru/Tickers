using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Users.Roles.Entities;

namespace Users.Roles.Endpoints;

public class Read : Endpoint<Dto, Dto>
{
    public override void Configure()
    {
        this.Version(0);
        this.Get("/users/roles/{id}");
        this.Description(b => b
                .Accepts<Dto>("application/json+custom")
                .Produces<Dto>(200, "application/json+custom")
                .Produces<ErrorResponse>(400, "application/json+problem")
                .ProducesProblem(403),
            clearDefaults: true);
        this.Summary(s => {
            s.Summary = "Read";
            s.Description = "Read existing";
            s[200] = "Read Succeded";
            s[403] = "Forbidden Access";
        });
        this.Options(b =>
            b.RequireCors(x =>
                    x.AllowAnyOrigin())
                .RequireHost("api.tickers.gr")
                .ProducesProblem(404));
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(Dto request_dto, CancellationToken cancellation_token)
    {
        var response = new Dto()
        {
            description = request_dto.description
        };

        await this.SendAsync(response, cancellation: cancellation_token);
    }
}

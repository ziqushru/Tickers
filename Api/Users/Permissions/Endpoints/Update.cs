using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Users.Permissions.Entities;

namespace Users.Permissions.Endpoints;

public class Update : Endpoint<Dto, Dto>
{
    public override void Configure()
    {
        this.Version(0);
        this.Put("/users/permissions/{id}");
        this.Description(b => b
                .Accepts<Dto>("application/json+custom")
                .Produces<Dto>(200, "application/json+custom")
                .Produces<ErrorResponse>(400, "application/json+problem")
                .ProducesProblem(403),
            clearDefaults: true);
        this.Summary(s => {
            s.Summary = "Update";
            s.Description = "Update existing";
            s[200] = "Update Succeded";
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

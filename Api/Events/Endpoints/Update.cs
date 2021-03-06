using Events.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Events.Endpoints;

public class Update : Endpoint<Dto, Dto>
{
    public override void Configure()
    {
        this.Version(0);
        this.Put("/events/{id}");
        this.Description(b => b
                .Accepts<Dto>("application/json+custom")
                .Produces<Dto>(200, "application/json+custom")
                .Produces<ErrorResponse>(400, "application/json+problem")
                .ProducesProblem(403),
            clearDefaults: true);
        this.Summary(s => {
            s.Summary = "Update";
            s.Description = "Update existing Event";
            s[200] = "Update Succeded";
            s[403] = "Forbidden Access";
        });
        this.Options(b =>
            b.RequireCors(x =>
                    x.AllowAnyOrigin())
                .RequireHost("api-bling.eetaa.gr")
                .ProducesProblem(404));
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(Dto request_dto, CancellationToken cancellation_token)
    {
        var response = new Dto()
        {
            name = request_dto.name,
            place = request_dto.place
        };

        await this.SendAsync(response, cancellation: cancellation_token);
    }
}

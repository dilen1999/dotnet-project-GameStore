using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games =
    [
        new(
        1,
        "Street Fighter II",
        "Fighting",
        19.9M,
        new DateOnly(1999, 2, 12)
    ),
    new(
        2,
        "Street Fighter II",
        "Fighting",
        12.9M,
        new DateOnly(1999, 2, 12)
    ),
    new(
        3,
        "Street Fighter II",
        "Fighting",
        59.9M,
        new DateOnly(1999, 2, 12)
    )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) => games.Find(game => game.Id == id))
           .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            if(string.IsNullOrEmpty(newGame.Name)){
                return Results.BadRequest("Name is required");
            }
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        })
        .WithParameterValidation();

        //put/ games 
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(games => games.Id == id);

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });


        // DELETE 
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(games => games.Id == id);

            return Results.NoContent();
        });

        return group;

    }

}

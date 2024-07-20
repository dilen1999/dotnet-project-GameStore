using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = new()
{
    new GameDto(
        1,
        "Street Fighter II",
        "Fighting",
        19.9M,
        new DateOnly(1999, 2, 12)
    ),
    new GameDto(
        2,
        "Street Fighter II",
        "Fighting",
        12.9M,
        new DateOnly(1999, 2, 12)
    ),
    new GameDto(
        3,
        "Street Fighter II",
        "Fighting",
        59.9M,
        new DateOnly(1999, 2, 12)
    )
};

// GET /games
app.MapGet("/games", () => games);

// GET /games/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
   .WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

app.Run();

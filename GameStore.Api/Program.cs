using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (
        1, 
        "Street Fight II",
        "Fighting ",
        19.9M,
        new DateOnly (1999,2,12)
    ),
     new (
        2, 
        "Street Fight II",
        "Fighting ",
        12.9M,
        new DateOnly (1999,2,12)
    ),
     new (
        3, 
        "Street Fight II",
        "Fighting ",
        59.9M,
        new DateOnly (1999,2,12)
    )
];

app.MapGet("/", () => "Hello World!");

app.Run();

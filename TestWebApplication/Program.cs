using TestWebApplication.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new GameDto(
        1,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)),
    new GameDto(
        2,
        "The Legend of Zelda: Breath of the Wild",
        "Action-adventure",
        59.99M,
        new DateOnly(2017, 3, 3)),
    new GameDto(
        3,
        "The Witcher 3: Wild Hunt",
        "Action role-playing",
        39.99M,
        new DateOnly(2015, 5, 19)),
    new GameDto(
        4,
        "Red Dead Redemption 2",
        "Action-adventure",
        59.99M,
        new DateOnly(2018, 10, 26)),
    new GameDto(
        5,
        "Persona 5",
        "Role-playing",
        49.99M,
        new DateOnly(2016, 9, 15)),
    new GameDto(
        6,
        "Cyberpunk 2077",
        "Action role-playing",
        49.99M,
        new DateOnly(2020, 12, 10)),
    new GameDto(
        7,
        "Horizon Zero Dawn",
        "Action role-playing",
        49.99M,
        new DateOnly(2017, 2, 28)),
    new GameDto(
        8,
        "God of War",
        "Action-adventure",
        39.99M,
        new DateOnly(2018, 4, 20)),
    new GameDto(
        9,
        "The Elder Scrolls V: Skyrim",
        "Action role-playing",
        39.99M,
        new DateOnly(2011, 11, 11))
];
// GET /games
app.MapGet("games", () => games);

// GET /games/{id}
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
    .WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
});

app.Run();
